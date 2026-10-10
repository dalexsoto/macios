# Experimental watchOS applications

The watchOS workload builds standalone watch applications with managed C# logic
and a SwiftUI interface. It uses the normal macios Foundation/ObjCRuntime
bindings, assembly preparation, trimmable-static registrar, native runtime
bridge, packaging, and signing pipeline.

This is a development integration, not a released workload or an App Store
compatibility claim. It currently requires an Apple Silicon Mac, Xcode 27 with
the watchOS SDK and simulator runtime, and the configured .NET 11 SDK. The
minimum deployment version is watchOS 26.0. Only `watchos-arm64` and
`watchossimulator-arm64` are supported; `arm64_32` is a separate, unimplemented ABI.

## Build and install from source

Configure the sibling runtime checkout before building:

```sh
./configure --disable-all-platforms --enable-watchos --custom-dotnet=/path/to/runtime
./dotnet/build-custom-runtime.sh
make -C builds .stamp-download-dotnet-packages
make -C runtime dotnet
make -C src dotnet
make -C msbuild install
make -C tools/dotnet-linker install
make -C dotnet .stamp-install-workloads
export PATH="$PWD/builds/downloads/dotnet:$PATH"
dotnet workload list
```

The watch-only custom-runtime recipe builds CoreCLR and NativeAOT, not a Mono
runtime. Some shared build-task assemblies retain historical Mono names.
The generated workload has no Mono runtime-pack dependencies.

The runtime checkout currently has product version `12.0.0-dev` while its managed
assets and this macios SDK target `net11.0`. The workload consumes the actual
runtime package version; it does not rename those packages to pretend they are
.NET 11 releases. ILC, Crossgen2, the reference pack, and both target runtime packs
are installed together. The matching ILLink tool and analyzer are also installed,
so application projects do not need private package sources or source-tree imports.

Use the local `dotnet` executable for workload installation and removal. A
`global.json` SDK search path can select the local SDK from a system muxer, but
`dotnet workload list` can still describe that muxer's installation root.
`make -C dotnet install-system-watchos` is a separate, privileged operation: it
requires the configured SDK in `SYSTEM_DOTNET_ROOT`, bootstraps the watch
manifest, and includes the configured runtime feed.
Aggregate system installation completes the non-watch workload transaction
before installing watchOS. Its single legacy rollback file excludes watchOS,
whose manifest is bootstrapped separately.

Development runtime packages have a fixed `-dev` version. Rebuilding a package
does not make an already installed copy change automatically. Reinstall the
affected development packs when testing changed binaries, and clean applications
before rebuilding against replaced fixed-version tools. For CoreLib changes,
include `clr.nativecorelib` and `libs` before `packs`: rebuilding only `clr.corelib`
updates its `IL/` output, not every runtime-pack staging input.
Rebuild macios candidate packages with `make -C dotnet pack-watchos` before
repairing the workload. A source install can use staging symlinks, but
`dotnet workload repair --no-http-cache` installs extracted package copies;
subsequent staging changes do not update those copies automatically.

## Create and run an application

```sh
dotnet new watch -n MyWatch
cd MyWatch
dotnet build
dotnet run -r watchossimulator-arm64 -p:Device="<simulator-identifier>"
```

`dotnet new watchoslib` creates a trimmable class library.
`dotnet new watchosbinding` creates an Objective-C binding project. Native
references must contain genuine watchOS or watchOS-simulator slices; an iOS slice
is not compatible.

The application template supports `--bundleId`, `--minOSVersion`, and
`--Framework net11.0-watchos`. The default build RID is the ARM64 simulator;
publishing defaults to the ARM64 device. Device deployment requires a development
signing identity and a provisioning profile that includes the target watch and
permits the application's bundle identifier.

```sh
dotnet publish -r watchos-arm64 -c Release \
  -p:CodesignKey="<development identity>" -p:CodesignProvision="<profile>"
dotnet run -r watchos-arm64 -p:Device="<watch-name-or-identifier>" \
  -p:CodesignKey="<development identity>" -p:CodesignProvision="<profile>"
```

`EnableCodeSigning=false` is useful for inspecting cross-built device artifacts,
but those artifacts cannot be installed as development-signed device apps.
The SDK uses `simctl` for simulators and `devicectl` for physical watches; it does
not require a TCP test/debugger connection or the legacy mlaunch WatchKit model.
Use `-p:WaitForExit=false` to return after launching a long-running UI app.
Keep the watch unlocked and connected during deployment. A launch acknowledgement
is not proof of managed execution; confirm a native log marker or managed UI
round trip. Stopping a `devicectl --console` session can terminate its application.

## Runtime selection

| Configuration | Execution |
| --- | --- |
| Default Debug build | No-JIT CoreCLR, composite Mach-O ReadyToRun for framework code, interpreted application code |
| Release build/publish | No-JIT CoreCLR with composite Mach-O ReadyToRun and retained-IL fallback |
| `PublishReadyToRun=false` | CoreCLR interpreter-only |
| `dotnet publish -p:PublishAot=true` | NativeAOT, with no deployed CoreCLR or managed DLLs |

For example:

```sh
dotnet run -r watchossimulator-arm64 -p:PublishReadyToRun=false
dotnet publish -r watchossimulator-arm64 -c Release -p:PublishAot=true
dotnet run --no-build -r watchossimulator-arm64 -c Release
```

As on the other Apple platforms, `PublishAot` changes `publish`, not an ordinary
`build`. Mono's `UseInterpreter`/`MtouchInterpreter` properties are not CoreCLR
interpreter switches and are rejected. **There is no NativeAOT IL interpreter.**

Both runtimes use software managed null checks and cooperative GC on device and
simulator. Physical watchOS rejects fatal-signal handlers and provides no native
context to activation signals, so asynchronous thread hijacking is not used.
All precompiled managed code must come from the matching watch compiler packs,
which insert method-entry, exception-entry, and loop rendezvous points. Native
memory faults and stack overflow remain fatal; private signal or Mach APIs are
not used to bypass platform restrictions.

## Native UI and managed ownership

`Program.Main` creates an `NSObject` model and calls
`WatchApplication.Run(model)`. This call never returns. The native macios host
has already initialized Objective-C interop and a main-thread synchronization
context. A native bridge then calls the Swift function named by
`WatchApplicationEntryPoint`; Swift enters `App.main()` without defining a second
`@main`.

The registrar generates an Objective-C header from public `[Register]` and
`[Export]` declarations. Swift compilation imports that header, and the generated
Swift export header type-checks the entry-point assignment in the native main.
Return-ownership attributes match the registrar's retain/autorelease behavior,
including selectors beginning with `new` or `copy` and `[return: Release]`.
Inherited selectors retain their native ownership convention; incompatible
managed return ownership is rejected. A public Swift-facing model cannot
currently derive from an external binding-library class without its native
superclass declaration in the generated header; use an `NSObject`-based model
that wraps that object instead.
Overrides of managed model exports preserve the base export's declared
ownership, including owning-family selectors explicitly marked as non-owning.
Put ownership attributes on the root declaration; an override does not change
that contract. Delegate-returning managed exports require `[return: Release]`
on the root method or property getter because the marshaller returns an owned
block copy.

The sample uses Foundation notifications to update SwiftUI on the main queue.
Keep model/UI operations on that thread. Native owners participate in the real
NSObject reference tracker; ordinary managed roots are not a substitute for
testing native ownership across garbage collection.

Watch apps require `Registrar=trimmable-static`, `PrepareAssemblies=true`, and
`DynamicRegistrationSupported=false`. Block signatures are prepared even in
untrimmed builds and copied binding libraries. Calls that still require dynamic
registration fail during the build rather than failing later on the watch.

Unhandled managed exceptions abort by default so they cannot unwind through
Swift frames. The runtime reports the managed exception event before aborting.
An unexpected return or uncaught native exception from the application loop is
fatal. This does not prevent catching a correctly marshalled Objective-C
exception inside managed code.

## Current coverage and limits

The managed platform assembly includes the foundational Foundation collections,
coding, run-loop, notification and URLSession closure; Objective-C interop;
CoreFoundation/dispatch and geometry support; Security trust/certificate/key
support; and a selected `WKApplication`/`WKInterfaceDevice` surface. It does not
claim all iOS frameworks, full WatchKit, WatchConnectivity, notification-service
bindings, or companion-iPhone project integration.

`HttpClient` uses the real URLSession handler. BSD sockets, Ping, managed socket
HTTP, and generic stream/WebSocket networking are not supported watch networking
paths. Simulator HTTP success is not proof of phone-independent hardware
networking.

Managed debugger attachment, Hot Reload, Mono, legacy WatchKit extensions, and
unsupported architectures are rejected. Native logging and Apple deployment
tools remain available.

Device publishing rejects app bundles larger than **75,000,000 uncompressed
bytes**, a conservative interpretation of Apple's 75 MB ceiling. Runtime native
libraries are packaged as signed frameworks. The CoreCLR and System.Native
frameworks include manifests for their identified required-reason imports.
The application template has its own privacy manifest for statically linked
runtime/application use. Developers must review declarations for their actual
functionality and additional dependencies; these files are not distribution
approval. See Apple's [required-reason API documentation](https://developer.apple.com/documentation/bundleresources/describing-use-of-required-reason-api).

## Integration validation

`tests/dotnet/WatchIntegrationTestApp` exercises managed state owned only by
Swift, autorelease-pool boundaries, collection/finalization after native release,
Objective-C return ownership, Swift construction, copied Foundation/binding
blocks, actual HTTPS through URLSession, main-thread continuations, native
exception conversion, and no-JIT feature reporting. It emits
`WATCH_INTEGRATION_PASS` through native logging. Its HTTPS check currently uses
`https://example.com/`; a CI lane needs an appropriately controlled reachable
endpoint.

Build and execute it with CoreCLR Debug, CoreCLR Release, interpreter-only, and
NativeAOT. Keep simulator results separate from signed physical-device results.
HelloWorld has signed, installed, and executed its C# counter on a physical ARM64
watch running watchOS 27.0.1, including the normal Debug configuration. The
integration app emits `WATCH_INTEGRATION_PASS` on that hardware with
interpreter-only CoreCLR, Release composite R2R, and NativeAOT; the same final
three-mode matrix also passes on the simulator. Its checks include offset-null
accesses, generic virtual calls, delegate construction, exception ordering, and
compacting collection while another managed thread holds movable interior roots
in a nonallocating loop.

The measured signed HelloWorld Debug bundle is 17,807,765 bytes; the larger
NativeAOT integration-test bundle is 4,176,297 bytes. These are application-size
measurements, not launch-time, memory, or energy-budget acceptance.
Store/TestFlight processing, broader runtime/BCL stress coverage, hardware
resource budgets, phone-disconnected networking, and official workload publishing
remain unverified.

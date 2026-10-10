import Combine
import Foundation
import SwiftUI

@MainActor
private var applicationModel: WatchIntegrationModel?

@MainActor
private final class IntegrationState: ObservableObject {
    @Published var status = "Starting"
    private let model: WatchIntegrationModel
    private var child: WatchIntegrationChild?
    private var changes: AnyCancellable?
    private var started = false

    init() {
        guard let model = applicationModel else { fatalError("Missing managed model") }
        NSLog("WATCH_INTEGRATION_UI_CREATED")
        self.model = model
        changes = NotificationCenter.default
            .publisher(for: Notification.Name("WatchIntegrationStateChanged"), object: model)
            .receive(on: DispatchQueue.main)
            .sink { [weak self] _ in
                Task { @MainActor [weak self] in self?.refresh() }
            }
        refresh()
    }

    func refresh() {
        precondition(Thread.isMainThread)
        status = model.status as String? ?? "Invalid managed status"
        if status == "PASS" {
            NSLog("WATCH_INTEGRATION_PASS: software null/arithmetic exceptions, cooperative GC, native-owned NSObject, release/finalization, Foundation block, URLSession HTTPS, main-thread await, Objective-C exception, no JIT")
        }
    }

    func start() async {
        guard !started else { return }
        started = true
        NSLog("WATCH_INTEGRATION_LIFETIME_START")
        child = model.createChild() as? WatchIntegrationChild
        precondition(child?.description == "Managed watch counter", "Native superclass override mismatch")
        precondition(child?.increment()?.intValue == 41)
        await nextPoolDrain()
        model.collect()
        NSLog("WATCH_INTEGRATION_FIRST_NATIVE_OWNED_GC")
        precondition(child?.increment()?.intValue == 42, "Swift-owned managed state did not survive GC")
        child = nil
        await nextPoolDrain()
        model.collect()
        model.assertChildReleased()
        child = model.newChild() as? WatchIntegrationChild
        await checkOwnership()
        child = model.copyState() as? WatchIntegrationChild
        await checkOwnership()
        child = model.retainedChild() as? WatchIntegrationChild
        await checkOwnership()
        child = WatchIntegrationChild()
        await nextPoolDrain()
        model.collect()
        precondition(child?.increment()?.intValue == 41, "Swift construction did not initialize managed state")
        child = nil
        let swiftOnly = WatchSwiftOnlyObject()
        precondition(swiftOnly?.value?.intValue == 99, "A type constructed only by Swift was trimmed")
        model.checkNativeException()
        model.startAsyncChecks()
    }

    private func checkOwnership() async {
        await nextPoolDrain()
        model.collect()
        precondition(child?.increment()?.intValue == 41, "The declared Objective-C return ownership is incorrect")
        child = nil
        await nextPoolDrain()
        model.collect()
        model.assertChildReleased()
    }

    private func nextPoolDrain() async {
        do {
            try await Task.sleep(for: .milliseconds(300))
        } catch {
            fatalError("The lifetime check was cancelled before the autorelease pool drained: \(error)")
        }
    }
}

private struct IntegrationView: View {
    @StateObject private var state = IntegrationState()

    var body: some View {
        Text(state.status)
            .task { await state.start() }
    }
}

private struct IntegrationApp: App {
    var body: some Scene {
        WindowGroup { IntegrationView() }
    }
}

@MainActor
@_cdecl("dotnet_watch_integration_main")
public func runIntegration(_ model: NSObject) {
    precondition(Thread.isMainThread)
    guard applicationModel == nil, let model = model as? WatchIntegrationModel else {
        fatalError("Invalid managed model")
    }
    applicationModel = model
    NSLog("WATCH_INTEGRATION_NATIVE_ENTRY")
    IntegrationApp.main()
    fatalError("Unexpected return from the watch event loop")
}

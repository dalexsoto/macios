import Foundation
import Combine
import SwiftUI

@MainActor
private var applicationModel: DotNetWatchModel?

@MainActor
private final class ManagedState: ObservableObject {
    @Published var title = ""
    @Published var value = ""
    private let model: DotNetWatchModel
    private var changes: AnyCancellable?

    init() {
        guard let model = applicationModel else {
            fatalError("The managed watch application did not provide its model.")
        }
        self.model = model
        changes = NotificationCenter.default
            .publisher(for: Notification.Name("DotNetWatchModelChanged"), object: model)
            .receive(on: DispatchQueue.main)
            .sink { [weak self] _ in
                Task { @MainActor [weak self] in
                    self?.refresh()
                }
            }
        refresh()
    }

    func refresh() {
        precondition(Thread.isMainThread)
        guard let title = model.title, let counter = model.counter else {
            fatalError("The managed model returned an invalid state.")
        }
        self.title = title as String
        value = "Count: \(counter.intValue)"
    }

    func increment() {
        precondition(Thread.isMainThread)
        model.increment()
    }
}

private struct WatchContentView: View {
    @StateObject private var state = ManagedState()

    var body: some View {
        VStack(spacing: 12) {
            Text(state.title)
            Text(state.value)
                .accessibilityIdentifier("managed-counter")
            Button("Increment in C#") {
                state.increment()
            }
            .accessibilityIdentifier("increment-counter")
        }
        .padding()
        .onAppear {
            if ProcessInfo.processInfo.arguments.contains("--self-test") {
                state.increment()
                Task { @MainActor in
                    guard let model = applicationModel, model.counter?.intValue == 1 else {
                        fatalError("The managed counter did not respond to its native selector.")
                    }
                    NSLog("WATCH_SAMPLE_MANAGED_COUNTER=1")
                }
            }
        }
    }
}

private struct DotNetWatchApp: App {
    var body: some Scene {
        WindowGroup {
            WatchContentView()
        }
    }
}

@MainActor
@_cdecl("dotnet_watch_application_main")
public func runManagedWatchApplication(_ model: NSObject) {
    precondition(Thread.isMainThread)
    guard applicationModel == nil, let model = model as? DotNetWatchModel else {
        fatalError("The managed watch application supplied an incompatible or duplicate model.")
    }
    applicationModel = model
    DotNetWatchApp.main()
    fatalError("The watchOS application event loop returned unexpectedly.")
}

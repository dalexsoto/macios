// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

#ifndef XAMARIN_WATCHOS_H
#define XAMARIN_WATCHOS_H

#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

typedef void (*XamarinWatchApplicationEntryPoint) (NSObject *application_model);
extern XamarinWatchApplicationEntryPoint xamarin_watch_application_entry_point;
__attribute__((noreturn)) void xamarin_watch_application_main (NSObject *application_model);

#ifdef __cplusplus
}
#endif

#endif

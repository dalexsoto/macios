#!/bin/bash -eu

TOP=$(git rev-parse --show-toplevel)

COLOR_GRAY=$(tput setaf 250 2>/dev/null)
COLOR_RED=$(tput setaf 1 2>/dev/null)
COLOR_CLEAR=$(tput sgr0 2>/dev/null)

if test -z "$TOP"; then
	echo "${COLOR_RED}Could not find the top-level dotnet/macios directory.${COLOR_CLEAR}"
	exit 1
fi

cd "$TOP"

if ! test -f configure.inc; then
	echo "${COLOR_RED}You must execute ${COLOR_GRAY}./configure --custom-dotnet=/path/to/dotnet/runtime${COLOR_RED} first.${COLOR_CLEAR}"
	exit 1
fi

DOTNET_RUNTIME_PATH=$(grep ^DOTNET_RUNTIME_PATH= configure.inc | sed 's/.*=//')
if test -z "$DOTNET_RUNTIME_PATH"; then
	echo "${COLOR_RED}You must execute ${COLOR_GRAY}./configure --custom-dotnet=/path/to/dotnet/runtime${COLOR_RED} first.${COLOR_CLEAR}"
	exit 1
elif ! test -d "$DOTNET_RUNTIME_PATH"; then
	echo "${COLOR_RED}The specified dotnet/runtime path (${COLOR_GRAY}$DOTNET_PATH${COLOR_RED}) does not exist.${COLOR_CLEAR}"
	exit 1
fi

cd "$DOTNET_RUNTIME_PATH"

if grep -q '^INCLUDE_WATCHOS=1' "$TOP/configure.inc"; then
	if test "$(uname -m)" != arm64; then
		echo "The watchOS runtime build requires an Apple Silicon Mac."
		exit 1
	fi
	use_installed_dotnet_cli=false ./build.sh clr+libs+host+packs -os osx -arch arm64 -c Release -rc Checked
	for watch_target in watchos watchossimulator; do
		use_installed_dotnet_cli=false ./build.sh clr.runtime+clr.corelib+clr.nativeaotruntime+clr.nativeaotlibs+clr.tools+clr.crossarchtools+clr.nativecorelib+libs \
			-os "$watch_target" -arch arm64 -cross -c Release
		use_installed_dotnet_cli=false ./build.sh packs -os "$watch_target" -arch arm64 -cross -c Release -p:DotNetBuildAllRuntimePacks=true
	done
	if ! grep -Eq '^INCLUDE_(IOS|TVOS|MAC|MACCATALYST)=1' "$TOP/configure.inc"; then
		exit 0
	fi
fi

use_installed_dotnet_cli=false ./build.sh clr+libs+host+packs+mono -configuration release -arch arm64  -cross /p:UseCrossGen2=false
use_installed_dotnet_cli=false ./build.sh libs+host+packs+mono     -configuration release -arch arm64  -cross /p:UseCrossGen2=false
use_installed_dotnet_cli=false ./build.sh clr+libs+host+packs+mono -configuration release -arch x86_64 -cross /p:UseCrossGen2=false
use_installed_dotnet_cli=false ./build.sh libs+host+packs+mono     -configuration release -arch x86_64 -cross /p:UseCrossGen2=false

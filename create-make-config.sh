#!/bin/bash -eu

set -o pipefail
IFS=$'\n\t '

OUTPUT=Make.config.inc
OUTPUT_FILE="$OUTPUT.tmp.$$"
trap 'rm -f "$OUTPUT_FILE"' EXIT

: > "$OUTPUT_FILE"

LANG=C
export LANG

# Compute commit distances
for platform in $ALL_DOTNET_PLATFORMS; do
	PLATFORM=$(echo "$platform" | tr '[:lower:]' '[:upper:]')
	COMMIT=$(git blame -- ./Make.versions HEAD | awk -v key="${PLATFORM}_NUGET_OS_VERSION=" 'index($0, key) { sub(/^\^/, "", $1); print $1 }')
	if test -n "$COMMIT"; then
		COMMIT_DISTANCE=$(git rev-list --count "$COMMIT..HEAD")
	elif grep -q "^${PLATFORM}_NUGET_OS_VERSION=" Make.versions; then
		# A newly introduced platform has no committed version line yet.
		COMMIT_DISTANCE=0
	else
		echo "Missing ${PLATFORM}_NUGET_OS_VERSION in Make.versions" >&2
		exit 1
	fi
	TOTAL_DISTANCE=$((NUGET_VERSION_COMMIT_DISTANCE_START+COMMIT_DISTANCE))
	printf "${PLATFORM}_NUGET_COMMIT_DISTANCE:=$TOTAL_DISTANCE\\n" >> "$OUTPUT_FILE"
done

STABLE_COMMIT=$(git blame -L '/^[#[:blank:]]*NUGET_RELEASE_BRANCH=/,+1' -- ./Make.config HEAD | sed 's/ .*//' | sed 's/^\^//')
STABLE_COMMIT_DISTANCE=$(git rev-list --count "$STABLE_COMMIT..HEAD")
STABLE_TOTAL_DISTANCE=$((STABLE_COMMIT_DISTANCE+NUGET_VERSION_STABLE_COMMIT_DISTANCE_START))

printf "NUGET_STABLE_COMMIT_DISTANCE:=$STABLE_TOTAL_DISTANCE\\n" >> "$OUTPUT_FILE"

# Detect ccache
if which ccache > /dev/null 2>&1; then
	printf "ENABLE_CCACHE=1\n" >> "$OUTPUT_FILE"
	printf "export CCACHE_BASEDIR=$(cd .. && pwd)\n" >> "$OUTPUT_FILE"
	echo "Found ccache on the system, enabling it"
fi

mv "$OUTPUT_FILE" "$OUTPUT"

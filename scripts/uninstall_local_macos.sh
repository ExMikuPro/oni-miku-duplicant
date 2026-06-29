#!/usr/bin/env bash

set -euo pipefail

TARGET_ROOT="${ONI_LOCAL_MODS_DIR:-$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Local}"
TARGET_LINK="${TARGET_ROOT}/oni-miku-duplicant"

if [[ -L "${TARGET_LINK}" ]]; then
  rm "${TARGET_LINK}"
  echo "Removed local mod symlink: ${TARGET_LINK}"
  exit 0
fi

if [[ -e "${TARGET_LINK}" ]]; then
  echo "Error: target exists but is not a symlink, refusing to remove: ${TARGET_LINK}" >&2
  exit 1
fi

echo "Nothing to uninstall: ${TARGET_LINK}"

#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"
SOURCE_DIR="${REPO_ROOT}/mod_folder"
TARGET_ROOT="${ONI_LOCAL_MODS_DIR:-$HOME/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Local}"
TARGET_LINK="${TARGET_ROOT}/oni-miku-duplicant"

if [[ ! -d "${SOURCE_DIR}" ]]; then
  echo "Error: source mod directory not found: ${SOURCE_DIR}" >&2
  exit 1
fi

mkdir -p "${TARGET_ROOT}"

if [[ -L "${TARGET_LINK}" ]]; then
  CURRENT_TARGET="$(readlink "${TARGET_LINK}")"
  if [[ "${CURRENT_TARGET}" == "${SOURCE_DIR}" ]]; then
    echo "Symlink already installed: ${TARGET_LINK} -> ${CURRENT_TARGET}"
    exit 0
  fi

  echo "Error: target symlink already exists and points elsewhere: ${TARGET_LINK} -> ${CURRENT_TARGET}" >&2
  exit 1
fi

if [[ -e "${TARGET_LINK}" ]]; then
  echo "Error: target path already exists and is not a symlink: ${TARGET_LINK}" >&2
  exit 1
fi

ln -s "${SOURCE_DIR}" "${TARGET_LINK}"
echo "Installed local mod symlink:"
echo "${TARGET_LINK} -> ${SOURCE_DIR}"

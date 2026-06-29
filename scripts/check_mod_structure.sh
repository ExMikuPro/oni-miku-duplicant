#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"
MOD_DIR="${REPO_ROOT}/mod_folder"

FAILURES=0

check_file() {
  local path="$1"

  if [[ -f "${path}" ]]; then
    echo "[OK] ${path}"
  else
    echo "[MISSING] ${path}" >&2
    FAILURES=$((FAILURES + 1))
  fi
}

check_file "${MOD_DIR}/mod.yaml"
check_file "${MOD_DIR}/personalities.json"

shopt -s nullglob
PO_FILES=("${MOD_DIR}"/strings/*.po)
shopt -u nullglob

if (( ${#PO_FILES[@]} > 0 )); then
  echo "[OK] Found ${#PO_FILES[@]} localization file(s) in ${MOD_DIR}/strings"
else
  echo "[MISSING] No .po files found in ${MOD_DIR}/strings" >&2
  FAILURES=$((FAILURES + 1))
fi

if (( FAILURES > 0 )); then
  echo "Mod structure check failed with ${FAILURES} issue(s)." >&2
  exit 1
fi

echo "Mod structure check passed."

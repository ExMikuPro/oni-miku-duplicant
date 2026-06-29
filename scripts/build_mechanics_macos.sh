#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "${SCRIPT_DIR}/.." && pwd)"
PROJECT_PATH="${REPO_ROOT}/src/PrintMiku.Mechanics/PrintMiku.Mechanics.csproj"

if [[ ! -f "${PROJECT_PATH}" ]]; then
  echo "Error: mechanics project not found: ${PROJECT_PATH}" >&2
  exit 1
fi

if command -v dotnet >/dev/null 2>&1; then
  dotnet build "${PROJECT_PATH}" -c Release
  exit 0
fi

if command -v msbuild >/dev/null 2>&1; then
  msbuild "${PROJECT_PATH}" /p:Configuration=Release
  exit 0
fi

echo "No supported CLI build tool found." >&2
echo "Build this project from Rider instead: ${PROJECT_PATH}" >&2
exit 1

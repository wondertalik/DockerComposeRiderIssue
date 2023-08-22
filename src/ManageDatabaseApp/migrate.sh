#!/bin/bash
set -eux

ARCH=amd64
if [ "$(uname -m)" = "aarch64" ]; then ARCH=arm64; fi
./efbundle-${ARCH}

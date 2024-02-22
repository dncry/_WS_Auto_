#!/usr/bin/env bash

set -e

docker run \
  -e UNITY_USERNAME \
  -e UNITY_PASSWORD \
  -e UNITY_SERIAL \
  -e BUILD_VERSION \
  -e BUILD_BUNDLECODE \
  -e BUILD_PATH \
  -e BUILD_SEPARATEASSET \
  -e BUILD_BUILDAAB \
  -e BUILD_TARGET \
  -e BUILD_METHOD \
  -w /project/ \
  -v $WORKSPACE_OS:/project:cached \
  -v $GRADLECACHE_OS:/root/.gradle \
  --rm \
  $IMAGE \
  /bin/bash -c "${CI_DIR_UNITY}/before_script.sh && ${CI_DIR_UNITY}/build.sh"

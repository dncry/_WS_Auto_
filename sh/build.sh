#!/usr/bin/env bash

set -e
set -x

echo "Building for $BUILD_TARGET"


${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
  -projectPath / \
  -quit \
  -nographics \
  -logFile /dev/stdout \
  -username $UNITY_USERNAME \
  -password $UNITY_PASSWORD \
  -serial $UNITY_SERIAL \
  -executeMethod $BUILD_METHOD  \
  -buildVersion $BUILD_VERSION \
  -buildBundleCode $BUILD_BUNDLECODE \
  -buildPath $BUILD_PATH \
  -buildSeparateAsset $BUILD_SEPARATEASSET \
  -buildAAB $BUILD_BUILDAAB \
  -buildTarget $BUILD_TARGET

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 101 ]; then
  echo "Run failure (other failure)";
elif [ $UNITY_EXIT_CODE -eq 102 ]; then
  echo "Run cancelled"; 
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi


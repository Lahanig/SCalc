#!/bin/bash
cd "$(dirname "$0")/.."

echo "Build SCalc for Windows..."

dotnet publish src/SCalc/SCalc.csproj \
	-c Release \
	-r win-x64 \
	--self-contained true \
	-p:PublishSingleFile=true \
	-p:PublishReadyToRun=true \
	-o ./artifacts/win-x64

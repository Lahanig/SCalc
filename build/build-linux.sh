#!/bin/bash
cd "$(dirname "$0")/.."

echo "Build SCalc for Linux..."

dotnet publish src/SCalc/SCalc.csproj \
	-c Release \
	-r linux-x64 \
	--self-contained true \
	-p:PublishSingleFile=true \
	-p:PublishReadyToRun=true \
	-o ./artifacts/linux-x64

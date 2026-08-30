#!/bin/bash
cd "$(dirname "$0")/.."

echo "Build SCalc for Linux..."

dotnet publish src/SCalc/SCalc.csproj \
	-c Release \
	-r linux-x64 \
	-p:PublishAot=true \
	-p:CppCompilerAndLinker=gcc \
	-o ./artifacts/linux-x64/native

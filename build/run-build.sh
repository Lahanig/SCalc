cd "$(dirname "$0")"

if [[ "$1" == *linux-native* ]]; then
	./build-linux-native.sh
	exec ../artifacts/linux-x64/native/publish/SCalc/release_linux-x64/SCalc
fi

if [[ "$1" == *linux* ]]; then
	./build-linux.sh
	exec ../artifacts/linux-x64/default/publish/SCalc/release_linux-x64/SCalc
fi

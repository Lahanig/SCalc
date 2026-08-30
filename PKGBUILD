pkgname=Scalc
pkgver=0.1.2
pkgrel=1
pkgdesc="Simple CLI calculator"
arch=('x86_64')
url="https://github.com/Lahanig/SCalc"
license=('MIT')
depends=('glibc')
makedepends=('dotnet-sdk>=10.0.11')

source=()
sha256sums=()

build() {
	cd "$startdir"

	echo "Compile NativeAOT with GCC..."

	dotnet publish $srcdir/SCalc/SCalc.csproj \
		-c Release \
		-r linux-x64 \
		-p:PublishAot=true \
		-p:CppCompilerAndLinker=gcc \
		-p:StripSymbols=true \
		--artifacts-path "$startdir/artifacts/linux-x64/native/"
}

package() {
	cd "$startdir"

	install -Dm755 "$startdir/artifacts/linux-x64/native/publish/SCalc/release_linux-x64/SCalc" "$pkgdir/usr/bin/$pkgname"

	install -Dm644 LICENSE "$pkgdir/usr/share/licenses/$pkgname/LICENSE"
	install -Dm644 README.md "$pkgdir/usr/share/doc/$pkgname/README.md"
}

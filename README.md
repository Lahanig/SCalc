# SCalc
 Simple CLI calculator

---

<h1>Installation</h1>

### Arch Linux

Download package from latest release
```bash
cd /directory/where/package/downloaded

sudo pacman -U SCalc-{pkgver}-1-x86_64.pkg.tar.zst 
```
or

```bash
git clone https://github.com/Lahanig/SCalc.git
cd SCalc
./build/make-pkg.sh # or makepkg -si
```

### Other Distros

Build bin file from sources

```bash
git clone https://github.com/Lahanig/SCalc.git
cd SCalc
./build/build-linux.sh # output path /artifacts/linux-x64/default/publish/SCalc/release_linux-x64
```
or

```bash
#Build AOT
git clone https://github.com/Lahanig/SCalc.git
cd SCalc
./build/build-linux-native.sh # output path /artifacts/linux-x64/native/publish/SCalc/release_linux-x64
```

### Windows
Build bin file from sources

```bash
git clone https://github.com/Lahanig/SCalc.git
cd SCalc
./build/build-win.sh # output path /artifacts/win-x64/default/publish/SCalc/release_win-x64
```

<h1>Usage</h1>

For run app from terminal

```bash
SCalc
```
or

```bash
./path/to/bin/file
```

#!/bin/bash

dotnet build

TMPDIR=zip
mkdir -p "$TMPDIR"

cp Assemblies/NoFog.dll "$TMPDIR"
cp icon.png manifest.json README.md CHANGELOG.md "$TMPDIR"

rm -f NoFog.zip
( cd "$TMPDIR";
zip -r ../NoFog.zip *
)

rm -r "$TMPDIR"

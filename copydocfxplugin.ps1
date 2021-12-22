# Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
New-Item -ItemType Directory -Force -Path ./samples/docfx/simple_template/plugins
copy ./src/shiny.docfx.extensions/bin/debug/net48/shiny.docfx.extensions.dll ./samples/docfx/simple_template/plugins/
docfx samples\docfx\docfx.json --serve
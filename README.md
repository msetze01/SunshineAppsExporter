# SunshineAppsExporter
SunshineAppsExporter is an extension for [Playnite](https://playnite.link/) which allows you to export your Playnite library 
as a list of apps for [Sunshine](https://github.com/LizardByte/Sunshine), enabling you to keep your gaming library in sync 
with your gamestream server.

## Features
* Export your Playnite library to a Sunshine-compatible `apps.json` file
* Automatically detect and use library plugin integrations like Steam
* Convert game cover images to Sunshine-friendly PNGs
* Restart Sunshine (if installed as a service)

## Todo
- [x] Steam launcher support
- [x] Epic launcher support
- [x] Playnite start support
- [x] Skip export on tag
- [ ] Export on tag / configurable tags
- [ ] Configuration settings and auto-detection for needed application path(s)
- [ ] Better sunshine service/exe management
- [ ] Support URL and database sources for cover images
- [ ] Support [game variables](https://playnite.link/docs/master/manual/gameVariables.html)
- [ ] Support pre-start [Game scripts](https://playnite.link/docs/master/manual/gameScripts.html)
- [ ] Actual user documentation

This is a work-in-progress, so things like hardcoded paths and data-corrupting bugs may be present. 
# Descenders Next Map Loader
BepInEx plugin for Descenders Next which allows you to play custom maps.

## Known issues
1. Sometimes the game will take you back to the original map.
    - **Workaround**: Launch the custom map again. Seems to be timing related.
2. Custom objectives are not supported, the previous map's objectives will stay on screen for now.
3. Creating replays does not work at the moment.
    - Unless you add EnviroManager to your scene if you own the [Enviro 3](https://assetstore.unity.com/packages/tools/particles-effects/enviro-3-sky-and-weather-236601) addon.
4. Trying to load the original map in the main menu will bring up an error.
    - You will need to restart the game.

## How to use
1. Download BepInEx v5 from [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
    -  BepInEx_win_x64_5.4.23.3.zip (latest version as of writing)
2. Extract and copy BepInEx's files into the game's root directory. For more information refer to [https://docs.bepinex.dev/articles/user_guide/installation/index.html](https://docs.bepinex.dev/articles/user_guide/installation/index.html)

   <img width="603" height="281" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/BepInEx.png" />

3. Launch the game at least once to generate BepInEx's configuration files.
4. Open `C:\Program Files (x86)\Steam\steamapps\common\Descenders Next\BepInEx\config\BepInEx.cfg` in a text editor, find and change `HideManagerGameObject = false` to `HideManagerGameObject = true` otherwise the map loader won't work.

   <img width="858" height="171" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/BepInEx_Config.png" />

5. Download the latest version of dnext_maploader from [here](https://github.com/Notexe/dnext_maploader/releases/latest/download/dnext_maploader.zip).
6. Copy `dnext_maploader.dll` into `C:\Program Files (x86)\Steam\steamapps\common\Descenders Next\BepInEx\plugins\`

    <img width="683" height="154" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/BepInEx_Plugins.png" />

7. Copy custom maps into `C:\Program Files (x86)\Steam\steamapps\common\Descenders Next\Descenders Next_Data\CustomMaps\`

    <img width="660" height="150" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/CustomMaps_Folder.png" />

8. Change your game to **offline mode** in the settings.
    - Multiplayer has not been tested at all.
9. Wait for a map to load fully in-game.
10. Press F4 and click on one of the buttons to launch into a custom map.

    <img width="310" height="209" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/MapLoader_GUI.png" />

11. Congratulations you should now be fully loaded into a custom map!

    <img width="1282" height="752" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/Ingame_Example.png" />

## Making a map

Go here [docs/MAKING_A_MAP.md](docs/MAKING_A_MAP.md) for instructions on how to make your own map.
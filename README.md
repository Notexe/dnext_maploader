# Descenders Next Map Loader
BepInEx plugin for Descenders Next which allows you to play custom maps.

## Known issues
1. Sometimes the game will take you back to the original map.
    - **Workaround**: Launch the custom map again. Seems to be timing related.
2. Respawn takes you to the wrong location.
    - Respawn on track works fine.
3. Timer does not reset back to 0 seconds when you respawn on track.
4. Custom objectives are not supported, the previous map's objectives will stay on screen for now.

## How to use
1. Download BepInEx v5 from [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
    -  BepInEx_win_x64_5.4.23.3.zip (latest version as of writing)
2. Extract and copy the files into the game's root directory.
3. Copy `dnext_maploader.dll` into `\Descenders Next\BepInEx\plugins\`
4. Copy custom maps into `\Descenders Next\Descenders Next_Data\CustomMaps\`

## Making a map
1. You will need Unity 2022.3.50f1 which you can get from [https://unity.com/releases/editor/whats-new/2022.3.50#installs](https://unity.com/releases/editor/whats-new/2022.3.50#installs)
2. Your scene will need the following game objects:
    1. `StartPoint` (untagged)
    2. `FinishPoint` (untagged)
        - `FinishPoint` will need a `Box Collider` component with "Is Trigger" enabled.
    3. Terrain with a `Terrain Collider` and whatever else you want in your map.
        - The MicroSplat terrain addon is supported since the game has the necessary scripts for it.
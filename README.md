# Descenders Next Map Loader
BepInEx plugin for Descenders Next which allows you to play custom maps.

## Known issues
1. Sometimes the game will take you back to the original map.
    - **Workaround**: Launch the custom map again. Seems to be timing related.
2. Respawn takes you to the wrong location.
    - Respawn on track works fine.
3. Timer does not reset back to 0 seconds when you respawn on track.
4. Custom objectives are not supported, the previous map's objectives will stay on screen for now.
5. Creating replays does not work at the moment.
6. Trying to load the original map in the main menu will bring up an error.
    - You will need to restart the game.

## How to use
1. Download BepInEx v5 from [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
    -  BepInEx_win_x64_5.4.23.3.zip (latest version as of writing)
2. In `\Descenders Next\BepInEx\config\BepInEx.cfg` change `HideManagerGameObject = false` to `HideManagerGameObject = true` otherwise the map loader won't work.
3. Extract and copy the files into the game's root directory.
4. Download the latest version of dnext_maploader from [here](https://github.com/Notexe/dnext_maploader/releases/latest/download/dnext_maploader.zip).
5. Copy `dnext_maploader.dll` into `\Descenders Next\BepInEx\plugins\`
6. Copy custom maps into `\Descenders Next\Descenders Next_Data\CustomMaps\`
7. Change your game to **offline mode** in the settings.
    - Multiplayer has not been tested at all.
8. Wait for a map to load fully in-game.
9. Press F4 and click on one of the buttons to launch into a custom map.

## Making a map
1. You will need Unity 2022.3.50f1 which you can get from [https://unity.com/releases/editor/whats-new/2022.3.50#installs](https://unity.com/releases/editor/whats-new/2022.3.50#installs)
2. Your scene will need the following game objects:
    1. Empty `StartPoint` (untagged)
    2. Empty `FinishPoint` (untagged)
        - `FinishPoint` will need a `Box Collider` component with "Is Trigger" enabled.
    3. Terrain with a `Terrain Collider` and whatever else you want in your map.
        - The MicroSplat terrain addon is supported since the game has the necessary scripts for it.
3. Creating the AssetBundle:
    1. Create a folder called `Editor` in your project's `Assets` folder.
    2. Create a new script called `CreateAssetBundles.cs` in that folder with the following script:
    ```csharp
    using UnityEditor;
    using System.IO;

    public class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/AssetBundles";
            if(!Directory.Exists(assetBundleDirectory))
                Directory.CreateDirectory(assetBundleDirectory);

            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            BuildTarget.StandaloneWindows);
        }
    }
    ```
    3. Click on your scene in the project window.
    4. In the inspector window you should find a `AssetBundle` field
    5. Click on the dropdown box and choose `New...` and give your AssetBundle a name (this will be the name of your map)
        - The mod loader only supports loading a single scene from an AssetBundle so adding in any extras won't work.
    6. Export the AssetBundle under `Assets` -> `Build AssetBundles` in the menu bar
    7. Copy the exported AssetBundle from the `AssetBundles` folder in your project to ``\Descenders Next\Descenders Next_Data\CustomMaps\`
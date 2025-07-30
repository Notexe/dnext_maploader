# Making a map

## Prerequisites

- **Unity Version:** Download and install [Download Unity 2022.3.50f1](https://unity.com/releases/editor/whats-new/2022.3.50#installs)

## Scene setup

### 1. `StartPoint` (Empty GameObject)
- **Must exist**
- No tag needed (`untagged`)

### 2. Terrain and Collision
- Add **any objects with collision**, such as:
  - `Terrain Collider`
  - `Box Collider`
  - Custom geometry with colliders
- **Optional Terrain Tool:** [MicroSplat](https://assetstore.unity.com/packages/tools/terrain/microsplat-96478) (already supported by the game)

## Optional Scene Features

### FinishPoint

- Create an empty GameObject named `FinishPoint`
- Add:
  - `Box Collider` component
  - Enable **"Is Trigger"**

<img width="1513" height="644" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/FinishPoint.png" />


### Checkpoints

- Parent GameObject: `Checkpoints` (Empty, untagged, **exact name**)
- Add child objects for each checkpoint:
  - Format: `Checkpoint_0`, `Checkpoint_1`, etc.
  - Each must have a `Box Collider` with **"Is Trigger"**

<img width="1346" height="647" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/Checkpoint_Example.png" />

### GrindObjects

- GameObject that you wish to have GrindObject support needs to start with the name either `GrindObject_Rail` or `GrindObject_Box`
    - `GrindObject_Rail` requires Empty `node_#` objects that run across the top of the rail

<img width="1567" height="646" alt="image" src="https://github.com/Notexe/dnext_maploader/raw/main/docs/screenshots/GrindObject_Example.png" />

## Creating the AssetBundle

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

using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RageSquid.BoardGame;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dnext_maploader;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Descenders Next.exe")]
public class Plugin : BaseUnityPlugin
{
    private bool menuToggle = false;
    private Rect windowRect = new Rect(10, 10, 300, 200);
    Vector2 scrollPosition;

    private AssetBundle bundle;
    private string[] bundles;
    private GameObject player;

    private string sceneName;
    private LocalPlayerInfo playerInfo;

    internal static new ManualLogSource Logger;

    public void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    }

    public void Start()
    {
        bundles = Directory.GetFiles(Application.dataPath + "\\CustomMaps");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            menuToggle = !menuToggle;
        }
    }

    public void OnGUI()
    {
        GUI.backgroundColor = new Color(0.18f, 0.18f, 0.18f, 1f);
        if (menuToggle)
        {
            windowRect = GUI.Window(0, windowRect, MapLoaderWindow, $"{MyPluginInfo.PLUGIN_NAME} - v{MyPluginInfo.PLUGIN_VERSION}");
        }
    }

    void MapLoaderWindow(int windowID)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(280), GUILayout.Height(180));

        GUILayout.BeginVertical();

        for (int i = 0; i < bundles.Length; i++)
        {
            if (GUILayout.Button(Path.GetFileNameWithoutExtension(bundles[i]), GUILayout.Width(250), GUILayout.Height(25)))
            {
                player = PlayerManager.GetPlayerGameObject();
                if (player == null)
                {
                    Logger.LogInfo("Player not found in scene.");
                }
                else
                {
                    Logger.LogInfo($"Found Player on GameObject: {player.name}");
                    DontDestroyOnLoad(player);
                }

                menuToggle = false;
                LoadBundle(i);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUI.DragWindow();
    }

    private void LoadBundle(int scene)
    {
        if (bundle != null)
        {
            bundle.Unload(true);
        }
        if (bundles != null && bundles.Length > 0 && File.Exists(bundles[scene]))
        {
            bundle = AssetBundle.LoadFromFile(bundles[scene]);
        }
        else
        {
            Logger.LogError("No valid asset bundle found in CustomMaps folder.");
            return;
        }

        base.StartCoroutine(this.LoadScene());
    }

    private IEnumerator LoadScene()
    {
        this.sceneName = bundle.GetAllScenePaths()[0];
        Services.SessionManager.currentLevel.levelData.SceneOverride = this.sceneName;
        yield return SceneManager.LoadSceneAsync(this.sceneName, LoadSceneMode.Single);
        yield return new WaitForEndOfFrame();

        GameObject startPoint = GameObject.Find("StartPoint");
        GameObject finishPoint = GameObject.Find("FinishPoint");

        startPoint.AddComponent<ParkLineStart>();
        finishPoint.AddComponent<LineFinish>();

        LineStart lineStart = startPoint.GetComponent<LineStart>();
        LineFinish lineFinish = finishPoint.GetComponent<LineFinish>();
        LineStart[] lineStarts;
        LineFinish[] lineFinishes;

        lineStarts = [lineStart];
        lineFinishes = [lineFinish];

        startPoint.tag = "Start";
        finishPoint.tag = "Finish";

        Vector3 position = startPoint.transform.position;
        Quaternion rotation = startPoint.transform.rotation;

        Logger.LogInfo(position);
        Logger.LogInfo(rotation);

        playerInfo = PlayerManager.SP.GetLocalPlayer();
        playerInfo.SetSpawnpoint(position, rotation, false, true);

        Services.SessionManager.currentLevel.SetStartFinish(lineStarts, lineFinishes);
        Services.SessionManager.timer.Reset();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("GrindObject_Rail"))
            {
                GrindObject grindComponent = obj.GetComponent<GrindObject>();
                if (grindComponent == null)
                {
                    grindComponent = obj.AddComponent<GrindObject>();
                }

                grindComponent.grindType = GrindType.Rail;

                Logger.LogInfo($"Found GrindObject: {obj.name}");

                foreach (Transform child in obj.transform)
                {
                    SplineExtraInfo splineInfo = child.GetComponent<SplineExtraInfo>();
                    if (splineInfo == null)
                    {
                        child.gameObject.AddComponent<SplineExtraInfo>();
                        Logger.LogInfo($"Added SplineExtraInfo to child: {child.name}");
                    }
                }
            }
            if (obj.name.StartsWith("GrindObject_Box"))
            {
                GrindObject grindComponent = obj.GetComponent<GrindObject>();
                if (grindComponent == null)
                {
                    grindComponent = obj.AddComponent<GrindObject>();
                }

                grindComponent.grindType = GrindType.Box;

                Logger.LogInfo($"Found GrindObject: {obj.name}");
            }
            if (obj.name.StartsWith("Checkpoints"))
            {
                foreach (Transform child in obj.transform)
                {
                    Checkpoint checkpoint = child.GetComponent<Checkpoint>();
                    if (checkpoint == null)
                    {
                        checkpoint = child.gameObject.AddComponent<Checkpoint>();
                        Logger.LogInfo($"Added Checkpoint to child: {child.name}");
                    }
                    
                    string name = child.name;
                    int index = name.LastIndexOf("_");
                    if (index >= 0 && index < name.Length - 1)
                    {
                        string checkpointId = name.Substring(index + 1);
                        if (int.TryParse(checkpointId, out int checkpointIdInt))
                        {
                            checkpoint.Id = checkpointIdInt;
                            Logger.LogInfo($"Assigned id: ${checkpointIdInt} to ${child.name}");
                        }
                    }
                }
            }
        }

        base.StartCoroutine(this.SetPlayerPosition());
    }

    private IEnumerator SetPlayerPosition()
    {
        while (!GameObject.Find("StartPoint"))
        {
            player.transform.position = new Vector3(1000f, 0, 1000f);
            yield return new WaitForEndOfFrame();
        }
        Vector3 position = GameObject.Find("StartPoint").transform.position;
        Quaternion rotation = GameObject.Find("StartPoint").transform.rotation;
        player.transform.position = position;
        player.transform.rotation = rotation;
        Logger.LogInfo(GameObject.Find("StartPoint"));
        Logger.LogInfo("position: " + player.transform.position);
        Logger.LogInfo("rotation" + player.transform.rotation);
        UnityEngine.Object.Destroy(GameObject.Find("StartPoint"));

        yield break;
    }
}

using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SFB;
using UnityEngine;
using System.IO;

namespace BopCustomTextures;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class ShellMixtapeOpenerPlugin : BaseUnityPlugin
{

    public static new ManualLogSource Logger;
    public Harmony Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        Harmony.PatchAll();
    }

    [HarmonyPatch(typeof(CreditsScript), "Update")]
    private static class CreditsScriptUpdatePatch
    {
        static void Postfix(CreditsScript __instance)
        {
            if (!__instance.IsOpen)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.M) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", StageSelectScript.LastMixtapeLocation, AlphaDisclaimerScript.GetMixtapeExtensionFilters(), multiselect: false);
                if (paths.Length != 0 && paths[0] != "")
                {
                    string mixtape = paths[0];
                    Logger.LogInfo($"Opening mixtape: {mixtape}");
                    StageSelectScript.LastMixtapeLocation = Path.GetDirectoryName(mixtape);
                    RiqLoader.path = mixtape;
                    Camera.main.enabled = false;
                    TempoSceneManager.LoadScene(SceneKey.RiqLoader);
                } 
                else
                {
                    Logger.LogInfo("Didn't provide mixtape to open");
                }
            }
        }
    }
}

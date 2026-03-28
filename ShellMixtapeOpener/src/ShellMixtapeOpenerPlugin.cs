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

    public static bool hasSelected = false;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        Harmony.PatchAll();
    }

    private static void OpenMixtape(bool autoplay)
    {
        StandaloneFileBrowser.OpenFilePanelAsync(autoplay ? "Open File (Autoplay)" : "Open File", StageSelectScript.LastMixtapeLocation, AlphaDisclaimerScript.GetMixtapeExtensionFilters(), multiselect: false, delegate (string[] paths)
        {
            if (paths.Length != 0 && paths[0] != "")
            {
                string mixtape = paths[0];
                Logger.LogInfo($"Opening mixtape: {mixtape}");
                StageSelectScript.LastMixtapeLocation = Path.GetDirectoryName(mixtape);
                MixtapeLoaderCustom.autoplay = autoplay;
                RiqLoader.path = mixtape;
                hasSelected = true;
            }
            else
            {
                Logger.LogInfo("Didn't provide mixtape to open");
            }
        });
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

            if (hasSelected)
            {
                Camera.main.enabled = false;
                hasSelected = false;
                TempoSceneManager.LoadScene(SceneKey.RiqLoader);
                return;
            } 
            
            if (Input.GetKeyDown(KeyCode.M) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                OpenMixtape(false);
            }
            else if (Input.GetKeyDown(KeyCode.A) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                OpenMixtape(true);
            }
        }
    }
}

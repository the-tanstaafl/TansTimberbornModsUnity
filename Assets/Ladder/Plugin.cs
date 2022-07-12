using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TimberbornAPI;
using TimberbornAPI.Common;


namespace Ladder
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.timberapi.timberapi")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "tobbert.ladder";
        public const string PluginName = "Ladder";
        public const string PluginVersion = "1.0.0";
        
        public static ManualLogSource Log;
        
        void Awake()
        {
            Log = Logger;
            
            Log.LogInfo($"Loaded {PluginName} Version: {PluginVersion}!");
            
            TimberAPI.AssetRegistry.AddSceneAssets(PluginGuid, SceneEntryPoint.Global);
            
            TimberAPI.DependencyRegistry.AddConfiguratorBeforeLoad(new LoadingAssets.LadderConfigurator(), SceneEntryPoint.MainMenu);
            
            // new Harmony(PluginGuid).PatchAll();
        }

    }
}
using System;
using BepInEx;
using BepInEx.Logging;
using TimberbornAPI;
using TimberbornAPI.Common;
using Timberborn.Warehouses;

using HarmonyLib;

namespace TinyLogpile
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.timberapi.timberapi")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "knattetobbert.tinylogpile";
        public const string PluginName = "Tinylogpile";
        public const string PluginVersion = "1.0.1";

        public static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            
            Log.LogInfo($"Loaded {PluginName} Version: {PluginVersion}!");

            TimberAPI.AssetRegistry.AddSceneAssets(PluginGuid, SceneEntryPoint.Global);
            new Harmony(PluginGuid).PatchAll();
        }
    }
    [HarmonyPatch(typeof(LogPileStockpileVisualizer), "InitializeLogStack", new Type[] { typeof(int) , typeof(int) })]
    public class FixInitialStackPatch
    {
        static bool Prefix(LogPileStockpileVisualizer __instance, int x, int y)
        {
            var BuildingComp = __instance.gameObject.GetComponent<Stockpile>().name.Replace("(Clone)", "");
            var BuildingnameName = BuildingComp.Split(".");

            bool Returnvalue = true;
            if (x == 0 & y == 0)
            {
                Plugin.Log.LogInfo($"*Right Stack startup X: {x} y: {y} Model: {BuildingnameName[0]}");
                return Returnvalue;
            }
            if (x == 0 & y == 1)
            {
                Plugin.Log.LogInfo($"*Right Stack startup X: {x} y: {y} Model: {BuildingnameName[0]}");
                return Returnvalue;
            }
            if (BuildingnameName[0] == "TinyLogPile" | BuildingnameName[0] == "TinyMetalLogPile")
            {
                Plugin.Log.LogInfo($" Wrong Stack Should place X: {x} y: {y} Model: {BuildingnameName[0]}");
                return !Returnvalue;
            }
            else
            {
                Plugin.Log.LogInfo($" Wrong Stack Should place X: {x} y: {y} Model: {BuildingnameName[0]}");
                return Returnvalue;
            }
        }
    }
}
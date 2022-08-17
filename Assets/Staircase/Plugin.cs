using BepInEx;
using BepInEx.Logging;
using TimberbornAPI;
using TimberbornAPI.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HarmonyLib;
using Timberborn.BuildingRange;
using Timberborn.Effects;
using Timberborn.NeedSpecifications;
using Timberborn.PreviewSystem;
using TimberbornAPI.AssetLoaderSystem.ResourceAssetPatch;
using Timberborn.RangedEffectSystem;
using UnityEngine;


namespace Staircase
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.timberapi.timberapi")]
    [BepInDependency("tobbert.categorybutton")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "knattetobbert.staircase";
        public const string PluginName = "Staircase";
        public const string PluginVersion = "1.3.4";

        public static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            
            Log.LogInfo($"Loaded {PluginName} Version: {PluginVersion}!");

            TimberAPI.AssetRegistry.AddSceneAssets(PluginGuid, SceneEntryPoint.Global);
            new Harmony(PluginGuid).PatchAll();
        }
    }
    [HarmonyPatch(typeof(Debug), "LogWarning", typeof(object))]
    public class LogWarningPatch
    {
        static bool Prefix(object message, bool __runOriginal)
        {
            if (__runOriginal)
            {
                string mess = message as string;
                if (mess.Contains("path marker mesh at"))
                {
                    return false;
                }
            }
            return __runOriginal;
        }
    }
    
    [HarmonyPatch(typeof(RangedEffectBuilding), "RangeNames", new Type[] { })]
    public class PreventOrangePatch
    {
        static void Postfix(ref IEnumerable<string> __result)
        {
            foreach (var rangeName in __result)
            {
                if (rangeName == "MetalStaircase")
                {
                    __result = Enumerable.Empty<string>();
                }
            }
        }
    }
    /*
    [HarmonyPatch(typeof(EffectDescriber), "DescribeRangeEffects", new Type[] { typeof(IEnumerable<ContinuousEffectSpecification>), typeof(StringBuilder), typeof(StringBuilder), typeof(int) })]
    public class PreventDescriber2Patch
    {
        static void Prefix(
            ref IEnumerable<ContinuousEffectSpecification> effects,
            StringBuilder description,
            StringBuilder tooltip,
            int range)
        {
            ;
            var Testvalue = "StairMovementSpeed";// Effect to test and skipp to show
            foreach (var continuousEffectSpecification in effects)
            {
                if (continuousEffectSpecification.NeedId == Testvalue)
                {
                    var effectList = effects.ToList();

                    effectList.Remove(effectList.First(x => continuousEffectSpecification.NeedId == Testvalue));

                    IEnumerable<ContinuousEffectSpecification> test = effectList;

                    effects = test;

                }
            }
        }
    }*/
}
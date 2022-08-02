using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using Bindito.Core;
using Bindito.Unity;
using HarmonyLib;
using Timberborn.Coordinates;
using Timberborn.PathSystem;
using Timberborn.TemplateSystem;
using TimberbornAPI;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using TimberbornAPI.AssetLoaderSystem.ResourceAssetPatch;
using TimberbornAPI.Common;
using TimberbornAPI.PluginSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Object = System.Object;

namespace MorePaths
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("com.timberapi.timberapi")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "tobbert.morepaths";
        public const string PluginName = "More Paths";
        public const string PluginVersion = "1.0.0";
        
        public Vector3Int localCoordinates { get; set; }
        
        public static ManualLogSource Log;

        void Awake()
        {
            Log = Logger;
            
            Log.LogInfo($"Loaded {PluginName} Version: {PluginVersion}!");
            
            TimberAPI.AssetRegistry.AddSceneAssets(PluginGuid, SceneEntryPoint.InGame);
            
            // TimberAPI.DependencyRegistry.AddPrefabConfigurator(gameObject.AddComponent<MorePathsConfigurator>());
            TimberAPI.DependencyRegistry.AddConfigurator(new MorePathsConfigurator());
            new Harmony(PluginGuid).PatchAll();
        }
    }

    [HarmonyPatch(typeof(TimberApiResourceAssetLoader), "FixMaterialShader", new Type[] {typeof(GameObject), typeof(Shader)})]
    public class ChangeShaderPatch
    {
        static void Prefix(
            ref GameObject obj,
            ref Shader shader)
        {
            Plugin.Log.LogFatal(obj.name);
            if (obj.GetComponent<MeshRenderer>())
            {
                if (obj.GetComponent<MeshRenderer>().materials[0])
                {
                    if (obj.GetComponent<MeshRenderer>().materials[0].name == "StonePath(Version 3) (Instance)")
                    {
                        shader = Resources.Load<GameObject>("Buildings/Paths/Path/DirtDrivewayStraightPath")
                            .GetComponent<MeshRenderer>().materials[0].shader;
                    }
                }
            }
        }
    }
    
    [HarmonyPatch(typeof(DrivewayModel), "UpdateModel", new Type[] {})]
    public class ChangeDrivewayModelPatch
    {
        
        // [HarmonyPatch(typeof(DrivewayModel), "GetLocalCoordinates", new Type[] {})]
        // public class GetLocalCoordinatesPatch
        // {
        //     static void Postfix(Vector3Int __result)
        //     {
        //         TimberAPI.DependencyContainer.GetInstance<CanReachThis>().LocalCoordinates = __result;
        //     }
        // }
        //
        // [HarmonyPatch(typeof(DrivewayModel), "GetLocalDirection", new Type[] {})]
        // public class GetLocalDirectionPatch
        // {
        //     static void Postfix(Direction2D __result)
        //     {
        //         TimberAPI.DependencyContainer.GetInstance<CanReachThis>().LocalDirection = __result;
        //     }
        // }

        static void Prefix(DrivewayModel __instance, ref GameObject ____model)
        {
            var parameters = new object[] { };
            MethodInfo methodInfo;
            
            methodInfo = typeof(DrivewayModel).GetMethod("GetLocalCoordinates", BindingFlags.NonPublic | BindingFlags.Instance);
            var LocalCoordinates = (Vector3Int)methodInfo.Invoke(__instance, parameters);
            // Plugin.Log.LogFatal(test.x.ToString() + "- -" + test.y.ToString() + "- -" + test.z.ToString());
            methodInfo = typeof(DrivewayModel).GetMethod("GetLocalDirection", BindingFlags.NonPublic | BindingFlags.Instance);
            var LocalDirection = (Direction2D)methodInfo.Invoke(__instance, parameters);
            // Plugin.Log.LogFatal(test.x.ToString() + "- -" + test.y.ToString() + "- -" + test.z.ToString());
            
            
            // foreach (var component in __instance.GetComponents())
            // {
            //     Plugin.Log.LogFatal(component.name);
            // }
            // Plugin.Log.LogFatal(__instance.GetComponent<StoneDrivewayModelInstantiator>().name);
            // Plugin.Log.LogFatal(__instance.GetComponent<StoneDrivewayModelInstantiatorConfigurator>().GetComponentInChildren<StoneDrivewayModelInstantiator>().name);
            // var _model = __instance.GetComponent<StoneDrivewayModelInstantiator>().InstantiateModel(__instance, new Vector3Int(0, 0, 0), Direction2D.Up);

            // AccessTools.GetMethodNames(__instance);
            // Plugin.Log.LogFatal(TimberAPI.DependencyContainer.GetInstance<CanReachThis>().LocalCoordinates);
            // Plugin.Log.LogFatal(TimberAPI.DependencyContainer.GetInstance<CanReachThis>().LocalDirection);
            // Vector3Int test = Traverse.Create<DrivewayModel>().Method("GetLocalCoordinates").GetValue<Vector3Int>();
            // Plugin.Log.LogFatal(test);
            // return;

            // Plugin.Log.LogFatal(____model);
            GameObject _model = TimberAPI.DependencyContainer.GetInstance<CanReachThis>().CanCallThis(__instance, LocalCoordinates, LocalDirection);
            // Plugin.Log.LogFatal(_model.name);
            ____model = _model;

            // var WorldField = typeof(DrivewayModel).GetField("_model", BindingFlags.Instance | BindingFlags.NonPublic);
            // WorldField.SetValue(_model, _model);



            // Vector3Int positionedCoordinates = !__instance._hasCustomCoordinates ? __instance._blockObject.PositionedEntrance.DoorstepCoordinates : __instance._blockObject.Transform(__instance._customCoordinates - __instance._customDirection.ToOffset());
            // Direction2D positionedDirection1 = __instance.GetPositionedDirection();
            // __instance.model
            // Plugin.Log.LogFatal("UpdateModel Drivewaymodel");
        }
    }
    
    public class CanReachThis
    {
        // private StoneDrivewayModelInstantiator _stoneDrivewayModelInstantiator;
        //
        // CanReachThis(StoneDrivewayModelInstantiator stoneDrivewayModelInstantiator)
        // {
        //     _stoneDrivewayModelInstantiator = stoneDrivewayModelInstantiator;
        // }
        
        public Vector3Int LocalCoordinates { get; set; }
        public Direction2D LocalDirection{ get; set; }
        
        public GameObject CanCallThis(DrivewayModel __instance, Vector3Int localCoordinates, Direction2D localDirection)
        {
            return __instance.GetComponent<StoneDrivewayModelInstantiator>().InstantiateModel(__instance, localCoordinates, localDirection);
            // return _stoneDrivewayModelInstantiator.InstantiateModel(__instance, new Vector3Int(0, 0, 0), Direction2D.Up);
        }
    }
}
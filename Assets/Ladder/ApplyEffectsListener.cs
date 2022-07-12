// using System.Reflection;
// using HarmonyLib;
// using Timberborn.RangedEffectSystem;
//
// namespace Ladder
// {
//     [HarmonyPatch]
//     public static class ApplyEffectsListener
//     {
//         private static MethodInfo TargetMethod()
//         {
//             return AccessTools.TypeByName("Timberborn.RangedEffectSystem.RangedEffectReceiver").GetMethod("ApplyEffects", BindingFlags.NonPublic | BindingFlags.Instance);
//         }
//
//         private static void Postfix()
//         {
//             // Plugin.Log.LogFatal("Applying affects");
//         }
//     }
// }
using System;
using CptSpeedMod;
using HarmonyLib;
using UnityEngine;


namespace CptCameraFOVMod
{


    internal class CameraXGamePatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::CameraXGame), "Awake")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void ChangeCamerazoom(CameraXGame __instance, ref float ___m_zoomOriginal)
        {
            ___m_zoomOriginal = UnityEngine.Mathf.Clamp(Plugin.DistanceToPlayer.Value, 30, 250);
        }
    }
}

using System;
using HarmonyLib;
using UnityEngine;

namespace CptSpeedMod
{
    [global::BepInEx.BepInPlugin("CptCameraFOVMod", "[twitter @Dru9Dealer] CptCameraFOVMod", "1.0.0")]
    [global::BepInEx.BepInProcess("Captivity.exe")]
    public class Plugin : BepInEx.BaseUnityPlugin
    {
        private void Awake()
        {
            DistanceToPlayer = base.Config.Bind<float>("Distance", "DistanceToPlayer", 50f, "Distance to player, reasonable value from 30 to 250");
            Log = base.Logger;
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::CptCameraFOVMod.CameraXGamePatch), null);
            LoggerMessage01 = "[Twitter @Dru9Dealer] Cpt Camera mod";
        }

        private void Update()
        {

        }

        private void OnGUI()
        {
            HandleLoggers(true);
        }

        private void HandleLoggers(bool on)
        {
            if (!on) { return; }

            // Logger 01            
            if (LogDat1.TimeRamained > 0)
            {
                UnityEngine.GUI.Box(LogDat1.rectangle, " " + LoggerMessage01);
                LogDat1.LastMessage = LoggerMessage01;
                LogDat1.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage01.Equals(LogDat1.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat1.TimeRamained > (20f - 0.8f))
                {
                    LoggerMessage01 = LogDat1.LastMessage;
                }
                else
                {
                    LogDat1.TimeRamained = 20f;
                }
            }
        }

        public Plugin()
        {
            // Logger 01
            LogDat1.LastMessage = LoggerMessage01;
            UnityEngine.Random.InitState(117411);
            float x = UnityEngine.Random.Range(10, 350);
            float y = UnityEngine.Random.Range(10, 350);
            LogDat1.rectangle = new global::UnityEngine.Rect(10f + 350f, 10, 350f, 24f);
        }
        public static global::BepInEx.Configuration.ConfigEntry<float> DistanceToPlayer;

        internal static global::BepInEx.Logging.ManualLogSource Log;

        // Logger 01
        public static string LoggerMessage01;
        private LogData LogDat1;

    }
    // Data for Logging messages
    public struct LogData
    {
        public Rect rectangle;
        public string LastMessage;
        public float TimeRamained;
    }
}

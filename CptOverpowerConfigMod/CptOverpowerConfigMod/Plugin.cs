using UnityEngine;

namespace CptOverpowerConfigMod
{
    [global::BepInEx.BepInPlugin("CptOverpowerCfgMod", "[twitter @Dru9Dealer] CptOverpowerCfgMod", "0.9.0")]
    [global::BepInEx.BepInProcess("Captivity.exe")]
    public class Plugin : BepInEx.BaseUnityPlugin
    {
        private void Awake()
        {
            ExSprintSpeedMult = base.Config.Bind<float>("Experiments Speed", "SpeedSprintMult", 0.33f, "Experiment speed multiplier (reasonable range  0.01 - 1)");
            ExPowerDashMult = base.Config.Bind<float>("Experiments Dash distance", "ExPowerDashMult", 0.27f, "Dash distance multiplier (reasonable range  0.01 - 1)");
            ExDamageMult = base.Config.Bind<float>("Experiments Damage", "ExDamageMult", 0.625f, "Damage multiplier (reasonable range  0.01 - 1)");
            ExEnergyMult = base.Config.Bind<float>("Experiments Energy", "ExEnergyMult", 3.33f, "Energy multiplier (reasonable range  0.1 - 5)");
            ExHealthMult = base.Config.Bind<float>("Experiments Health", "ExHealthMult", 0.4f, "Health multiplier (reasonable range  (0.1 - 5)");
            ExKnockbackMult = base.Config.Bind<float>("Experiments Knockback", "ExKnockbackMult", 2f, "Knockback multiplier (reasonable range 1 - 10)");
            ExLifeDrainMult = base.Config.Bind<float>("Experiments Life Drain", "ExLifeDrainMult", 1.2f, "Life Drain multiplier (reasonable range 0.1 - 5)");
            // Player base stats
            HealthMax = base.Config.Bind<float>("Player", "HealthMax", 100f, "Base Health (reasonable range from 100 - 200)"); //100f
            DamageMultiplier = base.Config.Bind<float>("Player", "DamageMultiplier", 1f, "Base damage multiplier (reasonable range 1 - 10)"); // 1f;
            Energy = base.Config.Bind<float>("Player", "Energy", 100f, "Base Energy (reasonable range 100 - 200)");  //100;
            EnergyRestoration = base.Config.Bind<float>("Player", "EnergyRestoration", 0.5f, "Base Energy Restoration (reasonable range 0.01 - 1)");  // 0.5f;
            PowerPerExperiment = base.Config.Bind<float>("Player", "PowerPerExperiment", 5f, " Base Power Per Experiment in percent (reasonable range 1 - 10)");   /// 5f));
            PowerDash = base.Config.Bind<float>("Player", "PowerDash", 20f, "Base Power Dash multiplier (reasonable range 20 - 40)");  // 40f;
            PowerJump = base.Config.Bind<float>("Player", "PowerJump", 18f, "Base Jump height (reasonable range 18 - 28)");  // 18f;
            SpeedSprint = base.Config.Bind<float>("Player", "SpeedSprint", 4f, "Base Run Speed (reasonable range 4 - 8)");  // 4f));
            LifeDrain = base.Config.Bind<float>("Player", "LifeDrain", 1f, "Base Life Drain (reasonable range 1 - 5)");  // 1f;
            PleasureRemoveOnKill = base.Config.Bind<float>("Player", "PleasureRemoveOnKill", 1f, "Base Pleasure Remove On Kill (reasonable range 1 - 5)");  // 1f;
            // Birth buffs
            BirthSpeedSprintBuff = base.Config.Bind<float>("Birth buff", "BirthSpeedSprintBuff", 2f, "Sprint buff on birth end in percent (reasonable range 1% - 5%)"); //2f;
            BirthHealthBuff = base.Config.Bind<float>("Birth buff", "BirthHealthBuff", 2f, "Health buff on birth end in percent (reasonable range 1% - 5%)"); // 2f);
            BirthEnergyBuff = base.Config.Bind<float>("Birth buff", "BirthEnergyBuff", 3f, "Energy buff on birth end in percent (reasonable range 1% - 5%)"); //3f);
            BirthDamageBuff = base.Config.Bind<float>("Birth buff", "BirthDamageBuff", 4f, "Damage buff on birth end in percent (reasonable range 1% - 5%)"); //3f);
            // Cum buffs
            CumSpeedSprintBuff = base.Config.Bind<float>("Cum buff", "CumSpeedSprintBuff", 1f, "Speed buff on cum in percent (reasonable range 1% - 5%)");//2f;
            CumHealthBuff = base.Config.Bind<float>("Cum buff", "CumHealthBuff", 1f, "Health buff on cum in percent (reasonable range 1% - 5%)"); // 2f);
            CumEnergyBuff = base.Config.Bind<float>("Cum buff", "CumEnergyBuff", 1f, "Energy buff on cum in percent (reasonable range 1% - 5%)"); //3f);
            CumDamageBuff = base.Config.Bind<float>("Cum buff", "CumDamageBuff", 1f, "Damage buff on cum in percent (reasonable range 1% - 5%)"); //3f);

            Log = base.Logger;
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::CptOverpowerConfigMod.ManagerWavePatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::CptOverpowerConfigMod.RaperPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::CptOverpowerConfigMod.PlayerPatch), null);
            LoggerMessage01 = "[Twitter @Dru9Dealer] Overpower Config";
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
            float mod_index = 2;
            LogDat1.rectangle = new global::UnityEngine.Rect(10f + 350f, 10 + 24 * mod_index, 350f, 24f);
        }
        public static global::BepInEx.Configuration.ConfigEntry<float> ExSprintSpeedMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExPowerDashMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExDamageMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExEnergyMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExHealthMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExKnockbackMult;
        public static global::BepInEx.Configuration.ConfigEntry<float> ExLifeDrainMult;

        // Player base stats
        public static global::BepInEx.Configuration.ConfigEntry<float> HealthMax; //100f
        public static global::BepInEx.Configuration.ConfigEntry<float> DamageMultiplier; // 1f;
        public static global::BepInEx.Configuration.ConfigEntry<float> Energy; // 100f;
        public static global::BepInEx.Configuration.ConfigEntry<float> EnergyRestoration; // 0.5f;
        public static global::BepInEx.Configuration.ConfigEntry<float> PowerPerExperiment;  /// 5f));
        public static global::BepInEx.Configuration.ConfigEntry<float> PowerDash; // 40f;
        public static global::BepInEx.Configuration.ConfigEntry<float> PowerJump; // 18f;
        public static global::BepInEx.Configuration.ConfigEntry<float> SpeedSprint; // 4f));
        public static global::BepInEx.Configuration.ConfigEntry<float> LifeDrain; // 1f;
        public static global::BepInEx.Configuration.ConfigEntry<float> PleasureRemoveOnKill; // 1f;
        // Birth buffs
        public static global::BepInEx.Configuration.ConfigEntry<float> BirthSpeedSprintBuff; //2f;
        public static global::BepInEx.Configuration.ConfigEntry<float> BirthHealthBuff; // 2f);
        public static global::BepInEx.Configuration.ConfigEntry<float> BirthEnergyBuff; //3f);
        public static global::BepInEx.Configuration.ConfigEntry<float> BirthDamageBuff; //3f);
        // Cum buffs
        public static global::BepInEx.Configuration.ConfigEntry<float> CumSpeedSprintBuff; //2f;
        public static global::BepInEx.Configuration.ConfigEntry<float> CumHealthBuff; // 2f);
        public static global::BepInEx.Configuration.ConfigEntry<float> CumEnergyBuff; //3f);
        public static global::BepInEx.Configuration.ConfigEntry<float> CumDamageBuff; //3f);
        // Logs
        internal static global::BepInEx.Logging.ManualLogSource Log;
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

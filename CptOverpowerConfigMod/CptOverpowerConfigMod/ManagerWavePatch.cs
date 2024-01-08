namespace CptOverpowerConfigMod
{
    internal class ManagerWavePatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::ManagerWave), "StartNewWave")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void OnWaveStart(ManagerWave __instance, ref int ___m_numWaveCurrent)
        {
            if (___m_numWaveCurrent == 1)
            {
                Player player = CommonReferences.Instance.GetPlayer();
                //Setup player base stats
                //player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase = 100f;

                float num = (float)ManagerDB.GetIdsCompletedSeenChallenges().Count * player.GetStat(StatNameActor.PowerPerExperiment).GetValueBase();
                string text = ManagerDB.GetIdsCompletedSeenChallenges().Count.ToString();
                player.GetStat(StatNamePlayer.SpeedSprint).AddModifier(player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase() * 0.01f * num * Plugin.ExSprintSpeedMult.Value);
                player.GetStat(StatNamePlayer.PowerDash).AddModifier(player.GetStat(StatNamePlayer.PowerDash).GetValueBase() * 0.01f * num * Plugin.ExPowerDashMult.Value);
                player.GetStat(StatNamePlayer.DamageMultiplierGun).AddModifier(player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase() * 0.01f * num * Plugin.ExDamageMult.Value);
                player.GetStat(StatNameActor.Energy).AddModifier(player.GetStat(StatNameActor.Energy).GetValueBase() * 0.01f * num * Plugin.ExEnergyMult.Value);
                player.GetStat(StatNameActor.HealthMax).AddModifier(player.GetStat(StatNameActor.HealthMax).GetValueBase() * 0.01f * num * Plugin.ExHealthMult.Value);
                player.GetStat(StatNameActor.KnockbackXMultiplier).AddModifier(player.GetStat(StatNameActor.KnockbackXMultiplier).GetValueBase() * 0.01f * num * Plugin.ExKnockbackMult.Value);
                player.GetStat(StatNameActor.KnockbackYMultiplier).AddModifier(player.GetStat(StatNameActor.KnockbackYMultiplier).GetValueBase() * 0.01f * num * Plugin.ExKnockbackMult.Value);
                player.GetStat(StatNameActor.LifeDrain).AddModifier(player.GetStat(StatNameActor.LifeDrain).GetValueBase() * 0.01f * num * Plugin.ExLifeDrainMult.Value);
                CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus(text + " Experiments empower you", string.Concat(new string[] { string.Concat(new string[]
            {
                "Speed increased to: ",
                (num * Plugin.ExSprintSpeedMult.Value).ToString(),
                "%\n",
                "Dash power increased to: ",
                (num * Plugin.ExPowerDashMult.Value).ToString(),
                "%\n",
                "Damage increased to: ",
                (num * Plugin.ExDamageMult.Value).ToString(),
                "%\n",
                "Energy increased to: ",
                (num * Plugin.ExEnergyMult.Value).ToString(),
                "%\n",
                "Health increased by: ",
                (num * Plugin.ExHealthMult.Value).ToString(),
                "%\n",
                "Knockback increased to: ",
                (num * Plugin.ExKnockbackMult.Value).ToString(),
                "%\n",
                "Life Drain increased to: ",
                (num * Plugin.ExLifeDrainMult.Value).ToString(),
                "%\n"
            }) }), StatusPlayerHudItemColor.Special, 20f);
            }
        }
    }
}

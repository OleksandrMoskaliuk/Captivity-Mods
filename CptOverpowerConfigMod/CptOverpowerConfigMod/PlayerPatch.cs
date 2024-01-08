using System.Collections.Generic;

namespace CptOverpowerConfigMod
{
    internal class PlayerPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::Player), "KillNpc")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void OnKillNpc(Player __instance, ref float ___m_pleasureCurrent, ref float ___m_libidoCurrent, ref float ___m_healthCurrent)
        {
            if (___m_pleasureCurrent > 0f)
            {
                ___m_pleasureCurrent -= Plugin.PleasureRemoveOnKill.Value;
                CommonReferences.Instance.GetManagerHud().GainPleasure();
            }
            if (___m_libidoCurrent > 0f)
            {
                ___m_libidoCurrent -= __instance.GetStat(StatNameActor.LifeDrain).GetValueTotal();
                CommonReferences.Instance.GetManagerHud().LoseLibido();
            }
            if (___m_healthCurrent < __instance.GetStat(StatNameActor.HealthMax).GetValueTotal())
            {
                ___m_healthCurrent += __instance.GetStat(StatNameActor.LifeDrain).GetValueTotal();
                CommonReferences.Instance.GetManagerHud().GetHealthBar().Increase();
            }
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::Player), "InitializeStats")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void EditBaseStats(Player __instance, ref List<Stat> ___m_stats)
        {
            // Actor class initialization
            ___m_stats.Add(new Stat(StatNameActor.HealthMax, Plugin.HealthMax.Value));
            ___m_stats.Add(new Stat(StatNameActor.DamageMultiplier, Plugin.DamageMultiplier.Value));
            ___m_stats.Add(new Stat(StatNameActor.KnockbackXMultiplier, 1f));
            ___m_stats.Add(new Stat(StatNameActor.KnockbackYMultiplier, 1f));
            ___m_stats.Add(new Stat(StatNameActor.SpeedAccel, 6f));
            ___m_stats.Add(new Stat(StatNameActor.SpeedMax, 6f));
            ___m_stats.Add(new Stat(StatNameActor.Traction, 0f));
            ___m_stats.Add(new Stat(StatNameActor.Energy, Plugin.Energy.Value));
            ___m_stats.Add(new Stat(StatNameActor.EnergyRestoration, Plugin.EnergyRestoration.Value));
            ___m_stats.Add(new Stat(StatNameActor.PowerPerExperiment, Plugin.PowerPerExperiment.Value));
            ___m_stats.Add(new Stat(StatNameActor.PowerDash, Plugin.PowerDash.Value));
            ___m_stats.Add(new Stat(StatNameActor.PowerJump, Plugin.PowerJump.Value));
            ___m_stats.Add(new Stat(StatNameActor.SpeedSprint, Plugin.SpeedSprint.Value));
            ___m_stats.Add(new Stat(StatNameActor.LifeDrain, Plugin.LifeDrain.Value));
            // Player class initialization
            ___m_stats.Add(new Stat(StatNamePlayer.DamageMultiplierGun, __instance.GetStat(StatNameActor.DamageMultiplier).GetValueBase()));
            ___m_stats.Add(new Stat(StatNamePlayer.SpeedSprint, __instance.GetStat(StatNameActor.SpeedSprint).GetValueBase()));
            ___m_stats.Add(new Stat(StatNamePlayer.PowerJump, __instance.GetStat(StatNameActor.PowerJump).GetValueBase()));
            ___m_stats.Add(new Stat(StatNamePlayer.PowerDash, __instance.GetStat(StatNameActor.PowerDash).GetValueBase()));
            ___m_stats.Add(new Stat("LibidoRegen", 0f));
            ___m_stats.Add(new Stat("LibidoRegenRate", 0f));
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::Player), "EndBirth")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void EndBirthBuff(Player __instance, ref List<Stat> ___m_stats)
        {
            Player player = CommonReferences.Instance.GetPlayer();
            player.GetStat(StatNamePlayer.SpeedSprint).AddModifier(player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase() * 0.01f * Plugin.BirthSpeedSprintBuff.Value);
            player.GetStat(StatNameActor.HealthMax).AddModifier(player.GetStat(StatNameActor.HealthMax).GetValueBase() * 0.01f * Plugin.BirthHealthBuff.Value);
            player.GetStat(StatNameActor.Energy).AddModifier(player.GetStat(StatNameActor.Energy).GetValueBase() * 0.01f * Plugin.BirthEnergyBuff.Value);
            player.GetStat(StatNamePlayer.DamageMultiplierGun).AddModifier(player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase() * 0.01f * Plugin.BirthDamageBuff.Value);
            int num = (int)((player.GetStat(StatNamePlayer.SpeedSprint).GetValueTotal() - player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase()) / (player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase() * 0.01f));
            int num2 = (int)((player.GetStat(StatNamePlayer.PowerDash).GetValueTotal() - player.GetStat(StatNamePlayer.PowerDash).GetValueBase()) / (player.GetStat(StatNamePlayer.PowerDash).GetValueBase() * 0.01f));
            int num3 = (int)((player.GetStat(StatNameActor.Energy).GetValueTotal() - player.GetStat(StatNameActor.Energy).GetValueBase()) / (player.GetStat(StatNameActor.Energy).GetValueBase() * 0.01f));
            int num4 = (int)((player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueTotal() - player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase()) / (player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase() * 0.01f));
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus(
                "Pussy trembling", string.Concat(new string[] { "Speed increased to: " + num.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 4.5f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus(
                "Strong urethra", string.Concat(new string[] { "Max health increased to: " + num2.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 6f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus(
                "Mutated womb", string.Concat(new string[] { "Stamina increased to: " + num3.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 7.5f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus(
                "Ferocity", string.Concat(new string[] { "Damage increased to: " + num4.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 9f);
        }
    }
}

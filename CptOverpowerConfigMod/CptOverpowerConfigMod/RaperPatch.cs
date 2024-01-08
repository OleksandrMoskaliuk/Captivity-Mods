namespace CptOverpowerConfigMod
{
    internal class RaperPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(Raper), "PlayerGainCumPleasure")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void CumBuff(Raper __instance)
        {
            Player player = CommonReferences.Instance.GetPlayer();
            player.GetStat(StatNamePlayer.SpeedSprint).AddModifier(player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase() * 0.01f * Plugin.CumSpeedSprintBuff.Value);
            player.GetStat(StatNameActor.HealthMax).AddModifier(player.GetStat(StatNameActor.HealthMax).GetValueBase() * 0.01f * Plugin.CumHealthBuff.Value);
            player.GetStat(StatNameActor.Energy).AddModifier(player.GetStat(StatNameActor.Energy).GetValueBase() * 0.01f * Plugin.CumEnergyBuff.Value);
            player.GetStat(StatNamePlayer.DamageMultiplierGun).AddModifier(player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase() * 0.01f * Plugin.CumDamageBuff.Value);
            int num = (int)((player.GetStat(StatNamePlayer.SpeedSprint).GetValueTotal() - player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase()) / (player.GetStat(StatNamePlayer.SpeedSprint).GetValueBase() * 0.01f));
            int num2 = (int)((player.GetStat(StatNamePlayer.PowerDash).GetValueTotal() - player.GetStat(StatNamePlayer.PowerDash).GetValueBase()) / (player.GetStat(StatNamePlayer.PowerDash).GetValueBase() * 0.01f));
            int num3 = (int)((player.GetStat(StatNameActor.Energy).GetValueTotal() - player.GetStat(StatNameActor.Energy).GetValueBase()) / (player.GetStat(StatNameActor.Energy).GetValueBase() * 0.01f));
            int num4 = (int)((player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueTotal() - player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase()) / (player.GetStat(StatNamePlayer.DamageMultiplierGun).GetValueBase() * 0.01f));
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus("Clitoral erection", string.Concat(new string[] { "Speed increased to: " + num.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 4.5f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus("Semen absorption", string.Concat(new string[] { "Max health increased to: " + num2.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 6f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus("Exhausted rape training", string.Concat(new string[] { "Energy increased to: " + num3.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 7.5f);
            CommonReferences.Instance.GetManagerHud().GetStatusPlayerHud().CreateAndAddStatus("Ferocity", string.Concat(new string[] { "Damage increased to: " + num4.ToString() + "%" }), StatusPlayerHudItemColor.Buff, 9f);
        }
    }
}

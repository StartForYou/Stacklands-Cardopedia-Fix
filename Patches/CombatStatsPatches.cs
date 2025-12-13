using HarmonyLib;

namespace CardopediaFix.Patches
{
    public class CombatStatsPatches
    {
        
        //[HarmonyPatch(typeof(BaseVillager), "UpdateCardText")]
        //[HarmonyPrefix]
        //public static bool OnUpdateCardText(BaseVillager __instance)
        //{
        //    GameCard myGameCard = __instance.MyGameCard;
        //    __instance.descriptionOverride = SokLoc.Translate(__instance.DescriptionTerm);
        //    __instance.descriptionOverride = __instance.descriptionOverride + "\n\n<i>" + "Nothing.";
        //    if (myGameCard != null && myGameCard.CardConnectorChildren.Count > 0 && __instance.MyGameCard.IsHovered)
        //    {
        //        __instance.descriptionOverride = SokLoc.Translate(__instance.DescriptionTerm);
        //        __instance.descriptionOverride = __instance.descriptionOverride + "\n\n<i>" + "Nothing.";//this.GetConnectorInfoString(this.MyGameCard) + "</i>";
        //    }
        //    return false; // false: 拦截，true: 继续执行原方法
        //}

        [HarmonyPatch(typeof(CombatStats), "InitStats")]
        [HarmonyPrefix]
        public static bool OnInitStats(CombatStats __instance, CombatStats stats)
        {
            if (null == stats)
            {
                CardopediaFix.Instance.logger.Log("InitStats failed: stats error!");
                __instance.HitChance = 0.5f; //by default
                __instance.AttackSpeed = 3.5f; //by default
                //__instance.MaxHealth = 1; // null
                __instance.AttackDamage = 1; //by default
                __instance.Defence = 1; //by default
                //SpecialHit specialHit = new() { Chance = 0, HitType = SpecialHitType.None, Target = SpecialHitTarget.Target};
                __instance.SpecialHits = []; // important
                return false;
            }
            __instance.HitChance = stats.HitChance;
            __instance.AttackSpeed = stats.AttackSpeed;
            __instance.MaxHealth = stats.MaxHealth;
            __instance.AttackDamage = stats.AttackDamage;
            __instance.Defence = stats.Defence;
            if (null != stats.SpecialHits)
            {
                //SpecialHit specialHit = new() { Chance = 0, HitType = SpecialHitType.None, Target = SpecialHitTarget.Target };
                __instance.SpecialHits = [];
            }
            //GameCard myGameCard = __instance.MyGameCard;
            //__instance.descriptionOverride = SokLoc.Translate(__instance.DescriptionTerm);
            //__instance.descriptionOverride = __instance.descriptionOverride + "\n\n<i>" + "Nothing.";
            //if (myGameCard != null && myGameCard.CardConnectorChildren.Count > 0 && __instance.MyGameCard.IsHovered)
            //{
            //    __instance.descriptionOverride = SokLoc.Translate(__instance.DescriptionTerm);
            //    __instance.descriptionOverride = __instance.descriptionOverride + "\n\n<i>" + "Nothing.";//this.GetConnectorInfoString(this.MyGameCard) + "</i>";
            //}
            return false; // false: 拦截，true: 继续执行原方法
        }
    }
}

using HarmonyLib;
using UnityEngine;

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
        //    return false;
        //}

        [HarmonyPatch(typeof(CombatStats), "InitStats")]
        [HarmonyPrefix]
        public static bool OnInitStats(CombatStats __instance, CombatStats stats)
        {
            if (null == stats)
            {
                if (!CardopediaFix.Instance.hasLogCardBug)
                {
                    CardopediaFix.Instance.hasLogCardBug = true;
                    CardopediaFix.Instance.logger.Log("InitStats failed: There are problematic cards exist!");
                }
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
            return false;
        }

        [HarmonyPatch(typeof(CombatStats), "SummarizeSpecialHits")]
        [HarmonyPrefix]
        public static bool OnSummarizeSpecialHits(CombatStats __instance, ref string __result)
        {
            if (__instance.SpecialHits == null)
            {
                __result = "";
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(CombatStats), "CombatLevel", MethodType.Getter)]
        [HarmonyPrefix]
        public static bool OnGerCombatLevel(CombatStats __instance, ref float __result)
        {
            float num = 0f;
            
            /*try
            {
                //CardopediaFix.Instance.logger.LogWarning("Null? : "+ (__instance.SpecialHits == null));

                
                if (__instance.SpecialHits != null && __instance.SpecialHits.Count > 0)
                {
                    float num2 = 0f;
                    foreach (SpecialHit specialHit in __instance.SpecialHits)
                    {
                        bool flag = specialHit.Target == SpecialHitTarget.Self || specialHit.Target == SpecialHitTarget.RandomFriendly || specialHit.Target == SpecialHitTarget.AllFriendly;
                        if (specialHit.IsDebuff())
                        {
                            if (flag)
                            {
                                num2 -= specialHit.Chance / 10f;
                            }
                            else
                            {
                                num2 += specialHit.Chance / 10f;
                            }
                        }
                        else if (flag)
                        {
                            num2 += specialHit.Chance / 10f;
                        }
                        else
                        {
                            num2 -= specialHit.Chance / 10f;
                        }
                    }
                    num += num2 * 2f;
                }
            }
            catch (Exception ex)
            {
                CardopediaFix.Instance.logger.LogWarning(ex.ToString());
            }*/
            if (__instance.SpecialHits == null)
            {
                num += CalculateAverageAttackDamagePerSecond(__instance) * 5f;
                num += (float)__instance.MaxHealth * 0.5f;
                num += (float)(__instance.Defence + __instance.DefenceIncrement) * 2f;
                __result = Mathf.Max(1f, num);
                return false;
            }
            return true;
        }

        private static float CalculateAverageAttackDamagePerSecond(CombatStats stats)
        {
            return (float)stats.AttackDamage / stats.AttackSpeed * stats.HitChance;
        }

    }
}

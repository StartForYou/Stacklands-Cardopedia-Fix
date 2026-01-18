using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardopediaFix.Patches
{
    public class CardopediaEntryElementPatches
    {
        [HarmonyPatch(typeof(CardopediaEntryElement), "UpdateText")]
        [HarmonyPrefix]
        public static bool OnInitStats(CardopediaEntryElement __instance)
        {
            if (__instance.MyCardData != null)
            {
                try
                {
                    __instance.MyCardData.UpdateCardText();
                }
                catch (Exception ex)
                {
                    CardopediaFix.Instance.logger.LogWarning(ex.ToString());
                }

            }
            if (__instance.wasFound)
            {
                __instance.Button.TextMeshPro.text = "• " + __instance.MyCardData.Name;
                return false;
            }
            __instance.Button.TextMeshPro.text = "• ???";
            return false;
        }

    }
}

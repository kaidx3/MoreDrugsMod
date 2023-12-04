using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDrugs.Patches
{
    [HarmonyPatch(typeof(TetraChemicalItem))]
    internal class InfiniteDrugPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        static void InfiniteGas(TetraChemicalItem __instance)
        {
            if (__instance.playerHeldBy != null)
            {
                float drunkness = Traverse.Create(__instance.playerHeldBy).Field("drunkness").GetValue<float>();
                if (drunkness > 0f)
                {
                    if (__instance.itemProperties.itemId == 123456789 && !__instance.isPocketed)
                    {
                        Traverse.Create(__instance.playerHeldBy).Field("sprintMultiplier").SetValue(15f);
                        Traverse.Create(__instance.playerHeldBy).Field("jumpForce").SetValue(15f);
                        Traverse.Create(__instance.playerHeldBy).Field("climbSpeed").SetValue(15f);
                    }
                }
            }
            
            Traverse.Create(__instance).Field("fuel").SetValue(1f);
        }

        [HarmonyPatch("ItemActivate")]
        [HarmonyPrefix]
        static void ItemActivateFix(TetraChemicalItem __instance)
        {
            Traverse.Create(__instance).Field("fuel").SetValue(1f);
        }
    }
}

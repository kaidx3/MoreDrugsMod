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

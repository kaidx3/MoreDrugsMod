using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDrugs.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        static void FastAsFuck(PlayerControllerB __instance, ref float ___sprintMeter, ref int ___health)
        {
            float drunkness = Traverse.Create(__instance).Field("drunkness").GetValue<float>();
            if (drunkness <= 0f)
            {
                Traverse.Create(__instance).Field("jumpForce").SetValue(10f);
                Traverse.Create(__instance).Field("climbSpeed").SetValue(7f);
            }
            else
            {
                if (__instance.currentlyHeldObject.itemProperties.itemId == 0123456789)
                {
                    bool isSprinting = Traverse.Create(__instance).Field("isSprinting").GetValue<bool>();
                    __instance.health = 100;
                    __instance.jumpForce = 30;
                    if (isSprinting)
                    {
                        Traverse.Create(__instance).Field("sprintMultiplier").SetValue(30f);
                    }
                }
            }
        }
    }
}

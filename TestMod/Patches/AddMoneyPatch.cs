using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreDrugs.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class AddMoneyPatch
    {
        [HarmonyPatch("StartGame")]
        [HarmonyPrefix]
        private static void StartMatchLeverPatch()
        {
            Terminal terminal = GameObject.FindObjectOfType<Terminal>();
            terminal.SyncGroupCreditsServerRpc(600, terminal.numberOfItemsInDropship);
        }
    }
}

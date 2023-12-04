using BepInEx.Logging;
using HarmonyLib;
using MoreDrugs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreDrugs.Patches
{
    [HarmonyPatch(typeof(TetraChemicalItem))]
    internal class TetraChemicalExistsTest
    {
        internal ManualLogSource mls;

        [HarmonyPatch(nameof(TetraChemicalItem.ItemActivate))]
        [HarmonyPostfix]
        private static void TetraChemicalItemDropTest()
        {

        }
    }
}

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
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class AddMoneyPatch
    {
        internal ManualLogSource mls;

        [HarmonyPatch("StartGame")]
        [HarmonyPrefix]
        private static void StartMatchLeverPatch()
        {
            Terminal terminal = GameObject.FindObjectOfType<Terminal>();
            terminal.SyncGroupCreditsServerRpc(600, terminal.numberOfItemsInDropship);

            var TZP = GameObject.FindAnyObjectByType(typeof(TetraChemicalItem));

            if (TZP != null)
            {
                try
                {
                    var TPZ = GameObject.Find(TZP.name);
                    if (TPZ != null)
                    {
                        Debug.Log("TetraChemicalItem found");
                        Debug.Log(TPZ.name);
                        Debug.Log(TPZ.GetType().Name);
                    }
                    else
                    {
                        Debug.Log("TetraChemicalItem not found");
                    }
                    var myItem = ScriptableObject.CreateInstance<Item>();
                    myItem.itemName = TZP.name;
                    myItem.itemId = TZP.GetInstanceID();
                    myItem.spawnPrefab = TPZ;
                    Debug.Log("spawn prefab assigned" + myItem.spawnPrefab.name);
                    //myItem.spawnPrefab.AddComponent<TestDrug>();
                    //Debug.Log("TestDrug component added");
                    LethalLib.Modules.Items.RegisterShopItem(myItem, 999);
                    Debug.Log("TestDrug registered");
                    LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(myItem.spawnPrefab);
                    Debug.Log("TestDrug network prefab registered");
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                Debug.Log("TetraChemicalItem was null");
            }
        }
    }
}

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MoreDrugs.Models;
using MoreDrugs.Patches;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine;
using LC_API;
using LethalLib;
using LC_API.ServerAPI;
using LethalLib.Modules;
using System.Resources;
using UnityEngine.Events;
using System.Reflection;
using System.Text;
using System.IO;
using System;

namespace MoreDrugs
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class TestModBase : BaseUnityPlugin
    {
        private const string modGUID = "Kaiden.Cody.Meghan.MoreDrugs";
        private const string modName = "MoreDrugs";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static TestModBase Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            try
            {
                if (Instance == null)
                {
                    Instance = this;
                }

                mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
                mls.LogInfo("The test mod has awakened.");

                harmony.PatchAll(typeof(TestModBase));
                harmony.PatchAll(typeof(PlayerControllerBPatch));
                harmony.PatchAll(typeof(AddMoneyPatch));
                harmony.PatchAll(typeof(InfiniteDrugPatch));
                mls.LogInfo("Hello i am alive still");
                var obj = GameObject.FindObjectOfType(typeof(TetraChemicalItem));
                if (obj != null)
                    mls.LogInfo("Found TetraChemicalItem: " + obj.ToString());
                else
                    mls.LogInfo("Could not find TetraChemicalItem.");
                Item myItem = ScriptableObject.CreateInstance<Item>();
                myItem.itemName = "TestDrug";
                myItem.itemId = 0987654321;
                myItem.spawnPrefab = (GameObject)obj;
                myItem.spawnPrefab.AddComponent<TestDrug>();
                LethalLib.Modules.Items.RegisterShopItem(myItem, 999);
                LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(myItem.spawnPrefab);
            }
            catch (Exception ex)
            {
                mls.LogError(ex.Message);
            }
        }
    }
}

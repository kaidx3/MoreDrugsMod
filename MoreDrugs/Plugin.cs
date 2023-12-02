using BepInEx;
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

            Item myItem = new Item();
            myItem.name = "TestDrug";
            myItem.spawnPrefab = Resources.Load<GameObject>("tzpPrefab");
            TestDrug testDrug = myItem.spawnPrefab.AddComponent<TestDrug>();
            LethalLib.Modules.Items.RegisterShopItem(myItem, 0);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(myItem.spawnPrefab);
        }
    }
}

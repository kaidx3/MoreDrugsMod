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

        public static AssetBundle TestAssets;

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

                if ((UnityEngine.Object)TestModBase.Instance == (UnityEngine.Object)null)
                    TestModBase.Instance = this;
                Debug.Log(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
                try
                {
                    TestModBase.TestAssets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "testdrug"));
                }
                catch (Exception ex)
                {
                    mls.LogError("Exception thrown at TestAssets assignment");
                    mls.LogError(ex.Message);
                }

                harmony.PatchAll(typeof(TestModBase));
                harmony.PatchAll(typeof(PlayerControllerBPatch));
                harmony.PatchAll(typeof(AddMoneyPatch));
                harmony.PatchAll(typeof(InfiniteDrugPatch));

                try
                {
                    var assetNames = TestModBase.TestAssets.GetAllAssetNames();
                    foreach (var assetName in assetNames)
                    {
                        mls.LogInfo(assetName);
                    }
                    Item obj = TestModBase.TestAssets.LoadAsset<Item>("assets/testdrug.asset");

                    if ((UnityEngine.Object)obj == (UnityEngine.Object)null)
                    {
                        this.mls.LogError((object)"Failed to load TestDrug prefab.");
                    }
                    else
                    {
                        LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(obj.spawnPrefab);
                        Items.RegisterShopItem(obj, 80);
                    }
                }
                catch (Exception ex)
                {
                    mls.LogError("Exception thrown at Item assignment");
                    mls.LogError(ex.Message);
                }
            }
            catch (Exception ex)
            {
                mls.LogError(ex.Message);
            }
        }
    }
}

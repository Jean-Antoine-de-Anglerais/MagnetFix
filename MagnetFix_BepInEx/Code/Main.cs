﻿using HarmonyLib;
using System.Reflection;
using UnityEngine;
using BepInEx;

namespace MagnetFix_BepInEx
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "jean.worldbox.mods.MagnetFix_BepInEx";
        public const string pluginName = "Magnet Fix";
        public const string pluginVersion = "4.0.0.0";

        public static Harmony harmony = new Harmony(MethodBase.GetCurrentMethod().DeclaringType.Namespace);
        private bool _initialized = false;

        public void Update()
        {
            if (global::Config.gameLoaded && !_initialized)
            {
                harmony.Patch(AccessTools.Method(typeof(Magnet).GetNestedType("<>c__DisplayClass12_0", BindingFlags.NonPublic), "<pickupUnits>b__0"),
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), "pickupUnits_Transpiler")));

                _initialized = true;
            }
        }
    }
}

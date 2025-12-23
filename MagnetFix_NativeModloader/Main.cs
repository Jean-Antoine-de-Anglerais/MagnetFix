using HarmonyLib;
using UnityEngine;

namespace MagnetFix_NativeModloader
{
    internal class Main : MonoBehaviour
    {
        public static Harmony harmony = new Harmony("jean.worldbox.mods.magnetfix");

        public void Awake()
        {
            harmony.Patch(AccessTools.Method(typeof(Magnet), "<pickupUnits>b__18_0"),
            transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.pickupUnits_Transpiler))));
        }
    }
}

using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace MagnetFix_Test_NativeModloader
{
    internal class Main : MonoBehaviour
    {
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

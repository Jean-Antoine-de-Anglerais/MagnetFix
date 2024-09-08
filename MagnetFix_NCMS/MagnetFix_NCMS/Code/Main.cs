using HarmonyLib;
using NCMS;
using System.Reflection;
using UnityEngine;

namespace MagnetFix_NCMS
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        public static Harmony harmony = new Harmony(MethodBase.GetCurrentMethod().DeclaringType.Namespace);

        void Awake()
        {
            harmony.Patch(AccessTools.Method(typeof(Magnet).GetNestedType("<>c__DisplayClass12_0", BindingFlags.NonPublic), "<pickupUnits>b__0"),
            transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), "pickupUnits_Transpiler")));        
        }
    }
}

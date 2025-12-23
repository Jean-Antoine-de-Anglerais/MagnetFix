using HarmonyLib;
using NeoModLoader.api;

namespace MagnetFix_NML
{
    class Main : BasicMod<Main>
    {
        public static Harmony harmony = new Harmony("jean.worldbox.mods.magnetfix");

        protected override void OnModLoad()
        {
            harmony.Patch(AccessTools.Method(typeof(Magnet), "<pickupUnits>b__18_0"),
            transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.pickupUnits_Transpiler))));
        }
    }
}

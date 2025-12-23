using BepInEx;
using HarmonyLib;

namespace MagnetFix_BepInEx
{
    [BepInPlugin("jean.worldbox.mods.magnetfix", "Magnet Fix", "5.0.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static Harmony harmony = new Harmony("jean.worldbox.mods.magnetfix");
        private bool _initialized = false;

        public void Update()
        {
            if (global::Config.game_loaded && !_initialized)
            {
                harmony.Patch(AccessTools.Method(typeof(Magnet), "<pickupUnits>b__18_0"),
                transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.pickupUnits_Transpiler))));

                _initialized = true;
            }
        }
    }
}

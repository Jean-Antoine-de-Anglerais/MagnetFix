using BepInEx;
using HarmonyLib;
using ReflectionUtility;
using System.Collections.Generic;
using static ConstantClassNamespace.ConstantClass;

namespace MagnetFix_Test_BepInEx
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class MagnetFixClass : BaseUnityPlugin
    {
        public static Harmony harmony = new Harmony(pluginName);
        private bool _initialized = false;

        public void Awake()
        {
            Logger.LogMessage("ХООООООООЙ");
        }

        public void Start()
        {
            if (global::Config.gameLoaded)
            {
                Logger.LogMessage("Пропатчено");
            }
        }

        // Метод, запускающийся каждый кадр (в моём случае он зависим от загрузки игры)
        public void Update()
        {
            if (global::Config.gameLoaded && !_initialized)
            {
                harmony.Patch(AccessTools.Method(typeof(Magnet), "pickupUnits"),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), "pickupUnits_Prefix")));

                _initialized = true;
            }
        }
    }

    public class Patches
    {
        public static bool pickupUnits_Prefix(Magnet __instance, WorldTile pTile, List<Actor> ___units, HashSet<Actor> ___magnetUnits, ref bool ____hasUnits)
        {
            BrushData currentBrushData = Config.currentBrushData;
            bool tUnitsAdded = false;
            for (int i = 0; i < currentBrushData.pos.Length; i++)
            {
                WorldTile tile = World.world.GetTile(currentBrushData.pos[i].x + pTile.x, currentBrushData.pos[i].y + pTile.y);
                if (tile == null || !tile.hasUnits())
                {
                    continue;
                }
                tile.doUnits(delegate (Actor tActor)
                {
                    if (tActor.asset.canBeMovedByPowers && !(bool)Reflection.GetField(tActor.GetType(), tActor, "is_inside_boat") && !___units.Contains(tActor))
                    {
                        tActor.cancelAllBeh();
                        ___units.Add(tActor);
                        ___magnetUnits.Add(tActor);
                        tUnitsAdded = true;
                        Reflection.SetField<bool>(tActor, "is_in_magnet", true);
                        Reflection.SetField<float>(__instance, "pickedup_mod", 2f);
                    }
                });
            }
            if (tUnitsAdded)
            {
                ____hasUnits = true;
            }
            return false;
        }
    }
}

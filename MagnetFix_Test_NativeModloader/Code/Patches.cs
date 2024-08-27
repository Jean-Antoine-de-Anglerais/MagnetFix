using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace MagnetFix_Test_NativeModloader
{
    public static class Patches
    {
        public static IEnumerable<CodeInstruction> fitToRule_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var codes = new List<CodeInstruction>(instructions);

            Label label = generator.DefineLabel();

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Is(OpCodes.Callvirt, AccessTools.Method(typeof(Actor), "isUnitOk")))
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": FOUNDED 1");

                    codes[i + 1] = new CodeInstruction(OpCodes.Brtrue_S, label);
                }

                if (codes[i].Is(OpCodes.Callvirt, AccessTools.Method(typeof(ActorBase), "isKing")))
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": FOUNDED 2");

                    codes[i - 1].labels.Add(label);
                }

                else
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": UNFOUNDED");
                }
            }

            return codes.AsEnumerable();
        }

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
                    if (tActor.asset.canBeMovedByPowers && !tActor.is_inside_boat && !___units.Contains(tActor))
                    {
                        tActor.cancelAllBeh();
                        ___units.Add(tActor);
                        ___magnetUnits.Add(tActor);
                        tUnitsAdded = true;
                        tActor.is_in_magnet = true;
                        __instance.pickedup_mod = 2f;
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

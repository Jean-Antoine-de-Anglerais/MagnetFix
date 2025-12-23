using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MagnetFix_NML
{
    public static class Patches
    {
        public static IEnumerable<CodeInstruction> pickupUnits_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var code in instructions)
            {
                if (code.Is(OpCodes.Callvirt, AccessTools.Method(typeof(Actor), nameof(Actor.isInsideSomething))))
                {
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Actor), nameof(Actor.is_inside_boat)));
                }
                else
                {
                    yield return code;
                }
            }
        }
    }
}

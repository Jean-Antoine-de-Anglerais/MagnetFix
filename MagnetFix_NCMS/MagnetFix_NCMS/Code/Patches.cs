using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace MagnetFix_NCMS
{
    public static class Patches
    {
        public static IEnumerable<CodeInstruction> pickupUnits_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Is(OpCodes.Callvirt, AccessTools.Method(typeof(Actor), "isInsideSomething")))
                {
                    codes[i] = new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(ActorBase), "is_inside_boat"));
                }
            }

            return codes.AsEnumerable();
        }
    }
}

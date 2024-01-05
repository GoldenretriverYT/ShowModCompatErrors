using System.Reflection;
using UnityEngine;
using HarmonyLib;
using System.Reflection.Emit;

namespace ShowModCompatErrors {
    public class ShowModCompatErrorsMeta : ModMeta {
        public override string Name => throw new NotImplementedException();
        public static bool GiveMeFreedom = true;

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame) {
        }

        public override void Initialize(ModController.DLLMod parentMod) {
            base.Initialize(parentMod);
        }
    }

    [HarmonyPatch(typeof(GUIListView), "ToggleMod")]
    public class ToggleModPatch {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            var skipNext = false;

            foreach (var instruction in instructions) {
                if (instruction.LoadsConstant("ModEnableError")) {
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    skipNext = true;
                }
                
                if(skipNext) {
                    skipNext = false;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
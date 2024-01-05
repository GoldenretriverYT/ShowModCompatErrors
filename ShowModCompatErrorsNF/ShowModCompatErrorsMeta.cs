using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using UnityEngine;
using HarmonyLib;
using System.Reflection.Emit;
using System.IO;

namespace ShowModCompatErrorsNF {
    public class ShowModCompatErrorsMeta : ModMeta {
        public override string Name => "ShowModCompatErrorsNF";
        public static bool GiveMeFreedom = true;

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame) {
        }

        public override void Initialize(ModController.DLLMod parentMod) {
            base.Initialize(parentMod);

            var harmony = new Harmony("at.goldenretriveryt.showmodcompaterrosnf");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
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

                if (skipNext) {
                    skipNext = false;
                    continue;
                }

                yield return instruction;
            }
        }
    }

    public class UselessBehaviour : ModBehaviour {
        public override void OnActivate() {
        }

        public override void OnDeactivate() {
        }
    }
}
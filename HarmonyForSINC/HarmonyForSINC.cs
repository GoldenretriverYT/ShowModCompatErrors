using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HarmonyForSINC
{
    public class HarmonyForSINCMeta : ModMeta {
        public override string Name => "HarmonyForSINC";
        public static bool GiveMeFreedom = true;

        public override void ConstructOptionsScreen(RectTransform parent, bool inGame) {
        }

        public override void Initialize(ModController.DLLMod parentMod) {
            AppDomain.CurrentDomain.AssemblyResolve += (x, y) => {
                var path = Path.Combine(parentMod.FolderPath(), "harmony/" + y.Name.Substring(0, y.Name.IndexOf(",")) + ".dll");

                DevConsole.Console.Log("Resolving " + y.Name + " to " + path);

                return Assembly.LoadFrom(path);
            };

            base.Initialize(parentMod);
        }
    }

    public class UselessBehaviour : ModBehaviour {
        public override void OnActivate() {
        }

        public override void OnDeactivate() {
        }
    }
}

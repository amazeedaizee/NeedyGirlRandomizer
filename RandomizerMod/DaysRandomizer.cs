

using HarmonyLib;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using UniRx;

namespace Randomizer
{
    [HarmonyPatch]
    public class DaysRandomizer
    {

        public static bool IsActiveOrNotDataZ()
        {
            var validSave = MiscPatches.CheckIfSceneOrSaveValid();
            if (!SettingsReader.currentSettings.RandomizeDays)
                return false;
            if (validSave && SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex) == 1)
                return false;
            if (SingletonMonoBehaviour<EventManager>.Instance.isHorror && SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex) > 24)
                return false;
            return validSave;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(DayPassing), "startEvent", new Type[] { typeof(CancellationToken) })]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> RandomizeDays(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label endEvent = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DaysRandomizer),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Pop),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DaysRandomizer),nameof(GetRandomDay))),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertySetter(typeof(ReactiveProperty<int>), "Value")),
                new CodeInstruction(OpCodes.Br, endEvent)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].operand as MethodInfo == AccessTools.PropertyGetter(typeof(ReactiveProperty<int>), "Value") && !startSearching)
                {
                    code[i + 1].labels.Add(normalDelta);
                    code.InsertRange(i + 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Ldloc_0)
                    {
                        code[i].labels.Add(endEvent);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }

        public static int GetRandomDay()
        {
            int currentDay = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex);
            int randDay;
            do
            {
                randDay = UnityEngine.Random.Range(2, 31);
                if (randDay == 30)
                {
                    randDay = UnityEngine.Random.Range(0, 2) == 0 ? 30 : currentDay;
                }
            }
            while (randDay == currentDay);
            return randDay;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventManager), "Start")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void NoHorror()
        {
            int currentDay = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex);
            int curStress = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Stress);
            if (currentDay != 25 && SingletonMonoBehaviour<EventManager>.Instance.isHorror && curStress < 80)
            {
                SingletonMonoBehaviour<EventManager>.Instance.isHorror = false;
            }
        }

        /*
        [HarmonyPrefix]
        [HarmonyPatch(typeof(EgosaView2D), "UpdateHorror")]
        public static bool NoHorrorResults(EgosaView2D __instance, int horrorDay)
        {
            if (horrorDay <= 0 && IsActiveOrNotDataZ())
            {
                __instance.UpdateContents();
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(KitsuneView), "UpdateContents")]
        public static bool NoHorrorRestriction(KitsuneView __instance)
        {
            if (SingletonMonoBehaviour<EventManager>.Instance.isHorror && SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex) < 25)
            {
                __instance._scrollRect.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                __instance._buttonRoot.SetActive(value: true);
                return false;
            }
            return true;
        }
        */

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Event_tekiTalk), "startEvent", new Type[] { typeof(CancellationToken) })]
        public static bool NoTekiTalk()
        {
            if (IsActiveOrNotDataZ())
            {
                return true;
            }
            SingletonMonoBehaviour<WebCamManager>.Instance.RandomizeAmeAnimation();
            return false;
        }
    }
}

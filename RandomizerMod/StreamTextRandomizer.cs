

using HarmonyLib;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Randomizer
{
    [HarmonyPatch]
    public class StreamTextRandomizer
    {
        static int maxComments;
        static int maxDialogue;
        public static void InitializeStreamTextList()
        {
            maxComments = NgoEx.getKusoComments().Count;
            maxDialogue = NgoEx.getTenComments().Count;
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeStreamText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Live), "GetTitle")]
        public static void AdjustTitle(Live __instance, ref string __result)
        {
            string count;
            if (!IsActiveOrNotDataZ()) return;
            count = __result.Replace("\r\n", " ").Replace('\n', ' ');
            if (__instance.gameObject.name.Contains("dark"))
            {
                __instance.haisinTitle.rectTransform.sizeDelta = new Vector2(1351.38f, 27.74f);
                __result = count;
                return;
            }
            for (int i = 0; i < count.Length; i++)
            {
                if (i % 55 == 0)
                {
                    __instance.haisinTitle.rectTransform.position = new Vector3(-2.36f, -1.25f, 100);
                    __result = count;
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NgoEx), "Kome")]
        public static string RandomizeComments(string com)
        {
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            if (!IsActiveOrNotDataZ()) return com;
            switch (lang)
            {
                case LanguageType.JP:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyJP;

                case LanguageType.CN:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyCn;

                case LanguageType.KO:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyKo;

                case LanguageType.TW:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyTw;

                case LanguageType.IT:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyIt;

                case LanguageType.SP:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodySp;

                case LanguageType.VN:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyVN;

                case LanguageType.GE:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyGe;

                case LanguageType.FR:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyFr;

                default:
                    return NgoEx.getKusoComments()[GetTextId(maxComments)].BodyEn;

            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NgoEx), "TenTalk", new Type[] { typeof(string), typeof(LanguageType) })]
        static string RandomizeKAngelDialogue(string diag)
        {
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            if (!IsActiveOrNotDataZ()) return diag;
            switch (lang)
            {
                case LanguageType.JP:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyJP;

                case LanguageType.CN:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyCn;

                case LanguageType.KO:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyKo;

                case LanguageType.TW:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyTw;

                case LanguageType.IT:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyIt;

                case LanguageType.SP:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodySp;

                case LanguageType.VN:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyVn;

                case LanguageType.GE:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyGe;

                case LanguageType.FR:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyFr;

                default:
                    return NgoEx.getTenComments()[GetTextId(maxDialogue)].BodyEn;

            }
        }


        static int GetTextId(int maxCount)
        {
            return UnityEngine.Random.Range(0, maxCount);
        }


        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Dielog), "OnLanguageChanged")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> SetCorrectDie(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldstr)
                {
                    code[i] = new CodeInstruction(OpCodes.Ldc_I4, 335);
                    continue;
                }
                if (code[i].operand as MethodInfo == AccessTools.Method(typeof(NgoEx), nameof(NgoEx.TenTalk), new Type[] { typeof(string), typeof(LanguageType) }))
                {

                    code[i] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(NgoEx), nameof(NgoEx.SystemTextFromType)));
                    break;
                }
            }
            return code.AsEnumerable();
        }

    }
}

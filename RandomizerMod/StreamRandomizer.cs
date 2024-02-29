

using HarmonyLib;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace Randomizer
{
    [HarmonyPatch]
    public class StreamRandomizer
    {
        const string TITLE_UNKNOWN = "???";
        const string DESC_UNKNOWN = "????????????????";

        static bool isNormalPlaying = false;
        static AlphaLevel origStream;
        static int horrorNumber = 50;
        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeStreams)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NetaChipAlpha), "Show")]
        public static void ObscureLabel(NetaChipAlpha __instance)
        {
            if (!IsActiveOrNotDataZ())
                return;
            __instance.genre.text = TITLE_UNKNOWN;
            __instance.label.text = DESC_UNKNOWN;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NetaChipAlpha), "GetLevelSprite")]
        public static void ObscureLevelSprite(ref int level)
        {
            if (!IsActiveOrNotDataZ())
                return;
            level = 1;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(TooltipManager), "ShowAction")]
        public static void StartRandomStream(ActionType a, ref CmdMaster.Param c)
        {
            if (a == ActionType.Haishin && IsActiveOrNotDataZ())
            {
                c = new CmdMaster.Param()
                {
                    ParentAct = "Haishin",
                    Id = "",
                    LabelJp = TITLE_UNKNOWN,
                    LabelEn = TITLE_UNKNOWN,
                    LabelCn = TITLE_UNKNOWN,
                    LabelKo = TITLE_UNKNOWN,
                    LabelTw = TITLE_UNKNOWN,
                    LabelVn = TITLE_UNKNOWN,
                    LabelFr = TITLE_UNKNOWN,
                    LabelIt = TITLE_UNKNOWN,
                    LabelGe = TITLE_UNKNOWN,
                    LabelSp = TITLE_UNKNOWN,
                    DescJp = DESC_UNKNOWN,
                    DescEn = DESC_UNKNOWN,
                    DescCn = DESC_UNKNOWN,
                    DescKo = DESC_UNKNOWN,
                    DescTw = DESC_UNKNOWN,
                    DescVn = DESC_UNKNOWN,
                    DescFr = DESC_UNKNOWN,
                    DescIt = DESC_UNKNOWN,
                    DescGe = DESC_UNKNOWN,
                    DescSp = DESC_UNKNOWN,
                    PassingTime = 1
                };
            }

        }

        static IEnumerable<CodeInstruction> SetExtraCondHorror(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            CodeInstruction copiedOperand = new CodeInstruction(OpCodes.Nop);
            Label horrorStream = generator.DefineLabel();
            Label newCondition = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(EventManager), nameof(EventManager.nowEnding))),
                new CodeInstruction(OpCodes.Ldc_I4_S, 17),
                new CodeInstruction(OpCodes.Beq, horrorStream),
            };
            bool operandFound = false;
            bool isFirstStrPass = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldfld && !operandFound)
                {
                    copiedOperand = new(code[i - 1].opcode, code[i - 1].operand);
                    operandFound = true;
                    continue;
                }
                if (operandFound)
                {
                    if (code[i].opcode == OpCodes.Ldstr)
                    {
                        if (!isFirstStrPass)
                        {
                            isFirstStrPass = true;
                            continue;
                        }
                        copiedOperand.labels.Add(newCondition);
                        code[i - 3].labels.Add(horrorStream);
                        code[i - 15] = new(OpCodes.Bne_Un, newCondition);
                        code[i - 20] = new(OpCodes.Brfalse, newCondition);
                        code.InsertRange(i - 11, conditions);
                        code.Insert(i - 11, copiedOperand);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(LiveComment), "highlighted")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> SetExtraCondHighlight(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            CodeInstruction copiedOperand = new CodeInstruction(OpCodes.Nop);
            Label horrorStream = generator.DefineLabel();
            Label newCondition = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(EventManager), nameof(EventManager.nowEnding))),
                new CodeInstruction(OpCodes.Ldc_I4_S, 17),
                new CodeInstruction(OpCodes.Beq, horrorStream),
            };
            bool operandFound = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ret && !operandFound)
                {
                    copiedOperand = new(code[i + 1].opcode, code[i + 1].operand);
                    operandFound = true;
                }
                if (operandFound)
                {

                    code[i + 9].labels.Add(horrorStream);
                    code.InsertRange(i, conditions);
                    code.Insert(i, copiedOperand);
                    code[i].labels.Add(newCondition);
                    code[i - 1] = new(OpCodes.Brfalse, newCondition);
                    break;
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(haishin_horror_day2), "StartScenario", new Type[] { })]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> OnlyProcEndIfHorror(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            CodeInstruction copiedOperand = new CodeInstruction(OpCodes.Nop);
            Label noEnding = generator.DefineLabel();
            Label newCondition = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(EventManager), nameof(EventManager.isHorror))),
                new CodeInstruction(OpCodes.Brfalse, noEnding),
            };
            bool operandFound = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i + 1].opcode == OpCodes.Ldc_I4_S && !operandFound)
                {
                    copiedOperand = new(code[i].opcode, code[i].operand);
                    operandFound = true;
                }
                if (operandFound)
                {

                    code[i + 3].labels.Add(noEnding);
                    code.InsertRange(i, conditions);
                    code.Insert(i, copiedOperand);
                    break;
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NetachipChooser), "StartHaisin")]
        public static bool StartRandomStream(ref AlphaType alpha, ref int level)
        {
            int rngNum = UnityEngine.Random.Range(0, 101);
            int rngAlpha = UnityEngine.Random.Range(0, 12);
            int rngLevel = UnityEngine.Random.Range(1, 6);
            bool isNotDark = true;

            if (!IsActiveOrNotDataZ())
                return true;
            if (isNormalPlaying)
                return false;
            if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding != EndingType.Ending_None)
                return false;
            if (rngNum > 89 && SettingsReader.currentSettings.IncludeSpecialStreams && SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex) > 2)
            {
                switch (rngNum)
                {
                    case 90:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Meta;
                        break;
                    case 91:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Happy;
                        break;
                    case 92:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Ginga;
                        SingletonMonoBehaviour<EventManager>.Instance.SetShortcutState(false, 0);
                        SingletonMonoBehaviour<TaskbarManager>.Instance.SetTaskbarInteractive(interactive: false);
                        SingletonMonoBehaviour<TaskbarManager>.Instance.TaskBarGroup.alpha = 0f;
                        SingletonMonoBehaviour<EventManager>.Instance.AddEvent<Action_HaishinDark>();
                        isNotDark = false;
                        break;
                    case 93:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Kyouso;
                        break;
                    case 94:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Sucide;
                        break;
                    case 95:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Grand;
                        SingletonMonoBehaviour<TaskbarManager>.Instance.SetTaskbarInteractive(interactive: false);
                        SingletonMonoBehaviour<TaskbarManager>.Instance.TaskBarGroup.alpha = 0f;
                        SingletonMonoBehaviour<EventManager>.Instance.AddEvent<Action_HaishinDark>();
                        isNotDark = false;
                        break;
                    case 96:
                        alpha = AlphaType.Angel;
                        level = 6;
                        isNormalPlaying = true;
                        break;
                    case 97:
                    case 98:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Ntr;
                        break;
                    case 99:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_KowaiInternet;
                        GameObject.Find("InvertVolume").GetComponent<Volume>().enabled = true;
                        SingletonMonoBehaviour<EventManager>.Instance.SetShortcutState(false, 0);
                        SingletonMonoBehaviour<TaskbarManager>.Instance.SetTaskbarInteractive(interactive: false);
                        SingletonMonoBehaviour<EventManager>.Instance.AddEvent<Action_HaishinDark>();
                        isNotDark = false;
                        break;
                    case 100:
                        SingletonMonoBehaviour<EventManager>.Instance.nowEnding = EndingType.Ending_Ideon;
                        break;
                    default:
                        break;
                }
                if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding != EndingType.Ending_None)
                {
                    Initializer.logger.LogInfo($"Ending: {SingletonMonoBehaviour<EventManager>.Instance.nowEnding},\n" +
                                               $"Number Generated: {rngNum},\n");
                }
                return isNotDark;
            }
            origStream = new AlphaLevel(alpha, level);
            do
            {
                rngAlpha = UnityEngine.Random.Range(0, 12);
                rngLevel = UnityEngine.Random.Range(1, 6);
                alpha = (AlphaType)rngAlpha;
                level = rngLevel;
                if (rngNum > 30 && level > 3)
                {
                    level--;
                    if (level < 1) level = 1;
                }
            }
            while (((int)alpha == 12 && level > 2) || IsStreamDone(alpha, level));
            Initializer.logger.LogInfo($"Number Generated: {rngNum},\n" +
                                       $"Topic: {alpha}\n" +
                                       $"Level: {level}");
            isNormalPlaying = true;
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NetaManager), nameof(NetaManager.Haishined))]
        public static void AddOriginalUsed(ref AlphaType alpha, ref int level)
        {
            if (isNormalPlaying && origStream != null && !(horrorNumber >= 0 && horrorNumber < 4) && IsActiveOrNotDataZ())
            {
                alpha = origStream.alphaType;
                level = origStream.level;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventManager), nameof(EventManager.EndHaishin))]
        public static void IsStopPlaying(EventManager __instance)
        {
            /*
            if (isNormalPlaying && origStream != null && !(horrorNumber >= 0 && horrorNumber < 4) && IsActiveOrNotDataZ())
            {
                __instance.alpha = origStream.alphaType;
                __instance.alphaLevel = origStream.level;
                if (origStream.alphaType == AlphaType.Yamihaishin && origStream.level == 3)
                {
                    __instance.kyuusiCount = 0;
                }
            }
            */
            isNormalPlaying = false;
            origStream = null;
        }

        public static bool IsStreamDone(AlphaType alpha, int level)
        {
            //if (SettingsReader.currentSettings.RandomizeDays)
            //    return false;
            //if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists(a => a.alphaType == alpha && a.level == level))
            //     return true;
            if (alpha == AlphaType.Angel)
                return !IsValidMilestone(level);
            return false;
        }

        public static bool IsValidMilestone(int level)
        {
            int followers = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            if (followers >= 1000000 && level == 5)
                return true;
            if (followers >= 500000 && level == 4)
                return true;
            if (followers >= 250000 && level == 3)
                return true;
            if (followers >= 100000 && level == 2)
                return true;
            if (followers >= 10000 && level == 1)
                return true;
            return false;
        }
        public static bool HorrorStreamChance(Live __instance)
        {
            int dayIndex = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex);
            int rngHorror = UnityEngine.Random.Range(0, 60);
            if (!IsActiveOrNotDataZ())
                return true;
            Initializer.logger.LogInfo($"Horror Number Generated: {rngHorror},");
            if (dayIndex < 3)
                return true;
            if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding != EndingType.Ending_None)
                return true;
            if (SingletonMonoBehaviour<EventManager>.Instance.isHorror && dayIndex > 24)
                return true;
            if (rngHorror > 3)
                return true;
            SingletonMonoBehaviour<StatusManager>.Instance.isTodayHaishined = true;
            SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.RenzokuHaishinCount, 1);
            horrorNumber = rngHorror;
            switch (rngHorror)
            {
                case 0:
                    __instance.SetScenario<haishin_horror_day1>();
                    break;
                case 1:
                    __instance.SetScenario<haishin_horror_day2>();
                    break;
                case 2:
                    __instance.SetScenario<haishin_horror_day3>();
                    break;
                case 3:
                    __instance.SetScenario<haishin_horror_day4>();
                    break;
            }
            isNormalPlaying = false;
            return false;


        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(KitsuneView), "UpdateContents")]
        public static void HorrorRestriction(KitsuneView __instance)
        {
            if ((horrorNumber >= 0 && horrorNumber < 4) && IsActiveOrNotDataZ())
            {
                __instance._scrollRect.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                __instance._buttonRoot.SetActive(value: false);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ngov3.EventManager), "Start")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void IsNotHorrorPlaying()
        {
            if (horrorNumber != 50)
                horrorNumber = 50;
        }


        public static void CheckScenario(LiveScenario __result)
        {
            Initializer.logger.LogInfo("Set Scenario: " + __result.GetType().Name);
        }
    }
}

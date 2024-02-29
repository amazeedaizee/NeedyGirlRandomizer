
using HarmonyLib;
using Newtonsoft.Json;
using ngov3;
using System.Collections.Generic;
using System.IO;

namespace Randomizer
{
    public class TextList
    {
        public List<string> textList;
    }

    [HarmonyPatch]
    public class EventTextRandomizer
    {
        static TextList currentTextList;
        static string loveOne;
        static string loveTwo;
        static string loveThree;
        static string loveFour;
        static string paperOne;
        static string paperTwo;
        static string paperThree;
        static string paperFour;
        static string rent;

        public static void InitializeEventTexts()
        {
            string notepadFile;
            if (File.Exists(Path.GetDirectoryName(Initializer.PInfo.Location) + "\\notepadTexts.json"))
            {
                notepadFile = File.ReadAllText(Path.GetDirectoryName(Initializer.PInfo.Location) + "\\notepadTexts.json");
                currentTextList = JsonConvert.DeserializeObject<TextList>(notepadFile);
            }
            else
            {

                currentTextList = JsonConvert.DeserializeObject<TextList>(texts.notepadTexts);
            }

            loveOne = GetRandomText();
            loveTwo = GetRandomText();
            loveThree = GetRandomText();
            loveFour = GetRandomText();
            paperOne = GetRandomText();
            paperTwo = GetRandomText();
            paperThree = GetRandomText();
            paperFour = GetRandomText();
            rent = GetRandomText();
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeNotepadText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(App_TextEditor), "Awake")]
        public static bool RandomizeEventTexts(App_TextEditor __instance)
        {
            if (!IsActiveOrNotDataZ()) return true;
            if (currentTextList == null || currentTextList.textList.Count == 0) return true;
            switch (__instance.nakami)
            {
                case TextType.Event_Aim_Rent_Mail001:
                    __instance.text.text = rent;
                    break;
                case TextType.Event_Psyche_Nikki_1:
                    __instance.text.text = paperOne;
                    break;
                case TextType.Event_Psyche_Nikki_2:
                    __instance.text.text = paperTwo;
                    break;
                case TextType.Event_Psyche_Nikki_3:
                    __instance.text.text = paperThree;
                    break;
                case TextType.Event_Psyche_Nikki_4:
                    __instance.text.text = paperFour;
                    break;
                case TextType.Event_Diary001:
                    __instance.text.text = loveOne;
                    break;
                case TextType.Event_Diary002:
                    __instance.text.text = loveTwo;
                    break;
                case TextType.Event_Diary003:
                    __instance.text.text = loveThree;
                    break;
                case TextType.Event_Diary004:
                    __instance.text.text = loveFour;
                    break;
                default:
                    return true;
            }
            return false;
        }

        public static string GetRandomText()
        {
            return currentTextList.textList[UnityEngine.Random.Range(0, currentTextList.textList.Count)];
        }
    }
}

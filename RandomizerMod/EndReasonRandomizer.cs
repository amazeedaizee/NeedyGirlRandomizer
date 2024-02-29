using HarmonyLib;
using Newtonsoft.Json;
using ngov3;
using System.IO;

namespace Randomizer
{
    [HarmonyPatch]
    public class EndReasonRandomizer
    {
        static TextList currentTextList;
        static string currentReason;
        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeEndMsgText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        public static void InitializeEndMsgTexts()
        {
            string notepadFile;
            if (File.Exists(Path.GetDirectoryName(Initializer.PInfo.Location) + "\\endMsgsTexts.json"))
            {
                notepadFile = File.ReadAllText(Path.GetDirectoryName(Initializer.PInfo.Location) + "\\endMsgsTexts.json");
                currentTextList = JsonConvert.DeserializeObject<TextList>(notepadFile);
            }
            else
            {
                currentTextList = JsonConvert.DeserializeObject<TextList>(texts.endMsgsTexts);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EndingDialog), "Start")]
        [HarmonyPatch(typeof(EndingDialog), "OnLanguageChanged")]
        public static void RandomizeEndReasons(EndingDialog __instance)
        {
            int endTextIndex = UnityEngine.Random.Range(0, currentTextList.textList.Count);
            if (!IsActiveOrNotDataZ()) return;
            if (string.IsNullOrEmpty(currentReason))
            {
                currentReason = currentTextList.textList[endTextIndex];
            }
            __instance._notionText.text = currentReason;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(EndingManager), "Awake")]
        public static void ClearCurrentReason()
        {
            if (!string.IsNullOrEmpty(currentReason))
            {
                currentReason = "";
            }
        }
    }
}



using HarmonyLib;
using NGO;
using ngov3;
using System;

namespace Randomizer
{
    [HarmonyPatch]
    public class JineTextRandomizer
    {
        static int maxJines;
        public static void InitializeJineTextList()
        {
            maxJines = Enum.GetValues(typeof(JineType)).Length - 1;
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeJineText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(JineCell2D), "SetBody")]
        public static void ObscurePiText(JineCell2D __instance, JineDrawable nakami)
        {
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            if (!IsActiveOrNotDataZ()) return;
            if (nakami.JineUserType != JineUserType.pi) return;
            if (nakami.StampType != StampType.None) return;
            if (nakami.BodyJp == nakami.BodyEn && nakami.BodyJp == nakami.BodyVn) return;
            if (!string.IsNullOrEmpty(nakami.BodyJp))
            {
                __instance.honbun.text = JineDataConverter.GetJineTextFromTypeId(JineType.Jine_Reply_ok_1);
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(JineManager), "AddJineHistory", new Type[] { typeof(JineData) })]
        public static void RandomizeJineHistory(ref JineData l)
        {
            JineType t;
            if (!IsActiveOrNotDataZ()) return;
            do
            {
                t = GetRandJineType();
            }
            while (t.ToString().StartsWith("Jine_Label")
            || t.ToString().StartsWith("Separator")
            || t.ToString().Contains(@"_Option")
             || t.ToString().Contains(@"_Oprtion")
              || t.ToString().Contains(@"_Optuion")
            || t.ToString().EndsWith("_pi"));
            if (l.user == JineUserType.ame)
            {
                l.id = t;
            }
        }

        public static JineType GetRandJineType()
        {
            return (JineType)UnityEngine.Random.Range(0, maxJines); ;
        }
    }
}

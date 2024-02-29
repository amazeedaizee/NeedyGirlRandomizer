

using HarmonyLib;
using ngov3;
using System;

namespace Randomizer
{
    [HarmonyPatch]
    public class SearchRandomizer
    {

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeSearchStText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(ImageViewer), "Blink")]
        public static void RandomizeBlinks(ref string nakami)
        {
            var value = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            if (!IsActiveOrNotDataZ()) return;
            nakami = NgoEx.EgosaString(value);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NgoEx), "EgosaString", new Type[] { typeof(LanguageType), typeof(int), typeof(string), typeof(string) })]
        public static bool RandomizeOrderSearch(LanguageType lang, ref string __result)
        {
            if (!IsActiveOrNotDataZ()) return true;
            __result = NgoEx.EgosaString(lang);
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NgoEx), "EgosaString", new Type[] { typeof(LanguageType), typeof(string), typeof(string) })]
        public static void RandomizeSearches(ref string type, ref string jouken)
        {
            int rngNum = UnityEngine.Random.Range(0, 101);
            int rngAlpha = UnityEngine.Random.Range(0, 13);
            int rngLevel = UnityEngine.Random.Range(1, 6);
            if (!IsActiveOrNotDataZ()) return;
            if (type == "NetaGet_PR5") return;
            if (rngNum < 40)
            {
                type = "random";
                jouken = "random";
            }
            else if (rngNum < 65)
            {
                type = "follower";
                jouken = RandomizeFollowerRank();
            }
            else if (rngNum < 90)
            {
                do
                {
                    if (rngAlpha == 12 && rngLevel > 2)
                    {
                        rngAlpha = UnityEngine.Random.Range(0, 13);
                        rngLevel = UnityEngine.Random.Range(1, 6);
                    }
                }
                while (rngAlpha == 12 && rngLevel > 2);
                type = "haisin";
                jouken = $"{(AlphaType)rngAlpha}_{rngLevel}";
            }
            else if (rngNum == 100)
            {
                type = "horror";
                jouken = "horrorday2Afterr";
            }
            else
            {
                rngNum = UnityEngine.Random.Range(0, 1);
                type = "horror";
                jouken = rngNum == 0 ? "horrorday1" : "horrorday2Before";
            }
        }

        public static string RandomizeFollowerRank()
        {
            int rngNum = UnityEngine.Random.Range(0, 5);
            switch (rngNum)
            {
                case 0:
                case 1:
                    return "small";
                case 2:
                case 3:
                    return "medium";
                default:
                    return "large";
            }
        }
    }
}

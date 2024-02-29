using HarmonyLib;
using ngov3;

namespace Randomizer
{
    [HarmonyPatch]
    public class AnonRandomizer
    {
        static int maxAnons;
        public static void InitializeAnonTextList()
        {
            maxAnons = NgoEx.getKitunes().Count - 1;
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeSearchStText)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NgoEx), "SuretaiFromType")]
        public static void RandomizeAnonTitle(ref string type)
        {
            type = "Suretai_";
            int rngNum = UnityEngine.Random.Range(0, 51);
            if (rngNum < 41)
            {
                if (rngNum == 0)
                {
                    type += "Ideon";
                }
                else if (rngNum < 11)
                {
                    type += "Normal";
                }
                else if (rngNum < 21)
                {
                    type += "Shuueki";
                }
                else if (rngNum < 31)
                {
                    type += "Angel";
                }
                else
                {
                    type += "Legend";
                }
            }
            else if (rngNum < 46)
            {
                type += "Charisma";
            }
            else
            {
                type += "Horror_";
                switch (rngNum)
                {
                    case 46:
                        type += "Day2_BeforeHaishin";
                        break;
                    case 47:
                        type += "Day2_AfterHaishin";
                        break;
                    case 48:
                        type += "Day3_first";
                        break;
                    case 49:
                        type += "Day3_second";
                        break;
                    case 50:
                        type += "Day3_third";
                        break;
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NgoEx), "KituneFromType")]
        public static string RandomizeAnonText(string anon)
        {
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            if (!IsActiveOrNotDataZ()) return anon;
            switch (lang)
            {
                case LanguageType.JP:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyJp;

                case LanguageType.CN:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyCn;

                case LanguageType.KO:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyKo;

                case LanguageType.TW:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyTw;

                case LanguageType.IT:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyIt;

                case LanguageType.SP:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodySp;

                case LanguageType.VN:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyVn;

                case LanguageType.GE:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyGe;

                case LanguageType.FR:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyFr;

                default:
                    return NgoEx.getKitunes()[GetTextId(maxAnons)].BodyEn;

            }
        }

        static int GetTextId(int maxCount)
        {
            return UnityEngine.Random.Range(0, maxCount);
        }
    }
}

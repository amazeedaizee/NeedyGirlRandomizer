

using HarmonyLib;
using ngov3;
using System;

namespace Randomizer
{
    [HarmonyPatch]
    public class TweetRepliesRandomizer
    {
        static int maxReplies;
        public static void InitializeTweetReplyList()
        {
            maxReplies = Enum.GetValues(typeof(KusoRepType)).Length - 1;
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeTweetReplies)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PoketterManager), "AddTweet", new Type[] { typeof(TweetData) })]
        public static void RandomizeTweetText(ref TweetData data)
        {
            KusoRepType t;
            int maxReplyPerTw = UnityEngine.Random.Range(0, 7);
            if (!IsActiveOrNotDataZ()) return;
            if (data.IsOmote)
            {
                data.kusoReps.Clear();
                while (data.kusoReps.Count < maxReplyPerTw)
                {
                    t = GetRandReplyType();
                    data.kusoReps.Add(t);
                }
            }

        }

        public static KusoRepType ValidateKusoRep(KusoRepType type)
        {
            KusoRepDrawable drawable;
            try
            {
                drawable = new KusoRepDrawable(type);
                return type;
            }
            catch { return KusoRepType.KusoRep001; }
        }


        public static KusoRepType GetRandReplyType(bool includeFanArt = true)
        {
            KusoRepType t;
            do
            {
                t = (KusoRepType)UnityEngine.Random.Range(0, maxReplies);
            } while (t.ToString().StartsWith("horror")
                    || (!includeFanArt && (t.ToString().StartsWith("FanRep") || t.ToString().StartsWith("Boshuu"))));
            return ValidateKusoRep(t); ;
        }

        public static string GetRandReplyText(bool includeFanArt = true)
        {
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            KusoRepDrawable drawable = new(GetRandReplyType(includeFanArt));
            switch (lang)
            {
                case LanguageType.JP:
                    return drawable.BodyJp;
                case LanguageType.CN:
                    return drawable.BodyCn;
                case LanguageType.KO:
                    return drawable.BodyKo;
                case LanguageType.TW:
                    return drawable.BodyTw;
                case LanguageType.IT:
                    return drawable.BodyIt;
                case LanguageType.SP:
                    return drawable.BodySp;
                case LanguageType.VN:
                    return drawable.BodyVn;
                case LanguageType.GE:
                    return drawable.BodyGe;
                case LanguageType.FR:
                    return drawable.BodyFr;
                default:
                    return drawable.BodyEn;

            }
        }
    }
}

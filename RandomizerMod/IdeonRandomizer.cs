
using HarmonyLib;
using NGO;
using ngov3;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Randomizer
{
    [HarmonyPatch]
    public class IdeonRandomizer
    {
        const string INSTA_TITLE_ID = "Ideon_Insta_res0";
        const string NEWS_TITLE_ID = "Ideon_news_res1";
        const string FIVECH_TITLE_ID = "Ideon_hutaba_res0";
        const string WIKI_TITLE_ID = "Ideon_wiki_res0";
        static List<string> titles = new()
        {
            INSTA_TITLE_ID, NEWS_TITLE_ID, FIVECH_TITLE_ID, WIKI_TITLE_ID
        };

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NgoEx), "SystemTextFromType")]
        public static void RandomizeIdeonText(SystemTextType type, LanguageType lang, ref string __result)
        {
            KusoRepView2D kusoRep;
            string instaText;
            string sysId = type.ToString();
            if (SceneManager.GetActiveScene().name == "BiosToLoad" || SingletonMonoBehaviour<Settings>.Instance.saveNumber < 1 || SingletonMonoBehaviour<Settings>.Instance.saveNumber > 4)
                return;
            if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding != EndingType.Ending_Ideon) return;
            if (titles.Contains(sysId)) return;
            else if (sysId.Contains("Ending_Ideon_taiki") && StreamTextRandomizer.IsActiveOrNotDataZ())
            {
                __result = StreamTextRandomizer.RandomizeComments(__result);
            }
            else if (sysId.Contains("Ideon_hutaba") && AnonRandomizer.IsActiveOrNotDataZ())
            {
                __result = AnonRandomizer.RandomizeAnonText(__result);
            }
            else if (sysId.Contains("Ideon_news") && EventTextRandomizer.IsActiveOrNotDataZ())
            {
                if (sysId.Contains("_res2"))
                {
                    __result = EventTextRandomizer.GetRandomText();
                }
                else
                {
                    __result = "";
                }
            }
            else if (sysId.Contains("Ideon_Insta") && !sysId.Contains("_res0") && TweetRepliesRandomizer.IsActiveOrNotDataZ())
            {
                __result = TweetRepliesRandomizer.GetRandReplyText(false);
            }
            else if (sysId.Contains("Ideon_wiki") && EventTextRandomizer.IsActiveOrNotDataZ())
            {
                if (sysId.Contains("_res1"))
                {
                    __result = EventTextRandomizer.GetRandomText();
                }
                else
                {
                    __result = "";
                }
            }
        }

    }
}

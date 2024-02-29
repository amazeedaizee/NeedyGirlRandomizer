

using HarmonyLib;
using ngov3;
using System;

namespace Randomizer
{
    [HarmonyPatch]
    public class TweetTextRandomizer
    {
        static int maxTweets;
        public static void InitializeTweetTextList()
        {
            maxTweets = Enum.GetValues(typeof(TweetType)).Length - 1;
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeTweets)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PoketterManager), "AddTweet", new Type[] { typeof(TweetData) })]
        public static void RandomizeTweetText(ref TweetData data)
        {
            int followers = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            TweetType t;
            bool isValid;
            PoketterManager inst = new PoketterManager();

            if (!IsActiveOrNotDataZ()) return;
            data.FavNumber = UnityEngine.Random.Range(0, followers);
            data.RtNumber = UnityEngine.Random.Range(0, (followers / 4 == 0) ? 40 : followers / 4);
            if (data.Type == TweetType.None) return;
            do
            {
                t = GetRandTweetType();
                data.Type = t;
                try
                {
                    isValid = inst.isValidTweetData(data);
                }
                catch { isValid = false; }

            } while (!isValid);
        }

        public static TweetType GetRandTweetType()
        {
            return (TweetType)UnityEngine.Random.Range(0, maxTweets); ;
        }
    }
}



using HarmonyLib;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer
{
    [HarmonyPatch]
    public class MusicRandomizer
    {
        static List<string> music = new();
        public static void InitializeMusictList()
        {
            var all = Enum.GetNames(typeof(SoundType)).ToList();
            music.AddRange(all.FindAll(m => m.Contains("BGM_")));
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeMusic)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), "PlayBgmByType", new Type[] { typeof(SoundType), typeof(bool) })]
        public static void RandomizeMusic(ref SoundType type)
        {
            if (!IsActiveOrNotDataZ()) return;
            if (type == SoundType.BANK_bank || type == SoundType.BGM_InternetOverdose) return;
            try
            {
                if (Enum.TryParse<SoundType>(music[UnityEngine.Random.Range(0, music.Count)], out var newType))
                {
                    type = newType;
                }
                else { type = SoundType.BGM_mainloop_normal; }
            }
            catch { type = SoundType.BGM_mainloop_normal; }
        }
    }
}



using HarmonyLib;
using NGO;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer
{
    [HarmonyPatch]
    public class SoundRandomizer
    {
        static List<string> sound = new();
        public static void InitializeSoundList()
        {
            var all = Enum.GetNames(typeof(SoundType)).ToList();
            sound.AddRange(all.FindAll(m => m.Contains("SE_")));
        }

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeSoundFX)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AudioManager), "PlaySeByType", new Type[] { typeof(SoundType), typeof(bool) })]
        public static void RandomizeMusic(ref SoundType type)
        {
            if (!IsActiveOrNotDataZ()) return;
            try
            {
                if (Enum.TryParse<SoundType>(sound[UnityEngine.Random.Range(0, sound.Count)], out var newType))
                {
                    type = newType;
                }
                else { type = SoundType.SE_poko; }
            }
            catch { type = SoundType.SE_poko; }
        }
    }
}

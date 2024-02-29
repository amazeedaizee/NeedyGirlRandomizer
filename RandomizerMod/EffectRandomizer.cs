using HarmonyLib;
using ngov3;
using ngov3.Effect;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Randomizer
{
    [HarmonyPatch]
    internal class EffectRandomizer
    {
        static List<EffectType> effectTypes = new List<EffectType>()
        {
            EffectType.Psyche,
            EffectType.Weed,
            EffectType.OD,
            EffectType.OD2,
            EffectType.OD3,
        };

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeEffects)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DayPassing2D), "dayPass")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SetChanceBorder()
        {
            int rngNum = UnityEngine.Random.Range(0, 4);
            int effectType = UnityEngine.Random.Range(0, effectTypes.Count);
            if (!IsActiveOrNotDataZ()) return;
            if (rngNum == 0)
            {
                SingletonMonoBehaviour<PostEffectManager>.Instance.SetShader(effectTypes[effectType]);
                SingletonMonoBehaviour<PostEffectManager>.Instance.SetShaderWeight(0.2f);
            }
            else
            {
                SingletonMonoBehaviour<PostEffectManager>.Instance.ResetShader();
            }
        }
    }
}

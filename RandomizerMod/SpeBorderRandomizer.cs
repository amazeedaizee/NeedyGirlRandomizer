using Cysharp.Threading.Tasks;
using HarmonyLib;
using ngov3;
using ngov3.Effect;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Randomizer
{
    [HarmonyPatch]
    public class SpeBorderRandomizer
    {
        public static List<ChanceEffectType> borderEffects = new List<ChanceEffectType>()
        {
            ChanceEffectType.Kenjo,
            ChanceEffectType.Body,
            ChanceEffectType.Chainsaw,
            ChanceEffectType.Danger,
            ChanceEffectType.Gothic,
            ChanceEffectType.Porori
        };
        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeSpecialBorders)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(DayPassing2D), "dayPass")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SetChanceBorder()
        {
            int rngNum = UnityEngine.Random.Range(0, 4);
            int borderType = UnityEngine.Random.Range(0, borderEffects.Count);
            if (!IsActiveOrNotDataZ()) return;
            if (rngNum == 0)
            {
                SetBorderLoop(borderEffects[borderType]);
            }

            async UniTask SetBorderLoop(ChanceEffectType chanceEffect)
            {
                SingletonMonoBehaviour<ChanceEffectController>.Instance.OnReach(chanceEffect);
                if (chanceEffect != ChanceEffectType.Kenjo)
                {
                    if (chanceEffect != ChanceEffectType.Danger)
                    {
                        await UniTask.Delay(2700);
                    }
                    SingletonMonoBehaviour<ChanceEffectController>.Instance.OnFever();
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ngov3.DayPassing), "startEvent", new Type[] { typeof(CancellationToken) })]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ResetEffect()
        {
            if (!IsActiveOrNotDataZ()) return;
            if (SingletonMonoBehaviour<ChanceEffectController>.Instance != null && SingletonMonoBehaviour<ChanceEffectController>.Instance._currentEffect != null)
            {
                SingletonMonoBehaviour<ChanceEffectController>.Instance.OnOut();
            }

        }
    }
}

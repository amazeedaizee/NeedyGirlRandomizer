using HarmonyLib;
using ngov3;

namespace Randomizer
{
    [HarmonyPatch]
    public class DayBorderRandomizer
    {
        public static bool IsActiveOrNotDataZ()
        {
            var validSave = MiscPatches.CheckIfSceneOrSaveValid();
            if (!SettingsReader.currentSettings.RandomizeDayBorders)
                return false;
            if (validSave && SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayIndex) == 1)
                return false;
            return validSave;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ngov3.Effect.DayAndNight), "DayToNight")]
        public static void RandomDayBorders(ref int dayPart)
        {
            if (!IsActiveOrNotDataZ()) return;
            dayPart = UnityEngine.Random.Range(0, 3);
        }
    }
}

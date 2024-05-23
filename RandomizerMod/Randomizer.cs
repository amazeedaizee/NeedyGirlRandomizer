
using BepInEx;
using BepInEx.Logging;
using Cysharp.Threading.Tasks;
using HarmonyLib;
using ngov3;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Randomizer
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInProcess("Windose.exe")]
    public class Initializer : BaseUnityPlugin
    {

        public const string pluginGuid = "needy.girl.random";
        public const string pluginName = "Randomizer";
        public const string pluginVersion = "2.0.0.1";

        public static ManualLogSource logger;

        public static PluginInfo PInfo;

        public void Awake()
        {
            logger = Logger;
            PInfo = Info;
            this.gameObject.hideFlags = HideFlags.HideAndDontSave;
            Harmony harmony = new Harmony(pluginGuid);

            /// Credits to zonni for their post I found on Stack Overflow regarding patching inner MoveNext's 
            /// https://stackoverflow.com/a/77701064
            /// V--- Below code is used based on their answer ---V
            var seaMethod = typeof(Event_Sea).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(m => m.Name.Contains("<startEvent>b__1"));
            var seaState = seaMethod.GetCustomAttribute<AsyncStateMachineAttribute>();
            var seaMoveNext = seaState.StateMachineType.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
            var startDialogMethod = typeof(Event_Dialog).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).ToList().FindAll(m => m.Name.Contains("<startEvent>b__2"));
            foreach (var m in startDialogMethod)
            {
                var diaState = m.GetCustomAttribute<AsyncStateMachineAttribute>();
                var diaMoveNext = diaState.StateMachineType.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
                harmony.Patch(diaMoveNext, null, null, new HarmonyMethod(AccessTools.Method(typeof(StatRandomizing), "Dialog_Transpiler")));
            }
            var dialogContMethod = typeof(Event_Dialog).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).ToList().FindAll(m => m.Name.Contains("<eventContinue"));
            foreach (var m in dialogContMethod)
            {
                var contState = m.GetCustomAttribute<AsyncStateMachineAttribute>();
                var contMoveNext = contState.StateMachineType.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
                harmony.Patch(contMoveNext, null, null, new HarmonyMethod(AccessTools.Method(typeof(StatRandomizing), "Dialog_Transpiler")));
            }
            /// ^-----------------------------------------------^

            var setSceneMethod = AccessTools.FirstMethod(typeof(Live), m => m.Name == "SetScenario");

            harmony.Patch(seaMoveNext, null, null, new HarmonyMethod(AccessTools.Method(typeof(StatRandomizing), "EventSea_Transpiler")));
            harmony.Patch(setSceneMethod, new HarmonyMethod(AccessTools.Method(typeof(StreamRandomizer), "HorrorStreamChance")));
            harmony.Patch(setSceneMethod, null, null, new HarmonyMethod(AccessTools.Method(typeof(StreamRandomizer), "SetExtraCondHorror")));
            harmony.PatchAll();
            logger.LogInfo("Randomizer started. Please note that Steam Achievements are currently diaabled while using this mod.");

            SettingsReader.LoadSettings();

            JineTextRandomizer.InitializeJineTextList();
            TweetTextRandomizer.InitializeTweetTextList();
            TweetRepliesRandomizer.InitializeTweetReplyList();
            StreamTextRandomizer.InitializeStreamTextList();
            MusicRandomizer.InitializeMusictList();
            SoundRandomizer.InitializeSoundList();
            AnonRandomizer.InitializeAnonTextList();
            EventTextRandomizer.InitializeEventTexts();
            EndReasonRandomizer.InitializeEndMsgTexts();
        }


    }

    [HarmonyPatch]
    public class MiscPatches
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(NgoEvent), "GoOut")]
        public static async UniTask GoOut(NgoEvent instance)
        {

        }
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(NgoEvent), "endEvent")]
        public static void endEvent(NgoEvent instance)
        {

        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(NgoEvent), "startEvent", new Type[] { typeof(CancellationToken) })]
        public static async UniTask startEvent(NgoEvent instance, CancellationToken cancellationToken = default(CancellationToken))
        {


        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(NgoEvent), "tweetFromTip")]
        public static void tweetFromTip(NgoEvent instance, AlphaType alpha = AlphaType.none, int level = 0)
        {

        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Boot), "StartGame")]
        [HarmonyPatch(typeof(App_LoadDataComponent), "StartGame")]
        public static void ResetSaveData()
        {
            int saveNumber = SingletonMonoBehaviour<Settings>.Instance.saveNumber;
            for (int i = 0; i <= 30; i++)
            {
                if (i != 1 && SaveRelayer.IsSlotDataExists(string.Format("Data{0}_Day{1}{2}", saveNumber, i, SaveRelayer.EXTENTION)))
                {
                    SaveRelayer.DeleteData(string.Format("Data{0}_Day{1}{2}", saveNumber, i, SaveRelayer.EXTENTION));
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Login), "SteamInput")]
        public static void AutoPassword(Login __instance)
        {
            __instance._input.text = "angelkawaii2";
        }

        public static bool CheckIfSceneOrSaveValid()
        {
            if (SingletonMonoBehaviour<Settings>.Instance.saveNumber < 1 || SingletonMonoBehaviour<Settings>.Instance.saveNumber > 4 || SceneManager.GetActiveScene().name == "BiosToLoad")
                return false;
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventKakusinikuru), "OnPointerEnter")]
        public static void ForceWebcamOpen()
        {
            var a = SingletonMonoBehaviour<WindowManager>.Instance.isAppOpen(AppType.Webcam);
            if (!CheckIfSceneOrSaveValid())
            {
                return;
            }
            if (!a)
            {
                SingletonMonoBehaviour<WindowManager>.Instance.NewWindow(AppType.Webcam);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AchievementStatsUpdater), "UpdateStats")]
        public static bool NoAchievements()
        {
            return false;
        }
    }

}



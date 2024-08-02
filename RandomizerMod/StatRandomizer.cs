

using HarmonyLib;
using ngov3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Randomizer
{
    //Main code used to randomize stat gain.

    [HarmonyPatch]
    public class StatRandomizing
    {
        public static StatRandomizing GetInstance()
        {
            return new StatRandomizing();
        }
        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeStats)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }
        //Randomizes Follower gain from Streams.
        public static void RandomFollowers()
        {
            int num;
            int status = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            System.Random random = new System.Random();
            int followers = 0;

            if (!IsActiveOrNotDataZ())
                return;

            if (random.Next(0, 50) == 0)
            {
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatusToNumber(StatusType.Follower, 9999999);
                Initializer.logger.LogInfo("Achieved max followers.");
                return;
            }
            else
            {
                num = random.Next(0, 21);
                if (num == 1)
                {
                    followers = random.Next(0, 100);
                    Initializer.logger.LogInfo("Follower increase: Lowest (0, 100).");
                }
                if (num > 1 && num <= 6)
                {
                    followers = random.Next(0, 1000);
                    Initializer.logger.LogInfo("Follower increase: Low (0, 1000).");
                }
                if (num > 6 && num <= 11)
                {
                    followers = random.Next(1000, 10000);
                    Initializer.logger.LogInfo("Follower increase: Subpar (1000, 10000).");
                }
                if (num > 11 && num <= 15)
                {
                    followers = random.Next(10000, 50000);
                    Initializer.logger.LogInfo("Follower increase: Medium (10000, 50000).");
                }
                if (num > 15 && num <= 18)
                {
                    followers = random.Next(10000, 100000);
                    Initializer.logger.LogInfo("Follower increase: High (10000, 100000).");
                }
                if (num > 18 && num <= 20)
                {
                    switch (num)
                    {
                        case 19:
                            if (status >= 500000)
                            {
                                followers = random.Next(-250000, -100000);
                                Initializer.logger.LogInfo("Follower decrease: High (-250000, -100000).");
                                break;
                            }
                            if (status >= 10000)
                            {
                                followers = random.Next(-5000, 0);
                                Initializer.logger.LogInfo("Follower decrease: Medium (-5000, 0).");
                                break;
                            }
                            if (status <= 1001)
                            {
                                followers = random.Next(0, 500);
                                Initializer.logger.LogInfo("Follower increase: Low (0, 500).");
                                break;
                            }
                            followers = random.Next(-500, 0);
                            Initializer.logger.LogInfo("Follower decrease: Low (-500, 0).");
                            break;
                        case 20:
                            if (status >= 100000)
                            {
                                followers = random.Next(-100000, 0);
                                Initializer.logger.LogInfo("Follower decrease: High (-100000, 0).");
                                break;
                            }
                            if (status >= 10000)
                            {
                                followers = random.Next(-10000, 0);
                                Initializer.logger.LogInfo("Follower decrease: Medium (-10000, 0).");
                                break;
                            }
                            if (status <= 1001)
                            {
                                followers = random.Next(0, 1000);
                                Initializer.logger.LogInfo("Follower increase: Low (0, 1000).");
                                break;
                            }
                            followers = random.Next(-1000, 0);
                            Initializer.logger.LogInfo("Follower decrease: Low (-1000, 0).");
                            break;
                        default:
                            Initializer.logger.LogInfo("No follower changes.");
                            break;
                    }
                }
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Follower, followers);
                if (followers == 0)
                {
                    Initializer.logger.LogInfo("No follower changes.");
                    return;
                }
                else
                {
                    Initializer.logger.LogInfo(followers < 0 ? "Lost " + followers + " followers." : "Gained " + followers + " followers!");
                    return;
                }

            }
        }

        //Randomizes Stress, Affection and Darkness gain.
        public static void RandomStats(bool isMissStream)
        {
            System.Random random = new System.Random();

            int num = random.Next(0, 11);
            int num2 = random.Next(0, 3);
            int stress = random.Next(-30, 31);
            int love = random.Next(-20, 21);
            int yami = random.Next(-20, 21);

            if (!IsActiveOrNotDataZ())
                return;
            LoseFollowerChance(random, num, isMissStream);
            if (num < 6 && num != 0)
            {
                switch (num2)
                {
                    case 0:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, stress);
                        Initializer.logger.LogInfo("Got " + stress + " Stress!");
                        return;
                    case 1:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Love, love);
                        Initializer.logger.LogInfo("Got " + love + " Affection!");
                        return;
                    case 2:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Yami, yami);
                        Initializer.logger.LogInfo("Got " + yami + " Darkness!");
                        return;
                    default:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, 0);
                        Initializer.logger.LogInfo("No stat changes.");
                        return;
                }
            }
            if (num >= 6 && num != 10)
            {
                switch (num2)
                {
                    case 0:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, stress);
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Love, love);
                        Initializer.logger.LogInfo("Got " + stress + " Stress and " + love + " Affection!");
                        return;
                    case 1:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Love, love);
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Yami, yami);
                        Initializer.logger.LogInfo("Got " + love + " Affection and " + yami + " Darkness!");
                        return;
                    case 2:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, stress);
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Yami, yami);
                        Initializer.logger.LogInfo("Got " + stress + " Stress and " + yami + " Darkness!");
                        return;
                    default:
                        SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, 0);
                        Initializer.logger.LogInfo("No stat changes.");
                        break;
                }
            }
            if (num == 10)
            {
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, stress);
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Love, love);
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Yami, yami);
                Initializer.logger.LogInfo("Got " + stress + " Stress and " + love + " Affection and " + yami + " Darkness!");
            }
            if (num == 0)
            {
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Stress, 0);
                Initializer.logger.LogInfo("No stat changes.");
            }
        }

        static void LoseFollowerChance(Random random, int num, bool isMissStream)
        {
            int negFollowChance = random.Next(0, 4);
            int curFollows = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            int curStress = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Stress);
            int followers = 0;
            int tempFollowLoss;
            if (!SettingsReader.currentSettings.IncludeLoseFollowChance)
            {
                return;
            }
            if (curStress >= 80)
            {
                negFollowChance *= 3;
            }
            else if (curStress >= 60)
            {
                negFollowChance = (int)(negFollowChance * 1.5);
            }
            if (curFollows >= 1000000)
            {
                negFollowChance /= 4;
            }
            else if (curFollows >= 100000)
            {
                negFollowChance /= 2;
            }
            if (negFollowChance == 0 && isMissStream)
            {
                if (curFollows >= 1000000 && num <= 1)
                {
                    followers = random.Next((-curFollows + 1000), -100000);
                }
                else if (curFollows >= 100000 && num <= 4)
                {
                    tempFollowLoss = random.Next((-curFollows + 1000), -10000);
                    followers = tempFollowLoss < -1000000 ? -1000000 : tempFollowLoss;
                }
                else if (curFollows >= 10000 && num <= 7)
                {
                    tempFollowLoss = random.Next((-curFollows + 1000), -1000);
                    followers = tempFollowLoss < -100000 ? -100000 : tempFollowLoss;
                }
                else if (curFollows >= 1001)
                {
                    tempFollowLoss = random.Next((-curFollows + 100), 0);
                    followers = tempFollowLoss < -10000 ? -10000 : tempFollowLoss;
                }
                if (followers != 0)
                {
                    SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Follower, followers);
                    Initializer.logger.LogInfo($"You lost followers! Followers lost: {followers}.");
                }

            }
        }
        //Randomizes Follower gain from Social Media.
        public static void RandomTweet()
        {
            System.Random random = new System.Random();
            int status = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            int followertweet;
            int num = random.Next(0, 100);
            int num2 = random.Next(0, 11);

            if (!IsActiveOrNotDataZ())
                return;

            if (num > 0)
            {
                if (num2 >= 3 && num2 < 7)
                {
                    followertweet = random.Next(0, 1000);
                    Initializer.logger.LogInfo("Low tweet follower increase.");
                }
                else if (num2 >= 7 && num2 < 10)
                {
                    followertweet = random.Next(0, 5000);
                    Initializer.logger.LogInfo("Medium tweet follower increase.");
                }
                else
                {
                    switch (num2)
                    {
                        case 0:
                            followertweet = random.Next(-(status / 100), 0);
                            if (followertweet != 0)
                                Initializer.logger.LogInfo("Low tweet follower decrease.");
                            break;
                        case 1:
                            followertweet = random.Next(-(status / 50), 0);
                            if (followertweet != 0)
                                Initializer.logger.LogInfo("Medium tweet follower decrease.");
                            break;
                        case 2:
                            followertweet = random.Next(-(status / 2), 0);
                            if (followertweet != 0)
                                Initializer.logger.LogInfo("High tweet follower decrease.");
                            break;
                        default:
                            followertweet = random.Next(0, 10000);
                            Initializer.logger.LogInfo("High tweet follower increase.");
                            break;
                    }

                }
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatus(StatusType.Follower, followertweet);
                if (followertweet > 0)
                    Initializer.logger.LogInfo("Got " + followertweet + " followers through Social Media!");
                else if (followertweet < 0)
                    Initializer.logger.LogInfo("Lost " + followertweet + " followers through Social Media!");
                else Initializer.logger.LogInfo("No followers earned through Social Media.");
            }
            if (num == 0)
            {
                SingletonMonoBehaviour<StatusManager>.Instance.UpdateStatusToNumber(StatusType.Follower, 9999999);
                Initializer.logger.LogInfo("Max tweet follower increase.");
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NetaChipAlpha), "SetData")]
        static void LimitMilestones(bool isUsed, NetaChipAlpha __instance, bool ___isDiscovered)
        {
            int followers = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.Follower);
            if (__instance.type != AlphaType.Angel)
            {
                return;
            }
            if (isUsed)
            {
                return;
            }
            switch (__instance.alphaLevel)
            {
                case 6:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 6))
                    {
                        return;
                    }
                    if (followers >= 1000000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 5:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 5))
                    {
                        return;
                    }
                    if (followers >= 1000000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 4:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 4))
                    {
                        return;
                    }
                    if (followers >= 500000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 3:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 3))
                    {
                        return;
                    }
                    if (followers >= 250000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 2:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 2))
                    {
                        return;
                    }
                    if (followers >= 100000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 1:
                    if (SingletonMonoBehaviour<NetaManager>.Instance.usedAlpha.Exists((AlphaLevel al) => al.alphaType == AlphaType.Angel && al.level == 1))
                    {
                        return;
                    }
                    if (followers >= 10000)
                    {
                        break;
                    }
                    ChangeStreamLabel(false);
                    return;
                case 0:
                    return;
            }
            if (___isDiscovered)
            {
                ChangeStreamLabel(true);
            }

            void ChangeStreamLabel(bool isEnabled)
            {
                __instance._tooltip.isShowTooltip = isEnabled;
                __instance.button.interactable = isEnabled;
            }
        }

        /// <summary>
        /// Applies random stat changes (specific)
        /// </summary>
        /// <param name="param">The parameters to use when applying randomized stat changes.</param>
        public static void ApplyRandomStatus(CmdMaster.Param param)
        {
            int day = SingletonMonoBehaviour<StatusManager>.Instance.GetStatus(StatusType.DayPart);
            bool isMissStream = false;
            if (!IsActiveOrNotDataZ())
                return;
            if (!param.ParentAct.Contains("Haishin")
                && param.PassingTime + day > 2
                && day != 3)
            {
                isMissStream = true;
            }
            if (param.StressDelta > 0 || param.StressDelta < 0 || param.FavorDelta > 0 || param.FavorDelta < 0 || param.YamiDelta > 0 || param.YamiDelta < 0) { RandomStats(isMissStream); };
            if (param.FollowerDelta > 0)
            {
                if (param.ParentAct.Contains("Haishin")) { RandomFollowers(); };
                if (param.ParentAct.Equals("InternetPoketter")) { RandomTweet(); };
            }
        }

        /// <summary>
        /// Applies random stat changes (general)
        /// <br/> Does not apply to Followers.
        /// </summary>
        public static void ApplyRandomStatus()
        {
            if (!IsActiveOrNotDataZ())
                return;
            RandomStats(false);

        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(EventManager), nameof(EventManager.MidokuMushiTweet))]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> Midokumushi_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label ignored = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, ignored)

            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_3 && !startSearching)
                {
                    code[i - 1].labels.Add(normalDelta);
                    code.InsertRange(i - 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].operand as FieldInfo == AccessTools.Field(typeof(EventManager), "midokumushi"))
                    {
                        code[i - 1].labels.Add(ignored);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }


        static IEnumerable<CodeInstruction> Dialog_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label eventContinue = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, eventContinue)

            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_3 && !startSearching)
                {
                    code[i - 1].labels.Add(normalDelta);
                    code.InsertRange(i - 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Pop)
                    {
                        code[i + 1].labels.Add(eventContinue);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(EventManager), "ApplyStatus")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> ApplyStatus_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label done = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{typeof(CmdMaster.Param)})),
                new CodeInstruction(OpCodes.Br, done)

            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Stloc_0 && !startSearching)
                {
                    code[i + 1].labels.Add(normalDelta);
                    code.InsertRange(i + 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Ret)
                    {
                        code[i].labels.Add(done);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }


        [HarmonyTranspiler]
        [HarmonyPatch(typeof(LiveComment), "sakujo")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> sakujo_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label done = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, done)

            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldfld && (FieldInfo)code[i].operand == AccessTools.Field(typeof(Playing), "diffStatusType"))
                {
                    code[i + 2].labels.Add(normalDelta);
                    code.InsertRange(i + 2, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    for (int j = code.Count - 1; j >= 0; j--)
                    {

                        if (code[j].opcode == OpCodes.Ldarg_0)
                        {
                            code[j].labels.Add(done);
                            return code.AsEnumerable();
                        }
                    }
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(TooltipManager), "ShowAction")]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> ShowAction_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label onlyDayPassing = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brtrue, onlyDayPassing)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].operand as MethodInfo == AccessTools.Method(typeof(StatusTooltip2D), "SetCommandDesc", new Type[] { typeof(CmdMaster.Param), typeof(LanguageType) })
                     && !startSearching)
                {
                    code.InsertRange(i + 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    for (int j = code.Count - 1; j >= 0; j--)
                    {
                        if (code[j].operand as MethodInfo == AccessTools.Method(typeof(StatusTooltip2D), "SetBonusLine", new Type[] { typeof(bool), typeof(LanguageType) }))
                        {
                            code[j + 1].labels.Add(onlyDayPassing);
                            return code.AsEnumerable();
                        }
                    }

                }
            }
            return code.AsEnumerable();
        }

        //Applies new starting stat for Mental Darkness
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StatusManager), "NewStatus")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void SetDarknessCurrent(StatusManager __instance)
        {
            if (!IsActiveOrNotDataZ())
                return;
            Status yami = __instance.statuses.Find((Status status) => status.statusType == StatusType.Yami);
            yami.currentValue.Value = 35;
        }


        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Status_Stress2_Day), "startEvent", new Type[] { typeof(CancellationToken) })]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> StressTwo_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label loopStart = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, loopStart)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].operand as MethodInfo == AccessTools.Method(typeof(PostEffectManager), "ResetShaderCalmly", new Type[] { typeof(bool) }) && !startSearching)
                {
                    code[i + 1].labels.Add(normalDelta);
                    code.InsertRange(i + 1, conditions);
                    startSearching = true;
                    i += 9;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Br_S || code[i].opcode == OpCodes.Br)
                    {
                        code[i - 4].labels.Add(loopStart);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }


        static IEnumerable<CodeInstruction> EventSea_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label startTweet = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, startTweet)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].operand as MethodInfo == AccessTools.Method(typeof(StatusManager), "TimeDelta", new Type[] { typeof(int) }) && !startSearching)
                {
                    code[i + 1].labels.Add(normalDelta);
                    code.InsertRange(i + 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Ldc_I4 && (code[i].operand as int?) == 200)
                    {
                        code[i - 1].labels.Add(startTweet);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }


        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Event_Okiru_Afternoon), "startEvent", new Type[] { typeof(CancellationToken) })]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> EventSunset_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label startJine = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, startJine)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_3 && !startSearching)
                {
                    code[i - 1].labels.Add(normalDelta);
                    code.InsertRange(i - 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Ldc_I4 && (code[i].operand as int?) == 1316)
                    {
                        code[i - 1].labels.Add(startJine);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Event_Okiru_Night), "startEvent", new Type[] { typeof(CancellationToken) })]
        [HarmonyPatch(MethodType.Enumerator)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        static IEnumerable<CodeInstruction> EventNight_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Label normalDelta = generator.DefineLabel();
            Label startJine = generator.DefineLabel();
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            List<CodeInstruction> conditions = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(IsActiveOrNotDataZ))),
                new CodeInstruction(OpCodes.Brfalse, normalDelta),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StatRandomizing),nameof(ApplyRandomStatus), new Type[]{})),
                new CodeInstruction(OpCodes.Br, startJine)
            };
            bool startSearching = false;
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Ldc_I4_3 && !startSearching)
                {
                    code[i - 1].labels.Add(normalDelta);
                    code.InsertRange(i - 1, conditions);
                    startSearching = true;
                    continue;
                }
                if (startSearching)
                {
                    if (code[i].opcode == OpCodes.Ldc_I4 && (code[i].operand as int?) == 1316)
                    {
                        code[i - 1].labels.Add(startJine);
                        break;
                    }
                }
            }
            return code.AsEnumerable();
        }
    }
}

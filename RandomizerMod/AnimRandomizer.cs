﻿using HarmonyLib;
using NGO;
using ngov3;

namespace Randomizer
{
    [HarmonyPatch]
    public class AnimRandomizer
    {
        static string[] angelList =
        {
                "stream_cho_100k_silver",
                "stream_cho_akaruku",
                "stream_cho_akaruku_fever",
                "stream_cho_akaruku_superchat",
                "stream_cho_akaruku_win_stop",
                "stream_cho_angel",
                "stream_cho_anguri",
                "stream_cho_anken_business1",
                "stream_cho_anken_business2",
                "stream_cho_anken_business3",
                "stream_cho_anken_business4",
                "stream_cho_anken_business5",
                "stream_cho_anken_business6",
                "stream_cho_anken_business7",
                "stream_cho_anken_business8",
                "stream_cho_anken_business9",
                "stream_cho_anken_business10",
                "stream_cho_anken_figure1",
                "stream_cho_anken_figure2",
                "stream_cho_anken_figure3",
                "stream_cho_anken_figure4",
                "stream_cho_anken_figure5",
                "stream_cho_anken_figure6",
                "stream_cho_anken_figure7",
                "stream_cho_anken_game1",
                  "stream_cho_anken_game2",
                  "stream_cho_anken_game3",
                  "stream_cho_anken_game4",
                  "stream_cho_anken_game5",
                  "stream_cho_anken_game6",
                  "stream_cho_anken_game7",
                  "stream_cho_anken_game8",
                  "stream_cho_anken_game9",
                "stream_cho_anken_juice1",
                "stream_cho_anken_juice2",
                "stream_cho_anken_juice3",
                "stream_cho_anken_juice4",
                "stream_cho_anken_juice5",
                "stream_cho_anken_juice6",
                "stream_cho_anken_juice7",
                "stream_cho_anken_juice8",
                "stream_cho_anken_make1",
                "stream_cho_anken_make2",
                "stream_cho_anken_make3",
                "stream_cho_anken_make4",
                "stream_cho_anken_make5",
                "stream_cho_anken_make6",
                "stream_cho_anken_make7",
                "stream_cho_anken_make8",
                "stream_cho_anken_make9",
                "stream_cho_asmr1",
                "stream_cho_asmr2",
                "stream_cho_asmr3",
                "stream_cho_asmr4",
                "stream_cho_asmr5",
                "stream_cho_asmr6",
                "stream_cho_asmr7",
                "stream_cho_asmr7_end",
                "stream_cho_asmr8",
                "stream_cho_b_angle",
                "stream_cho_b_craziness1",
                "stream_cho_b_craziness2",
                "stream_cho_b_idle",
                "stream_cho_b_idle2_1",
                "stream_cho_b_idle2_2",
                "stream_cho_b_lower1",
                "stream_cho_b_lower2",
                "stream_cho_b_neckcut",
                "stream_cho_b_wristcut1_1",
                "stream_cho_b_wristcut1_2",
                "stream_cho_b_wristcut1_3",
                "stream_cho_bad",
                "stream_cho_balanceball",
                "stream_cho_balanceball_asmr",
                "stream_cho_balanceball_smile",
                "stream_cho_balanceball_start",
                 "stream_cho_chance_blackout",
                "stream_cho_chance_blackout_back",
                "stream_cho_chance_porori_idle",
                "stream_cho_chance_porori_win",
                "stream_cho_chance_porori_win_stop",
                "stream_cho_chance_porori_win2",
                "stream_cho_chance_porori_win3",
                "stream_cho_craziness1",
                "stream_cho_craziness2",
                "stream_cho_craziness3",
                "stream_cho_craziness4",
                "stream_cho_crying_angry",
                "stream_cho_dokuzetsu",
                "stream_cho_dokuzetsu_fever",
                "stream_cho_dokuzetsu_superchat",
                "stream_cho_dokuzetsu_win_stop",
                "stream_cho_eeto",
                   "stream_cho_fan1",
                   "stream_cho_fan2",
                   "stream_cho_fan3",
                   "stream_cho_fan4",
                   "stream_cho_fan5",
                   "stream_cho_fan6",
                "stream_cho_fanservice",
                "stream_cho_game",
                   "stream_cho_game_ape1",
                   "stream_cho_game_ape2",
                   "stream_cho_game_ape3",
                   "stream_cho_game_ape4",
                   "stream_cho_game_ape5",
                "stream_cho_game_fever",
                  "stream_cho_game_sayo1",
                  "stream_cho_game_sayo2",
                  "stream_cho_game_sayo3",
                  "stream_cho_game_sayo4",
                  "stream_cho_game_sayo5",
                "stream_cho_game_superchat",
                   "stream_cho_game_twilight1",
                   "stream_cho_game_twilight2",
                   "stream_cho_game_twilight3",
                   "stream_cho_game_twilight4",
                   "stream_cho_game_twilight5",
                   "stream_cho_game_twilight6",
                   "stream_cho_game_twilight7",
                   "stream_cho_game_twilight8",
                "stream_cho_game_win_stop",
                "stream_cho_gaoo",
                "stream_cho_grgr",
                "stream_cho_grgr2",
                "stream_cho_grgr3",
                "stream_cho_grgr4",
                "stream_cho_h_fever",
                "stream_cho_h_superchat",
                "stream_cho_h_win_stop",
                "stream_cho_hera",
                "stream_cho_hera_fever",
                "stream_cho_hera_superchat",
                "stream_cho_hera_win_stop",
                "stream_cho_hera2",
                "stream_cho_hera3",
                "stream_cho_hitotabi1",
                "stream_cho_hitotabi2",
                "stream_cho_horror_anger",
                "stream_cho_horror_bokoboko",
                "stream_cho_horror_comment",
                "stream_cho_horror_eeto",
                "stream_cho_horror_glare",
                "stream_cho_horror_idle",
                "stream_cho_horror_kashikoma",
                "stream_cho_horror_laugh",
                "stream_cho_horror_lower",
                "stream_cho_horror_omae",
                "stream_cho_horror_smile",
                "stream_cho_horror_teach",
                "stream_cho_ice1",
                "stream_cho_ice2",
                   "stream_cho_ide_invoke",
                "stream_cho_kakkoyoku",
                "stream_cho_kakkoyoku_fever",
                "stream_cho_kakkoyoku_superchat",
                "stream_cho_kakkoyoku_win_stop",
                "stream_cho_kashikoma",
                "stream_cho_kashikoma_otaku",
                "stream_cho_kawaiku",
                "stream_cho_kawaiku_fever",
                "stream_cho_kawaiku_superchat",
                "stream_cho_kawaiku_win_stop",
                "stream_cho_kobiru",
                "stream_cho_kobiru_fever",
                "stream_cho_kobiru_microphone",
                "stream_cho_kobiru_superchat",
                "stream_cho_kobiru_superchat_microphone",
                "stream_cho_kobiru_win_stop",
                "stream_cho_kyouso1",
                "stream_cho_kyouso2",
                "stream_cho_kyouso3",
                "stream_cho_kyouso4",
                "stream_cho_kyouso5",
                   "stream_cho_ntr",
                   "stream_cho_ntr1",
                   "stream_cho_ntr2",
                   "stream_cho_ntr3",
                   "stream_cho_ntr4",
                   "stream_cho_ntr5",
                "stream_cho_nyo",
                "stream_cho_otaku",
                "stream_cho_otaku_fever",
                "stream_cho_otaku_superchat",
                "stream_cho_otaku_win_stop",
                "stream_cho_otaku2",
                "stream_cho_peace",
                "stream_cho_pointing",
                "stream_cho_pointing_microphone",
                "stream_cho_pointing_otaku",
                "stream_cho_pointing_porori",
                "stream_cho_pointing2",
                "stream_cho_porori_pokan",
                "stream_cho_pout",
                "stream_cho_pray",
                   "stream_cho_reaction_ape",
                "stream_cho_reaction_asmr1",
                "stream_cho_reaction_asmr2",
                   "stream_cho_reaction_game",
                "stream_cho_reaction_grgr",
                "stream_cho_reaction_juice",
                "stream_cho_reaction_make",
                "stream_cho_reaction_otaku",
                "stream_cho_reaction_porori",
                   "stream_cho_reaction_twilight",
                   "stream_cho_reaction_yukkuri1",
                "stream_cho_reaction1",
                "stream_cho_reaction2",
                "stream_cho_richter",
                   "stream_cho_sayonara",
                   "stream_cho_sayonara1",
                   "stream_cho_sayonara2",
                   "stream_cho_sayonara3",
                   "stream_cho_sayonaraend",
                "stream_cho_shobon",
                "stream_cho_shobon_smile",
                "stream_cho_sleepy1",
                "stream_cho_sleepy2",
                "stream_cho_sleepy3",
                   "stream_cho_slide_record",
                   "stream_cho_slide1",
                   "stream_cho_slide2",
                "stream_cho_sorrow_smile",
                "stream_cho_su",
                "stream_cho_su_fever",
                "stream_cho_su_smile",
                "stream_cho_su_superchat",
                "stream_cho_su_win_stop",
                "stream_cho_teach",
                "stream_cho_teach2",
                "stream_cho_teach3",
                "stream_cho_teitter_inbouron",
                "stream_cho_teitter_inbouron2",
                "stream_cho_teitter_inbouron3",
                "stream_cho_teitter_zatsudan",
                "stream_cho_tweet1",
                "stream_cho_tweet2",
                "stream_cho_unrest",
                "stream_cho_vomiting1",
                "stream_cho_vomiting2",
                   "stream_cho_yukkuri_anger",
                   "stream_cho_yukkuri_bye",
                   "stream_cho_yukkuri_cry",
                   "stream_cho_yukkuri_idle",
                   "stream_cho_yukkuri_smile",
                   "stream_cho_yukkuri_teach",
                   "stream_cho_yukkuri_wink"

        };

        static string[] ameList =
{
                "stream_ame_comic",
                "stream_ame_craziness",
                "stream_ame_drag",
                "stream_ame_drag_a",
                "stream_ame_drag_b",
                "stream_ame_drag_c",
                "stream_ame_drag_d",
                "stream_ame_drag_e",
                "stream_ame_drag_f",
                "stream_ame_drag_g",
                "stream_ame_egosearching",
                "stream_ame_egosearching_ura",
                "stream_ame_game_a",
                "stream_ame_game_b",
                "stream_ame_game_c",
                "stream_ame_game_d",
                "stream_ame_game_e",
                "stream_ame_game_f",
                "stream_ame_game_g",
                "stream_ame_h_heart",
                "stream_ame_hair_drill",
                "stream_ame_hair_long",
                "stream_ame_hair_poney",
                "stream_ame_hair_side",
                "stream_ame_handspinner1",
                "stream_ame_handspinner2",
                "stream_ame_headphone",
                "stream_ame_henoji",
                "stream_ame_henoji_dark",
                "stream_ame_idle",
                "stream_ame_idle_anxiety_a",
                "stream_ame_idle_anxiety_b",
                "stream_ame_idle_anxiety_c",
                "stream_ame_idle_anxiety_d",
                "stream_ame_idle_anxiety_e",
                "stream_ame_idle_anxiety_f",
                "stream_ame_idle_anxiety_g",
                "stream_ame_idle_happy_a",
                "stream_ame_idle_happy_b",
                "stream_ame_idle_happy_c",
                "stream_ame_idle_happy_d",
                "stream_ame_idle_happy_e",
                "stream_ame_idle_happy_f",
                "stream_ame_idle_happy_g",
                "stream_ame_idle_iraira_a",
                "stream_ame_idle_iraira_b",
                "stream_ame_idle_iraira_c",
                "stream_ame_idle_iraira_d",
                "stream_ame_idle_iraira_e",
                "stream_ame_idle_iraira_f",
                "stream_ame_idle_iraira_g",
                "stream_ame_idle_normal_a",
                "stream_ame_idle_normal_b",
                "stream_ame_idle_normal_c",
                "stream_ame_idle_normal_d",
                "stream_ame_idle_normal_e",
                "stream_ame_idle_normal_f",
                "stream_ame_idle_normal_g",
                "stream_ame_movie",
                "stream_ame_movie_a",
                "stream_ame_movie_b",
                "stream_ame_movie_c",
                "stream_ame_movie_d",
                "stream_ame_movie_e",
                "stream_ame_movie_f",
                "stream_ame_movie_g",
                "stream_ame_nail",
                "stream_ame_negative_a",
                "stream_ame_negative_b",
                "stream_ame_negative_c",
                "stream_ame_negative_d",
                "stream_ame_negative_e",
                "stream_ame_negative_f",
                "stream_ame_negative_g",
                "stream_ame_out_a",
                "stream_ame_out_b",
                "stream_ame_out_c",
                "stream_ame_out_d",
                "stream_ame_out_e",
                "stream_ame_out_f",
                "stream_ame_out_g",
                "stream_ame_positive_a",
                "stream_ame_positive_b",
                "stream_ame_positive_c",
                "stream_ame_positive_d",
                "stream_ame_positive_e",
                "stream_ame_positive_f",
                "stream_ame_positive_g",
                "stream_ame_selfie",
                "stream_ame_sleep",
                "stream_ame_smile",
                "stream_ame_talk_a",
                "stream_ame_talk_b",
                "stream_ame_talk_c",
                "stream_ame_talk_d",
                "stream_ame_talk_e",
                "stream_ame_talk_f",
                "stream_ame_talk_g",
                "stream_ame_tv",
                "stream_ame_voice_training",
                "stream_ame_vomiting1",
                "stream_ame_vomiting2",
                "stream_ame_yanda_carisma",
                "stream_ame_yanderu",
                "stream_ame_yanderu_random",

        };

        public static bool IsActiveOrNotDataZ()
        {
            if (!SettingsReader.currentSettings.RandomizeAnimations)
                return false;
            return MiscPatches.CheckIfSceneOrSaveValid();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(TenchanView), "PlayAnim")]
        public static void RandomizeStreamAnim(ref string name)
        {

            if (!IsActiveOrNotDataZ()) return;
            if (SingletonMonoBehaviour<Live>.Instance.gameObject.name.Contains("dark")) return;
            if (SettingsReader.currentSettings.IncludeAmeAndKAngel)
            {
                name = GetRandomAnim(true);
            }
            else if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding == EndingType.Ending_Yami)
            {
                name = GetRandomAnim(false, false);
            }
            else
            {
                name = GetRandomAnim(false, true);
            }


        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(App_Webcam), "PlayAnim")]
        public static void RandomizeWebcamAnim(ref string name)
        {
            if (!IsActiveOrNotDataZ()) return;
            if (SettingsReader.currentSettings.IncludeAmeAndKAngel)
            {
                name = GetRandomAnim(true);
            }
            else
            {
                name = GetRandomAnim(false, false);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Live), "bgView")]

        public static void CorrectBgView(Live __instance)
        {
            if (!IsActiveOrNotDataZ()) return;
            if (SingletonMonoBehaviour<EventManager>.Instance.nowEnding == EndingType.Ending_Ntr)
            {
                __instance.Tenchan._backGround.sprite = __instance.Tenchan.background_no_shield;
            }
        }

        public static string GetRandomAnim(bool combined, bool isKAngel = true)
        {
            int rngNum;
            if (combined)
            {
                rngNum = UnityEngine.Random.Range(0, 2);
                if (rngNum == 0)
                {
                    rngNum = UnityEngine.Random.Range(0, angelList.Length);
                    return angelList[rngNum];
                }
                else
                {
                    rngNum = UnityEngine.Random.Range(0, ameList.Length);
                    return ameList[rngNum];
                }
            }
            else if (isKAngel)
            {
                rngNum = UnityEngine.Random.Range(0, angelList.Length);
                return angelList[rngNum];
            }
            rngNum = UnityEngine.Random.Range(0, ameList.Length);
            return ameList[rngNum];
        }
    }
}
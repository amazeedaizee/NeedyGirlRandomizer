using Newtonsoft.Json;
using Randomizer;
using System.IO;
using System.Reflection;

namespace RandomSettings
{

    public static class SettingsEditor
    {
        public static MainForm MainForm;
        public static RandomizerOptions currentSettings = new RandomizerOptions();

        public static void SetMainForm(MainForm form)
        {
            MainForm = form;
        }
        public static void LoadSettings()
        {

            RandomizerOptions readSettings;
            string settingsFile;
            string settingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.json");
            try
            {
                if (File.Exists(settingsPath))
                {
                    settingsFile = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.json"));
                    readSettings = JsonConvert.DeserializeObject<RandomizerOptions>(settingsFile);
                    currentSettings = readSettings;
                }
                else
                {
                    CreateSettings();
                }
            }
            catch { CreateSettings(); }

            void CreateSettings()
            {
                readSettings = new RandomizerOptions();
                settingsFile = JsonConvert.SerializeObject(readSettings, Formatting.Indented);
                File.WriteAllText(settingsPath, settingsFile);
            }
        }

        public static void SaveSettings()
        {
            string settingsFile = JsonConvert.SerializeObject(currentSettings, Formatting.Indented);
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.json"), settingsFile);
        }

        public static void SetNewSettings()
        {
            currentSettings.RandomizeStats = MainForm.statPanel.checkBox.Checked != true ? false : true;
            currentSettings.IncludeLoseFollowChance = MainForm.loseFollowerPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeJineText = MainForm.jinePanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeSearchStText = MainForm.searchAnonPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeTweets = MainForm.tweetPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeTweetReplies = MainForm.replyPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeStreamText = MainForm.streTxtPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeNotepadText = MainForm.notePanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeEndMsgText = MainForm.endMsgPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeSpecialBorders = MainForm.speBorderPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeDayBorders = MainForm.dayBorderPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeEffects = MainForm.effectPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeMusic = MainForm.bgmPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeSoundFX = MainForm.sfxPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeDays = MainForm.dayPanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeStreams = MainForm.strePanel.checkBox.Checked != true ? false : true;
            currentSettings.IncludeSpecialStreams = MainForm.spePanel.checkBox.Checked != true ? false : true;
            currentSettings.RandomizeAnimations = MainForm.animPanel.checkBox.Checked != true ? false : true;
            currentSettings.IncludeAmeAndKAngel = MainForm.kAmePanel.checkBox.Checked != true ? false : true;

        }
    }
}

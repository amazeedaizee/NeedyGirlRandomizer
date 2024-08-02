

using Newtonsoft.Json;
using System.IO;

namespace Randomizer
{
    public class RandomizerOptions
    {
        public bool RandomizeStats = true;
        public bool IncludeLoseFollowChance = true;
        public bool RandomizeJineText = false;
        public bool RandomizeSearchStText = false;
        public bool RandomizeTweets = false;
        public bool RandomizeTweetReplies = false;
        public bool RandomizeStreamText = false;
        public bool RandomizeNotepadText = false;
        public bool RandomizeEndMsgText = false;
        public bool RandomizeSpecialBorders = false;
        public bool RandomizeDayBorders = false;
        public bool RandomizeEffects = false;
        public bool RandomizeMusic = false;
        public bool RandomizeSoundFX = false;
        public bool RandomizeDays = false;
        public bool RandomizeStreams = false;
        public bool IncludeSpecialStreams = false;
        public bool RandomizeAnimations = false;
        public bool IncludeAmeAndKAngel = false;

        public RandomizerOptions() { }

    }

    public static class SettingsReader
    {
        public static RandomizerOptions currentSettings = new RandomizerOptions();
        public static void LoadSettings()
        {

            RandomizerOptions readSettings;
            string settingsFile;
            string path = Path.Combine(Path.GetDirectoryName(Initializer.PInfo.Location), "settings.json");
            try
            {
                if (File.Exists(path))
                {
                    settingsFile = File.ReadAllText(path);
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
                File.WriteAllText(path, settingsFile);
            }
        }
    }
}

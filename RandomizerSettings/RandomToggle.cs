using Eto.Drawing;
using Eto.Forms;
using System;

namespace RandomSettings
{
    public partial class RandomToggle : Panel
    {
        const string titleId = "Title";
        const string descId = "Description";

        public CheckBox checkBox = new CheckBox();
        public string desc = "";
        public LinkButton? linkButton = null;

        public RandomToggle(RandomType type)
        {
            SetCheck(type);
            checkBox.Text = ResourceGetter.GetString(titleId, type);
            desc = ResourceGetter.GetString(descId, type);

            Content = new StackLayout
            {
                Spacing = 5,
                Padding = 10,
                Items =
                {
                    checkBox,
                    new Label() {Text = this.desc, TextColor = Color.FromArgb(128,128,128)}
                }
            };

            checkBox.CheckedChanged += (object? sender, EventArgs e) =>
            {
                SettingsEditor.SetNewSettings();
                SettingsEditor.SaveSettings();
            };
        }
        public RandomToggle(RandomType type, LinkButton link)
        {
            SetCheck(type);
            checkBox.Text = ResourceGetter.GetString(titleId, type);
            desc = ResourceGetter.GetString(descId, type);
            if (link == null)
            {
                throw new ArgumentNullException();
            }

            Content = new StackLayout
            {
                Spacing = 5,
                Padding = 10,
                Items =
                {
                    checkBox,
                    new Label() {Text = this.desc, TextColor = Color.FromArgb(128,128,128)},
                    link
                }
            };
        }

        public void SetCheck(RandomType type)
        {
            switch (type)
            {
                case RandomType.Stats:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeStats;
                    break;
                case RandomType.LoseFollowChance:
                    checkBox.Checked = SettingsEditor.currentSettings.IncludeLoseFollowChance;
                    break;
                case RandomType.JineText:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeJineText;
                    break;
                case RandomType.SearchStText:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeSearchStText;
                    break;
                case RandomType.Tweets:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeTweets;
                    break;
                case RandomType.TweetReplies:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeTweetReplies;
                    break;
                case RandomType.StreamText:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeStreamText;
                    break;
                case RandomType.NotepadText:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeNotepadText;
                    break;
                case RandomType.EndMsgText:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeEndMsgText;
                    break;
                case RandomType.SpeBorders:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeSpecialBorders;
                    break;
                case RandomType.DayBorders:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeDayBorders;
                    break;
                case RandomType.Effects:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeEffects;
                    break;
                case RandomType.Music:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeMusic;
                    break;
                case RandomType.SoundFX:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeSoundFX;
                    break;
                case RandomType.Days:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeDays;
                    break;
                case RandomType.Streams:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeStreams;
                    break;
                case RandomType.IncludeSpecial:
                    checkBox.Checked = SettingsEditor.currentSettings.IncludeSpecialStreams;
                    break;
                case RandomType.Animations:
                    checkBox.Checked = SettingsEditor.currentSettings.RandomizeAnimations;
                    break;
                case RandomType.IncludeBoth:
                    checkBox.Checked = SettingsEditor.currentSettings.IncludeAmeAndKAngel;
                    break;
            }
        }

    }
}

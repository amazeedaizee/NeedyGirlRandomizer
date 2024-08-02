using Eto.Drawing;
using Eto.Forms;
using System;
using System.IO;

namespace RandomSettings
{
    public partial class MainForm : Form
    {
        internal EditTextListWindow? notepadWindow;
        internal EditTextListWindow? endMsgsWindow;

        internal RandomToggle statPanel;
        internal RandomToggle loseFollowerPanel;
        internal RandomToggle jinePanel;
        internal RandomToggle searchAnonPanel;
        internal RandomToggle tweetPanel;
        internal RandomToggle replyPanel;
        internal RandomToggle streTxtPanel;
        internal RandomToggle notePanel;
        internal RandomToggle endMsgPanel;
        internal RandomToggle speBorderPanel;
        internal RandomToggle dayBorderPanel;
        internal RandomToggle effectPanel;
        internal RandomToggle bgmPanel;
        internal RandomToggle sfxPanel;
        internal RandomToggle dayPanel;
        internal RandomToggle strePanel;
        internal RandomToggle spePanel;
        internal RandomToggle animPanel;
        internal RandomToggle kAmePanel;

        internal LinkButton editNotepadLabel;
        internal LinkButton editEndMsgsLabel;

        static string notepadPath = Path.Combine(SettingsEditor.currentPath, "notepadTexts.json");
        static string endMsgsPath = Path.Combine(SettingsEditor.currentPath, "endMsgsTexts.json");
        public MainForm()
        {
            editNotepadLabel = new LinkButton();
            editNotepadLabel.Text = "Edit \"notepadTexts.json\"";
            editNotepadLabel.Click += (sender, e) =>
            {
                if (notepadWindow != null)
                {
                    notepadWindow.Focus();
                }
                else
                {
                    notepadWindow = new EditTextListWindow(this, notepadPath);
                    notepadWindow.Show();
                }

            };

            editEndMsgsLabel = new LinkButton();
            editEndMsgsLabel.Text = "Edit \"endMsgsTexts.json\"";
            editEndMsgsLabel.Click += (sender, e) =>
            {
                if (endMsgsWindow != null)
                {
                    endMsgsWindow.Focus();
                }
                else
                {
                    endMsgsWindow = new EditTextListWindow(this, endMsgsPath);
                    endMsgsWindow.Show();
                }

            };

            statPanel = new RandomToggle(RandomType.Stats);
            jinePanel = new RandomToggle(RandomType.JineText);
            loseFollowerPanel = new RandomToggle(RandomType.LoseFollowChance);
            searchAnonPanel = new RandomToggle(RandomType.SearchStText);
            tweetPanel = new RandomToggle(RandomType.Tweets);
            replyPanel = new RandomToggle(RandomType.TweetReplies);
            streTxtPanel = new RandomToggle(RandomType.StreamText);
            notePanel = new RandomToggle(RandomType.NotepadText, editNotepadLabel);
            endMsgPanel = new RandomToggle(RandomType.EndMsgText, editEndMsgsLabel);
            speBorderPanel = new RandomToggle(RandomType.SpeBorders);
            dayBorderPanel = new RandomToggle(RandomType.DayBorders);
            effectPanel = new RandomToggle(RandomType.Effects);
            bgmPanel = new RandomToggle(RandomType.Music);
            sfxPanel = new RandomToggle(RandomType.SoundFX);
            dayPanel = new RandomToggle(RandomType.Days);
            strePanel = new RandomToggle(RandomType.Streams);
            spePanel = new RandomToggle(RandomType.IncludeSpecial);
            animPanel = new RandomToggle(RandomType.Animations);
            kAmePanel = new RandomToggle(RandomType.IncludeBoth);
            Title = "Randomizer Settings";
            Size = new Size(800, 535);
            MinimumSize = new Size(800, 535);



            Content = new Scrollable
            {
                Content = new StackLayout
                {
                    Padding = 5,
                    Spacing = -10,
                    Items =
                {

                    statPanel,
                    loseFollowerPanel,
                    jinePanel,
                    searchAnonPanel,
                    tweetPanel,
                    replyPanel,
                    streTxtPanel,
                    notePanel,
                    editNotepadLabel,
                    endMsgPanel,
                    speBorderPanel,
                    dayBorderPanel,
                    effectPanel,
                    bgmPanel,
                    sfxPanel,
                    dayPanel,
                    strePanel,
                    spePanel,
                    animPanel,
                    kAmePanel
            // add more controls here
        }

                }
            };

            Closed += (object? sender, EventArgs e) =>
            {
                SettingsEditor.SetNewSettings();
                SettingsEditor.SaveSettings();
            };

            SettingsEditor.SetMainForm(this);
        }
    }

}

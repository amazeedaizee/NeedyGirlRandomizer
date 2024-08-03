using Eto.Drawing;
using Eto.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace RandomSettings
{
    public class EventTextList
    {
        public List<string>? textList = new List<string>();
    }
    public partial class EditTextListWindow : Form
    {
        string filePath;
        public MainForm mainForm;
        public EventTextList? currentTextList;
        public ListBox listOfTexts;
        public Button addToList;
        public TextArea selectedText;
        int pastSelectedIndex = -1;

        bool isDeleting;
        public EditTextListWindow(MainForm mainForm, string filePath)
        {
            this.mainForm = mainForm;
            listOfTexts = new ListBox();
            listOfTexts.Size = new Size(150, 340);
            listOfTexts.BackgroundColor = Color.FromArgb(235, 235, 235);
            addToList = new Button();
            addToList.Size = new Size(150, 30);
            addToList.Text = "Add Item";
            addToList.Click += AddNewTextItem;
            selectedText = new TextArea();
            selectedText.Size = new Size(550, 380);

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            this.filePath = filePath;

            MouseDown += (sender, e) => { e.Handled = true; };
            Title = $"Editing \"{Path.GetFileName(filePath)}\"";
            MinimumSize = new Size(600, 400);
            Icon = mainForm.Icon;

            Load += FillDataOnLoad;
            KeyDown += DeleteTextItem;
            Closed += (object? sender, EventArgs e) =>
            {
                SaveSelectedText(false, false);
                mainForm.Focus();
                if (mainForm.notepadWindow == this)
                {
                    mainForm.notepadWindow = null;
                }
                else if (mainForm.endMsgsWindow == this)
                {
                    mainForm.endMsgsWindow = null;
                }
                Dispose();
            };
            listOfTexts.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                SaveSelectedText(pastSelectedIndex != listOfTexts.SelectedIndex, true); FillTextBoxWithSelText();
            };

            Content = new StackLayout
            {

                Orientation = Orientation.Horizontal,
                Padding = 10,
                Spacing = 10,
                Items =
                {
                    new StackLayout
                    {
                         Spacing = 10,
                         Items =
                        {
                            listOfTexts,
                            addToList

                        }
                    },
                    selectedText
					// add more controls here
				}
            };
        }

        public void FillDataOnLoad(object? sender, EventArgs e)
        {
            LoadTexts();
            if (currentTextList == null || currentTextList.textList == null)
                return;
            listOfTexts.Items.Clear();
            for (int i = 0; i < currentTextList.textList.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(currentTextList.textList[i]))
                {
                    currentTextList.textList[i] = "...";
                }
                listOfTexts.Items.Add(currentTextList.textList[i].Replace('\n', ' ').Replace('\r', ' '));
            }
        }

        void AddNewTextItem(object? sender, EventArgs e)
        {

            var text = selectedText.Text;
            if (pastSelectedIndex != -1)
            {
                listOfTexts.Items.RemoveAt(pastSelectedIndex);
                listOfTexts.Items.Insert(pastSelectedIndex, new ListItem { Text = text.Replace('\n', ' ').Replace('\r', ' ') });
                currentTextList.textList[pastSelectedIndex] = text;
            }
            listOfTexts.Items.Add("...");
            currentTextList.textList.Add("...");
            SaveTexts();
        }

        void DeleteTextItem(object? sender, KeyEventArgs e)
        {
            int selectIndex;
            if (!(e.Key == Keys.Delete || e.Key == Keys.Backspace)) return;
            if (listOfTexts.SelectedIndex == -1) return;
            isDeleting = true;
            selectIndex = listOfTexts.SelectedIndex;
            listOfTexts.Items.RemoveAt(selectIndex);
            currentTextList.textList.RemoveAt(selectIndex);
            listOfTexts.SelectedIndex = -1;
            selectedText.Text = "";
            SaveTexts();
            isDeleting = false;
        }

        void FillTextBoxWithSelText()
        {
            if (listOfTexts.SelectedIndex != -1)
            {
                selectedText.Text = currentTextList.textList[listOfTexts.SelectedIndex];
                pastSelectedIndex = listOfTexts.SelectedIndex;
            }
        }
        void SaveSelectedText(bool usePastIndex, bool isUpdate)
        {
            if (isDeleting) return;
            if (listOfTexts.SelectedIndex == -1) return;
            if (string.IsNullOrWhiteSpace(selectedText.Text)) return;
            if (pastSelectedIndex != -1 && usePastIndex)
            {
                if (isUpdate)
                {
                    //listOfTexts.Items[pastSelectedIndex].Text = selectedText.Text.Replace('\n', ' ').Replace('\r', ' ');
                    listOfTexts.Items.Insert(pastSelectedIndex, new ListItem { Text = selectedText.Text.Replace('\n', ' ').Replace('\r', ' ') });
                    listOfTexts.Items.RemoveAt(pastSelectedIndex + 1);
                }
                currentTextList.textList[pastSelectedIndex] = selectedText.Text;
            }
            else
            {
                if (isUpdate)
                {
                    //listOfTexts.Items[listOfTexts.SelectedIndex].Text = selectedText.Text.Replace('\n', ' ').Replace('\r', ' ');
                    listOfTexts.Items.Insert(listOfTexts.SelectedIndex, new ListItem { Text = selectedText.Text.Replace('\n', ' ').Replace('\r', ' ') });
                    listOfTexts.Items.RemoveAt(listOfTexts.SelectedIndex + 1);

                }
                currentTextList.textList[listOfTexts.SelectedIndex] = selectedText.Text;
            }
            SaveTexts();
        }
        void LoadTexts()
        {
            EventTextList eventTexts;
            string eventTextFile;
            try
            {
                if (File.Exists(filePath))
                {
                    eventTextFile = File.ReadAllText(filePath);
                    eventTexts = JsonConvert.DeserializeObject<EventTextList>(eventTextFile);
                    currentTextList = new EventTextList();
                    currentTextList.textList.Clear();
                    for (int i = 0; i < eventTexts.textList.Count; i++)
                    {
                        currentTextList.textList.Add(eventTexts.textList[i]);
                    }
                }
                else
                {
                    CreateTexts();
                }
            }
            catch { CreateTexts(); }

            void CreateTexts()
            {
                eventTexts = new EventTextList();
                eventTexts.textList = new List<string>()
                            {
                                "This",
                                "Will",
                                "Appear",
                                "In",
                                "Diaries",
                                "And",
                                "Logs"
                            };
                eventTextFile = JsonConvert.SerializeObject(eventTexts, Formatting.Indented);
                File.WriteAllText(filePath, eventTextFile);
                currentTextList = eventTexts;
            }
        }

        void SaveTexts()
        {
            string eventTextFile = JsonConvert.SerializeObject(currentTextList, Formatting.Indented);
            File.WriteAllText(filePath, eventTextFile);
        }
    }
}

using Eto.Forms;
using System;

namespace RandomSettings
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SettingsEditor.LoadSettings();
            new Application(Eto.Platform.Detect).Run(new MainForm());
        }

    }
}

using Eto.Forms;
using System;

namespace RandomSettings
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Console.WriteLine("I am hooked!");
            // System.Diagnostics.Debug.WriteLine("I am debugged!");
            SettingsEditor.SetAppDirectory();
            SettingsEditor.LoadSettings();
            new Application(Eto.Platform.Detect).Run(new MainForm());
        }



    }
}

using Eto.Forms;
using System;

namespace RandomSettings
{
    internal class Program
    {
        internal static Application? app = null;
        [STAThread]
        static void Main(string[] args)
        {
            // Console.WriteLine("I am hooked!");
            // System.Diagnostics.Debug.WriteLine("I am debugged!");
            SettingsEditor.SetAppDirectory();
            SettingsEditor.LoadSettings();
            app = new Application(Eto.Platform.Detect);
            app.Run(new MainForm());
        }



    }
}

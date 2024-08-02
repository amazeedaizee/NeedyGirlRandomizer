using Eto.Forms;
using System;
using System.IO;
using System.Reflection;

namespace RandomSettings
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Current Assembly Location: " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine("Current Directory Location: " + Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            SettingsEditor.LoadSettings();
            new Application(Eto.Platform.Detect).Run(new MainForm());
        }

    }
}

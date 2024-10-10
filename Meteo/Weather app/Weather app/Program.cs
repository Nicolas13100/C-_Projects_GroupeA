namespace Weather_app;

using System;
using System.Windows.Forms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Initializes application settings like DPI awareness
        ApplicationConfiguration.Initialize();
        
        // Run the main form
        Application.Run(new Form1());
    }
}

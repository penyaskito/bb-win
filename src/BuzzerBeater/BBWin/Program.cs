using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BB;

namespace BBWin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            API API = null;

            LoginForm login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }

            API = login.API;
            Application.Run(new MainForm() { API = API });
        }
    }
}

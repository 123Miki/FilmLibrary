using System;
using System.IO;
using System.Windows;

namespace FilmLibrary.Core
{
    public static class Logger
    {
        public static void LogMessage(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(DataFilePosition.Log, true))
            {
                streamWriter.WriteLine("[" + DateTime.Now + "] : " + message);
                streamWriter.Close();
            }

            MessageBox.Show(message);
        }
    }
}

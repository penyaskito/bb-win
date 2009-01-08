using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BBLogic
{
    public class Util
    {

        public static string APPLICATION_NAME = "BBWin";

        public static string GetRootFolder()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATION_NAME);
            CreateIfNotExists(folder);
            return folder;
        }

        public static void CreateIfNotExists(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        internal static string GetNewFolderName(string folder)
        {
            int max = GetMaxNumber(folder);
            return (max + 1).ToString();
        }

        public static int GetMaxNumber(string folder)
        {
            string[] dirs = Directory.GetDirectories(folder);
            if (dirs.Length == 0)
            {
                return 0;
            }
            else
            {
                int[] numbers = new int[dirs.Length];
                for (int i = 0; i < dirs.Length; i++)
                {
                    numbers[i] = Convert.ToInt32(Path.GetFileName(dirs[i]));
                }
                Array.Sort(numbers);
                int max = numbers[dirs.Length - 1];
                return max;
            }
        }
    }
}

using System;
using System.IO;

namespace UTestUtil
{

    public class FileUtil
    {
        public static readonly string TEST_FILES_RELATIVE_PATH = "C:\\Users\\gilis\\Desktop\\Pesquisa\\PleTs\\PLeTs\\__utests\\TestFiles\\";

        //remove all files and folders from specified directory
        public static void CleanFolder(DirectoryInfo folder)
        {
            if (!folder.Exists)
            {
                return;
            }

            foreach (FileInfo file in folder.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in folder.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
    }

}

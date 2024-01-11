using System;
using System.IO;
using System.Collections;

namespace concatdf
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "/Users/charlotte/IsCool/GitHub/UnityPackage.Multi.ImageBundle/Thumbnail/Static";

            DirectoryInfo startDir = new DirectoryInfo(folderPath);

            RecurseFileStructure recurseFileStructure = new RecurseFileStructure();
            recurseFileStructure.TraverseDirectory(startDir);
        }

        public class RecurseFileStructure
        {
            public void TraverseDirectory(DirectoryInfo directoryInfo)
            {
                var subdirectories = directoryInfo.EnumerateDirectories();

                foreach (var subdirectory in subdirectories)
                {
                    TraverseDirectory(subdirectory);
                }

                var files = directoryInfo.EnumerateFiles();

                foreach (var file in files)
                {
                    var filename = file.Name.Split('.');
                    var newFileName = filename[0] + "_thumbnail"+ "." + filename[1];
                    if (filename[1] == "jpg" )
                    {
                        ExtendedMethod.Rename(file, newFileName);
                    }
                }
            }
        }
        
        public static class ExtendedMethod
        {
            public static void Rename(FileInfo fileInfo, string newName)
            {
                fileInfo.MoveTo(fileInfo.Directory.FullName + "/" + newName);
            }
        }
    }
}
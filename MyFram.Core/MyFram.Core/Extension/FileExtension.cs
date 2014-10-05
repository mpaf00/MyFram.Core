namespace MyFram.Core.Extension
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Ionic.Zip;

    public static class FileExtension
    {
        public static void ZipFiles(IList<string> pathFiles, string pathSaveZipFile)
        {
            using (var zip = new ZipFile())
            {
                foreach (var pathFile in pathFiles.Where(File.Exists))
                {
                    zip.AddFile(pathFile);
                }

                zip.Save(pathSaveZipFile);
            }
        }

        public static void UnzipFile(string zipFile, string pathSaveUnzipFile)
        {
            using (var zip = ZipFile.Read(zipFile))
            {
                zip.ExtractAll(pathSaveUnzipFile);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using java.util;
using java.util.zip;
using java.io;
namespace BHI.Rats
{
    /// <summary>
    /// compression component
    /// </summary>
    public static class RatsCompressionManager
    {
        #region Compress and Uncompress
        /// <summary>
        /// Zips the folder name to create a zip file. 
        /// </summary>
        /// <remarks>The function is used for running as a worker thread</remarks>
        /// <param name="parameters">An array of 2 objects - folderName and zipFileName</param>
        public static void Zip(object parameters)
        {
            object[] parms = (object[])parameters;
            Zip((string)parms[0], (string)parms[1]);
        }
        /// <summary>
        /// Zips the folder name to create a zip file
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="zipFileName"></param>
        public static void Zip(string folderName, string zipFileName)
        {
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fz.CreateZip(zipFileName, folderName, true, "");
            }
            catch (ICSharpCode.SharpZipLib.SharpZipBaseException)
            {
                //Fail silently on cancel
                if (Directory.Exists(folderName))
                {
                    Directory.Delete(folderName, true);
                }
                if (System.IO.File.Exists(zipFileName))
                {
                    System.IO.File.Delete(zipFileName);
                }
            }
            catch
            {
                //Close silently
            }
        }
        /// <summary>
        /// Compress list of files [sourceFiles] to [zipFileName]
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="sourceFiles"></param>
        public static void Zip(string zipFileName, FileInfo[] sourceFiles)
        {
            try
            {
                // get the root folder. NOTE: it assums that the first file is at the root.
                string rootFolder = Path.GetDirectoryName(sourceFiles[0].FullName);
                // compress
                ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fz.CreateZip(zipFileName, rootFolder, true, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Extract [zipFileName] to [destinationPath]
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="destinationPath"></param>
        public static void Extract(string zipFileName, string destinationPath)
        {
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fz.ExtractZip(zipFileName, destinationPath, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Extract [zipFileName] to [destinationPath]
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="destinationPath"></param>
        /// <param name="fileNameToExtract">need to be the relative path in the zip file</param>
        public static void ExtractSingleFile(string zipFileName, string destinationPath, string fileNameToExtract)
        {
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fz.ExtractZip(zipFileName, destinationPath, fileNameToExtract);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Return a list of zip entries (compressed files meta data)
        /// </summary>
        /// <param name="zipFile"></param>
        /// <returns></returns>
        private static List<ZipEntry> getZippedFiles(ZipFile zipFile)
        {
            List<ZipEntry> zipEntries = new List<ZipEntry>();
            java.util.Enumeration zipEnum = zipFile.entries();
            while (zipEnum.hasMoreElements())
            {
                ZipEntry zip = (ZipEntry)zipEnum.nextElement();
                zipEntries.Add(zip);
            }
            return zipEntries;
        }
        /// <summary>
        /// Get the list of files in the zip file
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <returns></returns>
        public static System.Collections.ArrayList GetZipEntries(string zipFileName)
        {
            System.Collections.ArrayList arrZipEntries = new System.Collections.ArrayList();
            ZipFile zipfile = new ZipFile(zipFileName);
            List<ZipEntry> zipFiles = getZippedFiles(zipfile);
            foreach (ZipEntry zipFile in zipFiles)
            {
                arrZipEntries.Add(zipFile.getName());
            }
            zipfile.close();
            System.Collections.ArrayList arrZipEntriesInside = new System.Collections.ArrayList();
            foreach (string strZipFile in arrZipEntries)
            {
                if (string.Compare(Path.GetExtension(strZipFile), ".zip", true) == 0)
                {
                    ExtractSingleFile(zipFileName, Path.GetDirectoryName(zipFileName), Path.GetFileName(strZipFile));
                    ZipFile zipfileinside = new ZipFile(Path.GetDirectoryName(zipFileName) + Path.DirectorySeparatorChar + strZipFile);
                    List<ZipEntry> zipFilesInside = getZippedFiles(zipfileinside);
                    foreach (ZipEntry zipFile in zipFilesInside)
                    {
                        arrZipEntriesInside.Add(zipFile.getName());
                    }
                    zipfileinside.close();
                }
            }
            foreach (string strFilesInsideZip in arrZipEntriesInside)
            {
                arrZipEntries.Add(strFilesInsideZip);
            }
            return arrZipEntries;
        }
        /// <summary>
        /// Extract [zipFileName] to [destinationPath] recursively
        /// </summary>
        /// <param name="zipFileName"></param>
        public static void ExtractRecursively(string zipFileName)
        {
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fz.ExtractZip(zipFileName, Path.GetDirectoryName(zipFileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(zipFileName), "");
                DirectoryInfo diInputDir = new DirectoryInfo(Path.GetDirectoryName(zipFileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(zipFileName));
                FileInfo[] fiInfoArr = diInputDir.GetFiles("*.zip", SearchOption.AllDirectories);
                foreach (FileInfo fiInfo in fiInfoArr)
                {
                    if (string.Compare(Path.GetExtension(fiInfo.FullName), ".zip", true) == 0)
                    {
                        ExtractRecursively(fiInfo.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
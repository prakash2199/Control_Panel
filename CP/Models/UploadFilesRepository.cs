using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CP.Models
{
    public class UploadFilesRepository
    {
        public static KeyValuePair<string, string> Uploadfiles(string filename, string fullpath)
        {
            int ctr = 0;
            string FilePath = Path.GetDirectoryName(fullpath);
            string FileName = Path.GetFileNameWithoutExtension(fullpath);
            string FileExtension = Path.GetExtension(fullpath);

            Match FileCtr = Regex.Match(FileName, @"\(([0-9]+)\)$");
            if (FileCtr.Success)
            {
                FileName = FileName.Substring(0, FileName.Length - (FileCtr.Length));
                ctr = Convert.ToInt32(FileCtr.Value.Substring(1, FileCtr.Length - 2));
            }
            while ((new System.IO.FileInfo(FilePath + "\\" + FileName + (ctr > 0 ? " (" + ctr + ")" : "") + FileExtension)).Exists)
            {
                ctr++;
            }
            filename = FileName + (ctr > 0 ? " (" + ctr + ")" : "") + FileExtension;
            fullpath = FilePath + "\\" + filename;
            return new KeyValuePair<string, string>(filename, fullpath);
        }
    }
}
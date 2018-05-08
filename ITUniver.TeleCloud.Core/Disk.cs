using ITUniver.TeleCloud.Core.DiskOpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCloud.Core
{
    public class Disk
    {
        private _File[] File1 { get; set; }
        private string _dir { get; set; }
        private _File CurrentFile { get; set; }
        public Disk(string Dir)
        {
            if (Directory.Exists(Dir))
            {
                _dir = Dir;
                string[] namefiles = Directory.GetFiles(Dir).Select(x => Path.GetFileName(x)).ToArray();
                _File[] temps = new _File[namefiles.Length];
                
                for (int i = 0; i < temps.Length; i++)
                {
                    temps[i] = new _File(namefiles[i]);
                }
                File1 = temps;
            }
        }

        public IEnumerable<string> FileList()
        {
            return File1.Select(x => x.FileName);
        }

        public bool NewFile(string Name, string[] Text)
        {
            try
            {
                FileInfo f = new FileInfo(_dir + Name);
                using (FileStream fs = f.Create())
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        for (int i = 0; i < Text.Length; i++)
                        {
                            sw.WriteLine(Text[i]);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool NewFile(string Name, string Text)
        {
            try
            {
                FileInfo f = new FileInfo(_dir + Name);
                using (FileStream fs = f.Create())
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(Text);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private _File SearchFile(string Name)
        {
            return File1.FirstOrDefault(x => x.FileName == Name);
        }
        public bool DeleteFile(string Name)
        {
            FileInfo f = new FileInfo(_dir + Name);
            if (f.Exists)
            {
                f.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }
        public string OpenFile(string Name)
        {
            CurrentFile = SearchFile(Name);
            FileInfo f = new FileInfo(_dir + CurrentFile.FileName);
            List<string> tempContent = new List<string>();
            if (f.Exists)
            {
                using (FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        CurrentFile.FileContent = sr.ReadToEnd();
                    }
                }
                return CurrentFile.FileContent;
            }
            else
            {
                return "Не удалось открыть файл";
            }
        }
    }
}

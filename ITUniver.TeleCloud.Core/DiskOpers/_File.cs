using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCloud.Core.DiskOpers
{
    public class _File
    {
        public _File() { }
        public _File(string Name)
        {
            FileName = Name;
        }
        public string FileName { get; set; }

        public string FileContent { get; set; }
    }
}

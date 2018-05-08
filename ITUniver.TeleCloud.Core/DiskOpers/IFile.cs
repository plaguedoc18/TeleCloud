using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCloud.Core.DiskOpers
{
    public interface IFile
    {
        string FileName { get; set; }
        string FileContent { set; }
    }
}

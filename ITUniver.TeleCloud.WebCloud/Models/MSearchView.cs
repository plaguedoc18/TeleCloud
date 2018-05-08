using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCloud.WebCloud.Models
{
    public class MSearchView
    {
        [DisplayName("Введите имя файла")]
        public string SearchField { get; set; }
    }
}
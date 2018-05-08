using ITUniver.TeleCloud.WebCloud.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCloud.WebCloud.Models
{
    public class FileItemModel : IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        public virtual string NameFile { get; set; }

        public virtual User IdUser { get; set; }

        public virtual DateTime DateCreate { get; set; }
    }
}
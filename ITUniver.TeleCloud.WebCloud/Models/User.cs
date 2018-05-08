using ITUniver.TeleCloud.WebCloud.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCloud.WebCloud.Models
{
    public class User : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }

        /// <summary>
        /// True - мужской, false - женский
        /// </summary>
        public virtual bool Sex { get; set; }
    }
}
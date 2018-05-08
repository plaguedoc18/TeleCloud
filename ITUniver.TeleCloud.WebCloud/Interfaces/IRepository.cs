using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCloud.WebCloud.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
    }
    public interface IRepository<T> where T : IEntity
    {
        T Load(int id);

        bool Save(T obj);

        bool Delete(string condition);

        T Clone(T obj);

        IEnumerable<T> Find(string condition);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIMSService.DataActions
{
    public interface IActions<T>
    {
        IEnumerable<T> Get(bool showInactive);
        T GetById(int id);
        void Insert(T modelObj, string user);
        void Update(T modelObj, string user);
        void Deactivate(int id, string user);
    }
}
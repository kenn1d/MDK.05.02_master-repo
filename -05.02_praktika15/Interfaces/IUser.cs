using praktika15.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika15.Interfaces
{
    public interface IUser
    {
        void Save(bool Update = false);
        List<UserContext> AllUsers();
        void Delete();
    }
}

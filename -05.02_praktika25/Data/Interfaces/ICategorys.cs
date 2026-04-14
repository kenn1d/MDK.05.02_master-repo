using Org.BouncyCastle.Asn1;
using praktika22.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace praktika22.Data.Interfaces
{
    public interface ICategorys
    {
        public IEnumerable<Categorys> AllCategorys { get; }

        public void Add(string Name, string Desc);
        public void Delete(int Id);
        public void Update(Categorys category);
    }
}

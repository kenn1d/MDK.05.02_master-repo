using praktika22.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace praktika22.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems {  get; }
    }
}

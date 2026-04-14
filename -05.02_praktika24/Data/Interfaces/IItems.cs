using praktika22.Data.Models;

namespace praktika22.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems {  get; }

        public int Add(Items Item);
        public void Delete(int idItem);
        public void Update(Items Item);
    }
}

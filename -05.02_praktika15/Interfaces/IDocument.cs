

using praktika15.Classes;
using System.Collections.Generic;

namespace praktika15.Interfaces
{
    public interface IDocument
    {
        void Save(bool Update = false);
        List<DocumentContext> AllDocuments();
        void Delete();
    }
}

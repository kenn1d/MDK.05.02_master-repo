using System.Collections.Generic;

namespace WpfApp1.Classes
{
    public class TypeOperation
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public TypeOperation(int id, string name, string description) {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        public static List<TypeOperation> AllTypeOperation()
        {
            List<TypeOperation> allTypeOperation = new List<TypeOperation>();

            allTypeOperation.Add(new TypeOperation(1, "Печать", ""));
            allTypeOperation.Add(new TypeOperation(2, "Копия", ""));
            allTypeOperation.Add(new TypeOperation(3, "Сканирование", ""));
            allTypeOperation.Add(new TypeOperation(4, "Ризограф", ""));

            return allTypeOperation;
        }
    }
}

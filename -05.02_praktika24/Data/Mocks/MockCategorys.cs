using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace praktika22.Data.Mocks
{
    public class MockCategorys : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                return new List<Categorys>
                {
                    new Categorys()
                    {
                        Id = 1,
                        Name = "Микроволновые печи",
                        Description = "Микроволновая печь - это электроприбор, использующий электромагнитное излучение (обычно с частотой 2450 МГц) для быстрого нагрева, приготовления или размораживания пищи."
                    },
                    new Categorys()
                    {
                        Id = 2,
                        Name = "Мультиварки",
                        Description = "Мультиварка - это многофункциональный электрический кухонный прибор с автоматическим управлением, предназначенный для приготовления разнообразных блюд."
                    }
                };
            }
        }

        public void Add(string Name, string Desc) { }
        public void Delete(int Id) { }
        public void Update(Categorys category) { }
    }
}

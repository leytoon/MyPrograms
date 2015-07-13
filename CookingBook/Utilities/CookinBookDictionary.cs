using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Utilities
{
    public class CookinBookDictionary
    { 
        private  Dictionary<string, CookingBookLabelNames> CookingDictionary;

        private static CookinBookDictionary _oinstance = null;

        public static CookinBookDictionary Instance
        {
            get
            {
                if (_oinstance == null)
                {
                    _oinstance = new CookinBookDictionary();
                }
                return _oinstance;
            }
        }
        public CookingBookLabelNames GetNames(string Key) { return CookingDictionary[Key]; }
       
        CookinBookDictionary() 
        {
            CookingDictionary = new Dictionary<string, CookingBookLabelNames>(){ 
            {"PL",new CookingBookLabelNames{ All="Wszystko", Refresh="Odśwież",Persons="Osoby" ,Amount="Ilość", Search="Wyszukaj", Chosen="Wybrany", Main="Główne", Add="Dodaj", Delete="Usuń", Update="Zaktualizuj", Component="Składnik", Name="Nazwa", Price="Cena", Recipe="Przepis", Window="Okno", Menu="Menu", Action="Akcja"  }},
            {"ENG",new CookingBookLabelNames{All="All", Refresh="Refresh", Persons="Persons", Amount="Amount", Search="Search", Chosen="Chosen", Main="Main", Add="Add", Delete="Delete", Update="Update", Component="Component", Name="Name", Price="Price", Recipe="Recipe", Window="Window", Menu="Menu", Action="Action"  }}};
        }
    }
}

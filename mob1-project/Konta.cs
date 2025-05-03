using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mob1_project
{
    public class Konta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }        
        public string Nazwa { get; set; }  
        public string Haslo { get; set; }  
        public string Telefon { get; set; }
    }
}

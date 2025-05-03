using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mob1_project
{
    public class Danie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public float Cena { get; set; }
        public int Kategoria { get; set; }
        public string Restauracja { get; set; } 
    }
}

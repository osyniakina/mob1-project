using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mob1_project
{
    public class Restauracja
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public string Miasto { get; set; }

        public string CzasPracy { get; set; }

        public string KosztDostawy { get; set; }

        public string CzasDostawy { get; set; }

        public string Kategoria { get; set; }
    }
}

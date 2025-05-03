using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mob1_project
{
    public class Zamowienie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int KontoId { get; set; } // foreign key to Konta
        public int DanieId { get; set; } // foreign key to Dania
        public int RestauracjaId { get; set; } // foreign key to Restauracja
        public DateTime Data { get; set; }
    }
}

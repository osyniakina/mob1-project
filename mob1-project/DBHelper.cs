using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;


namespace mob1_project
{
    internal class DBHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DBHelper()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database1.db3");

            // Create a connection to the database
            _database = new SQLiteAsyncConnection(dbPath);

            // Create the table (if it doesn't exist already)
            _database.CreateTableAsync<Konta>().Wait();
            _database.CreateTableAsync<Restauracja>().Wait();
            _database.CreateTableAsync<Danie>().Wait();
            _database.CreateTableAsync<Zamowienie>().Wait();
        }

        // Add a new account to the database
        public Task<int> AddKontaAsync(Konta konta)
        {
            return _database.InsertAsync(konta);
        }

        public Task<int> AddRestauracjaAsync(Restauracja rest)
        {
            return _database.InsertAsync(rest);
        }

        public async Task AddSampleRestauracjeAsync()
        {
            var existing = await _database.Table<Restauracja>().ToListAsync();
            if (existing.Count == 0)
            {
                var restauracje = new List<Restauracja>
                    {
                        new Restauracja { Nazwa = "McDonald’s", Miasto = "Warszawa", CzasPracy = "od 08:00 do 22:00", CzasDostawy = "20-30 min", KosztDostawy = "5 zł", Kategoria = "Amerykanska" },
                        new Restauracja { Nazwa = "Dominos Pizza", Miasto = "Warszawa", CzasPracy = "od 09:00 do 22:00", CzasDostawy = "30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Wloska" },
                        new Restauracja { Nazwa = "Sushi Wok", Miasto = "Warszawa", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30-40 min", KosztDostawy = "6 zł", Kategoria = "Azjatycka"},
                        new Restauracja { Nazwa = "McDonald’s", Miasto = "Lublin", CzasPracy = "od 08:00 do 22:00", CzasDostawy = "20-30 min", KosztDostawy = "5 zł", Kategoria = "Amerykanska"  },
                        new Restauracja { Nazwa = "Dominos Pizza", Miasto = "Lublin", CzasPracy = "od 09:00 do 22:00", CzasDostawy = "30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Wloska"},
                        new Restauracja { Nazwa = "Sushi Wok", Miasto = "Lublin", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30-40 min", KosztDostawy = "6 zł", Kategoria = "Azjatycka"},
                        new Restauracja { Nazwa = "McDonald’s", Miasto = "Bialystok", CzasPracy = "od 08:00 do 22:00", CzasDostawy = "20-30 min", KosztDostawy = "5 zł", Kategoria = "Amerykanska"  },
                        new Restauracja { Nazwa = "Dominos Pizza", Miasto = "Bialystok", CzasPracy = "od 09:00 do 22:00", CzasDostawy = "30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Wloska"},
                        new Restauracja { Nazwa = "Sushi Wok", Miasto = "Bialystok", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30-40 min", KosztDostawy = "6 zł", Kategoria = "Azjatycka" },
                    };
                await _database.InsertAllAsync(restauracje);
            }
        }

        public async Task AddRestauracje2Async()
        {
            var existing = await _database.Table<Restauracja>().ToListAsync();
            if (existing.Count == 9)
            {
                var restauracje = new List<Restauracja>
                    {
                        new Restauracja { Nazwa = "KFC", Miasto = "Warszawa", CzasPracy = "od 08:00 do 24:00", CzasDostawy = "20-30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Amerykanska" },
                        new Restauracja { Nazwa = "Pierogarnia", Miasto = "Warszawa", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30 min", KosztDostawy = "7 zł", Kategoria = "Europejska" },
                        new Restauracja { Nazwa = "Toni Pepperoni", Miasto = "Warszawa", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30-40 min", KosztDostawy = "6 zł", Kategoria = "Wloska"},
                        new Restauracja { Nazwa = "KFC", Miasto = "Lublin", CzasPracy = "od 08:00 do 24:00", CzasDostawy = "20-30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Amerykanska"  },
                        new Restauracja { Nazwa = "Pierogarnia", Miasto = "Lublin", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30 min", KosztDostawy = "7 zł", Kategoria = "Europejska"},
                        new Restauracja { Nazwa = "Toni Pepperoni", Miasto = "Lublin", CzasPracy = "od 09:00 do 21:00", CzasDostawy = "30-40 min", KosztDostawy = "6 zł", Kategoria = "Wloska"},
                        new Restauracja { Nazwa = "KFC", Miasto = "Bialystok", CzasPracy = "od 08:00 do 22:00", CzasDostawy = "20-30 min", KosztDostawy = "Darmowa dostawa", Kategoria = "Amerykanska"  },
                        new Restauracja { Nazwa = "Pierogarnia", Miasto = "Bialystok", CzasPracy = "od 09:00 do 22:00", CzasDostawy = "30 min", KosztDostawy = "7 zł", Kategoria = "Europejska"},
                        new Restauracja { Nazwa = "Kebab Super King", Miasto = "Bialystok", CzasPracy = "od 08:00 do 24:00", CzasDostawy = "25 min", KosztDostawy = "5 zł", Kategoria = "Europejska" },
                    };
                await _database.InsertAllAsync(restauracje);
            }
        }


        public async Task AddDaniaAsync()
        {
            var existing = await _database.Table<Danie>().ToListAsync();
            if (existing.Count == 0)
            {
                var danie = new List<Danie>
                    {
                        new Danie { Nazwa = "Cheeseburger", Cena = 7.00F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Hamburger", Cena = 6.00F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "McDouble", Cena = 10.20F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "BigMac", Cena = 21.30F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "McRoyal", Cena = 21.00F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "McChicken", Cena = 19.20F, Kategoria = 3, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Frytki małe", Cena = 8.00F, Kategoria = 5, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Frytki duże", Cena = 10.00F, Kategoria = 5, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Lody truskawkowe", Cena = 10.20F, Kategoria = 6, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Lody waniliowe", Cena = 10.00F, Kategoria = 6, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Lody karmelowe", Cena = 10.20F, Kategoria = 6, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Shake czekoladowy", Cena = 9.40F, Kategoria = 6, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Coca Cola", Cena = 7.40F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Fanta", Cena = 7.40F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 7.40F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Woda", Cena = 4.00F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Kawa czarna", Cena = 7.40F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Ketczup Dodatkowy", Cena = 1.00F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Sos Czosnkowy", Cena = 1.40F, Kategoria = 7, Restauracja = "McDonald’s"},
                        new Danie { Nazwa = "Sos Śmietankowy", Cena = 1.40F, Kategoria = 7, Restauracja = "McDonald’s"},

                        new Danie { Nazwa = "Margaritta", Cena = 28.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Pepperoni", Cena = 30.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Capricciosa", Cena = 30.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Hawajska", Cena = 30.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Carbonara", Cena = 30.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Farmhouse", Cena = 35.95F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Diavola", Cena = 35.95F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Vegetariana", Cena = 35.95F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "4 Cheese", Cena = 35.95F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "New Yorker", Cena = 35.95F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Frytki małe", Cena = 7.40F, Kategoria = 5, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Frytki duże", Cena = 9.75F, Kategoria = 5, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Colesław", Cena = 8.75F, Kategoria = 5, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Margaritta", Cena = 28.75F, Kategoria = 4, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Lava Cake", Cena = 13.65F, Kategoria = 6, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Calzone Sweet", Cena = 14.75F, Kategoria = 6, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Coca Cola", Cena = 7.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Fanta", Cena = 7.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 7.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Woda", Cena = 4.00F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Sos Pomidorowy", Cena = 2.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Sos Czosnkowy", Cena = 2.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},
                        new Danie { Nazwa = "Sos Śmietankowy", Cena = 2.40F, Kategoria = 7, Restauracja = "Dominos Pizza"},

                        new Danie { Nazwa = "Wołowina po tajsku", Cena = 29.40F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Kurczak po tajsku", Cena = 26.40F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Pad thai z kurczakiem", Cena = 35.70F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Kurczak teriyaki", Cena = 30.00F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Futomaki 10szt", Cena = 40.95F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Futomaki Tofu 10szt", Cena = 37.90F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Fhiladelfia 10szt", Cena = 38.40F, Kategoria = 1, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Zupa Pho z wołowiną", Cena = 25.30F, Kategoria = 2, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Zupa Pho z kurczakiem", Cena = 23.30F, Kategoria = 2, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Ramen", Cena = 30.40F, Kategoria = 2, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Sajgonki mięsne 3 szt", Cena = 15.00F, Kategoria = 5, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Sajgonki warzywne 3 szt", Cena = 13.00F, Kategoria = 5, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Surówka", Cena = 6.40F, Kategoria = 5, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Frytki", Cena = 6.40F, Kategoria = 5, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Coca Cola", Cena = 5.40F, Kategoria = 7, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Fanta", Cena = 5.40F, Kategoria = 7, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 5.40F, Kategoria = 7, Restauracja = "Sushi Wok"},
                        new Danie { Nazwa = "Woda", Cena = 3.00F, Kategoria = 7, Restauracja = "Sushi Wok"},

                        new Danie { Nazwa = "Kubełek 15 Hot Wings", Cena = 44.95F, Kategoria = 1, Restauracja = "KFC"},
                        new Danie { Nazwa = "Kubełek 11 Strips", Cena = 32.75F, Kategoria = 1, Restauracja = "KFC"},
                        new Danie { Nazwa = "Kubełek Classic", Cena = 45.00F, Kategoria = 1, Restauracja = "KFC"},
                        new Danie { Nazwa = "Kubełek Ketntucky", Cena = 34.45F, Kategoria = 1, Restauracja = "KFC"},
                        new Danie { Nazwa = "Margaritta", Cena = 28.75F, Kategoria = 1, Restauracja = "KFC"},
                        new Danie { Nazwa = "Cheeseburger", Cena = 9.75F, Kategoria = 3, Restauracja = "KFC"},
                        new Danie { Nazwa = "Zinger Burger", Cena = 20.25F, Kategoria = 3, Restauracja = "KFC"},
                        new Danie { Nazwa = "Grander Gurger", Cena = 27.00F, Kategoria = 3, Restauracja = "KFC"},
                        new Danie { Nazwa = "Frytki małe", Cena = 8.00F, Kategoria = 5, Restauracja = "KFC"},
                        new Danie { Nazwa = "Frytki duże", Cena = 10.00F, Kategoria = 5, Restauracja = "KFC"},
                        new Danie { Nazwa = "Lody waniliowe", Cena = 10.20F, Kategoria = 6, Restauracja = "KFC"},
                        new Danie { Nazwa = "Shake waniliowy", Cena = 9.40F, Kategoria = 6, Restauracja = "KFC"},
                        new Danie { Nazwa = "Coca Cola", Cena = 7.40F, Kategoria = 7, Restauracja = "KFC"},
                        new Danie { Nazwa = "Fanta", Cena = 7.40F, Kategoria = 7, Restauracja = "KFC"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 7.40F, Kategoria = 7, Restauracja = "KFC"},
                        new Danie { Nazwa = "Woda", Cena = 4.00F, Kategoria = 7, Restauracja = "KFC"},

                        new Danie { Nazwa = "Pierogi ruskie 10 szt", Cena = 33.00F, Kategoria = 1, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi wieprzowo-drobiowe 10 szt", Cena = 35.00F, Kategoria = 1, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi z kapustą 10 szt", Cena = 33.00F, Kategoria = 1, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi z pieczarkami 10 szt", Cena = 35.00F, Kategoria = 1, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi z ciecierzycą 10 szt", Cena = 33.00F, Kategoria = 1, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Rosół z makaronem", Cena = 17.00F, Kategoria = 2, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Zupa Dnia", Cena = 19.00F, Kategoria = 2, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi z czekoladą 10 szt", Cena = 33.00F, Kategoria = 6, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi sernikowe 10 szt", Cena = 32.00F, Kategoria = 6, Restauracja = "Pierogarnia"},
                        new Danie { Nazwa = "Pierogi z owocami 10 szt", Cena = 34.00F, Kategoria = 6, Restauracja = "Pierogarnia"},

                        new Danie { Nazwa = "Spaggeti Boloniese", Cena = 30.50F, Kategoria = 1, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Spaggeti Carbonara", Cena = 32.00F, Kategoria = 1, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Lasagne", Cena = 34.70F, Kategoria = 1, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Spaggeti Boloniese", Cena = 30.50F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Margaritta", Cena = 28.75F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Pepperoni", Cena = 30.75F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Capricciosa", Cena = 30.75F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "4 Sery", Cena = 30.75F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Diavola", Cena = 35.95F, Kategoria = 4, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Lava Cake", Cena = 13.65F, Kategoria = 6, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Calzone Sweet", Cena = 14.75F, Kategoria = 6, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Coca Cola", Cena = 7.40F, Kategoria = 7, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 7.40F, Kategoria = 7, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Woda", Cena = 4.00F, Kategoria = 7, Restauracja = "Toni Pepperoni"},
                        new Danie { Nazwa = "Sos Pomidorowy", Cena = 2.40F, Kategoria = 7, Restauracja = "Toni Pepperoni"},

                        new Danie { Nazwa = "Kebab tortilla wołowina", Cena = 29.00F, Kategoria = 1, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Kebab tortilla kurczak", Cena = 25.00F, Kategoria = 1, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Kebab tortilla mieszane", Cena = 26.00F, Kategoria = 1, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Kebab tortilla wołowina", Cena = 29.00F, Kategoria = 1, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Nuggets", Cena = 30.00F, Kategoria = 5, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Frytki małe", Cena = 7.00F, Kategoria = 5, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Frytki  duże", Cena = 9.00F, Kategoria = 5, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Coca Cola", Cena = 5.40F, Kategoria = 7, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Sok pomaranczowy", Cena = 5.40F, Kategoria = 7, Restauracja = "Kebab Super King"},
                        new Danie { Nazwa = "Woda", Cena = 3.00F, Kategoria = 7, Restauracja = "Kebab Super King"}


                    };
                await _database.InsertAllAsync(danie);
            }
        }



        // Get all accounts from the database
        public Task<List<Konta>> GetKontasAsync()
        {
            return _database.Table<Konta>().ToListAsync();
        }

        public Task<List<Restauracja>> GetRestAsync()
        {
            return _database.Table<Restauracja>().ToListAsync();
        }

        public Task<List<Danie>> GetDaniaAsync()
        {
            return _database.Table<Danie>().ToListAsync();
        }

        // Find an account by Id
        public Task<Konta> GetKontaByNamePassAsync(string name, string pass)
        {
            return _database.Table<Konta>().Where(i => i.Nazwa == name && i.Haslo == pass).FirstOrDefaultAsync();
        }

        // Update an existing account
        public Task<int> UpdateKontaAsync(Konta konta)
        {
            return _database.UpdateAsync(konta);
        }

        // Delete an account
        public Task<int> DeleteKontaAsync(Konta konta)
        {
            return _database.DeleteAsync(konta);
        }
    }
}

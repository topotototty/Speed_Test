using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speed_Test
{
    internal class Score_Table
    {
        public void Table()
        {
            Console.Clear();
            Console.WriteLine("______________________Таблица рекордов______________________");

            string usersInfo = File.ReadAllText("ScoreTable.json");
            List<User> Users_Info = JsonConvert.DeserializeObject<List<User>>(usersInfo);

            foreach (User user in Users_Info)
            {
                Console.WriteLine($"{user.Name}  -->  {user.Letter_Min}  -->  {user.Letter_Sec}");
            }

            Console.SetCursorPosition(0, 15);

            Console.WriteLine("Нажмите <<ENTER>> чтобы перезапустить программу\nНажмите <<ESCAPE>> чтобы выйти ");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Test test = new Test();
                    test.Start();
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

            }
            Console.ReadKey(true);
        }
    }
}

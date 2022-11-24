using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;

namespace Speed_Test
{
    internal class Test
    {
        public static string Name;
        public int Mistakes = 0;
        public float Correct = 0;
        bool Over;
        public void Start()
        {
            Over = false;
            Console.SetWindowSize(120, 30);

            Console.Clear();
            Console.WriteLine("Добро пожаловать, введите ваше имя: ");

            Name = Console.ReadLine();
            if (Name == "") Name = "Безымянный палец";

            Console.WriteLine("Нажмите << ENTER >> когда будете готовы");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Typing_Test();
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Нажмите << ENTER >> когда будете готовы");

                }
            }
        }
        private void Typing_Test()
        {
            Console.Clear();
            char[] text = Typing_Text();
            foreach (char letters in text)
            {
                Console.Write(letters);
            }


            new Thread(Timer).Start();

            int Letter_Position_x = 0;
            int Letter_Position_y = 0;

            Console.SetCursorPosition(0, 0);

            foreach (char chars in text)
            {
                if (Over) break;

                Console.CursorVisible = true;

                char CharKey = Console.ReadKey(true).KeyChar;

                if (Letter_Position_x >= 120)
                {
                    Letter_Position_y++;
                    Letter_Position_x = 0;
                }

                Console.SetCursorPosition(Letter_Position_x, Letter_Position_y);
                Letter_Position_x++;

                if (CharKey == chars)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(chars);
                    Console.ResetColor();
                    Correct++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(chars);
                    Console.ResetColor();
                    Mistakes++;
                }
            }            
            Result_Menu();
        }
        private void Timer()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            long Time_To_End = 60;
            do
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 15);
                Time_To_End = 60 - stopwatch.ElapsedMilliseconds / 1000;
                Console.WriteLine($"Time: 00:{Time_To_End}           ");
                Thread.Sleep(1000);
            }while (Time_To_End != 0);

            Over = true;
        }
        private void Result_Menu()
        {
            Console.SetCursorPosition(0, 16);
            Console.WriteLine($"Ошибки: {Mistakes}\nКоличество символов в минуту {Correct}\nКоличество символов в секунду: {Correct / 60}");

            Add_Info();
            Mistakes = 0;
            Correct = 0;

            Console.WriteLine("Нажмите <<ENTER>> чтобы перезапустить программу\nНажмите <<ESCAPE>> чтобы выйти\nНажмите <<TAB>> чтобы открвть таблицу рекордов");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Start();
                }

                else if (key.Key == ConsoleKey.Tab)
                {
                    Score_Table score = new Score_Table();
                    score.Table();
                }

                else if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

            }
        }
        private char[] Typing_Text()
        {
            string Text = "Однажды в канун Нового года кролики Пушинка и Прыгун поджидали в гости свою тётушку. Пушинка стала печь блины. Прыгун же оделся и пошёл на улицу расчистить тропинку для милой тётушки. Когда Прыгун вернулся в дом, на столике у плиты выросла горка аппетитных румяных блинчиков. Последний блин Пушинка легонько подкинула на сковородке, и он ловко прилепился к потолку.— Вот так новогоднее украшение! — ахнула Пушинка. В это время зазвонил колокольчик. На пороге стояла тётушка в новой шляпке. Эта шляпка была дивным сооружением с лентами и цветами. Прыгун и Пушинка восхищались тётушкой: «Ах, тётушка! Ах, шляпка!». Все пили душистый малиновый чай и беседовали о новостях в Волшебном Лесу. Вдруг часы пробили полночь. Холодный уже блинчик отклеился и нежно упал на шляпку тётушки. Зайчата не знали, что и предпринять! По дороге к тётушкиному дому зайчата пытались снять блинчик, но безуспешно. Вдруг раздался шелест крыльев и весёлое чириканье. Птички в один миг растащили весь блин, и все с облегчением вздохнули. А тётушка была счастлива и спокойна: «Какой милый Новый год!»";

            char[] CharText = Text.ToCharArray();
            return CharText;
        }

        private void Add_Info()
        {

            List<User> Users_Info;
            if (!File.Exists("ScoreTable.json"))
            {
                FileStream fileStream = File.Create("ScoreTable.json");
                Users_Info = new List<User>();
                fileStream.Dispose();
            }
            else
            {
                string usersInfo = File.ReadAllText("ScoreTable.json");
                Users_Info = JsonConvert.DeserializeObject<List<User>>(usersInfo);
            }

            Users_Info.Add(new User(Name, Correct / 60, Convert.ToInt32(Correct)));

            string json = JsonConvert.SerializeObject(Users_Info);
            File.WriteAllText("ScoreTable.json", json);
        }
    }
}

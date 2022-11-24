using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speed_Test
{
    public class User
    {
        public string Name;
        public float Letter_Sec;
        public int Letter_Min;
        public User(string name, float letter_sec, int letter_min)
        {
            Name = name;
            Letter_Sec = letter_sec;
            Letter_Min = letter_min;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_01.model
{
    public class Student
    {
        public string code { get; set; }
        public string name { get; set; }
        public DateTime birthday { get; set; }

        public bool gender { get; set; }
        public Class LopChuQuan { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}

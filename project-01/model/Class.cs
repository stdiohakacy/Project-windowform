using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_01.model
{
    public class Class
    {
        public string code { get; set; }
        public string name { get; set; }

        public string GiaoVienCoVan { get; set; }
        public Dictionary<string, Student> students { get; set; }

        public Class() { 
            students = new Dictionary<string,Student>();
        }

        public override string ToString()
        {
            return name;
        }

        public Major KhoaChuQuan { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_01.model
{
    public class Major
    {
        public string code { get; set; }
        public string name { get; set; }
        public Dictionary<string, Class> classes { get; set; }

        public Major() {
            classes = new Dictionary<string, Class>();
        }

        public override string ToString()
        {
            return name;
        }

        public Major LeaderOfMajor { get; set; }
    }
}

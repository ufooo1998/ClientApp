using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Entities
{
    class EditInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTimeOffset bod { get; set; }
        public string gender { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}

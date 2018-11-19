using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTests.Models
{
    class Pessoa
    {
        public int Age { get; set; }
        public IEnumerable<int> PhoneNumbers { get; set; }
    }
}

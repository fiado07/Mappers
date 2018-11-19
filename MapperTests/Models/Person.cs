using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTests.Models
{
    public class Person
    {
        public int Age { get; set; }
        public IEnumerable<int> PhoneNumbers { get; set; }
        public string Name { get; set; }
    }
}

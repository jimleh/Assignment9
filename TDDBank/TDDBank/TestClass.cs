using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDBank
{
    class TestClass
    {
        List<int> intList;

        public TestClass()
        {
            intList = new List<int>();
        }

        public TestClass(List<int> ints = null)
        {
            if(ints == null)
            {
                ints = new List<int>();
            }

            intList = ints;
        }


    }
}

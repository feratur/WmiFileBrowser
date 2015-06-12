using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmiFileBrowser
{
    class Program
    {
        static void Main()
        {
            try
            {
                var g = WmiUtils.GetConnectedScope("localhost", @"root\cimv2", null, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}

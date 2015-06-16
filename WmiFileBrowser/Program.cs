using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Implementations;

namespace WmiFileBrowser
{
    class Program
    {
        static void Main()
        {
            try
            {
                var ffds = new int[] {1, 2, 3}.AsEnumerable();
                var dsa = (int[]) ffds;
                var jdahg = new KeyValuePair<PropertyType, object>[2];
                
                
                var li = new List<string>();
                using (var reader = new StreamReader("d:\\test.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var ind = line.IndexOf('"') + 1;
                        li.Add(line.Substring(ind, line.LastIndexOf('"') - ind));
                    }
                }
                using (var stream = new FileStream("d:\\test2.txt", FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var stdr in li)
                    {
                        writer.WriteLine('"' + stdr + '"' + ',');
                    }
                }
                
                
                var path = new FilePath();
                var g = WmiUtils.GetConnectedScope("localhost", @"root\cimv2");
                var str = new List<string>();
                var og = new object();
                foreach (var obj in WmiUtils.GetWmiQuery(g, "win32_computersystem").Cast<ManagementObject>())
                {
                    using (obj)
                    {
                        var jsdd = obj.Properties["roles"];
                    }
                }
                foreach (var obj in WmiUtils.GetWmiQuery(g, "win32_volume").Cast<ManagementObject>())
                {
                    using (obj)
                    {
                        var gda = obj["InstallDate"];
                        str.Add((string)obj["Name"]);
                        og = obj["FreeSpace"];
                    }
                }
                var jhg = og;
                og = 1;
            }
            catch (Exception ex)
            {
            }
        }
    }
}

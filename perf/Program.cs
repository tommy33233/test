using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using perf.Export;
using perf.Import;
using perf.Import.EF;
using perf.ImportNL;
using System.Xml.Serialization;
using perf.ImportXML;

namespace perf
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new XmlDocument();
            doc.Load(@"D:\test.xml");

            //-<ArrayOfPerfume xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

            var bookNodes = doc.SelectNodes(@"//ArrayOfPerfume/Perfume");
            foreach (XmlNode item in bookNodes)
            {
                string title = item.FirstChild.InnerText;
                string price = item.ChildNodes[1].InnerText;
                Console.WriteLine("title {0} price: {1}", title, price); //just for demo
            }
        }
    }
}

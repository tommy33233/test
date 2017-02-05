using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using perf.Import;
using System.IO;
using perf.Export;

namespace perf.ImportXML
{
     public   class ImportFromXML
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Perfume));


         public void GetPerfumesFromFile()
         {
           // List<Perfume> perfumes=new List<Perfume>();
             perfumes perfs = null;
             try
             {
                 using (FileStream fileStream = new FileStream(@"D:\test.xml", FileMode.Open))
                 {
                    perfs = (perfumes) serializer.Deserialize(fileStream);
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }

             Console.WriteLine("");
         }


        
    }
}

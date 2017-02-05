using perf.Import;
using perf.Import.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace perf.Export
{
    static class XMLExport
    {
        public static void CreateAndExportXML()

        {
            using (PerfContext db = new PerfContext())
            {
                List<Perfume> perfumes = db.Perfumes.ToList();
                XmlSerializer serialiser = new XmlSerializer(typeof (List<Perfume>));
                TextWriter Filestream = new StreamWriter(@"D:\test.xml");

                serialiser.Serialize(Filestream, perfumes);

                Filestream.Close();
            }
        }

        public static void ExportUsingXDoc()
        {
            string path = @"D:\test.xml";
            XDocument xDoc = XDocument.Load(path);
            var perf = xDoc.Descendants("Perfume");

            foreach (var p in perf)
            {
                Console.WriteLine(p.NextNode);

            }
        }

        public static IEnumerable<Perfume> StreamBooks(string uri)
        {
            using (XmlReader reader = XmlReader.Create(uri))
            {
                Perfume perf = new Perfume();

                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name == "Perfume")
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "LineId")
                            {
                                int num;
                                Int32.TryParse(reader.ReadString(), out num);
                                perf.LineId = num;
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == "Number")
                            {
                                int num;
                                Int32.TryParse(reader.ReadString(), out num);
                                perf.Number = num;
                            }
                        }
                        yield return perf;
                    }
                }
            }
        }
    }
}

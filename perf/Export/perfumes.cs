using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using perf.Import;
using System.Xml.Serialization;

namespace perf.Export
{
   public class perfumes
    {
        [XmlElement("Perfume")]
        public  List<Perfume> perfs { get; set; }
    }
}

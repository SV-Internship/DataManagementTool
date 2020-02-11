using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV_final.Model
{
    public class Objects
    {
        [XmlAttribute("Count")]
        public int Count { get; set; }
        [XmlElement("Object")]
        public List<_Object> Object { get; set; }
        public Objects() 
        {
            this.Object = new List<_Object>();
        }
    }
}

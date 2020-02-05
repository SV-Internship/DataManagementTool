using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SV_final.Model
{
    public class _File
    {
        [XmlAttribute("FileName")]
        public string FileName { get; set; }
        [XmlElement("Objects")]
        public Objects Objects { get; set; }

        public _File() { }

    }
}
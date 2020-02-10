using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SV_final.Model
{
    public class Files
    {
        [XmlAttribute("FileCount")]
        public int FileCount { get; set; }
        [XmlElement("File")]
        public List<_File> File { get; set; }

        public Files()
        {
            this.File = new List<_File>();
        }

    }
}

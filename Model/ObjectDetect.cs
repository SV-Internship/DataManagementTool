using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SV_final.Model
{
    public class ObjectDetect
    {
        [XmlElement("Files")]
        public Files Files { get; set; }

        public ObjectDetect()
        {
            this.Files = new Files();
        }
    }
}

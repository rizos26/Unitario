using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiCalled
{
    [XmlRoot("item")]
    public class ItemsReaded
    {
        [XmlElement(ElementName ="Name")]

        public string Name { get; set; }

        [XmlElement("Itemid")]

        public int itemid { get; set; }

        public static implicit operator ItemsReaded?(PizzasReaded? v)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiCalled
{
    [XmlRoot("pizzas")]
    public class PizzasReaded
    {
        [XmlElement("pizza")]
        public List <Pizza> Pizzas { get; set; }

    }

    public class Pizza
    {
        [XmlAttribute("nombre")]
        public string Nombre { get; set; }

        [XmlAttribute("precio")]
        public string Precio { get; set; }

        [XmlElement("ingrediente")]
        public List< ingrediente> Ingredientes { get; set; }

    }

    public class ingrediente
    {
        [XmlAttribute("nombre")]
        public string Nombre { get; set; }
    }
}



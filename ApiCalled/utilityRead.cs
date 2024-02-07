using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ApiCalled
{
    public class utilityRead
    {
        public void lecturaXML_Mode(string filepath)
        {
            string text = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);  
            foreach (XmlNode node in doc.DocumentElement.ChildNodes) 
            {
               
                text += node.InnerText;

            }
            MessageBox.Show(text);  
        }

        public ItemsReaded? LecturaXML_Deserialize(string filepath)
        {
            ItemsReaded? i = null;
            var serializer = new XmlSerializer(typeof(ItemsReaded));
            using ( Stream reader = new FileStream(filepath, FileMode.Open))
            {
                i= serializer.Deserialize(reader) as ItemsReaded;
            }
            return i;
        }
        public PizzasReaded? LecturaXML_PizzaDesearialize(string filepath)
        {
            PizzasReaded? i = null;
            var serializer = new XmlSerializer(typeof(PizzasReaded));
            using (Stream reader = new FileStream(filepath, FileMode.Open))
            {
                i = serializer.Deserialize(reader) as PizzasReaded;
            }
            return i;
        }
    }
}

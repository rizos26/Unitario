using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCalled
{
    public class ConfigReader
    {
        public string fichero { get; set; }
      
        public string test { get; set; }

        public string ficheroPizza { get; set; }
        public ConfigReader() 
        {
            test = ConfigurationManager.AppSettings["test"]; 

            fichero = ConfigurationManager.AppSettings["fichero"];
            ficheroPizza = ConfigurationManager.AppSettings["ficheroPizza"];
        }
    }
   
  
}

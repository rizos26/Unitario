using ClassLibrary1.PostgreDataStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class users: ResponseBase
    {
        [Key] 
        public int? idusers { get; set; }
        public string? usuario { get; set; }
        public string? pass { get; set;}

        public string ? email { get; set; }  

        public int? administrador { get; set; }

        public int? manager { get; set; }

        public int? idNegocio { get; set; }

        public int? validated { get; set;}

        public UsuarioPostGre? usuarioPost { get; set; }
    }
}

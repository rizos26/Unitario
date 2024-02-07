
    
using ClassLibrary1;
using ClassLibrary1.PostgreDataStruct;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
             "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        [HttpGet(Name = "TestController")]
        public IList<users> Get([FromQuery]users us)
        {
            Class1 t = new Class1();
            t.connect();
           

            return t.GetUsers(us); 
            //return t.TestDB().Tables[0].Rows.Count;
        }

        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public IList<users> Put([FromBody] users us)
        {
            PostgreeCon Con = new PostgreeCon();
            Con.IniciarCon();
            var y = new ClassLibrary1.Class1();
            var t = y.GetUsers(us);


            for (int i = 0; i < t.Count; i++)
            {
                t[i].usuarioPost = Con.ConsultaTest<UsuarioPostGre>(string.Format(" select  * from usuario where email like '%{0}%'", t[i].usuario))[0];
            }
            return t;
        }


        /* [HttpDelete]
         public users Delete([FromBody] users username)
         {
             PostgreeCon Con = new PostgreeCon();
             Con.IniciarCon();
             var y = new ClassLibrary1.Class1();
             y.connect(true);



             y.DeleteUser(username.usuario);



             return username;
         }*/

        [HttpDelete]
        public users Delete([FromBody] users username)
        {
            PostgreeCon Con = new PostgreeCon();
            Con.IniciarCon();
            var y = new ClassLibrary1.Class1();
            y.connect(true);

            try
            {
                // Elimina el usuario de la tabla usuario
                y.DeleteUser(username.usuario);

                // Consulta y asigna UsuarioPostGre al usuario
               // username.usuarioPost = Con.ConsultaTest<UsuarioPostGre>(string.Format("SELECT * FROM usuarioPostGre WHERE email LIKE '%{0}%'", username.usuario))[0];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el usuario: {ex.Message}");
            }

            return username;
        }
        
        
        /*
        [HttpGet]
             public IList<users> Get([FromBody] string username)
             {
                 var y = new ClassLibrary1.Class1();
                 y.connect(true);
                 var t = y.GetUserByUsername(username);


                 return t;
             }/*

       /* [HttpGet]
        public IList<users> GetUserByUsername(string userUsername)
        {
            try
            {
                PostgreeCon Con = new PostgreeCon();
                Con.IniciarCon();

                var y = new ClassLibrary1.Class1();
                y.connect(true);


                var usersList = y.GetUserByUsername(userUsername);


                foreach (var user in usersList)
                {
                   // user.usuarioPost = Con.ConsultaTest<UsuarioPostGre>(string.Format("SELECT * FROM usuarioPostGre WHERE email LIKE '%{0}%'", user.usuario))[0];
                }



                return usersList;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al obtener usuarios por nombre de usuario: {ex.Message}");
            }
        }*/
        /*[HttpPost]
            public users Post([FromBody] users us)
            {
                var y = new ClassLibrary1.Class1();
                y.connect(true);
                y.ModifyUser(us);


                return us;
            }
        }*/
        [HttpPost]
        public users Post([FromBody] users us)
        {
            try
            {
                PostgreeCon Con = new PostgreeCon();
                Con.IniciarCon();

                var y = new ClassLibrary1.Class1();
                y.connect(true);

                // Modificar el usuario
                y.ModifyUser(us);

                mailinfo mail1 = new mailinfo();
                mail1.mail = "aragonesalonso00@gmail.com";
                mail1.pass = "gean prpl plrg vpjo";
                // Consultar y asignar UsuarioPostGre al usuario
                mail email = new mail();
                email.send(mail1, us.email, "", "", "", at: PDFManage.test(us).CreateAsAttachment("test.pdf"));

                return us;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al modificar el usuario: {ex.Message}");
            }

        }
    }
}

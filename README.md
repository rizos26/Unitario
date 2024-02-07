WebApplication1
=================

[WebApplication1]() It´s an API created by Irene Aragonés for the second-year Data Access subject in the Multiplatform Application Development course.

Table of contents
=================

- [Clases](#clases)
  - [Usuario.cs](#usuariocs)
  - [Users.cs](#userscs)
- [Controllers](#controllers)
  - [InventarioController.cs](#inventariocontrollercs)
  - [UsuarioController.cs](#usuariocontrollercs)
- [Json](#json)
  - [JsonUsuario](#jsonusuario)
  - [JsonInventario](#jsoninventario)

## Clases

### Usuario.cs

Usuario.cs is a class within the ClassLibrary1 class that represents the attributes that the inventory class must have in the project.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.PostgreDataStruct
{
      public class InventarioSQL
    {
        public int? id { get; set; }
        public string? codigo { get; set; }
        public string? nombre { get; set; }
        public string? proveedor { get; set; }
    }
}
```
- id: ID is a unique attribute to identify each inventory item of the application who represents the identifier.
- codigo: A string representing the code of the inventory item.
- nombre: A string representing the name of the inventory item.
- proveedor: A string that represent the supplier of the inventory item of the application.

The design of the class is that it has the same attributes as the SQL table to be able to link the API and the application later.

### Users.cs

Users.cs This class named users represents user entities in a system.

```csharp
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


```

- idUser: id user is an attribute that is used to identify each user of an application or system.
- Usuario: A string representing the username of the user of the application
- pass: A string representing the password of the user of the application
- email: A string representing the email of the user of the application
- Administrador: An integer that represente if the user is an administrator.
- Manager: An integer that represente if the user is an manager.
- idNegocio: An integer that represente the id of the negocio of the user
- validated: An integer that represente if the user is or not validated

The design of the class is that it has the same attributes as the SQL table to be able to link the API and the application later.

## Controllers

### InventarioController.cs

This class, InventarioController, is a controller in an ASP.NET Core Web API.

- The methods (GetInventario, GetItemByCode, ModifyItem, DeleteItem) interact with the methods of InventarioController (Get, Post, Update and Delete) to connect the previous class with the application
- The class is configured as an API controller with routing information specified.
- Dependency injection is used to inject a logger (ILogger<InventarioController>) into the controller.

```csharp
﻿using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.PostgreDataStruct;
using ClassLibrary1;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly ILogger<InventarioController> _logger;

        public InventarioController(ILogger<InventarioController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public IList<InventarioSQL> Put([FromBody] InventarioSQL ini)
        {
            PostgreeCon Con = new PostgreeCon();
            Con.IniciarCon();
            var y = new ClassLibrary1.Class1();
            // y.connect(true);

            var t = y.GetInventario(ini);

            return t;
        }

        [HttpDelete]
        public Inventario Delete([FromBody] Inventario ini)
        {
            PostgreeCon Con = new PostgreeCon();
            Con.IniciarCon();
            var y = new ClassLibrary1.Class1();
            // y.connect(true);

            try
            {
                // Elimina el inventario de la tabla de inventario
                y.DeleteItem(ini.nombre);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el inventario: {ex.Message}");
            }

            return ini;
        }

        [HttpGet]
        public IList<InventarioSQL> GetInventarioByInventario(InventarioSQL inv)
        {
            try
            {
                PostgreeCon Con = new PostgreeCon();
                Con.IniciarCon();

                var y = new ClassLibrary1.Class1();
                // y.connect(true);

                var inventariosList = y.GetInventario(inv);

                return inventariosList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los inventarios: {ex.Message}");
            }
        }

        [HttpPost]
        public InventarioSQL Post([FromBody] InventarioSQL ini)
        {
            try
            {
                var y = new ClassLibrary1.Class1();
                // y.connect(true);

                // Modificar el inventario
                y.ModifyItem(ini);

                return ini;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar el inventario: {ex.Message}");
            }
        }
    }
}

```
- HTTP GET requests is the responsible of retrieving information from the inventory.
- HTTP PUT requests is the responsible of updatign information in the inventory.
- HTTP POST requests is the responsible of creating new items in the inventory.
- HTTP DELETE requests is the responsible of deleting items from the inventory.

### UsuarioController.cs

This class, UsuarioController, is a controller in an ASP.NET Core Web API.

- As the previous class, the methods of Class 1 interact with the methods of UsuarioController to connect the previous class with the application.
- The class is configured as an API controller with routing information specified.
- Dependency injection is used to inject a logger (ILogger<UsuarioController>) into the controller.

```csharp
﻿using ClassLibrary1;
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

```

- HTTP GET requests is the responsible of retrieving information from the user.
- HTTP PUT requests is the responsible of updating information in the user.
- HTTP POST requests is the responsible of creating new items in the user.
- HTTP DELETE requests and is used to delete items from the user.

## Json

json is a class that represents the data that the Api return as

### JsonUsuario

jsonusuario  is a lightweight data interchange format commonly used for communication between systems on the web.

```post json
{
  "status": 0,
  "message": "string",
  "idusers": 0,
  "usuario": "string",
  "pass": "string",
  "email": "string",
  "administrador": 0,
  "manager": 0,
  "idNegocio": 0,
  "validated": 0,
  "usuarioPost": {
    "id": 0,
    "email": "string",
    "apodos": "string",
    "imagenusuario": {
      "imgbase": "string",
      "cabezera": "string"
    }
  }
}
```
- code: 200
- description: Sucess

```put json
{
  "status": 0,
  "message": "string",
  "idusers": 0,
  "usuario": "string",
  "pass": "string",
  "email": "string",
  "administrador": 0,
  "manager": 0,
  "idnegocio": 0,
  "validated": 0,
  "usPostGree": {
    "id": 0,
    "email": "string",
    "apodos": "string",
    "imagenusuario": {
      "imgbase": "string",
      "cabezera": "string"
    }
  }
}
```
- code: 200
- description: Sucess

```delete json
{
  "status": 0,
  "message": "string",
  "idusers": 0,
  "usuario": "string",
  "pass": "string",
  "email": "string",
  "administrador": 0,
  "manager": 0,
  "idnegocio": 0,
  "validated": 0,
  "usPostGree": {
    "id": 0,
    "email": "string",
    "apodos": "string",
    "imagenusuario": {
      "imgbase": "string",
      "cabezera": "string"
    }
  }
}
```
- code: 200
- description: Sucess

```get json
{
  "status": 0,
  "message": "string",
  "idusers": 0,
  "usuario": "string",
  "pass": "string",
  "email": "string",
  "administrador": 0,
  "manager": 0,
  "idnegocio": 0,
  "validated": 0,
  "usPostGree": {
    "id": 0,
    "email": "string",
    "apodos": "string",
    "imagenusuario": {
      "imgbase": "string",
      "cabezera": "string"
    }
  }
}
```
- code: 200
- description: Sucess

In these class we see the json that every method return as

###JsonInventario

jsoninventario is a class that represente the data that the methods get, post, update and delete return as

```post json
{
  "id": 0,
  "codigo": "string",
  "nombre": "string",
  "proveedor": "string"
}
```
- code: 200
- description: Sucess

```put json
{
  "id": 0,
  "codigo": "string",
  "nombre": "string",
  "proveedor": "string"
}
```
- code: 200
- description: Sucess

```delete json
{
  "id": 0,
  "codigo": "string",
  "nombre": "string",
  "proveedor": {
    "nombreprov": "string",
    "pais": "string",
    "cif": "string",
    "volumencompra": "string"
  }
}
```
- code: 200
- description: Sucess

```get json
{
  "id": 0,
  "codigo": "string",
  "nombre": "string",
  "proveedor": "string"
}
```
- code: 200
- description: Sucess

In these class we see the json that every method return as

---

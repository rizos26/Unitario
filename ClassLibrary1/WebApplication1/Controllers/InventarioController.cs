using Microsoft.AspNetCore.Mvc;
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

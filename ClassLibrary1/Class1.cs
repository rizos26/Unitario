using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ClassLibrary1.PostgreDataStruct;

namespace ClassLibrary1
{
    public class Class1
    {
        public static SqlConnection? connection { get; set; }

        public void connect()
        {
            connection = new SqlConnection();
            connection.ConnectionString = "Initial Catalog=users;User ID=irene;Password=irene2000;Data Source=RIZOS";
            connection.Open();
        }

        public void connect(bool t)
        {
            connection = new SqlConnection();
            connection.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=users;Data Source=RIZOS;";
            connection.Open();
        }

        public DataSet TestDB()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM usuario", connection);
            adapter.Fill(ds);
            return ds;
        }

        public DataSet queryGenericStored(string query, List<KeyValuePair<string, dynamic>> parameters = null)
        {
            DataSet dt = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Clear();
            if (parameters != null)
            {
                foreach (KeyValuePair<string, dynamic> param in parameters)
                {
                    AddParameter(ref adapter, param);
                }
            }
            adapter.Fill(dt);
            return dt;
        }

        public void AddParameter(ref SqlDataAdapter sel, KeyValuePair<string, dynamic> val)
        {
            if (val.Value != null)
            {
                sel.SelectCommand.Parameters.AddWithValue(val.Key, val.Value);
            }
        }

        public IList<users> GetUsers(users user_to_search)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@usuario", user_to_search.usuario));
            userparam.Add(new KeyValuePair<string, dynamic>("@pass", user_to_search.pass));
            userparam.Add(new KeyValuePair<string, dynamic>("@email", user_to_search.email));
            userparam.Add(new KeyValuePair<string, dynamic>("@administrador", user_to_search.administrador));
            userparam.Add(new KeyValuePair<string, dynamic>("@manager", user_to_search.manager));
            userparam.Add(new KeyValuePair<string, dynamic>("@idNegocio", user_to_search.idNegocio));
            userparam.Add(new KeyValuePair<string, dynamic>("@validated", user_to_search.validated));
            DataSet dt = queryGenericStored("svp_usuario_create", userparam);
            IList<users> items = dt.Tables[0].AsEnumerable().Select(row =>
            new users
            {
                usuario = row.Field<string>("usuario"),
                pass = row.Field<string>("pass"),
                email = row.Field<string>("email"),
                administrador = row.Field<int?>("administrador"),
                manager = row.Field<int?>("manager"),
                idNegocio = row.Field<int?>("idNegocio"),
                validated = row.Field<int?>("validated"),
            }).ToList();
            return items;
        }

        public IList<InventarioSQL> GetInventario(InventarioSQL item_to_search)
        {
            List<KeyValuePair<string, dynamic>> itemParams = new List<KeyValuePair<string, dynamic>>();
            itemParams.Add(new KeyValuePair<string, dynamic>("@id", item_to_search.id));
            itemParams.Add(new KeyValuePair<string, dynamic>("@codigo", item_to_search.codigo));
            itemParams.Add(new KeyValuePair<string, dynamic>("@nombre", item_to_search.nombre));
            itemParams.Add(new KeyValuePair<string, dynamic>("@proveedor", item_to_search.proveedor));
            DataSet ds = queryGenericStored("usp_GetInventario", itemParams);
            IList<InventarioSQL> items = ds.Tables[0].AsEnumerable().Select(row =>
                new InventarioSQL
                {
                    id = row.Field<int?>("id"),
                    codigo = row.Field<string?>("codigo"),
                    nombre = row.Field<string?>("nombre"),
                    proveedor = row.Field<string?>("proveedor")
                }).ToList();
            return items;
        }

        public void DeleteUser(string username)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@usuario", username));
            queryGenericStored("svp_usuarios_eliminar", parameters);
        }

        public void DeleteItem(string codigo)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@codigo", codigo));
            queryGenericStored("usp_DeleteItem", parameters);
        }

        public void ModifyUser(users user)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@usuario", user.usuario));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevopass", user.pass));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevoemail", user.email));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevoadministrador", user.administrador));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevomanager", user.manager));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevoidnegocio", user.idNegocio));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevovalidated", user.validated));
            queryGenericStored("svp_usuario_modify", parameters);
        }

        public void ModifyItem(InventarioSQL item)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@codigo", item.codigo));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevonombre", item.nombre));
            parameters.Add(new KeyValuePair<string, dynamic>("@nuevoproveedor", item.proveedor));
            queryGenericStored("usp_ModifyItem", parameters);
        }

        public IList<users> GetUserByUsername(string username)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@usuario", username));
            DataSet ds = queryGenericStored("svp_usuarios_consultar", parameters);
            IList<users> items = ds.Tables[0].AsEnumerable().Select(row =>
                new users
                {
                    usuario = row.Field<string?>("usuario"),
                    pass = row.Field<string?>("pass"),
                    email = row.Field<string?>("email"),
                    administrador = row.Field<int?>("administrador"),
                    manager = row.Field<int?>("manager"),
                    idNegocio = row.Field<int?>("idnegocio"),
                    validated = row.Field<int?>("validated"),
                }).ToList();
            return items;
        }

        public IList<InventarioSQL> GetItemByCode(string codigo)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            parameters.Add(new KeyValuePair<string, dynamic>("@codigo", codigo));
            DataSet ds = queryGenericStored("usp_GetItemByCode", parameters);
            IList<InventarioSQL> items = ds.Tables[0].AsEnumerable().Select(row =>
                new InventarioSQL
                {
                    id = row.Field<int?>("id"),
                    codigo = row.Field<string?>("codigo"),
                    nombre = row.Field<string?>("nombre"),
                    proveedor = row.Field<string?>("proveedor")
                }).ToList();
            return items;
        }
    }
}

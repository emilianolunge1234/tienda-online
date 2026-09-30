using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tienda_Online
{
    public partial class Baja : Page
    {
        private const string SqlListado =
            "SELECT p.idProducto, " +
            "p.nombre, " +
            "p.precio, " +
            "c.descripcion AS categoria " +
            "FROM productos p " +
            "INNER JOIN categorias c ON c.idCategoria = p.idCategoria " +
            "ORDER BY p.idProducto";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListado();
            }
        }

        private void CargarListado()
        {
            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(SqlListado, conexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                gvProductos.DataSource = tabla;
                gvProductos.DataBind();
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!string.Equals(e.CommandName, "Eliminar", StringComparison.Ordinal))
            {
                return;
            }

            int idProducto;
            if (!int.TryParse(e.CommandArgument.ToString(), out idProducto))
            {
                Mostrar("No se pudo identificar el producto a eliminar.", false);
                return;
            }

            const string sql = "DELETE FROM productos WHERE idProducto = @idProducto";

            int filas;

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                SqlParameter parametro = new SqlParameter("@idProducto", SqlDbType.Int);
                parametro.Value = idProducto;
                comando.Parameters.Add(parametro);

                filas = comando.ExecuteNonQuery();
            }

            if (filas == 1)
            {
                Mostrar("Producto eliminado correctamente.", true);
                CargarListado();
            }
            else
            {
                Mostrar("No se encontró el producto a eliminar.", false);
            }
        }

        private void Mostrar(string texto, bool esExito)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = esExito ? "mensaje mensaje--ok" : "mensaje mensaje--error";
        }
    }
}

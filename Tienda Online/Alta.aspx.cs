using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Tienda_Online
{
    public partial class Alta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        /// <summary>
        /// Llena el DropDownList con las categorías existentes usando
        /// SqlConnection + SqlCommand + SqlDataAdapter (ADO.NET explícito).
        /// </summary>
        private void CargarCategorias()
        {
            const string sql = "SELECT idCategoria, descripcion FROM categorias ORDER BY descripcion";

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                ddlCategoria.DataSource = tabla;
                ddlCategoria.DataTextField = "descripcion";
                ddlCategoria.DataValueField = "idCategoria";
                ddlCategoria.DataBind();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            decimal precio;
            if (!Utilidades.TryParsePrecio(txtPrecio.Text.Trim(), out precio))
            {
                MostrarMensaje("El precio no es un número válido. Ejemplos válidos: 1250, 1250,50 o 1250.50", false);
                return;
            }

            int idCategoria;
            if (!int.TryParse(ddlCategoria.SelectedValue, out idCategoria))
            {
                MostrarMensaje("Tenés que elegir una categoría.", false);
                return;
            }

            const string sql = "INSERT INTO productos (nombre, precio, idCategoria) VALUES (@nombre, @precio, @idCategoria)";

            int filas;

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                SqlParameter parametroNombre = new SqlParameter("@nombre", SqlDbType.VarChar, 100);
                parametroNombre.Value = txtNombre.Text.Trim();
                comando.Parameters.Add(parametroNombre);

                // DECIMAL(10,2): sin Scale = 2 se truncan los decimales.
                SqlParameter parametroPrecio = new SqlParameter("@precio", SqlDbType.Decimal);
                parametroPrecio.Precision = 10;
                parametroPrecio.Scale = 2;
                parametroPrecio.Value = precio;
                comando.Parameters.Add(parametroPrecio);

                SqlParameter parametroCategoria = new SqlParameter("@idCategoria", SqlDbType.Int);
                parametroCategoria.Value = idCategoria;
                comando.Parameters.Add(parametroCategoria);

                filas = comando.ExecuteNonQuery();
            }

            if (filas == 1)
            {
                MostrarMensaje("Producto cargado correctamente.", true);
                LimpiarFormulario();
            }
            else
            {
                MostrarMensaje("No se pudo guardar el producto.", false);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            ddlCategoria.ClearSelection();
            ddlCategoria.SelectedIndex = 0;
        }

        private void MostrarMensaje(string texto, bool esExito)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = esExito ? "mensaje mensaje--ok" : "mensaje mensaje--error";
        }
    }
}

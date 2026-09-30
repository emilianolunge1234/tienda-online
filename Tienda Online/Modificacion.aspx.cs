using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Tienda_Online
{
    public partial class Modificacion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListaProductos();
                CargarCategorias();
            }
        }

        /// <summary>
        /// Llena el DropDownList de productos con su categoría (JOIN) usando
        /// SqlConnection + SqlCommand + SqlDataAdapter (ADO.NET explícito).
        /// </summary>
        private void CargarListaProductos()
        {
            const string sql = "SELECT p.idProducto, " +
                               "p.nombre + ' - ' + c.descripcion AS descripcionProducto " +
                               "FROM productos p " +
                               "INNER JOIN categorias c ON c.idCategoria = p.idCategoria " +
                               "ORDER BY p.nombre";

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                ddlProducto.DataSource = tabla;
                ddlProducto.DataTextField = "descripcionProducto";
                ddlProducto.DataValueField = "idProducto";
                ddlProducto.DataBind();
            }
        }

        /// <summary>
        /// Llena el DropDownList de categorías para poder cambiar la asignación.
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

        /// <summary>
        /// Trae los datos actuales del producto elegido y los deja en el formulario.
        /// Guardamos una copia de los valores originales en ViewState para poder
        /// comprobar después si el usuario modificó algo.
        /// </summary>
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            int idProducto;
            if (!int.TryParse(ddlProducto.SelectedValue, out idProducto))
            {
                Mostrar("No se encontró el producto seleccionado.", false);
                return;
            }

            const string sql = "SELECT nombre, precio, idCategoria FROM productos WHERE idProducto = @idProducto";

            bool encontrado = false;
            string nombre = string.Empty;
            decimal precio = 0m;
            int idCategoria = 0;

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                SqlParameter parametro = new SqlParameter("@idProducto", SqlDbType.Int);
                parametro.Value = idProducto;
                comando.Parameters.Add(parametro);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        encontrado = true;
                        nombre = Convert.ToString(lector["nombre"]).Trim();
                        precio = Convert.ToDecimal(lector["precio"]);
                        idCategoria = Convert.ToInt32(lector["idCategoria"]);
                    }
                }
            }

            if (!encontrado)
            {
                Mostrar("No se encontró el producto seleccionado.", false);
                return;
            }

            txtNombre.Text = nombre;
            txtPrecio.Text = Utilidades.FormatearPrecio(precio);
            ddlCategoria.SelectedValue = idCategoria.ToString();

            ViewState["OriginalNombre"] = txtNombre.Text;
            ViewState["OriginalPrecio"] = precio;
            ViewState["OriginalCategoria"] = ddlCategoria.SelectedValue;

            Mostrar("Datos cargados. Editá los campos y presioná Guardar cambios.", true);
        }

        /// <summary>
        /// Guarda los cambios, siempre y cuando el usuario haya modificado
        /// al menos un campo respecto de los valores originales.
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (ViewState["OriginalNombre"] == null)
            {
                Mostrar("Primero tenés que elegir un producto y presionar Cargar datos.", false);
                return;
            }

            decimal precio;
            if (!Utilidades.TryParsePrecio(txtPrecio.Text.Trim(), out precio))
            {
                Mostrar("El precio no es un número válido. Ejemplos: 1250, 1250,50 o 1250.50", false);
                return;
            }

            string nombreOriginal = Convert.ToString(ViewState["OriginalNombre"]).Trim();
            decimal precioOriginal = Convert.ToDecimal(ViewState["OriginalPrecio"]);
            string categoriaOriginal = Convert.ToString(ViewState["OriginalCategoria"]);

            string nombreNuevo = txtNombre.Text.Trim();

            bool cambioNombre = nombreNuevo != nombreOriginal;
            bool cambioPrecio = precio != precioOriginal;
            bool cambioCategoria = ddlCategoria.SelectedValue != categoriaOriginal;

            if (!cambioNombre && !cambioPrecio && !cambioCategoria)
            {
                Mostrar("No modificaste ningún campo: no hay nada para guardar.", false);
                return;
            }

            int idProducto;
            if (!int.TryParse(ddlProducto.SelectedValue, out idProducto))
            {
                Mostrar("No se encontró el producto seleccionado.", false);
                return;
            }

            int idCategoria;
            if (!int.TryParse(ddlCategoria.SelectedValue, out idCategoria))
            {
                Mostrar("Tenés que elegir una categoría.", false);
                return;
            }

            const string sql = "UPDATE productos SET nombre = @nombre, precio = @precio, idCategoria = @idCategoria WHERE idProducto = @idProducto";

            int filas;

            using (SqlConnection conexion = Utilidades.AbrirConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                SqlParameter parametroNombre = new SqlParameter("@nombre", SqlDbType.VarChar, 100);
                parametroNombre.Value = nombreNuevo;
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

                SqlParameter parametroId = new SqlParameter("@idProducto", SqlDbType.Int);
                parametroId.Value = idProducto;
                comando.Parameters.Add(parametroId);

                filas = comando.ExecuteNonQuery();
            }

            if (filas == 1)
            {
                // Los originales ahora pasan a ser los valores recién guardados,
                // así la próxima validación parte desde el estado actual.
                ViewState["OriginalNombre"] = nombreNuevo;
                ViewState["OriginalPrecio"] = precio;
                ViewState["OriginalCategoria"] = ddlCategoria.SelectedValue;

                Mostrar("Producto modificado correctamente.", true);
            }
            else
            {
                Mostrar("No se pudo actualizar el producto.", false);
            }
        }

        private void Mostrar(string texto, bool esExito)
        {
            lblMensaje.Text = texto;
            lblMensaje.CssClass = esExito ? "mensaje mensaje--ok" : "mensaje mensaje--error";
        }
    }
}

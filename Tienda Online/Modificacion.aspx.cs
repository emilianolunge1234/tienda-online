using System;
using System.Data;
using System.Web.UI;

namespace Tienda_Online
{
    public partial class Modificacion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            DataView filas = (DataView)dsProducto.Select(DataSourceSelectArguments.Empty);

            if (filas == null || filas.Count == 0)
            {
                Mostrar("No se encontró el producto seleccionado.", false);
                return;
            }

            DataRowView fila = filas[0];
            decimal precio = Convert.ToDecimal(fila["precio"]);

            txtNombre.Text = Convert.ToString(fila["nombre"]).Trim();
            txtPrecio.Text = Utilidades.FormatearPrecio(precio);
            ddlCategoria.SelectedValue = Convert.ToInt32(fila["idCategoria"]).ToString();

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

            dsActualizar.UpdateParameters["nombre"].DefaultValue = nombreNuevo;
            dsActualizar.UpdateParameters["precio"].DefaultValue = Utilidades.FormatearPrecio(precio);
            dsActualizar.UpdateParameters["idCategoria"].DefaultValue = ddlCategoria.SelectedValue;
            dsActualizar.UpdateParameters["idProducto"].DefaultValue = ddlProducto.SelectedValue;

            int filas = dsActualizar.Update();

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

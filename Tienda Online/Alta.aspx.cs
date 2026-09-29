using System;
using System.Web.UI;

namespace Tienda_Online
{
    public partial class Alta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            // El SqlDbType Decimal del parámetro convierte usando la cultura actual,
            // por eso serializamos el precio con esa misma cultura.
            dsProductos.InsertParameters["nombre"].DefaultValue = txtNombre.Text.Trim();
            dsProductos.InsertParameters["precio"].DefaultValue = Utilidades.FormatearPrecio(precio);
            dsProductos.InsertParameters["idCategoria"].DefaultValue = ddlCategoria.SelectedValue;

            int filas = dsProductos.Insert();

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

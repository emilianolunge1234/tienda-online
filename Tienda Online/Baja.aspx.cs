using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tienda_Online
{
    public partial class Baja : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Se dispara cuando se hace clic en "Eliminar" de una fila.
        /// Borra únicamente esa fila de la tabla productos.
        /// </summary>
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

            dsEliminar.DeleteParameters["idProducto"].DefaultValue = idProducto.ToString();

            int filas = dsEliminar.Delete();

            if (filas == 1)
            {
                Mostrar("Producto eliminado correctamente. Las categorías no se modificaron.", true);
                // El SqlDataSource no refresca la grilla solo: hay que volver a enlazarla.
                gvProductos.DataBind();
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

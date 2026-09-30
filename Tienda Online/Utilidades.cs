using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace Tienda_Online
{
    /// <summary>
    /// Utilidades compartidas entre los formularios.
    /// </summary>
    public static class Utilidades
    {
        /// <summary>
        /// Convierte el texto del usuario en decimal sin importar qué separador usó.
        /// Si hay coma, es el separador decimal de la cultura local (es-ES: 1250,50).
        /// Si no hay coma, lo tomamos como formato invariante (1250.50 o 1250).
        /// Sin esto, "1250.50" en es-ES se leería como 125050 (el punto es de miles).
        /// </summary>
        public static bool TryParsePrecio(string texto, out decimal precio)
        {
            texto = texto ?? string.Empty;

            NumberStyles estilos = NumberStyles.Number;
            CultureInfo cultura = texto.Contains(",")
                ? CultureInfo.CurrentCulture
                : CultureInfo.InvariantCulture;

            return decimal.TryParse(texto, estilos, cultura, out precio);
        }

        /// <summary>
        /// Serializa el precio con la cultura actual (es-ES: 25.999,50) para
        /// mostrarlo en los formularios. El guardado no pasa por acá: el
        /// parámetro SqlParameter recibe el decimal directamente.
        /// </summary>
        public static string FormatearPrecio(decimal precio)
        {
            return precio.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Abre y devuelve una conexión a la base TiendaOnline.
        /// La conexión sale ya abierta: quien la usa debe envolverla en un bloque
        /// using para que se cierre (y se devuelva al pool) automáticamente.
        /// </summary>
        public static SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TiendaOnlineDB"].ConnectionString);
            conexion.Open();
            return conexion;
        }
    }
}

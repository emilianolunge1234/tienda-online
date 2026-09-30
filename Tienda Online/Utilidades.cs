using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace Tienda_Online
{
    public static class Utilidades
    {
        public static bool TryParsePrecio(string texto, out decimal precio)
        {
            texto = texto ?? string.Empty;

            NumberStyles estilos = NumberStyles.Number;
            CultureInfo cultura = texto.Contains(",")
                ? CultureInfo.CurrentCulture
                : CultureInfo.InvariantCulture;

            return decimal.TryParse(texto, estilos, cultura, out precio);
        }

        public static string FormatearPrecio(decimal precio)
        {
            return precio.ToString(CultureInfo.CurrentCulture);
        }

        public static SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TiendaOnlineDB"].ConnectionString);
            conexion.Open();
            return conexion;
        }
    }
}

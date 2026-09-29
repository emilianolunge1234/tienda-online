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
        /// Serializa el precio con la misma cultura que usa el parámetro Type="Decimal"
        /// del SqlDataSource, para que lo lea igual al guardar.
        /// </summary>
        public static string FormatearPrecio(decimal precio)
        {
            return precio.ToString(CultureInfo.CurrentCulture);
        }
    }
}

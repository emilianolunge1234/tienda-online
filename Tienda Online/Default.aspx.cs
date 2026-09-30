using System;
using System.IO;
using System.Web.UI;

namespace Tienda_Online
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ruta = Server.MapPath(".") + "/contador.txt";

                if (File.Exists(ruta))
                {
                    StreamReader archLectura = new StreamReader(ruta);
                    string valor = archLectura.ReadToEnd();
                    archLectura.Close();

                    int contador = int.Parse(valor);
                    contador++;

                    StreamWriter archEscritura = new StreamWriter(ruta);
                    archEscritura.WriteLine(contador.ToString());
                    archEscritura.Close();

                    lblVisitas.Text = contador.ToString();
                }
                else
                {
                    StreamWriter archEscritura = new StreamWriter(ruta);
                    archEscritura.WriteLine("1");
                    archEscritura.Close();

                    lblVisitas.Text = "1";
                }
            }
        }
    }
}
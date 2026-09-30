<%@ Page Title="Consulta de productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="Tienda_Online.Consulta" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="pagina" aria-labelledby="tituloConsulta">
            <h1 id="tituloConsulta">Consulta de productos</h1>
            <p class="lead">Listado completo de productos junto a su categoría correspondiente.</p>

            <div class="acciones">
                <asp:HyperLink ID="lnkVolver" runat="server"
                    NavigateUrl="~/Default.aspx"
                    CssClass="btn btn--secundario">&laquo; Volver a la página principal</asp:HyperLink>
            </div>

            <asp:GridView ID="gvProductos" runat="server"
                DataSourceID="dsProductos"
                CssClass="grilla"
                AutoGenerateColumns="False"
                AllowPaging="False"
                AllowSorting="False"
                EmptyDataText="No hay productos cargados.">
                <Columns>
                    <asp:BoundField DataField="idProducto" HeaderText="Código" ReadOnly="True" SortExpression="idProducto" />
                    <asp:BoundField DataField="nombre" HeaderText="Producto" SortExpression="nombre" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:N2}" SortExpression="precio" />
                    <asp:BoundField DataField="categoria" HeaderText="Categoría" SortExpression="categoria" />
                </Columns>
                <EmptyDataTemplate>
                    <p class="mensaje mensaje--error">No hay productos cargados.</p>
                </EmptyDataTemplate>
            </asp:GridView>

            <p class="nota">
                <strong>Nota:</strong> Los datos se presentan unificados, asociando cada producto con la información completa de su categoría.
            </p>
        </section>

        <asp:SqlDataSource ID="dsProductos" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            SelectCommand="SELECT p.idProducto,
                                  p.nombre,
                                  p.precio,
                                  c.descripcion AS categoria
                           FROM productos p
                           INNER JOIN categorias c ON c.idCategoria = p.idCategoria
                           ORDER BY p.idProducto" />

    </main>

</asp:Content>

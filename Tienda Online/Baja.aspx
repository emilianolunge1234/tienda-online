<%@ Page Title="Baja de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Baja.aspx.cs" Inherits="Tienda_Online.Baja" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="pagina" aria-labelledby="tituloBaja">
            <h1 id="tituloBaja">Baja de producto</h1>
            <p class="lead">Presioná <strong>Eliminar</strong> en la fila del producto y confirmá la acción en el mensaje que aparece.</p>

            <div class="acciones">
                <asp:HyperLink ID="lnkVolver" runat="server"
                    NavigateUrl="~/Default.aspx"
                    CssClass="btn btn--secundario">&laquo; Volver a la página principal</asp:HyperLink>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

            <asp:GridView ID="gvProductos" runat="server"
                DataSourceID="dsListado"
                DataKeyNames="idProducto"
                CssClass="grilla"
                AutoGenerateColumns="False"
                OnRowCommand="gvProductos_RowCommand"
                EmptyDataText="No hay productos cargados.">
                <Columns>
                    <asp:BoundField DataField="idProducto" HeaderText="Código" ReadOnly="True" />
                    <asp:BoundField DataField="nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEliminar" runat="server"
                                Text="Eliminar"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("idProducto") %>'
                                CssClass="btn btn--peligro"
                                OnClientClick="return confirm('¿Seguro que querés eliminar este producto? Esta acción no se puede deshacer.');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <p class="mensaje mensaje--error">No hay productos cargados.</p>
                </EmptyDataTemplate>
            </asp:GridView>

            <p class="nota">
                El <code>DELETE</code> afecta <strong>únicamente</strong> la fila de <code>productos</code>.
                La tabla <code>categorias</code> no se modifica: la clave foránea está definida sin
                acción en cascada, así que las categorías quedan intactas.
            </p>
        </section>

        <%-- Listado con JOIN para mostrar producto + categoría --%>
        <asp:SqlDataSource ID="dsListado" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            SelectCommand="SELECT p.idProducto,
                                  p.nombre,
                                  p.precio,
                                  c.descripcion AS categoria
                           FROM productos p
                           INNER JOIN categorias c ON c.idCategoria = p.idCategoria
                           ORDER BY p.idProducto" />

        <%-- Borra SOLO de productos. Sin cascada sobre categorias. --%>
        <asp:SqlDataSource ID="dsEliminar" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            DeleteCommand="DELETE FROM productos WHERE idProducto = @idProducto">
            <DeleteParameters>
                <asp:Parameter Name="idProducto" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>

    </main>

</asp:Content>

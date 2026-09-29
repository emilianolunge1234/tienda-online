<%@ Page Title="Modificación de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Modificacion.aspx.cs" Inherits="Tienda_Online.Modificacion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="pagina" aria-labelledby="tituloModificacion">
            <h1 id="tituloModificacion">Modificación de producto</h1>
            <p class="lead">Elegí un producto, presioná <strong>Cargar datos</strong>, editá los campos y luego <strong>Guardar cambios</strong>.</p>

            <div class="formulario">

                <div class="campo">
                    <asp:Label ID="lblProducto" runat="server" AssociatedControlID="ddlProducto" Text="Producto a modificar" />
                    <asp:DropDownList ID="ddlProducto" runat="server"
                        DataSourceID="dsProductosLista"
                        DataTextField="descripcionProducto"
                        DataValueField="idProducto"
                        AppendDataBoundItems="True">
                        <asp:ListItem Text="-- Seleccioná un producto --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvProducto" runat="server"
                        ControlToValidate="ddlProducto"
                        ValidationGroup="cargar"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* Elegí un producto." />
                </div>

                <div class="acciones">
                    <asp:Button ID="btnCargar" runat="server"
                        Text="Cargar datos"
                        CssClass="btn btn--secundario"
                        ValidationGroup="cargar"
                        OnClick="btnCargar_Click" />
                </div>

                <hr class="separador" />

                <div class="campo">
                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre" />
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                        ControlToValidate="txtNombre"
                        ValidationGroup="guardar"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* El nombre es obligatorio." />
                </div>

                <div class="campo">
                    <asp:Label ID="lblPrecio" runat="server" AssociatedControlID="txtPrecio" Text="Precio" />
                    <asp:TextBox ID="txtPrecio" runat="server" MaxLength="12" />
                    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        ValidationGroup="guardar"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* El precio es obligatorio." />
                    <asp:RegularExpressionValidator ID="revPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        ValidationExpression="^\d{1,7}([.,]\d{1,2})?$"
                        ValidationGroup="guardar"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* Formato inválido. Usá 1250 o 1250,50" />
                </div>

                <div class="campo">
                    <asp:Label ID="lblCategoria" runat="server" AssociatedControlID="ddlCategoria" Text="Categoría" />
                    <asp:DropDownList ID="ddlCategoria" runat="server"
                        DataSourceID="dsCategorias"
                        DataTextField="descripcion"
                        DataValueField="idCategoria"
                        AppendDataBoundItems="True">
                        <asp:ListItem Text="-- Seleccioná una categoría --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server"
                        ControlToValidate="ddlCategoria"
                        ValidationGroup="guardar"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* Tenés que elegir una categoría." />
                </div>

                <div class="acciones">
                    <asp:Button ID="btnGuardar" runat="server"
                        Text="Guardar cambios"
                        CssClass="btn btn--primario"
                        ValidationGroup="guardar"
                        OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID="lnkVolver" runat="server"
                        NavigateUrl="~/Default.aspx"
                        CssClass="btn btn--secundario">Volver a la página principal</asp:HyperLink>
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

            </div>
        </section>

        <%-- Lista desplegable: producto con su categoría (JOIN) --%>
        <asp:SqlDataSource ID="dsProductosLista" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            SelectCommand="SELECT p.idProducto,
                                  p.nombre + ' - ' + c.descripcion AS descripcionProducto
                           FROM productos p
                           INNER JOIN categorias c ON c.idCategoria = p.idCategoria
                           ORDER BY p.nombre" />

        <%-- Devuelve los valores actuales del producto elegido --%>
        <asp:SqlDataSource ID="dsProducto" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            SelectCommand="SELECT nombre, precio, idCategoria
                           FROM productos
                           WHERE idProducto = @idProducto">
            <SelectParameters>
                <asp:ControlParameter Name="idProducto" ControlID="ddlProducto"
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

        <%-- Categorías para poder cambiar la asignación --%>
        <asp:SqlDataSource ID="dsCategorias" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            SelectCommand="SELECT idCategoria, descripcion FROM categorias ORDER BY descripcion" />

        <%-- UPDATE del producto --%>
        <asp:SqlDataSource ID="dsActualizar" runat="server"
            ConnectionString="<%$ ConnectionStrings:TiendaOnlineDB %>"
            UpdateCommand="UPDATE productos
                              SET nombre = @nombre,
                                  precio = @precio,
                                  idCategoria = @idCategoria
                            WHERE idProducto = @idProducto">
            <UpdateParameters>
                <asp:Parameter Name="nombre" Type="String" />
                <asp:Parameter Name="precio" Type="Decimal" />
                <asp:Parameter Name="idCategoria" Type="Int32" />
                <asp:Parameter Name="idProducto" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>

    </main>

</asp:Content>

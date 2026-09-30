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
                <strong>Atención:</strong> La eliminación es definitiva y afecta exclusivamente al producto seleccionado. Las categorías del catálogo permanecen intactas.
            </p>
        </section>

    </main>

</asp:Content>

<%@ Page Title="Alta de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alta.aspx.cs" Inherits="Tienda_Online.Alta" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="pagina" aria-labelledby="tituloAlta">
            <h1 id="tituloAlta">Alta de producto</h1>
            <p class="lead">Completá los datos del producto y presioná <strong>Confirmar</strong>.</p>

            <div class="formulario">

                <div class="campo">
                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre" />
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" placeholder="Ej: Auriculares inalámbricos" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                        ControlToValidate="txtNombre"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* El nombre es obligatorio." />
                </div>

                <div class="campo">
                    <asp:Label ID="lblPrecio" runat="server" AssociatedControlID="txtPrecio" Text="Precio" />
                    <asp:TextBox ID="txtPrecio" runat="server" MaxLength="12" placeholder="Ej: 25999,50" />
                    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* El precio es obligatorio." />
                    <asp:RegularExpressionValidator ID="revPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        ValidationExpression="^\d{1,7}([.,]\d{1,2})?$"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* Formato inválido. Usá 1250 o 1250,50" />
                </div>

                <div class="campo">
                    <asp:Label ID="lblCategoria" runat="server" AssociatedControlID="ddlCategoria" Text="Categoría" />
                    <asp:DropDownList ID="ddlCategoria" runat="server"
                        DataTextField="descripcion"
                        DataValueField="idCategoria"
                        AppendDataBoundItems="True">
                        <asp:ListItem Text="-- Seleccioná una categoría --" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server"
                        ControlToValidate="ddlCategoria"
                        CssClass="validacion" Display="Dynamic"
                        ErrorMessage="* Tenés que elegir una categoría." />
                </div>

                <div class="acciones">
                    <asp:Button ID="btnConfirmar" runat="server"
                        Text="Confirmar"
                        CssClass="btn btn--primario"
                        OnClick="btnConfirmar_Click" />
                    <asp:HyperLink ID="lnkVolver" runat="server"
                        NavigateUrl="~/Default.aspx"
                        CssClass="btn btn--secundario">Volver a la página principal</asp:HyperLink>
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

            </div>
        </section>

    </main>

</asp:Content>

<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tienda_Online._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="hero" aria-labelledby="tituloPrincipal">
            <h1 id="tituloPrincipal">Tienda Online</h1>
            <p class="lead">Administración de productos y categorías. Elegí una operación para comenzar.</p>
        </section>

        <div class="menu-grid">

            <asp:HyperLink ID="lnkAlta" runat="server" NavigateUrl="~/Alta.aspx" CssClass="menu-card menu-card--alta">
                <span class="menu-card__icon">＋</span>
                <span class="menu-card__titulo">Alta de producto</span>
                <span class="menu-card__desc">Cargar un nuevo producto en la base de datos.</span>
                <span class="menu-card__accion">Ir al formulario &raquo;</span>
            </asp:HyperLink>

            <asp:HyperLink ID="lnkConsulta" runat="server" NavigateUrl="~/Consulta.aspx" CssClass="menu-card menu-card--consulta">
                <span class="menu-card__icon">🔍</span>
                <span class="menu-card__titulo">Consulta de productos</span>
                <span class="menu-card__desc">Listar los productos junto a su categoría (JOIN).</span>
                <span class="menu-card__accion">Ir al formulario &raquo;</span>
            </asp:HyperLink>

            <asp:HyperLink ID="lnkModificacion" runat="server" NavigateUrl="~/Modificacion.aspx" CssClass="menu-card menu-card--modificacion">
                <span class="menu-card__icon">✎</span>
                <span class="menu-card__titulo">Modificación</span>
                <span class="menu-card__desc">Editar los datos de un producto existente.</span>
                <span class="menu-card__accion">Ir al formulario &raquo;</span>
            </asp:HyperLink>

            <asp:HyperLink ID="lnkBaja" runat="server" NavigateUrl="~/Baja.aspx" CssClass="menu-card menu-card--baja">
                <span class="menu-card__icon">✕</span>
                <span class="menu-card__titulo">Baja de producto</span>
                <span class="menu-card__desc">Eliminar un producto seleccionado.</span>
                <span class="menu-card__accion">Ir al formulario &raquo;</span>
            </asp:HyperLink>

        </div>
    </main>

</asp:Content>

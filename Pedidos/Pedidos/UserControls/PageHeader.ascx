<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageHeader.ascx.cs" Inherits="Pedidos.UserControls.PageHeader" %>

<header class="tema-cinza-escuro">
    <i class="fa fa-bars menu-resizer"></i>
    <img src="<%= ResolveUrl("~/Content/images/books8.png") %>" class="logo-app" />            
    <h1><%= Text %></h1>

    <div class="loading">
        <img src="<%= ResolveUrl("~/Content/images/ajax-loader.gif") %>" style="width:32px;height:32px;" />
    </div>
</header>
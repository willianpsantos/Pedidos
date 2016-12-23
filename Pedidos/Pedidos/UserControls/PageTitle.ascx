<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageTitle.ascx.cs" Inherits="Pedidos.UserControls.PageTitle" %>

<div class="title">
    <h1>
        <i class="<%= IconCssClass %>"></i> &nbsp;
        <%= Text %>
    </h1>

    <asp:ImageButton ID="btnAdd" 
                     runat="server" 
                     ImageUrl="~/Content/images/add.png" 
                     BorderStyle="None" 
                     CssClass="button-new right" 
                     ToolTip="ADICIONAR Novo Registro"                      
                     OnClick="btnAdd_Click" />
</div>
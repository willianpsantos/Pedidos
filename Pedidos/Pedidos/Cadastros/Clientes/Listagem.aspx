<%@ Page Title="Clientes | Listagem" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="Pedidos.Cadastros.Clientes.Listagem" Theme="GridTheme" %>
<%@ Register Src="~/UserControls/FilterBar.ascx" TagName="FilterBar" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/Lookup.ascx" TagName="Lookup" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">  
    <%: Scripts.Render("~/Cadastros/Clientes/list.js") %>

    <usc:PageTitle runat="server" Text="Listagem de Clientes" ShowButtonAdd="true" OnButtonAddClick="ButtonAdd_Click" />

    <usc:FilterBar ID="filteBarCliente" DataTextField="CLIRAZAOSOCIAL" DataValueField="CLICODIGO" runat="server" OnSearch="FilterBar_Search" OnClear="FilterBar_Clear">  
        <FilterByItems>
            <asp:ListItem Value="CLICODIGO" Text="Código" Selected="True"></asp:ListItem>
            <asp:ListItem Value="CLIRAZAOSOCIAL" Text="Nome / Razão Social" Selected="False"></asp:ListItem>
            <asp:ListItem Value="CLICNPJ" Text="CNPJ" Selected="False"></asp:ListItem>
            <asp:ListItem Value="CLICPF" Text="CPF" Selected="False"></asp:ListItem>
        </FilterByItems>      
    </usc:FilterBar>

    <asp:DataGrid ID="gridDados" SkinID="GridSkin" runat="server" OnPageIndexChanged="Grid_PageIndexChanged" AllowCustomPaging="True" AllowPaging="True" PageSize="50" AutoGenerateColumns="False" OnItemCommand="Grid_ItemCommand">
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSelect" runat="server" CommandName='<%# global_asax.COMMANDSELECT %>' CommandArgument='<%# Eval("CLICODIGO") %>' ImageUrl="~/Content/images/check.png" ToolTip="SELECIONAR este Cliente" style="margin-right:5px;" />                    
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:BoundColumn DataField="CLICODIGO" HeaderText="Código"></asp:BoundColumn>
            <asp:BoundColumn DataField="CLIRAZAOSOCIAL" HeaderText="Nome / Razão Social"></asp:BoundColumn>
            <asp:BoundColumn DataField="CLICNPJ" HeaderText="CNPJ"></asp:BoundColumn>
            <asp:BoundColumn DataField="CLICPF" HeaderText="CPF"></asp:BoundColumn>
            <asp:BoundColumn DataField="CLITELFONE" HeaderText="Telefone"></asp:BoundColumn>
            <asp:BoundColumn DataField="CLIDATACADASTRO" HeaderText="Data de Cadastro" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>

            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName='<%# global_asax.COMMANDEDIT %>' CommandArgument='<%# Eval("CLICODIGO") %>' ImageUrl="~/Content/images/edit.png" ToolTip="EDITAR este Cliente" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnDelete" runat="server" CommandName='<%# global_asax.COMMANDDELETE %>' OnClientClick="return confirmarExclusao();"  CommandArgument='<%# Eval("CLICODIGO") %>' ImageUrl="~/Content/images/delete.png" ToolTip="EXCLUIR este Cliente" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnView" runat="server" CommandName='<%# global_asax.COMMANDVIEW %>'  CommandArgument='<%# Eval("CLICODIGO") %>' ImageUrl="~/Content/images/view.png" ToolTip="VISUALIZAR este Cliente" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>        
    </asp:DataGrid>
</asp:Content>

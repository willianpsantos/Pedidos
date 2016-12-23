<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="Pedidos.Cadastros.Fornecedor.Listagem" Theme="GridTheme" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/UserControls/FilterBar.ascx" TagName="FilterBar" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/Lookup.ascx" TagName="Lookup" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">  
    <%: Scripts.Render("~/Cadastros/Fornecedor/list.js") %>

    <usc:PageTitle runat="server" Text="Listagem de Fornecedores" ShowButtonAdd="true" OnButtonAddClick="ButtonAdd_Click" />

    <usc:FilterBar ID="filteBarFornecedor" DataTextField="FORNOME" DataValueField="FORCODG" runat="server" OnSearch="FilterBar_Search" OnClear="FilterBar_Clear">  
        <FilterByItems>
            <asp:ListItem Value="FORCODG" Text="Código" Selected="True"></asp:ListItem>
            <asp:ListItem Value="FORNOME" Text="Nome / Razão Social" Selected="False"></asp:ListItem>
            <asp:ListItem Value="FORCNPJ" Text="CNPJ" Selected="False"></asp:ListItem>
        </FilterByItems>      
    </usc:FilterBar>

    <asp:DataGrid ID="gridDados" SkinID="GridSkin" runat="server" OnPageIndexChanged="Grid_PageIndexChanged" AllowCustomPaging="True" AllowPaging="True" PageSize="50" AutoGenerateColumns="False" OnItemCommand="Grid_ItemCommand">
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSelect" runat="server" CommandName='<%# global_asax.COMMANDSELECT %>' CommandArgument='<%# Eval("FORCODG") %>' ImageUrl="~/Content/images/check.png" ToolTip="SELECIONAR este Fornecedor" style="margin-right:5px;" />                    
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:BoundColumn DataField="FORCODG" HeaderText="Código"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORNOME" HeaderText="Nome / Razão Social"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORCNPJ" HeaderText="CNPJ"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORENDERECO" HeaderText="Endereço"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORBAIRRO" HeaderText="Bairro"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORCIDADE" HeaderText="Cidade"></asp:BoundColumn>
            <asp:BoundColumn DataField="FORTELEFONE" HeaderText="Telefone"></asp:BoundColumn>

            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName='<%# global_asax.COMMANDEDIT %>' CommandArgument='<%# Eval("FORCODG") %>' ImageUrl="~/Content/images/edit.png" ToolTip="EDITAR este Fornecedor" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnDelete" runat="server" CommandName='<%# global_asax.COMMANDDELETE %>' OnClientClick="return confirmarExclusao();"  CommandArgument='<%# Eval("FORCODG") %>' ImageUrl="~/Content/images/delete.png" ToolTip="EXCLUIR este Fornecedor" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnView" runat="server" CommandName='<%# global_asax.COMMANDVIEW %>'  CommandArgument='<%# Eval("FORCODG") %>' ImageUrl="~/Content/images/view.png" ToolTip="VISUALIZAR este Fornecedor" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>        
    </asp:DataGrid>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="Pedidos.Cadastros.Produto.Listagem" Theme="GridTheme"  MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/UserControls/FilterBar.ascx" TagName="FilterBar" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/Lookup.ascx" TagName="Lookup" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">  
    <%: Scripts.Render("~/Cadastros/Produto/list.js") %>

    <usc:PageTitle runat="server" Text="Listagem de Produtos" ShowButtonAdd="true" OnButtonAddClick="ButtonAdd_Click" />

    <usc:FilterBar ID="filteBarProduto" DataTextField="PRODESCRICAO" DataValueField="PROCODIGO" runat="server" OnSearch="FilterBar_Search" OnClear="FilterBar_Clear">  
        <FilterByItems>
            <asp:ListItem Value="PROCODIGO" Text="Código" Selected="True"></asp:ListItem>
            <asp:ListItem Value="PRODESCRICAO" Text="Nome / Razão Social" Selected="False"></asp:ListItem>
            <asp:ListItem Value="GRUCODIGO" Text="Código do Grupo" Selected="False"></asp:ListItem>
            <asp:ListItem Value="GrupoNome" Text="Nome do Grupo" Selected="False"></asp:ListItem>            
            <asp:ListItem Value="SUBGCODIGO" Text="Código do Subgrupo" Selected="False"></asp:ListItem>
            <asp:ListItem Value="SubgrupoNome" Text="Nome do Subgrupo" Selected="False"></asp:ListItem>
            <asp:ListItem Value="PROUNIDADE" Text="Unidade" Selected="False"></asp:ListItem>
        </FilterByItems>      
    </usc:FilterBar>

    <asp:DataGrid ID="gridDados" SkinID="GridSkin" runat="server" OnPageIndexChanged="Grid_PageIndexChanged" AllowCustomPaging="True" AllowPaging="True" PageSize="50" AutoGenerateColumns="False" OnItemCommand="Grid_ItemCommand">
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSelect" runat="server" CommandName='<%# global_asax.COMMANDSELECT %>' CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/check.png" ToolTip="SELECIONAR este Produto" style="margin-right:5px;" />                    
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:BoundColumn DataField="PROCODIGO" HeaderText="Código"></asp:BoundColumn>
            <asp:BoundColumn DataField="PRODESCRICAO" HeaderText="Descrição"></asp:BoundColumn>
            <asp:BoundColumn DataField="PROPRECOUNITARIO" HeaderText="Preço Unitário"></asp:BoundColumn>
            <asp:BoundColumn DataField="PROUNIDADE" HeaderText="Unidade"></asp:BoundColumn>
            <asp:BoundColumn DataField="GrupoNome" HeaderText="Grupo"></asp:BoundColumn>
            <asp:BoundColumn DataField="SubgrupoNome" HeaderText="Subgrupo"></asp:BoundColumn>

            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName='<%# global_asax.COMMANDEDIT %>' CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/edit.png" ToolTip="EDITAR este Produto" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnDelete" runat="server" CommandName='<%# global_asax.COMMANDDELETE %>' OnClientClick="return confirmarExclusao();"  CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/delete.png" ToolTip="EXCLUIR este Produto" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnView" runat="server" CommandName='<%# global_asax.COMMANDVIEW %>'  CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/view.png" ToolTip="VISUALIZAR este Produto" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>        
    </asp:DataGrid>
</asp:Content>


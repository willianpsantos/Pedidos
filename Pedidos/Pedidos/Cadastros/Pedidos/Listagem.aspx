<%@ Page Title="Pedidos | Listagem" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="Pedidos.Cadastros.Pedidos.Listagem" Theme="GridTheme"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Src="~/UserControls/FilterBar.ascx" TagName="FilterBar" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">  
    <%: Scripts.Render("~/Cadastros/Pedidos/list.js") %>
      
    <usc:PageTitle runat="server" Text="Listagem de Pedidos" ShowButtonAdd="true" OnButtonAddClick="ButtonAdd_Click" />

    <usc:FilterBar ID="filteBarPedidos" runat="server" OnSearch="FilterBar_Search" OnClear="FilterBar_Clear">  
        <FilterByItems>
            <asp:ListItem Value="PEDCODIGO" Text="Código"></asp:ListItem>
            <asp:ListItem Value="ORCCODIGO" Text="Cód. Orçamento"></asp:ListItem>
            <asp:ListItem Value="ClienteNome" Text="Cliente"></asp:ListItem>            
            <asp:ListItem Value="PEDDATAEMISSAO" Text="Data de Emissão"></asp:ListItem>
            <asp:ListItem Value="PEDDATALOCACAO" Text="Data de Locação"></asp:ListItem>
            <asp:ListItem Value="PEDDATASAIDA" Text="Data de Saída"></asp:ListItem>
            <asp:ListItem Value="PEDDATADEVOLUCAO" Text="Data de Devolução"></asp:ListItem>
        </FilterByItems>      
    </usc:FilterBar>

    <asp:DataGrid ID="gridDados" SkinID="GridSkin" runat="server" AllowCustomPaging="True" AllowPaging="True" PageSize="50" AutoGenerateColumns="False" OnItemCommand="Grid_ItemCommand" OnPageIndexChanged="Grid_PageIndexChanged" OnItemDataBound="gridDados_ItemDataBound">        
        <Columns>            
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSelect" runat="server" CommandName='<%# global_asax.COMMANDSELECT %>' CommandArgument='<%# Eval("PEDCODIGO") %>' ImageUrl="~/Content/images/check.png" ToolTip="SELECIONAR este pedido" style="margin-right:5px;" />                    
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:BoundColumn DataField="PEDCODIGO" HeaderText="Código"></asp:BoundColumn>
            <asp:BoundColumn DataField="ORCCODIGO" HeaderText="Cód. Orçamento"></asp:BoundColumn>
            <asp:BoundColumn DataField="ClienteNome" HeaderText="Cliente"></asp:BoundColumn>            
            <asp:BoundColumn DataField="PEDDATAEMISSAO" HeaderText="Data de Emissão" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
            <asp:BoundColumn DataField="PEDDATALOCACAO" HeaderText="Data de Locação" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
            <asp:BoundColumn DataField="PEDDATASAIDA" HeaderText="Data de Saída" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
            <asp:BoundColumn DataField="PEDDATADEVOLUCAO" HeaderText="Data de Devolução" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>                        

            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName='<%# global_asax.COMMANDEDIT %>' CommandArgument='<%# Eval("PEDCODIGO") %>' ImageUrl="~/Content/images/edit.png" ToolTip="EDITAR este Pediso" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnDelete" runat="server" CommandName='<%# global_asax.COMMANDDELETE %>' OnClientClick="return confirmarExclusao();"  CommandArgument='<%# Eval("PEDCODIGO") %>' ImageUrl="~/Content/images/delete.png" ToolTip="EXCLUIR este Pedido" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnView" runat="server" CommandName='<%# global_asax.COMMANDVIEW %>'  CommandArgument='<%# Eval("PEDCODIGO") %>' ImageUrl="~/Content/images/view.png" ToolTip="VISUALIZAR este Pedido" style="margin-right:5px;" />
                    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Content/images/print.png" ToolTip="IMPRIMIR este Pedido" style="margin-right:5px;" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>        
    </asp:DataGrid>
</asp:Content>

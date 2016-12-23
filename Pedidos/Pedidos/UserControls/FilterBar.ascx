<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterBar.ascx.cs" Inherits="Pedidos.UserControls.FilterBar" %>

<style type="text/css">
    table.filter-bar {
        width:100%;        
        margin-bottom:10px;
        line-height:0;
    }

    table tr td {
        padding:5px;
    }

    thead {
        font-weight:600;
    }

    .filter-by {        
        width:18%;        
    }

    .operator {
        width:18%;
    }

    .value {
        width:48%;
    }

    .buttons {
        width:180px;
    }

    .search-button {
        width:110px;
        color:darkseagreen;
    }

    .clear-button {
        color:firebrick;
        width:32px !important;
    }
</style>

<table border="0" class="filter-bar">
    <thead>
        <tr>
            <td class="filter-by">Filtrar por: </td>                 
            <td class="operator">Operator: </td>         
            <td class="value">Valor: </td> 
            <td class="search-button">&nbsp; </td> 
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class="filter-by"> 
                <asp:DropDownList ID="ddlFilterBy" CssClass="k-dropdown" runat="server" Width="100%"  DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true"></asp:DropDownList>
            </td>        
            <td class="operator"> 
                <asp:DropDownList ID="ddlOperator" CssClass="k-dropdown" runat="server" Width="100%" DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true"></asp:DropDownList>
            </td>
            <td class="value"> 
                <asp:TextBox ID="txtValue" CssClass="k-textbox" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td class="buttons"> 
                <button id="buttonSearch" runat="server" class="k-button search-button" onserverclick="buttonSearch_ServerClick">
                    <i class="fa fa-search"></i>
                    &nbsp; Pesquisar
                </button>

                <button id="buttonClear" runat="server" class="k-button clear-button" onserverclick="buttonClear_ServerClick">
                    <i class="fa fa-times-circle"></i>                    
                </button>
            </td>
        </tr>
    </tbody>
</table>
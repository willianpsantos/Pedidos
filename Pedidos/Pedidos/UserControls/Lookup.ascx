<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lookup.ascx.cs" Inherits="Pedidos.UserControls.Lookup" %>

<style type="text/css">
    .lookup-container {
        padding:0;
        margin:0;
    }

    .lookup-container > * {
        margin-right:0px;
    }

    .lookup-container #btnOpen {
        color:#30782b;
    }

    .lookup-container #btnClear {
        color:firebrick;
    }

</style>

<div data-id="<%= Uid %>" data-last-filter="<%= LastAppliedFilter %>" class="lookup-container">
    <asp:HiddenField ID="hdnInternalValue" runat="server" />

    <asp:DropDownList ID="InternalDropDownList" runat="server" AppendDataBoundItems="false" required="<%= Required %>" onchange="window.app.validateOnSubmit = false;" OnSelectedIndexChanged="InternalDropDownList_SelectedIndexChanged"></asp:DropDownList>

    <button id="btnOpen" class="k-button" type="button" <%: !Enabled ? "disabled=disabled" : "" %>>
        <i class="fa fa-search"></i>
    </button>

    <button id="btnClear" class="k-button" type="button" <%: !Enabled ? "disabled=disabled" : "" %>>
        <i class="fa fa-times-circle"></i>
    </button>
</div>

<script type="text/javascript">
    $(function () {
        $('div[data-id=<%= Uid %>]').kendoLookupManager({
            lookupType: '<%= Enum.GetName(typeof(Pedidos.UserControls.LookupType), LookupType).ToLower() %>',

            id: '<%= ClientID %>',

            url: { 
                service: '<%= !string.IsNullOrEmpty(ServiceUrl) ? ResolveUrl(ServiceUrl) : string.Empty %>',
                lookupQueryString: '<%= global_asax.QUERYSTRINGLOOKUP %>',
                page: '<%= !string.IsNullOrEmpty(PageUrl) ? ResolveUrl(PageUrl) : string.Empty %>' 
            },

            grid: {
                columns: <%= JsonColumns %>
            },

            filter: {
                field: '<%= DataTextField %>'
            },

            select: {
                valueField: '<%= DataValueField %>',
                textField: '<%= DataTextField %>'
            },

            selectors: {                
                select: '#<%= InternalDropDownList.ClientID %>',
                openButton: '#btnOpen',
                clearButton: '#btnClear'
            }
        });
    });
</script>
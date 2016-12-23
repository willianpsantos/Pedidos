<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="Pedidos.UserControls.MainMenu" %>

<div class="<%= MenuCssClass %> left"></div>

<script type="text/javascript" src="<%= MenuJsFile %>" ></script>

<script type="text/javascript">
    $(function () {
        window.app.MainMenu = $('.<%= MenuCssClass %>').kendoMainMenuWidget({            
            items: <%= Newtonsoft.Json.JsonConvert.SerializeObject(Items, Settings) %>,

            cssClass: {
                menu: '<%= MenuCssClass %>',
                item: '<%= MenuItemCssClass %>'
            }
        }).data('kendoMainMenuWidget');
    });
</script>
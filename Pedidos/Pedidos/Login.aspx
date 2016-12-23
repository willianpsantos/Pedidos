<%@ Page Title="Login" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pedidos.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%: Styles.Render("~/Content/css/login.css") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <section id="loginForm">
        <div class="login-box">            
            <div class="login-header tema-cinza-escuro">
                <h1>
                    <i class="fa fa-user"></i> &nbsp;
                    Acessar o Sistema
                </h1>
            </div>

            <div class="login-content">                
                <asp:TextBox runat="server" ID="txtLogin" CssClass="k-textbox" AutoCompleteType="None" placeholder="Usuário" required="O campo <b> USUÁRIO </b> é obrigatório!"></asp:TextBox> <br />
                <asp:TextBox runat="server" ID="txtPassword" CssClass="k-textbox" AutoCompleteType="None" TextMode="Password" placeholder="Senha" required="O campo <b> SENHA </b> é obrigatório!"></asp:TextBox>                
                <asp:Label ID="lblMensagem" CssClass="message" runat="server" Visible="false"></asp:Label>
            </div>

            <asp:Button ID="btnLogin" runat="server" CssClass="k-button btn-login" Text="Entrar" OnClick="btnLogin_Click" />
        </div>                       
    </section>                    
</asp:Content>

<%@ Page Title="Clientes | Cadastro" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Pedidos.Cadastros.Clientes.Form" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">
    <usc:PageTitle runat="server" Text="Cadastrar Cliente" ShowButtonAdd="false" />

    <asp:FormView ID="frmCadastroCliente" DefaultMode="Edit" runat="server" AllowPaging="false" ItemType="Pedidos.Model.CLIENTES" Width="100%" InsertMethod="FormView_InsertItem" UpdateMethod="FormView_UpdateItem" SelectMethod="FormView_GetItem">
        <ItemTemplate>
            <div class="table">
                <div class="table-row">
                    <div class="table-cell label required">
                        Código:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCodigo" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" data-required="O Campo <b> CÓDIGO </b> é obrigatório" Text="<%# BindItem.CLICODIGO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label required">
                        Nome / Razão Social:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRazaoSocial" MaxLength="80" CssClass="k-textbox fundo-cinza" ReadOnly="true" Width="80%" runat="server" data-required="O Campo <b> NOME / RAZÃO SOCIAL </b> é obrigatório" Text="<%# BindItem.CLIRAZAOSOCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CPF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCPF" MaxLength="14" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLICPF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CNPJ:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCNPJ" MaxLength="19" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLICNPJ %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        RG:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRG" MaxLength="20" runat="server" ReadOnly="true" CssClass="k-textbox fundo-cinza" Text="<%# BindItem.CLIRGIE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Estadual:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoEstadual" MaxLength="30" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLIINSCRESTADUAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Municipal:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoMunicipal" MaxLength="30"  CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLIINSCRMUNICIPAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Fixo:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefone" MaxLength="14"  CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLITELFONE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Comercial:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefoneComercial" MaxLength="14" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLITELEFONECOMERCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                 <div class="table-row">
                    <div class="table-cell label">
                        Telefone Celular:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCelular" MaxLength="14" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLICELULAR %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Endereço:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtxEndereco"  MaxLength="100"  Width="70%" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLIENDERECO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Bairro:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtBairro" MaxLength="40"  Width="70%" CssClass="k-textbox fundo-cinza" runat="server" ReadOnly="true" Text="<%# BindItem.CLIBAIRRO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Cidade / UF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCidadeUF" MaxLength="35"  Width="60%" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLICIDADEUF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CEP:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCEP"  MaxLength="10" CssClass="k-textbox fundo-cinza" runat="server" ReadOnly="true" Text="<%# BindItem.CLICEP %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Ponto de Referência:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtPontoReferencia" MaxLength="100" Width="60%" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLIPONTOREFERENCIA %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Email:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="k-textbox fundo-cinza" Width="60%" ReadOnly="true" runat="server" Text="<%# BindItem.CLIEMAIL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Contato:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtContato" MaxLength="40" CssClass="k-textbox fundo-cinza" Width="60%" ReadOnly="true" runat="server" Text="<%# BindItem.CLICONTATO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Observações:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtObservacoes" CssClass="k-textbox fundo-cinza" ReadOnly="true" Width="80%" runat="server" Text="<%# BindItem.ObservacaoString %>" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                    </div>        
                </div>
            </div>
        </ItemTemplate>

        <InsertItemTemplate>
            <div class="table">
                <div class="table-row">
                    <div class="table-cell label required">
                        Código:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCodigo" TextMode="Number" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="0" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label required">
                        Nome / Razão Social:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRazaoSocial" MaxLength="80" CssClass="k-textbox" Width="80%" runat="server" data-required="O Campo <b> NOME / RAZÃO SOCIAL </b> é obrigatório" Text="<%# BindItem.CLIRAZAOSOCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CPF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCPF" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICPF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CNPJ:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCNPJ" MaxLength="19" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICNPJ %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        RG:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRG" MaxLength="20" runat="server" CssClass="k-textbox" Text="<%# BindItem.CLIRGIE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Estadual:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoEstadual" MaxLength="30" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIINSCRESTADUAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Municipal:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoMunicipal" MaxLength="30"  CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIINSCRMUNICIPAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Fixo:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefone" MaxLength="14"  CssClass="k-textbox" runat="server" Text="<%# BindItem.CLITELFONE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Comercial:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefoneComercial" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLITELEFONECOMERCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Celular:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCelular" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICELULAR %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Endereço:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtxEndereco"  MaxLength="100"  Width="70%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIENDERECO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Bairro:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtBairro" MaxLength="40"  Width="70%" CssClass="k-textbox" runat="server"  Text="<%# BindItem.CLIBAIRRO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Cidade / UF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCidadeUF" MaxLength="35"  Width="60%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICIDADEUF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CEP:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCEP"  MaxLength="10" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICEP %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Ponto de Referência:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtPontoReferencia" MaxLength="100" Width="60%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIPONTOREFERENCIA %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Email:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="k-textbox" Width="60%" runat="server" Text="<%# BindItem.CLIEMAIL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Contato:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtContato" MaxLength="40" CssClass="k-textbox" Width="60%" runat="server" Text="<%# BindItem.CLICONTATO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Observações:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtObservacoes" CssClass="k-textbox" Width="80%" runat="server" Text="<%# BindItem.ObservacaoString %>" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                    </div>        
                </div>
            </div>
        </InsertItemTemplate>

        <EditItemTemplate>
            <div class="table">
                <div class="table-row">
                    <div class="table-cell label required">
                        Código:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCodigo" TextMode="Number" CssClass="k-textbox fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.CLICODIGO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label required">
                        Nome / Razão Social:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRazaoSocial" MaxLength="80" CssClass="k-textbox" Width="80%" runat="server" data-required="O Campo <b> NOME / RAZÃO SOCIAL </b> é obrigatório" Text="<%# BindItem.CLIRAZAOSOCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label required">
                        CPF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCPF" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICPF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label required">
                        CNPJ:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCNPJ" MaxLength="19" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICNPJ %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        RG:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtRG" MaxLength="20" runat="server" CssClass="k-textbox" Text="<%# BindItem.CLIRGIE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Estadual:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoEstadual" MaxLength="30" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIINSCRESTADUAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Inscrição Municipal:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtInscricaoMunicipal" MaxLength="30"  CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIINSCRMUNICIPAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Fixo:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefone" MaxLength="14"  CssClass="k-textbox" runat="server" Text="<%# BindItem.CLITELFONE %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Comercial:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtTelefoneComercial" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLITELEFONECOMERCIAL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Telefone Celular:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCelular" MaxLength="14" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICELULAR %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Endereço:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtxEndereco"  MaxLength="100"  Width="70%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIENDERECO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Bairro:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtBairro" MaxLength="40"  Width="70%" CssClass="k-textbox" runat="server"  Text="<%# BindItem.CLIBAIRRO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Cidade / UF:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCidadeUF" MaxLength="35"  Width="60%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICIDADEUF %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        CEP:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtCEP"  MaxLength="10" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLICEP %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Ponto de Referência:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtPontoReferencia" MaxLength="100" Width="60%" CssClass="k-textbox" runat="server" Text="<%# BindItem.CLIPONTOREFERENCIA %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Email:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="k-textbox" Width="60%" runat="server" Text="<%# BindItem.CLIEMAIL %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Contato:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtContato" MaxLength="40" CssClass="k-textbox" Width="60%" runat="server" Text="<%# BindItem.CLICONTATO %>" ></asp:TextBox>
                    </div>        
                </div>

                <div class="table-row">
                    <div class="table-cell label">
                        Observações:
                    </div>

                    <div class="table-cell field">
                        <asp:TextBox ID="txtObservacoes" CssClass="k-textbox" Width="80%" runat="server" Text="<%# BindItem.ObservacaoString %>" TextMode="MultiLine" Height="100px" ></asp:TextBox>
                    </div>        
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>

<asp:Content ID="ContentFooter" ContentPlaceHolderID="cphSiteFooter" runat="server">
    <div class="right height-100-perc">
        <asp:Button ID="btnSalvar" runat="server" CssClass="k-button botao-padrao tema-verde" Text="Salvar" OnClientClick="window.app.validateOnSubmit = true;" OnClick="SaveButton_Click"></asp:Button>    
        <button id="btnVoltar" class="k-button botao-padrao tema-verde" onclick="window.app.validateOnSubmit = false; window.location.href = '/Cadastros/Clientes/Listagem'; return false; ">Voltar</button>    
    </div>

    <%: Scripts.Render("~/Cadastros/Clientes/form.js") %>
</asp:Content>

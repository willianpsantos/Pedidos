<%@ Page Title="Pedidos | Cadastro" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Pedidos.Cadastros.Pedidos.Form" Theme="GridTheme"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Src="~/UserControls/PageTitle.ascx" TagName="PageTitle" TagPrefix="usc" %>
<%@ Register Src="~/UserControls/Lookup.ascx" TagName="Lookup" TagPrefix="usc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSite" runat="server">
    <usc:PageTitle runat="server" Text="Cadastrar Pedido" ShowButtonAdd="false" />

    <asp:FormView ID="frmCadastroPedidos" DefaultMode="ReadOnly" runat="server" AllowPaging="false" ItemType="Pedidos.Model.PEDIDOS" Width="100%" SelectMethod="FormView_GetItem" InsertMethod="FormView_InsertItem" UpdateMethod="FormView_UpdateItem" OnDataBound="frmCadastroPedidos_DataBound">
        <ItemTemplate>
            <div class="table">
                <div class="table-row">                    
                    <div class="table-cell">
                        <span class="label">Código</span> <br />
                        <asp:TextBox ID="txtCodigo" AutoPostBack="true" CssClass="k-textbox width-100-perc fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.PEDCODIGO %>" ></asp:TextBox>
                    </div>
               
                    <div class="table-cell">
                        <span class="label">Data de Emissão</span> <br />
                        <asp:TextBox ID="txtDataEmissao" CssClass="k-textbox width-100-perc fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.PEDDATAEMISSAOString %>" data-required="O campo <b> DATA DE EMISSÃO </b> é obrigatório!"></asp:TextBox>
                    </div>   
                
                    <div class="table-cell">
                        <span class="label">Data de Locação</span> <br />
                        <asp:TextBox ID="txtDataLocacao" CssClass="k-textbox width-100-perc fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.PEDDATALOCACAOString %>" data-required="O campo <b> DATA DE LOCAÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>            

                    <div class="table-cell">
                        <span class="label">Data de Devolução</span> <br />
                        <asp:TextBox ID="txtDataDevolucao" CssClass="k-textbox width-100-perc fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.PEDDATADEVOLUCAOString %>" data-required="O campo <b> DATA DE DEVOLUÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>

                    <div class="table-cell">
                        <span class="label">Data de Saída</span> <br />
                        <asp:TextBox ID="txtDataSaida" CssClass="k-textbox width-100-perc fundo-cinza" ReadOnly="true" runat="server" Text="<%# BindItem.PEDDATASAIDAString %>" data-required="O campo <b> DATA DE SAÍDA </b> é obrigatório!"></asp:TextBox>
                    </div>   
                </div>        

                <div class="table-row">                    
                    <div class="table-cell width-40-perc" style="padding-top:12px;">
                        <span class="label">Cliente</span> <br />
                        <usc:Lookup ID="lkpCliente" CssClass="fundo-cinza" Enabled="false" runat="server" Width="85%" DataTextField="CLIRAZAOSOCIAL" DataValueField="CLICODIGO" PageUrl="~/Cadastros/Clientes/Listagem.aspx">                           
                        </usc:Lookup>
                    </div>
                </div>

                <div class="table-row">
                    <div class="table-cell width-40-perc" style="height:50px;">
                        <span class="label">Situação de Contrato</span> <br />
                        <asp:RadioButtonList ID="rdlStatus" runat="server" Enabled="false" CssClass="border-radius" Height="100%" Width="100%" BorderWidth="1" BorderStyle="Solid" style="padding:5px;">
                            <asp:ListItem Text="Sem Contrato" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Contrato Resumido" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Contrato Completo" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="table-cell width-50-perc" style="height:83px;">
                        <span class="label">Observação</span> <br />
                        <asp:TextBox ID="txtObservacao" Text="<%# BindItem.ObservacaoString %>" Enabled="false" ReadOnly="true" CssClass="k-textbox fundo-cinza" runat="server" TextMode="MultiLine" Width="100%" Height="100%"></asp:TextBox>
                    </div>
                </div>                
            </div>
        </ItemTemplate>

        <InsertItemTemplate>
            <div class="table">
                <div class="table-row">                    
                    <div class="table-cell">
                        <span class="label">Código</span> <br />
                        <asp:TextBox ID="txtCodigo" AutoPostBack="true" CssClass="k-textbox width-100-perc" ReadOnly="true" runat="server" Text="<%# BindItem.PEDCODIGO %>" ></asp:TextBox>
                    </div>
               
                    <div class="table-cell">
                        <span class="label">Data de Emissão</span> <br />
                        <asp:TextBox ID="txtDataEmissao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATAEMISSAOString %>" data-required="O campo <b> DATA DE EMISSÃO </b> é obrigatório!"></asp:TextBox>
                    </div>   
                
                    <div class="table-cell">
                        <span class="label">Data de Locação</span> <br />
                        <asp:TextBox ID="txtDataLocacao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATALOCACAOString %>" data-required="O campo <b> DATA DE LOCAÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>            

                    <div class="table-cell">
                        <span class="label">Data de Devolução</span> <br />
                        <asp:TextBox ID="txtDataDevolucao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATADEVOLUCAOString %>" data-required="O campo <b> DATA DE DEVOLUÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>

                    <div class="table-cell">
                        <span class="label">Data de Saída</span> <br />
                        <asp:TextBox ID="txtDataSaida" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATASAIDAString %>" data-required="O campo <b> DATA DE SAÍDA </b> é obrigatório!"></asp:TextBox>
                    </div>   
                </div>        

                <div class="table-row">                    
                    <div class="table-cell width-40-perc" style="padding-top:12px;">
                        <span class="label required">Cliente</span> <br />
                        <usc:Lookup ID="lkpCliente"  data-required="O campo CLIENTE é obrigatório!" runat="server" Width="85%" DataTextField="CLIRAZAOSOCIAL" DataValueField="CLICODIGO" PageUrl="~/Cadastros/Clientes/Listagem.aspx">                           
                        </usc:Lookup>
                    </div>
                </div>

              <div class="table-row">
                    <div class="table-cell width-40-perc" style="height:50px;">
                        <span class="label">Situação de Contrato</span> <br />
                        <asp:RadioButtonList ID="rdlStatus" runat="server" CssClass="border-radius" Height="100%" Width="100%" BorderWidth="1" BorderStyle="Solid" style="padding:5px;">
                            <asp:ListItem Text="Sem Contrato" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Contrato Resumido" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Contrato Completo" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="table-cell width-50-perc" style="height:83px;">
                        <span class="label">Observação</span> <br />
                        <asp:TextBox ID="txtObservacao" runat="server" Text="<%# BindItem.ObservacaoString %>" CssClass="k-textbox" TextMode="MultiLine" Width="100%" Height="100%"></asp:TextBox>
                    </div>
                </div>                
            </div>
        </InsertItemTemplate>

        <EditItemTemplate>
            <div class="table">
                <div class="table-row">                    
                    <div class="table-cell">
                        <span class="label">Código</span> <br />
                        <asp:TextBox ID="txtCodigo" AutoPostBack="true" CssClass="k-textbox width-100-perc" ReadOnly="true" runat="server" Text="<%# BindItem.PEDCODIGO %>" ></asp:TextBox>
                    </div>
               
                    <div class="table-cell">
                        <span class="label">Data de Emissão</span> <br />
                        <asp:TextBox ID="txtDataEmissao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATAEMISSAOString %>" data-required="O campo <b> DATA DE EMISSÃO </b> é obrigatório!"></asp:TextBox>
                    </div>   
                
                    <div class="table-cell">
                        <span class="label">Data de Locação</span> <br />
                        <asp:TextBox ID="txtDataLocacao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATALOCACAOString %>" data-required="O campo <b> DATA DE LOCAÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>            

                    <div class="table-cell">
                        <span class="label">Data de Devolução</span> <br />
                        <asp:TextBox ID="txtDataDevolucao" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATADEVOLUCAOString %>" data-required="O campo <b> DATA DE DEVOLUÇÃO </b> é obrigatório!"></asp:TextBox>
                    </div>

                    <div class="table-cell">
                        <span class="label">Data de Saída</span> <br />
                        <asp:TextBox ID="txtDataSaida" data-role="datepicker" CssClass="k-textbox width-100-perc" runat="server" Text="<%# BindItem.PEDDATASAIDAString %>" data-required="O campo <b> DATA DE SAÍDA </b> é obrigatório!"></asp:TextBox>
                    </div>   
                </div>        

                <div class="table-row">                    
                    <div class="table-cell width-40-perc" style="padding-top:12px;">
                        <span class="label">Cliente</span> <br />
                        <usc:Lookup ID="lkpCliente" data-required="O campo CLIENTE é obrigatório!" runat="server" Width="85%" DataTextField="CLIRAZAOSOCIAL" DataValueField="CLICODIGO" PageUrl="~/Cadastros/Clientes/Listagem.aspx">                           
                        </usc:Lookup>
                    </div>
                </div>

                <div class="table-row">
                    <div class="table-cell width-40-perc" style="height:50px;">
                        <span class="label">Situação de Contrato</span> <br />
                        <asp:RadioButtonList ID="rdlStatus" runat="server" CssClass="border-radius" Height="100%" Width="100%" BorderWidth="1" BorderStyle="Solid" style="padding:5px;">
                            <asp:ListItem Text="Sem Contrato" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Contrato Resumido" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Contrato Completo" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="table-cell width-50-perc" style="height:83px;">
                        <span class="label">Observação</span> <br />
                        <asp:TextBox ID="txtObservacao" runat="server" Text="<%# BindItem.ObservacaoString %>" CssClass="k-textbox" TextMode="MultiLine" Width="100%" Height="100%"></asp:TextBox>
                    </div>
                </div>                
            </div>
        </EditItemTemplate>
    </asp:FormView>

    <br />
    <hr />    

    <div class="table">
        <div class="table-row">
            <div class="table-cell">
                <asp:FormView ID="frmItemPedido" DefaultMode="ReadOnly" runat="server" ItemType="Pedidos.Model.ITENSPEDIDOS" DataKeyNames="PROCODIGO" SelectMethod="frmItemPedido_GetItem" OnDataBound="frmItemPedido_DataBound" UpdateMethod="frmItemPedido_UpdateItem">
                    <ItemTemplate>      
                        <div class="table">
                            <div class="table-row">
                                <div class="table-cell" style="width:350px;">
                                    <span class="label required">Produto</span>
                                    <usc:Lookup ID="lkpProduto" OnSelectedIndexChanged="lkpProduto_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Required="false" CssClass="fundo-cinza" runat="server" Width="80%" DataTextField="PRODESCRICAO" DataValueField="PROCODIGO" PageUrl="~/Cadastros/Produto/Listagem.aspx">                           
                                    </usc:Lookup>
                                </div>

                                <div class="table-cell width-20-perc">
                                    <span class="label required">Preço Unitário</span>                        
                                    <asp:TextBox ID="txtPrecoUnitario" Enabled="false" runat="server" CssClass="k-textbox fundo-cinza width-100-perc" Text="<%# BindItem.IPPRECOUNITARIO %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Quantidade</span>                        
                                    <asp:TextBox ID="txtQuantidade" TextMode="Number" runat="server" CssClass="k-textbox fundo-cinza width-100-perc" ReadOnly="true" Enabled="false" Text="<%# BindItem.IPQUANTIDADE %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Total</span>                        
                                    <asp:TextBox ID="txtItemTotal" runat="server" CssClass="k-textbox fundo-cinza width-100-perc" ReadOnly="true" Enabled="false" Text="<%# BindItem.IPTOTAL %>"></asp:TextBox>
                                </div>  
                            </div>
                        </div>                      
                    </ItemTemplate>

                    <InsertItemTemplate>
                        <div class="table">
                            <div class="table-row">
                                <div class="table-cell" style="width:350px;">
                                    <span class="label required">Produto</span>
                                    <usc:Lookup ID="lkpProduto" data-required="O campo PRODUTO é obrigatório!" OnSelectedIndexChanged="lkpProduto_SelectedIndexChanged"  AutoPostBack="true" Required="false" runat="server" Width="80%" DataTextField="PRODESCRICAO" DataValueField="PROCODIGO" PageUrl="~/Cadastros/Produto/Listagem.aspx">                           
                                    </usc:Lookup>
                                </div>

                                <div class="table-cell width-20-perc">
                                    <span class="label required">Preço Unitário</span>                        
                                    <asp:TextBox ID="txtPrecoUnitario" runat="server" CssClass="k-textbox width-100-perc" Text="<%# BindItem.IPPRECOUNITARIO %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Quantidade</span>                        
                                    <asp:TextBox ID="txtQuantidade" TextMode="Number" runat="server" CssClass="k-textbox width-100-perc" Text="<%# BindItem.IPQUANTIDADE %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Total</span>                        
                                    <asp:TextBox ID="txtItemTotal" runat="server" ReadOnly="true" Enabled="false"  CssClass="k-textbox fundo-cinza width-100-perc" Text="<%# BindItem.IPTOTAL %>"></asp:TextBox>
                                </div>  
                            </div>      
                        </div>                
                    </InsertItemTemplate>

                    <EditItemTemplate>     
                        <div class="table">
                            <div class="table-row">
                                <div class="table-cell" style="width:350px;">
                                    <span class="label required">Produto</span>
                                    <usc:Lookup ID="lkpProduto" data-required="O campo PRODUTO é obrigatório!"  OnSelectedIndexChanged="lkpProduto_SelectedIndexChanged"  AutoPostBack="true" runat="server" Required="false"  Width="80%" DataTextField="PRODESCRICAO" DataValueField="PROCODIGO" PageUrl="~/Cadastros/Produto/Listagem.aspx">                           
                                    </usc:Lookup>
                                </div>

                                <div class="table-cell width-20-perc">
                                    <span class="label required">Preço Unitário</span>                        
                                    <asp:TextBox ID="txtPrecoUnitario" runat="server" CssClass="k-textbox width-100-perc" Text="<%# BindItem.IPPRECOUNITARIO %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Quantidade</span>                        
                                    <asp:TextBox ID="txtQuantidade" TextMode="Number" runat="server" CssClass="k-textbox width-100-perc" Text="<%# BindItem.IPQUANTIDADE %>"></asp:TextBox>
                                </div>

                                <div class="table-cell width-10-perc">
                                    <span class="label required">Total</span>                        
                                    <asp:TextBox ID="txtItemTotal" runat="server" required="true" ReadOnly="true" Enabled="false" CssClass="k-textbox fundo-cinza width-100-perc" Text="<%# BindItem.IPTOTAL %>"></asp:TextBox>
                                </div>    
                            </div>   
                        </div>                 
                    </EditItemTemplate>
                </asp:FormView>
            </div>
            
            <div class="table-cell" style="width:40px; margin-top:22px; margin-left:-240px;">
                <asp:ImageButton ID="btnAddItem" runat="server" ImageUrl="~/Content/images/add-small.png" ImageAlign="Middle" OnClientClick="window.app.validateOnSubmit = true;" OnClick="btnAddItem_Click" />
            </div>            
        </div>
    </div>

    <div class="table">
        <div class="table-row">
            <div class="table-cell" style="width:80%">
                <asp:DataGrid ID="gridItems" runat="server" SkinID="GridSkin" AllowCustomPaging="false" AllowPaging="false" AutoGenerateColumns="false" Width="100%" Height="100%" OnItemCommand="gridItems_ItemCommand">
                    <Columns>
                        <asp:BoundColumn DataField="IPQUANTIDADE" HeaderText="Qtde. (UN)" ItemStyle-Width="10%" DataFormatString="{0:n2}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ProdutoNome" HeaderText="Produto" ItemStyle-Width="60%"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="IPPRECOUNITARIO" HeaderText="Preço Unit. (R$)" DataFormatString="{0:c}" ItemStyle-Width="10%"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="IPTOTAL" HeaderText="Total (R$)" ItemStyle-Width="10%" DataFormatString="{0:c}"></asp:BoundColumn> 

                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CommandName='<%# global_asax.COMMANDEDIT %>' CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/edit.png" ToolTip="EDITAR este Produto" style="margin-right:5px;" OnClientClick="window.app.validateOnSubmit = false; " />
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName='<%# global_asax.COMMANDDELETE %>' CommandArgument='<%# Eval("PROCODIGO") %>' ImageUrl="~/Content/images/delete.png" ToolTip="EXCLUIR este Produto" style="margin-right:5px;" OnClientClick="window.app.validateOnSubmit = false; " />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>

            <div class="table-cell" style="width:18%">
                <fieldset>
                    <legend>Totalizadores</legend>

                    <span class="label">Subtotal (R$)</span> <br />
                    <asp:TextBox ID="txtSubTotal" CssClass="k-textbox" runat="server" Width="99%" style="text-align:right;"></asp:TextBox>

                    <br />

                    <span class="label">Desconto (R$)</span> <br />
                    <asp:TextBox ID="txtDesconto" CssClass="k-textbox" runat="server" Width="99%" style="text-align:right;"></asp:TextBox>

                    <br />

                    <span class="label">Total</span> <br />
                    <asp:TextBox ID="txtTotal" CssClass="k-textbox" runat="server" Width="99%" style="text-align:right;"></asp:TextBox>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContentFooter" ContentPlaceHolderID="cphSiteFooter" runat="server">
    <div class="right height-100-perc">
        <asp:Button ID="btnSalvar" runat="server" CssClass="k-button botao-padrao tema-verde" Text="Salvar" OnClientClick="return window.app.validate();" OnClick="SaveButton_Click"></asp:Button>    
        <button id="btnVoltar" class="k-button botao-padrao tema-verde" onclick="window.app.validateOnSubmit = false; window.location.href = '/Cadastros/Pedidos/Listagem'; return false; ">Voltar</button>    
    </div>

    <%: Scripts.Render("~/Cadastros/Pedidos/form.js") %>
</asp:Content>


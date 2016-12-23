using Pedidos.DataAccess;
using Pedidos.Model;
using System.Web.UI.WebControls;
using Pedidos.Classes;
using Pedidos.UserControls;
using System;
using Pedidos.Util;
using Pedidos.Model.Views;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using System.Globalization;

namespace Pedidos.Cadastros.Pedidos
{
    public partial class Form : PageForm<PEDIDOS, int>
    {
        protected override string ListUrl { get { return "~/Cadastros/Pedidos/Listagem"; } }

        protected override string InsertMessage { get { return "Pedido inserido com sucesso!"; } }

        protected override string UpdateMessage { get { return "Pedido atualizado com sucesso!"; } }

        protected override string EntityNotFoundMessage { get { return "Pedido não encontrado!"; } }

        protected override FormView FormView { get { return frmCadastroPedidos; } }

        protected override Button SaveButton { get { return btnSalvar; } }

        private ClienteDataAccess ClienteDao { get; set; }

        private ProdutoDataAccess ProdutoDao { get; set; }

        private ItensPedidosDataAccess ItensDao { get; set; }

        private List<ITENSPEDIDOSView> ItensAdicionados
        {
            get { return Session["ItensAdicionados"] as List<ITENSPEDIDOSView>; }
            set { Session["ItensAdicionados"] = value; }
        }

        private bool ItensAlterados
        {
            get { return (bool)ViewState["ItensAlterados"]; }
            set { ViewState["ItensAlterados"] = value; }
        } 
        

        public Form()
        {
            var datacontext = new PedidosDataContext();
            Dao = new PedidosDataAccess(datacontext);
            ClienteDao = new ClienteDataAccess(datacontext);
            ProdutoDao = new ProdutoDataAccess(datacontext);
            ItensDao = new ItensPedidosDataAccess(datacontext);
        }


        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.BeforeSave += new SaveEventHandler<PEDIDOS>(OnBeforeSave);
            this.AfterSave += new SaveEventHandler<PEDIDOS>(OnAfterSave);

            if (!IsPostBack)
            {
                this.ItensAlterados = false;
                string mode = Request.QueryString[Global.QUERYSTRINGMODE];

                switch (mode)
                {
                    case Global.COMMANDVIEW:
                        frmItemPedido.ChangeMode(System.Web.UI.WebControls.FormViewMode.ReadOnly);
                        SaveButton.Visible = false;
                        break;
                    case Global.COMMANDINSERT:
                    case Global.COMMANDEDIT:
                        frmItemPedido.ChangeMode(System.Web.UI.WebControls.FormViewMode.Edit);
                        SaveButton.Visible = true;
                        break;
                }

                ItensAdicionados = new List<ITENSPEDIDOSView>();
                BindGridItems();
            }
        }

        
        private void BindGridItems()
        {
            gridItems.DataSource = ItensAdicionados;
            gridItems.DataBind();
        }

        public override Lookup GetLookupRequested(string clientId)
        {
            if (clientId.Contains("lkpCliente"))
                return GetClienteLookup();

            if (clientId.Contains("lkpProduto"))
                return GetProdutoLookup();

            return null;
        }

        private Lookup GetClienteLookup()
        {
            var lookup = (Lookup)frmCadastroPedidos.FindControl("lkpCliente");
            lookup.FilterMethod = new LookupFilterMethod(FiltrarCliente);
            return lookup;
        }

        private Lookup GetProdutoLookup()
        {
            var lookup = (Lookup)frmItemPedido.FindControl("lkpProduto");
            lookup.FilterMethod = new LookupFilterMethod(FiltrarProduto);            
            return lookup;
        }

        private RadioButtonList GetStatusRadioButtonList()
        {
            RadioButtonList rdlTipContrato = frmCadastroPedidos.FindControl("rdlStatus") as RadioButtonList;
            return rdlTipContrato;
        }
       
        public void FiltrarCliente(string field, string operation, string value)
        {
            Filter args = new Filter()
            {
                FilterBy = field,
                Operator = operation,
                Value = value,
                ItemType = typeof(CLIENTES).GetPropertyType(field)
            };

            var expression = ClienteDao.GetExpression(args.ToString());
            var data = ClienteDao.Filter(expression);
            var lookupCliente = frmCadastroPedidos.FindControl("lkpCliente") as Lookup;

            lookupCliente.LastAppliedFilter = $"field={field}&operation={operation}&value={value}";
            lookupCliente.Items.Clear();
            lookupCliente.DataSource = data;
            lookupCliente.DataBind();

            if (lookupCliente.Items.Count > 1)
            {
                lookupCliente.SelectedIndex = 1;
            }
        }

        public void FiltrarProduto(string field, string operation, string value)
        {
            Filter args = new Filter()
            {
                FilterBy = field,
                Operator = operation,
                Value = value,
                ItemType = typeof(PRODUTOS).GetPropertyType(field)
            };

            var expression = ProdutoDao.GetExpression(args.ToString());
            var data = ProdutoDao.Filter(expression);
            var lookupProduto = GetProdutoLookup();

            lookupProduto.LastAppliedFilter = $"field={field}&operation={operation}&value={value}";
            lookupProduto.Items.Clear();
            lookupProduto.DataSource = data;
            lookupProduto.DataBind();
        }
        
        public ITENSPEDIDOS frmItemPedido_GetItem([ViewState] int? id)
        {
            if (!id.HasValue)
                return new ITENSPEDIDOS();

            var query = ItensAdicionados.Where(x => x.PROCODIGO == id.Value).FirstOrDefault();

            if (query == null)
                return new ITENSPEDIDOS();

            ViewState["id"] = null;
            return query;
        }

        public void frmItemPedido_UpdateItem([ViewState] int id)
        {            
            ITENSPEDIDOSView item = new ITENSPEDIDOSView();
            TryUpdateModel(item);

            PRODUTOS produto = ProdutoDao.GetById(id);
            Lookup lkpProduto = GetProdutoLookup();
            ListItem selectedItem = lkpProduto.SelectedItem;
            int codProduto = Convert.ToInt32(selectedItem.Value);
            item.IPTOTAL = item.IPPRECOUNITARIO * item.IPQUANTIDADE;
            item.PROCODIGO = codProduto;
            item.ProdutoNome = selectedItem.Text;

            var count = ItensAdicionados.Where(x => x.PROCODIGO == id).Count();

            if (count > 0)
            {
                var addedItem = ItensAdicionados.Where(x => x.PROCODIGO == item.PROCODIGO).FirstOrDefault();
                var index = ItensAdicionados.LastIndexOf(addedItem);
                ItensAdicionados[index] = item;
                ViewState["id"] = null;
            }
            else
            {
                ItensAdicionados.Add(item);
            }

            BindGridItems();
            CalcularSubtotalItens();
            CalcularTotalItens();
            LimparFormItemPedido();
        }

        public override PEDIDOS FormView_GetItem([QueryString] int id)
        {
            PEDIDOS entity = base.FormView_GetItem(id);
            CarregarItens(id);
            txtDesconto.Text = entity.PEDDESCONTO?.ToString("n2");

            var total = ItensAdicionados.Sum(x => x.IPTOTAL);
            var desconto = entity.PEDDESCONTO;
            txtDesconto.Text = desconto?.ToString("n2");
            txtTotal.Text = ((total.HasValue ? total.Value : 0) - (desconto.HasValue ? desconto.Value : 0)).ToString("n2");

            return entity;
        }

        protected void OnBeforeSave(object sender, PageFormSaveEventArgs<PEDIDOS> e)
        {
            decimal desconto = 0;
            bool success = Decimal.TryParse(txtDesconto.Text, out desconto);

            e.Entity.PEDDESCONTO = success ? desconto : (decimal?)null;

            Lookup lkpCliente = GetClienteLookup();
            e.Entity.CLICODIGO = Convert.ToInt32(lkpCliente.SelectedValue);

            e.Entity.PEDSTATUS = null;
            RadioButtonList rdlStatus = GetStatusRadioButtonList();

            if (rdlStatus.SelectedIndex < 0)
                return;

            e.Entity.PEDSTATUS = Convert.ToInt32(rdlStatus.SelectedIndex);        
        }

        protected void OnAfterSave(object sender, PageFormSaveEventArgs<PEDIDOS> e)
        {
            if (!ItensAlterados)
            {                
                return;
            }

            try
            {
                ItensDao.DeleteAll(e.Entity.PEDCODIGO);

                foreach (var item in this.ItensAdicionados)
                {
                    var itemPedido = new ITENSPEDIDOS()
                    {
                        IPCODIGO = 0,
                        IPDATA = item.IPDATA,
                        IPLOCADO = item.IPLOCADO,
                        IPPRECOREPOSICAO = item.IPPRECOREPOSICAO,
                        IPPRECOUNITARIO = item.IPPRECOUNITARIO,
                        IPQUANTIDADE = item.IPQUANTIDADE,
                        IPTOTAL = item.IPTOTAL,
                        IPTOTALREPOSICAO = item.IPTOTALREPOSICAO,
                        PEDCODIGO = e.Entity.PEDCODIGO,
                        PROCODIGO = item.PROCODIGO
                    };
                   
                    ItensDao.Save(itemPedido);
                }
                
                ItensAlterados = false;
            }
            catch (Exception ex)
            {
                this.SendErrorMessage();
            }
        }

        public decimal CalcularSubtotalItens()
        {
            var soma = ItensAdicionados.Sum(x => x.IPTOTAL);
            txtSubTotal.Text = soma?.ToString("n2");
            return soma != null ? soma.Value : 0;
        }

        public decimal CalcularTotalItens()
        {
            var culture = new CultureInfo("pt-BR");
            var subtotal = CalcularSubtotalItens();
            decimal desconto = 0;

            bool success = Decimal.TryParse(txtDesconto.Text, out desconto);
            decimal total = 0;

            if (success)
                total = subtotal - desconto;
            else
                total = subtotal;

            txtTotal.Text = total.ToString(culture);
            return total;
        }

        public void LimparFormItemPedido()
        {
            TextBox txtPrecoUnitario = (TextBox)frmItemPedido.FindControl("txtPrecoUnitario");
            TextBox txtQuantidade = (TextBox)frmItemPedido.FindControl("txtQuantidade");
            TextBox txtItemTotal = (TextBox)frmItemPedido.FindControl("txtItemTotal");
            Lookup lkpProduto = GetProdutoLookup();

            txtPrecoUnitario.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtItemTotal.Text = string.Empty;
            lkpProduto.SelectedIndex = -1;
        }

        public void CarregarItens(int id)
        {
            ItensAdicionados = new List<ITENSPEDIDOSView>();
            var itens = ItensDao.Filter(x => x.PEDCODIGO == id, 0);

            if (itens == null)
            {                
                return;
            }

            var ordered = itens.OrderBy(x => x.IPCODIGO).ToArray();

            for (var i = 0; i < ordered.Length; i++)
            {
                ItensAdicionados.Add((ITENSPEDIDOSView)ordered[i]);
            }

            BindGridItems();
            CalcularSubtotalItens();
        }


        protected void frmCadastroPedidos_DataBound(object sender, EventArgs e)
        {
            Lookup lookupCliente = GetClienteLookup();
            RadioButtonList rdlStatus = GetStatusRadioButtonList();
            PEDIDOS dataItem = frmCadastroPedidos.DataItem as PEDIDOS;
            
            if (dataItem == null)
            {
                this.LoadLookupFirstTime(lookupCliente);
                rdlStatus.SelectedIndex = 0;
                return;
            }

            if (dataItem.CLICODIGO == 0)
            {
                this.LoadLookupFirstTime(lookupCliente);
                rdlStatus.SelectedIndex = 0;
                return;
            }

            if (dataItem.PEDSTATUS.HasValue)
            {
                rdlStatus.SelectedIndex = dataItem.PEDSTATUS.Value;
            }

            lookupCliente?.Filter("CLICODIGO", "=", dataItem.CLICODIGO.ToString());
        }

        protected void frmItemPedido_DataBound(object sender, EventArgs e)
        {
            Lookup lookupProduto = GetProdutoLookup();
            ITENSPEDIDOS dataItem = (ITENSPEDIDOS)frmItemPedido.DataItem;            

            if (dataItem == null)
            {
                this.LoadLookupFirstTime(lookupProduto);                
                return;
            }

            if (!dataItem.PROCODIGO.HasValue || dataItem.PROCODIGO == 0)
            {
                this.LoadLookupFirstTime(lookupProduto);
                return;
            }

            lookupProduto?.Filter("PROCODIGO", "=", dataItem.PROCODIGO.ToString());

            if (lookupProduto != null)
                lookupProduto.SelectedIndex = 1;
        }
        
        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            Lookup lookupProduto = GetProdutoLookup();
            int idProduto = Convert.ToInt32(lookupProduto.SelectedValue);
            ViewState["id"] = idProduto;
            frmItemPedido.UpdateItem(false);
            ItensAlterados = true;
            frmItemPedido.ChangeMode(FormViewMode.Edit);
        }

        protected void lkpProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lookup lkpProduto = (Lookup)sender;

            if (lkpProduto.SelectedIndex <= 0)
            {
                return;
            }
                        
            int id = Convert.ToInt32(lkpProduto.SelectedValue);
            PRODUTOS produto = ProdutoDao.GetById(id);

            if (produto == null)
            {
                return;
            }

            TextBox txtPrecoUnitario = (TextBox)frmItemPedido.FindControl("txtPrecoUnitario");
            TextBox txtQuantidade = (TextBox)frmItemPedido.FindControl("txtQuantidade");
            TextBox txtItemTotal = (TextBox)frmItemPedido.FindControl("txtItemTotal");

            txtPrecoUnitario.Text = produto.PROPRECOUNITARIO?.ToString("n2");
            txtQuantidade.Text = "1";
            txtItemTotal.Text = produto.PROPRECOUNITARIO?.ToString("n2");
        }

        protected void gridItems_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case Global.COMMANDDELETE:
                    ItensAdicionados.RemoveAt(e.Item.ItemIndex);
                    BindGridItems();
                    ItensAlterados = true;
                    break;
                case Global.COMMANDEDIT:
                    var id = Convert.ToInt32(e.CommandArgument);
                    ViewState["id"] = id;
                    break;
            }
        }
    }
}
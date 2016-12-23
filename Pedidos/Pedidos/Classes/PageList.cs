using Pedidos.DataAccess;
using System;
using System.Web.UI.WebControls;
using Pedidos.UserControls;
using Pedidos.Util;
using Pedidos.Classes;
using System.Web.UI;

namespace Pedidos.Classes
{
    public abstract class PageList<TEntity, TId> : System.Web.UI.Page, ILookupRequestResolver where TEntity : class
    {
        protected IDataAccessObject<TEntity, TId> Dao;

        protected virtual string PropertyId { get; }

        protected virtual DataGrid Grid{ get; }

        protected virtual FilterBar FilterBar { get; }

        protected virtual string DeleteMessage { get; }

        protected virtual string FormUrl { get; }

        protected virtual int SelectColumnIndex { get; private set; } = 0;

        protected virtual int CommandsColumnsIndex { get; private set; } = -1;


        public PageList()
        {
            
        }


        protected virtual void Page_Init(object sender, EventArgs e)
        {
            FilterBar.ItemType = typeof(TEntity);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            Filter filter = null;

            if (CommandsColumnsIndex < 0)
            {
                CommandsColumnsIndex = Grid.Columns.Count - 1;
            }

            if (Page.IsLookupRequest())
            {
                Grid.Columns[SelectColumnIndex].Visible = true;
                Grid.Columns[CommandsColumnsIndex].Visible = false;
            }
            else
            {
                Grid.Columns[SelectColumnIndex].Visible = false;
                Grid.Columns[CommandsColumnsIndex].Visible = true;
            }

            if (Page.IsLookupControlRequest())
            {
                Lookup control = GetLookupRequested();

                if (this.IsLookupControlClearRequest())
                {
                    this.ClearLookup(control);
                }
                else
                {
                    this.FilterLookup(control);
                }
            }

            if (!IsPostBack)
            {
                if (Page.IsLookupRequest())
                {
                    filter = Page.GetLookupLastFilter(typeof(TEntity));
                }

                RefreshGrid(0, filter, true);
            }
        }
        

        protected virtual TId Parse(string arg)
        {
            return Dao.ConvertValue(arg);
        }

        protected virtual int Count(Filter filter, bool recount = false, params object[] parameters)
        {
            try
            {
                string aux = filter?.ToString();
                int count = 0;

                if (string.IsNullOrEmpty(aux))
                {
                    count = Dao.Count(recount);
                }
                else
                {
                    var expression = Dao.GetExpression(aux, parameters);
                    count = Dao.Count(expression);
                }

                return count;
            }
            catch
            {
                return 0;
            }
        }

        protected virtual PageableData<TEntity> GetData(int pageIndex, Filter filter, bool recount = false, params object[] parameters)
        {
            try
            {
                TEntity[] dados = default(TEntity[]);
                string aux = filter?.ToString();

                if (string.IsNullOrEmpty(aux))
                {
                    dados = Dao.GetAll(pageIndex);
                }
                else
                {
                    var expression = Dao.GetExpression(aux, parameters);
                    dados = Dao.Filter(expression, pageIndex);
                }

                int count = Count(filter, recount, parameters);

                return new PageableData<TEntity>()
                {
                    Data = dados,
                    Count = count
                };
            }
            catch(Exception ex)
            {
                return new PageableData<TEntity>()
                {
                    Data = new TEntity[] { },
                    Count = 0
                };
            }
        }

        protected virtual void RefreshGrid(int pageIndex, Filter filter = null, bool recount = false, params object[] parameters)
        {
            Grid.PageSize = Constantes.PAGE_SIZE;            
            PageableData<TEntity> dados = GetData(pageIndex, filter, recount, parameters);

            Grid.VirtualItemCount = dados.Count;
            Grid.CurrentPageIndex = pageIndex;
            Grid.DataSource = dados.Data;
            Grid.DataBind();
        }

        
        public virtual Lookup GetLookupRequested()
        {
            var target = Request.Params[Constantes.EventTargetParam];
            var clientId = target.Replace(Constantes.LookupControlIdentifier, string.Empty);
            return GetLookupRequested(clientId);
        }
                
        public abstract Lookup GetLookupRequested(string clientId);


        protected virtual object ParseParameters(Filter filter)
        {
            if (!filter.HasParameter)
                return null;

            if (filter.ItemType.Equals(typeof(DateTime)) || filter.ItemType.Equals(typeof(DateTime?)))
            {
                return Convert.ToDateTime(filter.Value);
            }

            return filter.Value;
        }

        protected virtual void FilterBar_Search(object sender, FilterBarSearchEventArgs e)
        {            
            RefreshGrid(
                Grid.CurrentPageIndex, 
                e.Filter, 
                true, 
                this.ParseParameters(e.Filter)
            );
        }

        protected virtual void FilterBar_Clear(object sender, EventArgs e)
        {            
            RefreshGrid(Grid.CurrentPageIndex, null, true);
        }

        protected virtual void ButtonAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect($"{FormUrl}/?{Global.QUERYSTRINGID}=0&{Global.QUERYSTRINGMODE}={Global.COMMANDINSERT}");
        }

        protected virtual void Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            RefreshGrid(e.NewPageIndex);
        }

        protected virtual void Grid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string commandArgument = e.CommandArgument?.ToString();

                if (string.IsNullOrEmpty(commandArgument))
                    return;
                                
                TId id = Parse(commandArgument);

                if (id.Equals(default(TId)))
                    return;

                switch (e.CommandName)
                {
                    case Global.COMMANDSELECT:
                        string script = $"window.parent.$(window.frameElement.parentNode).data('kendoMessageBox').lookupManager.setData({{ field: '{PropertyId}', operation: '=', value: {id}}}); ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "modal-select", script, true);
                        break;
                    case Global.COMMANDEDIT:
                    case Global.COMMANDVIEW:
                        Response.Redirect($"{FormUrl}?{Global.QUERYSTRINGID}={id}&{Global.QUERYSTRINGMODE}={e.CommandName}");
                        break;

                    case Global.COMMANDDELETE:
                        Dao.AutoCloseConnection = false;
                        Dao.Delete(id);
                        this.SendMessage(DeleteMessage);
                        FilterBar.ExecuteSearch();
                        Dao.CloseConnection();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.SendErrorMessage();
            }
        }                
    }
}

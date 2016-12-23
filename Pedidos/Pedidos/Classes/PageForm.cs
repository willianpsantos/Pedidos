using Pedidos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Pedidos.UserControls;
using Pedidos.Util;

namespace Pedidos.Classes
{
    public delegate void SaveEventHandler<TEntity>(object sender, PageFormSaveEventArgs<TEntity> e);


    public abstract class PageForm<TEntity, TId> : System.Web.UI.Page, ILookupRequestResolver where TEntity : class
    {        
        protected IDataAccessObject<TEntity, TId> Dao;

        protected virtual string ListUrl { get; }

        protected virtual string InsertMessage { get; }

        protected virtual string UpdateMessage { get; }

        protected virtual string EntityNotFoundMessage { get; }

        protected virtual FormView FormView { get; }

        protected virtual Button SaveButton { get; }

        protected event SaveEventHandler<TEntity> BeforeSave;

        protected event SaveEventHandler<TEntity> AfterSave;


        public PageForm()
        {
            
        }


        protected virtual void Page_Load(object sender, EventArgs e)
        {
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
                SaveButton.Click += SaveButton_Click;

                string mode = Request.QueryString[Global.QUERYSTRINGMODE];

                switch (mode)
                {
                    case Global.COMMANDVIEW:
                        FormView.ChangeMode(System.Web.UI.WebControls.FormViewMode.ReadOnly);
                        SaveButton.Visible = false;
                        break;
                    case Global.COMMANDEDIT:
                        FormView.ChangeMode(System.Web.UI.WebControls.FormViewMode.Edit);
                        SaveButton.Visible = true;
                        break;
                    case Global.COMMANDINSERT:
                        FormView.ChangeMode(System.Web.UI.WebControls.FormViewMode.Insert);
                        SaveButton.Visible = true;
                        break;
                }
            }
        }
        

        protected virtual TEntity CreateInstance()
        {
            var type = typeof(TEntity);
            var entity = type.Assembly.CreateInstance(type.FullName);
            return (TEntity)entity;
        }

        public virtual Lookup GetLookupRequested()
        {
            var target = Request.Params[Constantes.EventTargetParam];
            var clientId = target.Replace(Constantes.LookupControlIdentifier, string.Empty);
            return GetLookupRequested(clientId);
        }

        public abstract Lookup GetLookupRequested(string clientId);

        public virtual TEntity FormView_GetItem([QueryString] TId id)
        {
            if (id.Equals(default(TId)))
            {
                return CreateInstance();
            }

            TEntity entity = Dao.GetById(id);
            return entity;
        }

        public virtual void FormView_UpdateItem([QueryString] TId id)
        {
            try
            {
                TEntity item = CreateInstance();
                TryUpdateModel(item);

                BeforeSave?.Invoke(this, new PageFormSaveEventArgs<TEntity>() { Entity = item });
                Dao.Save(item);
                AfterSave?.Invoke(this, new PageFormSaveEventArgs<TEntity>() { Entity = item });

                string url = this.ResolveUrl(this.Request.Url.AbsolutePath);
                url += $"?{Global.QUERYSTRINGMODE}={Global.COMMANDEDIT}&{Global.QUERYSTRINGID}={Dao.GetIdValue(item).ToString()}";
                this.SendMessage(UpdateMessage, handler: "window.location.href = '" + url + "'; ");
            }
            catch (Exception ex)
            {
                this.SendErrorMessage();
            }
        }

        public virtual void FormView_InsertItem()
        {
            try
            {
                TEntity item = CreateInstance();
                TryUpdateModel(item);

                BeforeSave?.Invoke(this, new PageFormSaveEventArgs<TEntity>() { Entity = item });
                Dao.Save(item);
                AfterSave?.Invoke(this, new PageFormSaveEventArgs<TEntity>() { Entity = item });

                string url = this.ResolveUrl(this.Request.Url.AbsolutePath);
                url += $"?{Global.QUERYSTRINGMODE}={Global.COMMANDEDIT}&{Global.QUERYSTRINGID}={Dao.GetIdValue(item).ToString()}";
                this.SendMessage(InsertMessage, handler: "window.location.href = '" + url + "'; ");
            }
            catch (Exception ex)
            {
                this.SendErrorMessage();
            }
        }


        public virtual void SaveButton_Click(object sender, EventArgs e)
        {
            switch (FormView.CurrentMode)
            {
                case System.Web.UI.WebControls.FormViewMode.Insert:
                    FormView.InsertItem(true);
                    break;

                case System.Web.UI.WebControls.FormViewMode.Edit:
                    FormView.UpdateItem(true);
                    break;
            }
        }        
    }
}

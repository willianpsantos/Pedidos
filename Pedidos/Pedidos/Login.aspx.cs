using Pedidos.Web.Security;
using System;

namespace Pedidos
{
    public partial class Login : System.Web.UI.Page
    {
        public Login()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string senha = txtPassword.Text;            

            if (string.IsNullOrEmpty(login))
            {
                lblMensagem.Text = "Informe o Usuário";
                lblMensagem.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(senha))
            {
                lblMensagem.Text = "Informe a Senha";
                lblMensagem.Visible = true;
                return;
            }

            var user = WebSecurity.Autenticate(login, senha);

            if (user == null)
            {
                lblMensagem.Text = "Usuário ou Senha inválidos";
                lblMensagem.Visible = true;
                return;
            }

            WebSecurity.SetUser(user);
            Response.Redirect("~/Main.aspx");
        }
    }
}
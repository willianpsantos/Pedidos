using Pedidos.DataAccess;
using System.Web;

namespace Pedidos.Web.Security
{
    public static class WebSecurity
    {
        private const string USER_SESSION_NAME = "pedidos.user.session";
        private const string LOGIN_PAGE = "~/Login.aspx";

        public static User Autenticate(string login, string password)
        {
            UsuarioDataAccess dao = new UsuarioDataAccess(new Pedidos.Model.PedidosDataContext());
            var obj = dao.Autenticate(login, password);

            if (obj == null || obj.CODG == 0)
            {
                return null;
            }

            return new User()
            {
                Id = obj.CODG,
                Login = obj.LOGIN,
                Profile = obj.PERFIL
            };
        }

        public static void SetUser(User user)
        {
            HttpContext.Current.Session[USER_SESSION_NAME] = user;
        }

        public static User GetUser()
        {
            return HttpContext.Current.Session[USER_SESSION_NAME] as User;
        }

        public static void Logout()
        {
            HttpContext.Current.Session[USER_SESSION_NAME] = null;
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Server.Transfer(LOGIN_PAGE, false);
        }

        public static void Validate()
        {
            var user = GetUser();

            if (user == null)
            {
                Logout();
                return;
            }

            if(user.Id == 0 || string.IsNullOrEmpty(user.Login))
            {
                Logout();
            }
        }
    }
}

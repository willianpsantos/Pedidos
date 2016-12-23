using Pedidos.Model;
using System.Data.Entity;
using System.Linq;
using System.Security;

namespace Pedidos.DataAccess
{
    public class UsuarioDataAccess : GenericDataAccessObject<USUARIO, int>
    {
        public UsuarioDataAccess(DbContext dataContext) : base(dataContext)
        {

        }
             
        public USUARIO Autenticate(string login, string password)
        {
            var user = Filter(x => x.LOGIN.ToLower() == login.ToLower() && x.SENHA == password).FirstOrDefault();
            return user;
        }
    }
}

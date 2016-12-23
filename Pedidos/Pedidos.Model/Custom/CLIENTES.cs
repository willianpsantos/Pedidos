using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Model
{
    public partial class CLIENTES
    {
        public string ObservacaoString
        {
            get
            {
                if (this.CLIOBS == null)
                    return string.Empty;

                return Encoding.UTF8.GetString(this.CLIOBS);
            }
            set
            {
                this.CLIOBS = Encoding.UTF8.GetBytes(value);
            }
        }
    }
}

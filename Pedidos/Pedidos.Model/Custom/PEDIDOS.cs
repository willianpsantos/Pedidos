using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Model
{
    public partial class PEDIDOS
    {
        public string ObservacaoString
        {
            get
            {
                if (this.PEDOBS == null)
                    return string.Empty;

                return Encoding.UTF8.GetString(this.PEDOBS);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.PEDOBS = null;
                    return;
                }

                this.PEDOBS = Encoding.UTF8.GetBytes(value);
            }
        }

        public string Observacao2String
        {
            get
            {
                if (this.PEDOBS2 == null)
                    return string.Empty;

                return Encoding.UTF8.GetString(this.PEDOBS2);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.PEDOBS2 = null;
                    return;
                }

                this.PEDOBS2 = Encoding.UTF8.GetBytes(value);
            }
        }

        public string PEDDATAEMISSAOString
        {
            get
            {
                if (!this.PEDDATAEMISSAO.HasValue)
                {
                    return string.Empty;
                }

                return this.PEDDATAEMISSAO.Value.ToShortDateString();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    this.PEDDATAEMISSAO = null;
                }

                this.PEDDATAEMISSAO = Convert.ToDateTime(value);
            }
        }

        public string PEDDATALOCACAOString
        {
            get
            {
                if (!this.PEDDATALOCACAO.HasValue)
                {
                    return string.Empty;
                }

                return this.PEDDATALOCACAO.Value.ToShortDateString();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    this.PEDDATALOCACAO = null;
                }

                this.PEDDATALOCACAO = Convert.ToDateTime(value);
            }
        }

        public string PEDDATADEVOLUCAOString
        {
            get
            {
                if (!this.PEDDATADEVOLUCAO.HasValue)
                {
                    return string.Empty;
                }

                return this.PEDDATADEVOLUCAO.Value.ToShortDateString();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    this.PEDDATADEVOLUCAO = null;
                }

                this.PEDDATADEVOLUCAO = Convert.ToDateTime(value);
            }
        }

        public string PEDDATASAIDAString
        {
            get
            {
                if (!this.PEDDATASAIDA.HasValue)
                {
                    return string.Empty;
                }

                return this.PEDDATASAIDA.Value.ToShortDateString();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    this.PEDDATASAIDA = null;
                }

                this.PEDDATASAIDA = Convert.ToDateTime(value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Util
{
    public class Filter
    {
        private Type _itemType;

        public string FilterBy { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public Type ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;

                if ((_itemType.Equals(typeof(DateTime)) || _itemType.Equals(typeof(DateTime?))))
                {
                    HasParameter = true;
                }
            }
        }

        public bool HasParameter { get; set; } = false;
        

        public Filter()
        {
            this.ItemType = typeof(string);
        }


        private string CastValue()
        {
            if (ItemType == null)
            {
                ItemType = typeof(String);
            }

            if (ItemType.Equals(typeof(String)))
            {
                string aux = Value == null ? "" : Value;
                return $"\"{aux}\"";
            }

            if ((ItemType.Equals(typeof(Int16)) || ItemType.Equals(typeof(Int16?))) ||
                 (ItemType.Equals(typeof(Int32)) || ItemType.Equals(typeof(Int32?))) ||
                   (ItemType.Equals(typeof(Int64)) || ItemType.Equals(typeof(Int64?))) ||
                     (ItemType.Equals(typeof(Decimal)) || ItemType.Equals(typeof(Decimal?))) ||
                       (ItemType.Equals(typeof(Double)) || ItemType.Equals(typeof(Double?))) ||
                         (ItemType.Equals(typeof(float)) || ItemType.Equals(typeof(float?))))
            {
                return $"{Value}";
            }

            if ((ItemType.Equals(typeof(DateTime)) || ItemType.Equals(typeof(DateTime?))))
            {
                return "@0"; //parameter
            }

            return $"{Value}";
        }

        public override string ToString()
        {
            HasParameter = false;

            

            if (string.IsNullOrEmpty(FilterBy) || string.IsNullOrEmpty(Operator))
                return string.Empty;

            if (Value == null)
                Value = string.Empty;

            var expresion = $"{Constantes.DEFAULT_ALIAS}.{FilterBy} {Operator} {CastValue()}";

            switch (Operator)
            {
                case "startswith":
                    expresion = $"{Constantes.DEFAULT_ALIAS}.{FilterBy}.StartsWith({CastValue()})";
                    break;
                case "endswith":
                    expresion = $"{Constantes.DEFAULT_ALIAS}.{FilterBy}.EndsWith({CastValue()})";
                    break;
                case "contains":
                    expresion = $"{Constantes.DEFAULT_ALIAS}.{FilterBy}.Contains({CastValue()})";
                    break;
            }

            if ((ItemType.Equals(typeof(DateTime)) || ItemType.Equals(typeof(DateTime?))))
            {
                if (ItemType.Equals(typeof(DateTime)))
                {
                    expresion = $"({Constantes.DEFAULT_ALIAS}.{FilterBy} {Operator} {CastValue()}"; ;
                }
                else if (ItemType.Equals(typeof(DateTime?)))
                {
                    expresion = $"({Constantes.DEFAULT_ALIAS}.{FilterBy} != null && {Constantes.DEFAULT_ALIAS}.{FilterBy} {Operator} {CastValue()})"; ;
                }
                
                HasParameter = true;
            }

            return expresion;
        }        
    }
}

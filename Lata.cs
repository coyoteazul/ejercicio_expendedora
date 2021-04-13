using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio_expendedora
{
    class Lata
    {
        private string _codigo,
                       _nombre,
                       _sabor;
        private double _precio,
                       _volumen;
        private int _cantidad;
        
        public double precio
        {
            get { return _precio; }
        }
        public string codigo
        {
            get { return _codigo; }
        }
        public int cantidad
        {
            set
            {
                if (value < 0)
                    throw new ExceptionStockCero();
                else
                    _cantidad = value;
            }
            get { return _cantidad; }
        }

        public Lata(string codigo,
                    string nombre,
                    string sabor,
                    double precio,
                    double volumen,
                    int cantidad)
        {
            this._codigo = codigo;
            this._nombre = nombre;
            this._sabor = sabor;
            this._precio = precio;
            this._volumen = volumen;
            this._cantidad = cantidad;
        }

        public double GetPrecioPorLitro()
        {
            return (_precio / _volumen * 1000);
        }

        /// <summary>
        /// Devuelve string formateado
        /// </summary>
        /// <param name="ver">0 detalle completo
        ///1 codigo, nombre y cantidad
        ///2 1 + precio</param>
        ///<exception cref="ExceptionLataString">Cuando se solicito un tipo de reporte no formateado</exception>
        public string ToString(short ver)
        {
            switch (ver) {
                case 0:
                    return string.Format("{0} - {1} ${2:N2} / $/L {3:N4} - [{4:N0}]", _nombre, _sabor, _precio, GetPrecioPorLitro(), cantidad);
                case 1:
                    return string.Format("{0}) {1} [{2:N0}]", _codigo, _nombre, _cantidad);
                case 2:
                    return string.Format("{0}) {1} [{2:N0}] ${3:N2}", _codigo, _nombre, _cantidad, _precio);
                default:
                    throw new ExceptionLataString();
            }

        }

        private class LataComparer : IEqualityComparer <Lata>
        {
            public bool Equals (Lata a, Lata b)
            {
                return a._codigo == b._codigo;
            }
            public int GetHashCode (Lata a)
            {
                return a._codigo.GetHashCode();
            }
        }
        public static IEqualityComparer<Lata> Comparer {
            get { return new LataComparer(); }
        }

        public class ExceptionLataString : Exception
        {
            public ExceptionLataString() : base("El tipo de reporte es incorrecto") { }
        }
        public class ExceptionStockCero : Exception
        {
            public ExceptionStockCero() : base("El stock es cero") { }
        }

    }
}

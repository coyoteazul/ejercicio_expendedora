using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio_expendedora
{
    class Expendedora
    {
        private HashSet<Lata> latas = new HashSet<Lata>(Lata.Comparer);
        string proveedor = "Bernardo";
        int capacidad = 50;
        double dinero = 0;
        bool encendida = false;

        public void EncenderMaquina()
        {
            encendida = true;
        }

        /// <summary>
        /// Lista las latas disponible con el formato especificado por ver
        /// </summary>
        /// <exception cref="ExceptionMaquinaVacia">No hay latas para mostrar</exception>
        /// <exception cref="ExceptionEncendido">Cuando se llama a la funcion mientras la maquina no esta encendida</exception>
        public List<string> ListarDisponibles(short ver)
        {
            if (encendida)
            {
                if (!EstaVacia())
                {
                    List<string> retorno = new List<string>();
                    foreach (Lata i in latas)
                    {
                        retorno.Add(i.ToString(ver));
                    }
                    return retorno;
                }
                else
                {
                    throw new ExceptionMaquinaVacia();
                }
            }
            else
            {
                throw new ExceptionEncendido();
            }
            
        }

        /// <summary>
        /// Inserta un nuevo tipo de lata o agrega el stock si el codigo ya existia
        /// </summary>
        /// <exception cref="ExceptionCapacidad">Cuando si se anadieran las latas solicitadas se superaria la capacidad disponible</exception>
        /// <exception cref="ExceptionEncendido">Cuando se llama a la funcion mientras la maquina no esta encendida</exception>
        public void AgregarLata(Lata _lata)
        {
            if (this.encendida)
            {
                if (this.GetCapacidadRestante() > _lata.cantidad)
                {
                    if (!latas.Add(_lata))
                    {
                        latas.First(p => p.codigo == _lata.codigo).cantidad += _lata.cantidad;
                    }
                }
                else
                {
                    throw new ExceptionCapacidad();
                }
            }
            else
            {
                throw new ExceptionEncendido();
            }
        }

        private int GetCapacidadRestante()
        {
            int stock = 0;
            foreach (Lata i in this.latas)
            {
                stock += i.cantidad;
            }
            return capacidad - stock;
        }

        /// <summary>
        /// Reduce en 1 el stock de la lata solicitada e incrementa dinero por el precio de la lata
        /// </summary>
        /// <param name="_codigo">Codigo de lata a vender</param>
        /// <param name="dinero_insertado">Dinero insertado por el comprador</param>
        /// <param name="cambio"> Dinero a devolver</param>
        /// <exception cref="InvalidOperationException">Cuando el codigo de lata ingresado no existe</exception>
        /// <exception cref="Lata.ExceptionStockCero">El stock de la lata solicitada es cero</exception>
        /// <exception cref="ExceptionEncendido">Cuando se llama a la funcion mientras la maquina no esta encendida</exception>
        public void VenderLata (string _codigo, double dinero_insertado, out double cambio)
        {
            cambio = dinero_insertado; //Se setea primero para que devuelva el dinero en caso de throw
            if (this.encendida)
            {
                Lata tmp = latas.First<Lata>(i => i.codigo == _codigo);
                if (dinero_insertado >= tmp.precio)
                {
                    tmp.cantidad--;
                    dinero += tmp.precio;
                    cambio = dinero_insertado - tmp.precio;
                }
                else
                {
                    throw new ExceptionDineroInsuficiente();
                }
            }
            else
            {
                throw new ExceptionEncendido();
            }
        }

        /// <summary>
        /// Devuelve el saldo en dinero y el total de latas
        /// </summary>
        /// <exception cref="ExceptionEncendido">Cuando se llama a la funcion mientras la maquina no esta encendida</exception>
        public string GetBalance()
        {
            if (encendida)
                return string.Format("Balance ${0:N2} Latas [{1}]", dinero, (capacidad - GetCapacidadRestante()));
            else
                throw new ExceptionEncendido();
        }

        private bool EstaVacia()
        {
            return capacidad == GetCapacidadRestante();
        }

        public class ExceptionEncendido : Exception
        {
            public ExceptionEncendido() : base("La expendedora no esta encendida") { }
        }
        public class ExceptionCapacidad : Exception
        {
            public ExceptionCapacidad() : base("Capacidad insuficiente") { }
        }
        public class ExceptionDineroInsuficiente : Exception
        {
            public ExceptionDineroInsuficiente() : base("Se ingreso menos dinero del necesario para compra la lata selecionada") { }
        }
        public class ExceptionMaquinaVacia : Exception
        {
            public ExceptionMaquinaVacia() : base("La maquina no tiene latas para mostrar") { }
        }


    }
}

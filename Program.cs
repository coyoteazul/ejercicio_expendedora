using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ejercicio_expendedora
{
    class Program
    {
        static void Main(string[] args)
        {
            Expendedora expen = new Expendedora();
            while (true)
            {
                int opcion = GetIntegerInput("# Opciones\n" +
                                             "1 - Encender la Expendedora\n" +
                                             "2 - Comprar lata\n" +
                                             "3 - Ingresar lata\n" +
                                             "4 - Obtener balance\n" +
                                             "5 - Obtener inventario\n");
                switch (opcion)
                {
                    case 1:
                        expen.EncenderMaquina();
                        Console.WriteLine("Expendedora encendida");
                        break;
                    case 2:
                        VenderLata(expen);
                        break;
                    case 3:
                        IngresarLata(expen);
                        break;
                    case 4:
                        ObtenerBalance(expen);
                        break;
                    case 5:
                        MostrarStock(expen);
                        break;
                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }
            }
        }

        static string GetStringInput (string message)
        {
            
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine("Debes ingresar un valor");
                return GetStringInput(message);
            }
            else
            {
                return input;
            }
        }

        static double GetDoubleInput (string message)
        {
            string input = "";
            double retorno = 0;
            Console.WriteLine(message);
            input = Console.ReadLine();

            if (double.TryParse(input, out retorno))
            {
                return retorno;
            }
            else
            {
                Console.WriteLine("Debes ingresar un numero");
                return GetDoubleInput(message);
            }
        }

        static int GetIntegerInput (string message)
        {
            string input = "";
            int retorno = 0;
            Console.WriteLine(message);
            input = Console.ReadLine();

            if (int.TryParse(input, out retorno))
            {
                return retorno;
            }
            else
            {
                Console.WriteLine("Debes ingresar un numero");
                return GetIntegerInput(message);
            }
        }

        static void IngresarLata (Expendedora expe)
        {
            try
            {
                expe.AgregarLata(new Lata(GetStringInput ("Ingresar codigo"),
                                          GetStringInput ("Ingresar nombre"),
                                          GetStringInput ("Ingresar sabor"),
                                          GetDoubleInput ("Ingresar el precio"),
                                          GetDoubleInput ("Ingresar el volumen"),
                                          GetIntegerInput("Ingresa la cantidad de latas a ingresar")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void VenderLata (Expendedora expe)
        {
            try
            {
                Console.WriteLine("Latas disponibles");
                PrintList(expe.ListarDisponibles(1));

                double cambio;
                expe.VenderLata(GetStringInput("Ingresar codigo"),
                                GetDoubleInput("Ingresar el dinero"),
                                out cambio);
                Console.WriteLine(string.Format("Retira tu lata de la bandeja de latas y tus {0:N2} de la bandeja de cambio", cambio));
            }
            catch (InvalidOperationException)
            {
                //Para personalizar este mensaje desde el Exception tendria que capturar una excepcion y tirar otra
                Console.WriteLine("El codigo de lata no existe");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ObtenerBalance (Expendedora expe)
        {
            try
            {
                Console.WriteLine(expe.GetBalance());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void MostrarStock (Expendedora expe)
        {
            try
            {
                PrintList(expe.ListarDisponibles(0));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void PrintList (List<string> lista)
        {
            foreach (string i in lista)
            {
                Console.WriteLine(i);
            }
        }
    }
}

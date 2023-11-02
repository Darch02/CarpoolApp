using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarpooling
{
    //Clase Viaje
    internal class Viaje
    {
        //Atributos
        public string origen;
        public string destino;
        public double montoDinero;
        public string estado;
        public uint tipoPago;

        //Constructor
        public Viaje()
        {
            origen = Aplicacion.Preguntas("Escriba la dirección de origen", 1);
            destino = Aplicacion.Preguntas("Escriba la dirección de destino",1);

            //Condicional para validar que el monto de dinero sea correcto
            bool flag2 = false;
            while (!flag2)
            {
                montoDinero = Aplicacion.Preguntas("Escriba el monto a pagar", 2);
                if (montoDinero < 0)
                {
                    Console.WriteLine("Monto ingresado inválido");
                    flag2 = false;

                }
                else
                {
                    flag2 = true;
                }
            }

            //Se actualiza el estado como pendiente en el arreglo
            estado = "Pendiente";
            
            //Condicional para validar el método de pago
            flag2 = false;
            while (!flag2)
            {
                tipoPago = Aplicacion.Preguntas("Escriba el tipo de pago que desea utilizar. \n1. Efectivo \n2. Transefernecia", 2);
                if (tipoPago == 1 || tipoPago == 2 || tipoPago == 0)
                {
                    flag2 = true;
                }
                else
                {
                    Console.WriteLine("Método de pago inválido");
                    flag2 = false;
                }
            }
        }

        
    }
}

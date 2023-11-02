using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarpooling
{
    //Clase Pago
    internal class Pago
    {
        //Atributos
        public short tipoPago;
        public string banco;

        //Constructor
        public Pago(short tipoPago) 
        {
            this.tipoPago = tipoPago;
            realizarPago(tipoPago);
        }

        //Método para seleccionar el pago
        public void realizarPago(short tipoPago)
        {
            switch (tipoPago)
            { 
                case 1:
                    Console.WriteLine("La opción elegida es Pago en efectivo");
                    break;
                case 2:
                    Console.WriteLine("La opción elegida es Pago por transferencia");
                    banco = Aplicacion.Preguntas("Escribe el banco con el que deseas pagar:", 1);
                    break;
                default:
                    Console.WriteLine("La opción es incorrecta");
                    break;

            }
        }
    }


}

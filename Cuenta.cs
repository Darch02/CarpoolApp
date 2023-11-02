using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AppCarpooling
{
    //Clase Cuenta
    internal class Cuenta
    {
        //Atributos
        public string nombre;
        public uint Id;
        public string email;
        private string password;
        public uint celular;
        public string vehiculo;

        //Constructor
        public Cuenta(string nombre, uint id, string email, string password, uint celular, string vehiculo)
        {
            this.nombre = nombre;
            this.password = password;
            this.celular = celular;
            this.Id = id;
            this.email = email;
            this.vehiculo = vehiculo;
        }

    }

    //Se crea la clase Usuario de la cuál va a heredar de Cuenta
    class Usuario : Cuenta
    {
        //Constructor
        public Usuario(string nombre, uint id, string email, string password, uint celular, string vehiculo) : base(nombre, id, email, password, celular, vehiculo)
        {

        }

        //Métodos
        //Función para que el usuario pueda crear el viaje
        public void ArmarViaje()
        {
            Viaje viaje = new Viaje();
            StreamWriter sw = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\viajes.txt", true);
            sw.WriteLine(nombre+"," + viaje.origen + "," + viaje.destino + "," + viaje.montoDinero + ","  + viaje.estado+","+viaje.tipoPago);
            sw.Close();
        }

        //Función donde el usuario confirma las rutas ofrecidas
        public void ConfirmarViaje()
        {

            //Se crea un s¿arreglo de string donde se agregarán los viajes disponibles o rutas solicitadas
            string[] viajesUsuarios;
            viajesUsuarios = File.ReadAllLines("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\viajes.txt");
            
            //Se lista el arreglo
            Console.WriteLine("propuestas de viaje:");
            int i = 0;
            foreach (var item in viajesUsuarios)
            {
                i++;
                Console.WriteLine(i + ". " + item);
            }

            Console.WriteLine("escriba el número del viaje que quiere confirmar:");
            int numViaje = int.Parse(Console.ReadLine());
            StreamWriter sw = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\viajes.txt");
            StreamWriter sw2 = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\pagosPendientes.txt", true);

            //Capturar excepción y eliminar la ruta seleccionada
            if (numViaje > 0 && numViaje <= (viajesUsuarios.Length))
            {
                foreach (var linea in viajesUsuarios)
                {
                    if (linea != viajesUsuarios[numViaje - 1])
                    {
                        sw.WriteLine(linea);
                    }
                    else sw2.WriteLine(linea);
                }
            }
            else
            {
                Console.WriteLine("\nViaje no existente");
                foreach (var linea in viajesUsuarios)
                {
                    sw.WriteLine(linea);
                }
            }
            sw.Close();
            sw2.Close();
        }

        //Funcion para pagar los viajes confirmados
        public void pagar()
        {
            //Arreglo de string que contendrán los viajes disponibles
            string[] archivoViajes;
            archivoViajes = File.ReadAllLines("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\pagosPendientes.txt");
            string[] camposviajes;
            short tipoPago = 0;
            int i = 0;
            StreamWriter sw2 = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\pagosPendientes.txt");

            //Listar el arreglo
            Console.WriteLine("viajes pendientes por pagar:");
            foreach (string viaje in archivoViajes)
            {
                camposviajes = viaje.Split(',');
                if (camposviajes[0].Equals(nombre))
                {
                    i++;
                    Console.WriteLine(i + ". " + viaje);

                }
            }
            uint viajePagar = Aplicacion.Preguntas("escoja el numero del viaje a pagar", 2);

            //Capturar la excepción y el viaje
            if (viajePagar > 0 && viajePagar <= (archivoViajes.Length))
            {
                foreach (var viaje in archivoViajes)
                {
                    if (viaje != archivoViajes[viajePagar - 1])
                    {
                        sw2.WriteLine(viaje);
                    }
                    else
                    {
                        camposviajes = viaje.Split(",");
                        tipoPago = short.Parse(camposviajes[5]);
                    }

                }
            }
            else
            {
                Console.WriteLine("\nViaje no existente");
                foreach (var viaje in archivoViajes)
                {
                    sw2.WriteLine(viaje);
                }
                sw2.Close();
            }
            sw2.Close();
            //Capturar el pago
            Pago pago = new Pago(tipoPago);
            

        }

    }

}



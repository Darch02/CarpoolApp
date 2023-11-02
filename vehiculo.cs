using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarpooling
{
    //Clase Vehiculo
    internal class Vehiculo
    {
        //Clase atributos
        public string color;
        public string marca;
        public string modelo;
        public string placa;
        public uint nroPasajeros;

        //Constructor
        public Vehiculo()
        {
            color = Aplicacion.Preguntas("Escribe el color de tu carro", 1);
            placa = Aplicacion.Preguntas("Escribe la placa de tu vehiculo", 1);
            marca = Aplicacion.Preguntas("Escribe la marca de tu vehiculo", 1);
            modelo = Aplicacion.Preguntas("Escribe el modelo de tu vehiculo", 1);
            nroPasajeros = Aplicacion.Preguntas("¿Cuántos pasajeros puedes llevar?", 2);

        }
    }
}

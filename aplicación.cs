using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppCarpooling
{
    public class Aplicacion
    {
        //Función para hacer preguntas tipo Int y String capturando las excepciones
        public static dynamic Preguntas(string pregunta, int tipoResp)
        {
            Console.WriteLine(pregunta);
            dynamic data = null; // Data de tipo dynamic para que pueda convertirse en otros valores (int o string)
            if (tipoResp == 1) // Para String
            {
                try
                {
                    data = Console.ReadLine();
                    return data ?? "";
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else if (tipoResp == 2) // Para int
            {
                try
                {
                    data = uint.Parse(Console.ReadLine() ?? "");
                    return data ?? 0; //Devuelve un entero y captura la excepción
                }
                catch (Exception)
                {
                    Console.WriteLine("No se admiten carácteres");
                    return 0;
                }
            }
            else
            {
                return data;
            }
        }


        //Variables para correr la aplicación
        string nombreLog;
        string contraseñaLog;
        Usuario user;

        //Funciones para correr la aplicación
        public void login()
        {
            bool flag = true;
            //Ciclo que valida la creación de cuenta e inicio de sesión
            while (flag)
            {
                string crearCuenta = Aplicacion.Preguntas("INICIAR SESIÓN/CREAR CUENTA\n\n¿Desea crear una nueva cuenta? (si/no): \nElige 'no' si ya posees una.", 1);
                string respuesta = RemoverTildePonerMinuscula(crearCuenta);
                if (respuesta == "si")
                {
                    CrearCuenta();
                    flag = false;
                }
                else if (respuesta == "no")
                {
                    Console.WriteLine("¡OK! Ingresa las credenciales de tu usuario...");
                    flag = false;
                }
                else Console.WriteLine("Escriba una respuesta válida");

            }

            nombreLog = Aplicacion.Preguntas("USUARIO: ", 1);
            contraseñaLog = Aplicacion.Preguntas("CONTRASEÑA: ",1);
        }

        //Función para pasar las respuestas sin tildes y en minúscula
        public static string RemoverTildePonerMinuscula(string respuesta)
        { 
            string normalizedString = respuesta.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            string result = stringBuilder.ToString().ToLowerInvariant();
            return result;
    }

        //Función para verificar el usuario
        public void VerifLogin()
        {
            //Se guarda en un arreglo de strings el texto
            string[] archivoUsuarios;
            archivoUsuarios = File.ReadAllLines("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\usuarios.txt");
            char c = ',';
            bool log = false;
            string[] camposCuenta = null;

            foreach (string usuario in archivoUsuarios)
            {
                camposCuenta = usuario.Split(c);

                if (String.Equals(nombreLog, camposCuenta[0]) && String.Equals(contraseñaLog, camposCuenta[3]))
                {
                    log = true;
                    break;
                }
            }
            if (log)
            {
                user = new Usuario(camposCuenta[0], uint.Parse(camposCuenta[1]), camposCuenta[2], camposCuenta[3], uint.Parse(camposCuenta[4]), camposCuenta[5]);

                Console.WriteLine($"Bienvenido {user.nombre}");
            }
            else
            {
                Console.WriteLine("Usuario y/o contraseña incorrectos");
                login();
            }
        }
        public void CrearCuenta()
        {
            //Solicitud de datos
            string nombre = Aplicacion.Preguntas("Escriba el nombre de usuario", 1);
            string contraseña = Aplicacion.Preguntas("Escriba la contraseña", 1);
            uint celular = 0;

            bool flag = false;
            while (flag == false)
            {
                Console.WriteLine("Escriba el número de teléfono");

                if (uint.TryParse(Console.ReadLine(), out celular))
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Entrada inválida, escriba un número de teléfono");
                    flag = false;
                }
            }

            Console.WriteLine("escriba el ID: ");
            uint id;
            flag = uint.TryParse(Console.ReadLine(), out id);
            while (flag == false)
            {
                Console.WriteLine("error: por favor escriba un número de ID válido:");
                flag = uint.TryParse(Console.ReadLine(), out id);
            }

            string email = Aplicacion.Preguntas("Escriba la dirección de correo", 1);

            string poseeVehiculo = Aplicacion.Preguntas("¿Tiene vehículo? (si/no)",1);
            string respuesta = RemoverTildePonerMinuscula(poseeVehiculo);

            if (respuesta == "si")
            {
                Vehiculo vehiculo = new Vehiculo();
                StreamWriter sw1 = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\vehiculos.txt", true);
                sw1.WriteLine(nombre + "," + vehiculo.color + "," + vehiculo.placa + "," + vehiculo.marca + "," + vehiculo.modelo + "," + vehiculo.nroPasajeros);
                sw1.Close();

                StreamWriter sw2 = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\usuarios.txt", true);
                sw2.WriteLine(nombre + "," + id + "," + email + "," + contraseña + "," + celular + ","+"si");
                sw2.Close();

                Console.WriteLine("la cuenta de ha creado exitosamente");
            }
            else
            {
                StreamWriter sw2 = new StreamWriter("C:\\Users\\Juana\\OneDrive - UPB\\Documents\\POO\\AppCarpooling\\usuarios.txt", true);
                sw2.WriteLine(nombre + "," + id + "," + email + "," + contraseña + "," + celular+","+"no");
                sw2.Close();

                Console.WriteLine("la cuenta de ha creado exitosamente");

            }
        }

        //Función para ejecutar el menú principal        
        public void MenuPrincipal()
        {
            
            bool flag = false;
            object seleccion = 0;
            while (flag == false)
            {
                Console.WriteLine("\nEscoja una de las siguientes opciones:\n1.pedir ruta\n2.confirmar ruta" +
                    "\n3.ver usuario\n4.Pagar\n5. Salir\nNOTA: Ingresa el número correspondiente a la opción");
                seleccion = Console.ReadLine();
                if (seleccion != null)
                {                    
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            switch (seleccion)
            {
                case "1": user.ArmarViaje();
                        MenuPrincipal();
                        break;
                case "2": user.ConfirmarViaje();
                        MenuPrincipal();
                        break;
                case "3": Console.WriteLine($"nombre = {user.nombre}\nId = {user.Id}\nemail = {user.email}\nCelular= {user.celular}");
                        MenuPrincipal();
                        break;
                case "4":user.pagar();
                        MenuPrincipal();
                        break;
                case "5": Console.WriteLine("hasta luego");
                    break;
                default: Console.WriteLine("¡UPS! Parece que la opción seleccionada no se encuentra en el menú.");
                        MenuPrincipal();
                        break;
            }
        }

        
       
    }

}

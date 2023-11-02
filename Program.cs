using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace AppCarpooling
{
    class Program
    {
        static void Main(string[] args)
        {
            Aplicacion app = new Aplicacion();
            app.login();
            app.VerifLogin();
            app.MenuPrincipal();

        }
       
    }
}

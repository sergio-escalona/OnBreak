using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onbreak;

namespace ProyectoPrueba
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pruebas de integración
            
            // Crear nuevo cliente
            Cliente cli = new Cliente()
            {
                RutCliente = "172034802",
                RazonSocial = "DUOC",
                NombreContacto = "Sergio",
                MailContacto = "sergio@duoc.cl",
                Direccion = "Viña del Mar",
                Telefono = "98543249",
                IdActividadEmpresa = 3,
                IdTipoEmpresa = 10
            };

            if (cli.Create())
            {
                Console.Write("EXITO -  Create()");
            }

            else
            {
                Console.Write("ERROR - Create()");
            }

            Console.ReadKey();

            Cliente readcli = new Cliente();

            //Listar todos los clientes
            List<Cliente> lista = readcli.ReadAll();
            foreach (Cliente item in lista)
            {
                string salida = string.Format("RUT: {0} || Razon Social: {1} || Nombre: {2} || Mail: {3} || Direccion: {4} || Telefono: {5} || ID Actividad: {6} || ID Tipo: {7} \n",
                                                item.RutCliente,
                                                item.RazonSocial,
                                                item.NombreContacto,
                                                item.MailContacto,
                                                item.Direccion,
                                                item.Telefono,
                                                item.IdActividadEmpresa,
                                                item.IdTipoEmpresa);
                Console.WriteLine(salida);
            }
            Console.ReadKey();

            //Crear nuevo contrato
            TipoEvento te = new TipoEvento();
            Contrato con = new Contrato()
            {
                Numero = "202005281926",
                Creacion = new DateTime(2020, 05, 28, 19, 26, 00),
                Termino = SqlDateTime.MinValue.Value,
                RutCliente = "172034802",
                IdModalidad = "CE001",
                IdTipoEvento = 30,
                FechaHoraInicio = new DateTime(2020, 06, 15, 20, 00, 00),
                FechaHoraTermino = new DateTime(2020, 06, 15, 23, 30, 00),
                Asistentes = 30,
                PersonalAdicional = 3,
                Realizado = false,
                ValorTotalContrato = te.ValorizaPago(30,3,25),
                Observaciones = "Tener Alternativa vegana"
            };

            if (con.Create())
            {
                Console.Write("EXITO - Create()");
            }

            else
            {
                Console.Write("Error - Create()");
            }

            Console.ReadKey();
        }
    }
}

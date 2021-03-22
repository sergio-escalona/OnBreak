using Microsoft.VisualStudio.TestTools.UnitTesting;
using Onbreak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onbreak.Tests
{
    [TestClass()]
    public class ClienteTests
    {
        //Pruebas Unitarias

        //Crear un cliente que ya existe
        [TestMethod()]
        public void CreateTest()
        {
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

            bool resp = cli.Create();
            Assert.AreEqual(false, resp);
        }

        //Buscar un cliente que se encuentre en la base de datos
        [TestMethod()]
        public void ReadTest()
        {
            Cliente cli = new Cliente()
            {
                RutCliente = "172034802"
            };

            bool resp = cli.Read();
            Assert.AreEqual(true, resp);
        }

        //Eliminar cliente de la base de datos
        [TestMethod()]
        public void DeleteTest()
        {
            Cliente cli = new Cliente()
            {
                RutCliente = "20356842k"
            };

            bool resp = cli.Delete();
            Assert.AreEqual(true, resp);
        }
        
        //Pruebas funcionales
        
        //Valorizador realiza el cálculo adecuado
        [TestMethod()]
        public void ValorizadorTest()
        {
            TipoEvento te = new TipoEvento();

            double valor = te.ValorizaPago(20, 5, 6);
            Assert.AreEqual(13, valor);
        }

        //Busqueda por tipo de evento
        [TestMethod()]
        public void BuscarPorEventoTest()
        {
            Contrato con = new Contrato();

            int resp = con.ReadTipo(20).Count();
            Assert.AreEqual(1, resp);
        }

        //Carga de modalidades según tipo de evento
        [TestMethod()]
        public void CargarModalidadTest()
        {
            ModalidadServicio ms = new ModalidadServicio();

            int resp = ms.ReadTipo(10).Count();
            Assert.AreEqual(3, resp);
        }
    }
}
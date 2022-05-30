using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;
using DemoCasoPracticoShigui.Models;
using DemoCasoPracticoShigui.Controllers;

namespace TestDemoApi
{
    [TestClass]
    public class ServiceMethodTest : BasseConnection
    {
        [TestMethod]
        public async Task getCliente()
        {
            string Bdname = Guid.NewGuid().ToString();
            var bdContex = getCxData(Bdname);
            bdContex.Clientes.Add(
                new Cliente { 
                    Identificacion = "0503618464",
                    ClContrasenia = "12345",
                    ClEstado = true,
                    Nombre = "Eduardo Shigui",
                    Genero ="Masculino",
                    Edad = 30,
                    Direccion = "La Napo",
                    Telefono = "0985462351"
                });
            await bdContex.SaveChangesAsync();

            var contexto = getCxData(Bdname);
            var controlador = new ClientesController(contexto);
            var respuesta = await controlador.GetClientes();
            //Validación respuesta
            var cliente = respuesta.Value;
            Assert.AreEqual(1, cliente.Count);
        }

        [TestMethod]
        public async Task getCuentas()
        {
            string Bdname = Guid.NewGuid().ToString();
            var bdContex = getCxData(Bdname);
            bdContex.Cuentas.Add(
                new Cuenta
                {
                    CuNumeroCuenta = "256541",
                    CuIdCliente = 1 ,
                    CuTipo = "Ahorros",
                    CuEstado = true
                });
            await bdContex.SaveChangesAsync();

            var contexto = getCxData(Bdname);
            var controlador = new CuentasController(contexto);
            var respuesta = await controlador.GetCuentas();
            //Validación respuesta
            var cuentas = respuesta.Value;
            Assert.AreEqual(1, cuentas.Count);
        }

        [TestMethod]
        public async Task getMovimientos()
        {
            string Bdname = Guid.NewGuid().ToString();
            var bdContex = getCxData(Bdname);
            bdContex.Movimientos.Add(
                new Movimiento { 
                    MoNumeroCuenta ="23641265",
                    MoFecha = DateTime.Now,
                    MoTipoMovimiento = "Deposito",
                    MoSaldoInicial = 1000,
                    MoMovimientos = 50,
                    MoSaldoDisponible = 2000
                });
            await bdContex.SaveChangesAsync();

            var contexto = getCxData(Bdname);
            var controlador = new MovimientosController(contexto);
            var respuesta = await controlador.GetMovimientos();
            //Validación respuesta
            var movimientos = respuesta.Value;
            Assert.AreEqual(1, movimientos.Count);
        }
    }
}

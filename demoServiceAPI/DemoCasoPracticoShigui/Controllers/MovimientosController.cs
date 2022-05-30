
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoCasoPracticoShigui.Data;
using DemoCasoPracticoShigui.Models;
using DemoCasoPracticoShigui.Response;
using DemoCasoPracticoShigui.Utils;
using Newtonsoft.Json;
using DemoCasoPracticoShigui.RequestModel;

namespace DemoCasoPracticoShigui.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly BBDDCasoPracticoContext _context;


        public MovimientosController(BBDDCasoPracticoContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<List<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.MoIdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseServices> PostMovimiento(Movimiento objMovimiento)
        {
            // Validar valor monto
            ResponseServices response = new ResponseServices();
            objMovimiento.MoFecha = DateTime.Now;
            if (objMovimiento.MoMovimientos <= 0)
            {
                response.Exito = false;
                response.Mensaje = MensajesServicio.Monto;
                return response;
            }
            Movimiento datoMovimientos = await ConsultarMovimientos(objMovimiento);
            Cuenta datoCuenta = await ObtenerCuenta(objMovimiento);
            if (datoMovimientos == null)
            {
                datoMovimientos = new Movimiento();
                if (datoCuenta == null)
                {
                    response.Exito = false;
                    response.Mensaje = MensajesServicio.NoExisteCuenta;
                    return response;
                }
                datoMovimientos.MoSaldoDisponible = Math.Abs(datoCuenta.CuSaldoInicial);
                if (string.Equals(objMovimiento.MoTipoMovimiento.ToUpper(), AccionCuenta.Credito.ToUpper()))
                    objMovimiento.MoSaldoDisponible = objMovimiento.MoSaldoInicial + objMovimiento.MoMovimientos;
                else
                    objMovimiento.MoSaldoDisponible = objMovimiento.MoSaldoInicial - objMovimiento.MoMovimientos;
            }
            objMovimiento.MoSaldoInicial = Math.Abs(datoCuenta.CuSaldoInicial);
            if (string.Equals(objMovimiento.MoTipoMovimiento.ToUpper(), AccionCuenta.Credito.ToUpper())
                || string.Equals(objMovimiento.MoTipoMovimiento.ToUpper(), AccionCuenta.Debito.ToUpper()))
            {
                if (string.Equals(objMovimiento.MoTipoMovimiento.ToUpper(), AccionCuenta.Credito.ToUpper()))
                    objMovimiento.MoSaldoDisponible = datoMovimientos.MoSaldoDisponible + objMovimiento.MoMovimientos;
                else
                {
                    if (Math.Abs(datoMovimientos.MoSaldoDisponible) < Math.Abs(objMovimiento.MoMovimientos))
                    {
                        response.Exito = false;
                        response.Mensaje = MensajesServicio.SaldoNo;
                        return response;
                    }
                    else
                    {
                        response = await ValidarLimiteDiarioRetiro(objMovimiento.MoNumeroCuenta, objMovimiento.MoMovimientos);
                        if (!response.Exito)
                            return response;
                        objMovimiento.MoSaldoDisponible = datoMovimientos.MoSaldoDisponible - objMovimiento.MoMovimientos;
                    }
                }
                response.Exito = true;
                _context.Movimientos.Add(objMovimiento);
                await _context.SaveChangesAsync();
                response.Data = objMovimiento;
            }
            else {
                if (string.Equals(objMovimiento.MoTipoMovimiento, "Cupo"))
                    response = await ValidarLimiteDiarioRetiro(objMovimiento.MoNumeroCuenta, objMovimiento.MoMovimientos);
                else {
                    response.Exito = false;
                    response.Mensaje = MensajesServicio.TrassacionError;
                }
            }
            return response;
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MoIdMovimiento == id);
        }
        private async Task<Movimiento> ConsultarMovimientos(Movimiento movimiento)
        {
            // Recuperar 
            Movimiento dataMovimientos =
                await _context.Movimientos.Where(x => x.MoNumeroCuenta == movimiento.MoNumeroCuenta)
                .OrderByDescending(x => x.MoFecha).FirstOrDefaultAsync();
            return dataMovimientos;
        }
        private async Task<Cuenta> ObtenerCuenta(Movimiento objMovimiento)
        {
            Cuenta objCuenta = await _context.Cuentas.Where(
                   x => x.CuNumeroCuenta == objMovimiento.MoNumeroCuenta).FirstOrDefaultAsync();
            return objCuenta;
        }
        [HttpPost("{NumeroCuenta},{MontoTransaccion}")]
        public async Task<ResponseServices> ValidarLimiteDiarioRetiro(string NumeroCuenta, decimal MontoTransaccion)
        {
            ResponseServices cupoResponse = new ResponseServices();
            decimal valorFinal = 0;
            DateTime diaActual = DateTime.Now;
            decimal cupoMovimientos = await _context.Movimientos.Where(
                x => x.MoNumeroCuenta == NumeroCuenta
                && x.MoFecha >= DateTime.ParseExact(diaActual.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null)
                && x.MoFecha <= DateTime.ParseExact(diaActual.ToString("dd-MM-yyyy") + " 23:59:59", "dd-MM-yyyy HH:mm:ss", null)
                && x.MoTipoMovimiento == AccionCuenta.Debito).SumAsync(a => a.MoMovimientos);
            valorFinal = Math.Abs(cupoMovimientos) + Math.Abs(MontoTransaccion);

            if (valorFinal >= AccionCuenta.CupoMaximoRetiro)
            {
                cupoResponse.Exito = false;
                cupoResponse.Mensaje = MensajesServicio.CupoLimite;
                cupoResponse.Data = Math.Abs(valorFinal);
            }
            else
                cupoResponse.Exito = true;

            return cupoResponse;
        }
        [HttpGet("{strIdentificacion}&{dtFechaInicio}&{dtFechaFin}")]
        public async Task<ResponseServices> PostConsultaFechas(string strIdentificacion, string dtFechaInicio, string dtFechaFin)
        {
            List<Movimiento> lstPMovimiento = new List<Movimiento>();
            ResponseServices response = new ResponseServices();
            try
            {
                response.Data = await _context.Movimientos.Select(x => new ListaMovimientos
                {
                    Fecha = x.MoFecha,
                    Nombre = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.Nombre,
                    NumeroCuenta = x.MoNumeroCuentaNavigation.CuNumeroCuenta,
                    Tipo = x.MoNumeroCuentaNavigation.CuTipo,
                    SaldoInicial = x.MoSaldoInicial,
                    Estado = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.ClEstado,
                    Movimiento = x.MoMovimientos,
                    SaldoDisponible = x.MoSaldoDisponible,
                    Identificacion = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.Identificacion,

                }).Where(s => s.Identificacion == strIdentificacion && s.Fecha >= Convert.ToDateTime(dtFechaInicio) && s.Fecha <= Convert.ToDateTime(dtFechaFin)).ToListAsync();
                response.Exito = true;
            }
            catch (Exception x)
            {
                response.Mensaje = "Hubo un error: " + x.StackTrace;
            }
            return response;
        }

    }
}

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

namespace DemoCasoPracticoShigui.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly BBDDCasoPracticoContext _context;
        private readonly ResponseServices response = new ResponseServices();

        public CuentasController(BBDDCasoPracticoContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<List<Cuenta>>> GetCuentas()
        {
            return await _context.Cuentas.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(string id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(string id, Cuenta cuenta)
        {
            if (id != cuenta.CuNumeroCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseServices> PostCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            try
            {
                await _context.SaveChangesAsync();
                response.Data = CreatedAtAction("GetCuenta", new { id = cuenta.CuNumeroCuenta }, cuenta);
                if (response.Data != null)
                    response.Exito = true;
            }
            catch (DbUpdateException)
            {
                if (CuentaExists(cuenta.CuNumeroCuenta))
                    response.Mensaje = MensajesServicio.ExisteCuenta;
            }
            return response;
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(string id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaExists(string id)
        {
            return _context.Cuentas.Any(e => e.CuNumeroCuenta == id);
        }
    }
}

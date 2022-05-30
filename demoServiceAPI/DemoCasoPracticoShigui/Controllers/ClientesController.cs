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
    public class ClientesController : ControllerBase
    {
        private readonly BBDDCasoPracticoContext _context;
        private readonly ResponseServices response = new ResponseServices();

        public ClientesController(BBDDCasoPracticoContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            try
            {
                
                return await _context.Clientes.ToListAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ResponseServices> GetCliente(int id)
        {
            try
            {
                response.Exito = true;
                response.Data = await _context.Clientes.FindAsync(id);
                if (response.Data == null) {
                    response.Exito = false;
                    response.Mensaje = MensajesServicio.NohayRegistro;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = MensajesServicio.ErrorServicio + ex.StackTrace.ToString();
            }
            return response;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClIdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseServices> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
                response.Data = CreatedAtAction("GetCliente", new { id = cliente.ClIdCliente }, cliente);
                if (response.Data != null)
                    response.Exito = true;
            }
            catch (Exception ex)
            {
                response.Mensaje = MensajesServicio.ErrorServicio + ex.StackTrace.ToString();
            }
            return response;
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClIdCliente == id);
        }
    }
}

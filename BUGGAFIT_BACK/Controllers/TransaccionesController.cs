﻿using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ICatalogoTransacciones catalogo;

        public TransaccionesController(ICatalogoTransacciones catalogo)
        {
            this.catalogo = catalogo;
        }
        // GET: api/GetTransacciones
        [HttpGet("GetTransacciones")]
        public async Task<ActionResult<ResponseObject>> GetTransacciones()
        {
            try
            {
                var result = await catalogo.ListarTrasaccionesAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        // POST api/GetTransaccionesPorFecha
        [HttpPost("GetTransaccionesPorFecha")]
        public async Task<IActionResult> GetDashboard([FromBody] FiltrosDTO filtro)
        {
            try
            {
                var result = await catalogo.ListarTrasaccionesPorFechaAsync(filtro);
                if (result.StatusCode == 400)
                    return BadRequest(ResponseClass.ErrorResponse(statusCode: 400, message: result.Message, error: new Exception()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPost("PostCrearTransaccion")]
        public async Task<ActionResult<ResponseObject>> PostCrearTransaccion(Transacciones transaccion)
        {
            try
            {
                return Ok(await catalogo.CrearTrasaccionAsync(transaccion));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPut("AnularTransaccion/{id}")]
        public async Task<ActionResult<ResponseObject>> AnularTransaccion(int id)
        {
            try
            {
                return Ok(await catalogo.AnularTrasaccionesAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPut("ConfirmarTransaccion/{id}")]
        public async Task<ActionResult<ResponseObject>> ConfirmarTransaccion(int id)
        {
            try
            {
                return Ok(await catalogo.ConfirmarTrasaccionesAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

    }
}
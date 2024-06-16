// PipeClinic\Controllers\PacientesController.cs

using Microsoft.AspNetCore.Mvc;
using PipeClinic.Models;
using PipeClinic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipeClinic.Controllers
{
    [ApiController]
    [Route("api/v1/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetAllPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
                return NotFound();
            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> AddPaciente(Paciente paciente)
        {
            var novoPaciente = await _pacienteService.AddPacienteAsync(paciente);
            return CreatedAtAction(nameof(GetPaciente), new { id = novoPaciente.Id }, novoPaciente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, Paciente pacienteAtualizado)
        {
            await _pacienteService.UpdatePacienteAsync(id, pacienteAtualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            await _pacienteService.DeletePacienteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/pesoideal")]
        public async Task<ActionResult<double>> ObterPesoIdeal(int id)
        {
            var pesoIdeal = await _pacienteService.ObterPesoIdealAsync(id);
            if (pesoIdeal == null)
                return NotFound();
            return Ok(pesoIdeal);
        }

        [HttpGet("{id}/situacaoimc")]
        public async Task<ActionResult<string>> ObterSituacaoIMC(int id)
        {
            var situacaoIMC = await _pacienteService.ObterSituacaoIMCAsync(id);
            if (situacaoIMC == null)
                return NotFound();
            return Ok(situacaoIMC);
        }

        [HttpGet("{id}/cpfofuscado")]
        public async Task<ActionResult<string>> ObterCpfOfuscado(int id)
        {
            var cpfOfuscado = await _pacienteService.ObterCpfOfuscadoAsync(id);
            if (cpfOfuscado == null)
                return NotFound();
            return Ok(cpfOfuscado);
        }

        [HttpPost("{id}/validarcpf")]
        public async Task<ActionResult<bool>> ValidarCpf(int id)
        {
            var isValido = await _pacienteService.ValidarCpfAsync(id);
            if (!isValido)
                return NotFound();
            return Ok(isValido);
        }
    }
}

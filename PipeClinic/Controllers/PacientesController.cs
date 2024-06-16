// PipeClinic/Controllers/PacientesController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/v1/pacientes")]
public class PacientesController : ControllerBase
{
    private static List<Paciente> pacientes = new List<Paciente>();

    // GET /api/v1/pacientes/all
    [HttpGet("all")]
    public ActionResult<IEnumerable<Paciente>> GetAllPacientes()
    {
        return Ok(pacientes);
    }

    // GET /api/v1/pacientes/{id}
    [HttpGet("{id}")]
    public ActionResult<Paciente> GetPaciente(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();
        return Ok(paciente);
    }

    // POST /api/v1/pacientes
    [HttpPost]
    public ActionResult<Paciente> AddPaciente(Paciente paciente)
    {
        paciente.Id = pacientes.Count + 1;
        pacientes.Add(paciente);
        return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
    }

    // PUT /api/v1/pacientes/{id}
    [HttpPut("{id}")]
    public IActionResult UpdatePaciente(int id, Paciente pacienteAtualizado)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        paciente.Nome = pacienteAtualizado.Nome;
        paciente.Sobrenome = pacienteAtualizado.Sobrenome;
        paciente.Sexo = pacienteAtualizado.Sexo;
        paciente.Nascimento = pacienteAtualizado.Nascimento;
        paciente.Altura = pacienteAtualizado.Altura;
        paciente.Peso = pacienteAtualizado.Peso;
        paciente.Cpf = pacienteAtualizado.Cpf;

        return NoContent();
    }

    // DELETE /api/v1/pacientes/{id}
    [HttpDelete("{id}")]
    public IActionResult DeletePaciente(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        pacientes.Remove(paciente);
        return NoContent();
    }

    // GET /api/v1/pacientes/{id}/pesoideal
    [HttpGet("{id}/pesoideal")]
    public ActionResult<double> ObterPesoIdeal(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        return Ok(paciente.ObterPesoIdeal());
    }

    // GET /api/v1/pacientes/{id}/situacaoimc
    [HttpGet("{id}/situacaoimc")]
    public ActionResult<string> ObterSituacaoIMC(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        return Ok(paciente.ObterSituacaoIMC());
    }

    // GET /api/v1/pacientes/{id}/cpf
    [HttpGet("{id}/cpf")]
    public ActionResult<string> ObterCpfOfuscado(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        return Ok(paciente.ObterCpfOfuscado());
    }

    // POST /api/v1/pacientes/{id}/validarcpf
    [HttpPost("{id}/validarcpf")]
    public ActionResult<bool> ValidarCpf(int id)
    {
        var paciente = pacientes.Find(p => p.Id == id);
        if (paciente == null)
            return NotFound();

        return Ok(paciente.ValidarCpf());
    }
}

// PipeClinic/Models/Paciente.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace PipeClinic.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public char Sexo { get; set; }
        public DateTime Nascimento { get; set; }
        public short Altura { get; set; }
        public double Peso { get; set; }
        public string Cpf { get; set; } = string.Empty;

        // Construtor padrão vazio (necessário para Entity Framework)
        public Paciente() { }

        // Métodos para cálculos
        public double ObterPesoIdeal()
        {
            if (Sexo == 'M')
                return (72.7 * Altura) - 58;
            else if (Sexo == 'F')
                return (62.1 * Altura) - 44.7;
            else
                throw new InvalidOperationException("Sexo inválido.");
        }

        public double CalcularIMC()
        {
            return Peso / Math.Pow(Altura / 100.0, 2);
        }

        public int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - Nascimento.Year;
            if (Nascimento.Date > hoje.AddYears(-idade))
                idade--;
            return idade;
        }

        public string ObterSituacaoIMC()
        {
            var imc = CalcularIMC();
            if (imc < 17)
                return "Muito abaixo do peso";
            else if (imc >= 17 && imc < 18.5)
                return "Abaixo do peso";
            else if (imc >= 18.5 && imc < 25)
                return "Peso normal";
            else if (imc >= 25 && imc < 30)
                return "Acima do peso";
            else if (imc >= 30 && imc < 35)
                return "Obesidade I";
            else if (imc >= 35 && imc < 40)
                return "Obesidade II (severa)";
            else
                return "Obesidade III (mórbida)";
        }

        public string ObterCpfOfuscado()
        {
            return $"{Cpf.Substring(0, 3)}.***.{Cpf.Substring(9)}";
        }

        public bool ValidarCpf()
        {
            var cpfArray = Cpf.ToCharArray();
            int v1 = 0, v2 = 0;

            for (int i = 0; i < 9; i++)
            {
                v1 += Convert.ToInt32(cpfArray[i].ToString()) * (9 - (i % 10));
                v2 += Convert.ToInt32(cpfArray[i].ToString()) * (9 - ((i + 1) % 10));
            }

            v1 = (v1 % 11) % 10;
            v2 += v1 * 9;
            v2 = (v2 % 11) % 10;

            return v1 == Convert.ToInt32(cpfArray[9].ToString()) && v2 == Convert.ToInt32(cpfArray[10].ToString());
        }
    }
}

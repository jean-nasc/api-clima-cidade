using System;
using System.ComponentModel.DataAnnotations;

namespace Clima.Cidade.Api.Models
{
    public class ClimaCidade
    {
        [Key]
        public Guid Id { get; private set; }
        
        [StringLength(80, MinimumLength = 3)]
        public string Cidade { get; private set; }
        public double TemperaturaAtual { get; private set; }
        public double TemperaturaMin { get; private set; }
        public double TemperaturaMax { get; private set; }
        public DateTime DataRegistro { get; private set; }

        public ClimaCidade(string cidade, double temperaturaAtual, double temperaturaMin, double temperaturaMax)
        {
            this.Id = Guid.NewGuid();
            this.Cidade = cidade;
            this.TemperaturaAtual = temperaturaAtual;
            this.TemperaturaMin = temperaturaMin;
            this.TemperaturaMax = temperaturaMax;
            this.DataRegistro = DateTime.UtcNow;
        }

        public void Atualizar(double temperaturaAtual, double temperaturaMin, double temperaturaMax)
        {
            this.TemperaturaAtual = temperaturaAtual;
            this.TemperaturaMin = temperaturaMin;
            this.TemperaturaMax = temperaturaMax;
            this.DataRegistro = DateTime.UtcNow;
        }
    }
}
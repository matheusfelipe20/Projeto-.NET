using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinal.Models
{
    public class Manutencao
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Quantidade de Km percorrido pelo veículo")]
        [Required]
        public int QuantidadeKm { get; set; }
        [DisplayName("Registro de manutenções anteriores")]
        public string Registro { get; set; }
        [DisplayName("Data de agendamento da manutenção")]
        [Required]
        public DateTime Agenda { get; set; }
        [DisplayName("Placa do veículo")]
        [Required]
        public string PlacaVeiculo { get; set; }
    }
}

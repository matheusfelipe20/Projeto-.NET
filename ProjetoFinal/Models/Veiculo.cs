using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinal.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome do veículo")]
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        [DisplayName("Ano de lançamento")]
        public int Ano { get; set; }
        [Required]
        public string Cor { get; set; }
        [Required]
        public string Placa { get; set; }

    }
}

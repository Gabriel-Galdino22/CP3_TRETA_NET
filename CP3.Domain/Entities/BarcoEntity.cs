using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_barco")]
    public class BarcoEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public double Tamanho { get; set; }
    }
}

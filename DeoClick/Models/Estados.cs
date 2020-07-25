using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Estados")]
    public class Estados
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome do Estado")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(100, ErrorMessage = "O Nome do Estado deve ter no máximo 100 caracteres!", MinimumLength = 2)]
        public string nomeEstado { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(2, ErrorMessage = "A UF deve ter no máximo 2 caracteres", MinimumLength = 2)]
        public string uf { get; set; }
    }
}
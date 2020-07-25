using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Forma de Pagamento")]
    public class FormaPagamento
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Tipo de Pagamento")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(2, ErrorMessage = "O Tipo de Pagamento deve ter no máximo 2 caracteres", MinimumLength = 2)]
        public string tipoPagamento { get; set; }
    }
}

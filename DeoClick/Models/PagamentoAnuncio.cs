using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Pagamento Anuncio")]
    public class PagamentoAnuncio
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public int idFormaPagamento { get; set; }

        [ForeignKey("idFormaPagamento")]
        public FormaPagamento FormaPagamento { get; set; }
    }
}

    //idPagamentoAnuncio int not null primary key auto_increment,
    //idFormaPagamento int not null,
    //FOREIGN KEY(idFormaPagamento) REFERENCES FormaPagamento(idFormaPagamento)

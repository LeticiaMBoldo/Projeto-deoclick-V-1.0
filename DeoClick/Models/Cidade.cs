using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome da Cidade")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(150, ErrorMessage = "O Nome da Cidade deve ter no máximo 150 caracteres", MinimumLength = 2)]
        public string nomeCidade { get; set; }

        /*foreign key (Estado)*/
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public int idEstado { get; set; }

        [ForeignKey("idEstado")]
        public Estados Estados { get; set; }

        [NotMapped]
        public string NomeCompleto { get => nomeCidade + " (" + idEstado + ")"; }
    }
}




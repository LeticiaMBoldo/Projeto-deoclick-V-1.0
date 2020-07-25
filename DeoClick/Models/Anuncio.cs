using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Anúncio")]
    public class Anuncio
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome do Anúncio")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(150, ErrorMessage = "O Nome do Anúncio deve ter no máximo 150 caracteres")]
        public string nomeAnuncio { get; set; }

        [Display(Name = "Tipo do Anúncio")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        [StringLength(1, ErrorMessage = "O tipo de anúncio deve ter no máximo 1 caracter", MinimumLength = 1)]
        public string tipoAnuncio { get; set; }

        [Display(Name = "Valor do Anuncio")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public decimal valorAnuncio { get; set; }

        [Display(Name = "Foto do Anúncio")]
        [StringLength(500, ErrorMessage = "A foto do Anuncio deve ter no máximo 500 caracteres")]
        public string FotoAnuncio { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public int idCliente { get; set; }
        
        [ForeignKey("idCliente")]
        public Clientes Clientes { get; set; }

    }
}
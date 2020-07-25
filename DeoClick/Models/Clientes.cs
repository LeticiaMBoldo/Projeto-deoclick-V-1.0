using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(150, ErrorMessage = "O nome do Cliente deve ter no máximo 150 caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string nomeCliente { get; set; }


        [Display(Name = "Empresa")]
        [StringLength(150, ErrorMessage = "O nome da Empresa deve ter no máximo 150 caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string nomeEmpresa { get; set; }

        [Display(Name = "CEP")]
        [StringLength(9, ErrorMessage = "O CEP da Empresa deve ter no máximo 9 caracteres", MinimumLength = 7)]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string cepEmpresa { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(150, ErrorMessage = "O Endereco deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string endereco { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(150, ErrorMessage = "O Bairro deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string bairro { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(150, ErrorMessage = "O Complemento deve ter no máximo 150 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string complemento { get; set; }

        [Display(Name = "Número Residencial")]
        [StringLength(10, ErrorMessage = "O Residencial deve ter no máximo 10 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string numeroResidencial { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20, ErrorMessage = "O Telefone deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string telefoneContato { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(180, ErrorMessage = "O e-mail deve ter no máximo 180 caracteres", MinimumLength = 7)]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string email { get; set; }

        [Display(Name = "Senha")]
        [StringLength(30, ErrorMessage = "A Senha deve ter no máximo 30 caracteres")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public string senha { get; set; }

        [Display(Name = "Foto")]
        [StringLength(500, ErrorMessage = "A foto de perfil deve ter no máximo 500 caracteres")]
        public string caminhoImagemPerfil { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO!")]
        public int idCidade { get; set; }

        [ForeignKey("idCidade")]
        public Cidade Cidade { get; set; }

    }
}



//FOREIGN KEY(idCidade) REFERENCES Cidade(idcidade)

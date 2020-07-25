using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeoClick.Models
{
    [Table("Evento")]
    public class Evento
    {
        [Key]
        public int id { get; set; }

        //StatusEvento não vai ser colocado para o usuário visualizar, e vai vir como 0 e quando ele for confirmado como 
        //evento o status dele passa a ser 1 como verdadeiro
        [Required(ErrorMessage = "ESTE CAMPO NÃO PODE SER NULO")]
        public Boolean statusEvento { get; set; }

        [Display(Name = "Nome do Evento")]
        [Required(ErrorMessage = "O NOME DO EVENTO NÃO PODE SER NULO!")]
        [StringLength(180, ErrorMessage = "O Nome do Evento deve ter no máximo 180 caracteres")]
        public string nomeEvento { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "o ENDEREÇO NÃO PODE SER NULO!")]
        [StringLength(120, ErrorMessage = "O Endereço deve ter no máximo 120 caracteres")]
        public string endereco { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O BAIRRO NÃO PODE SER NULO!")]
        [StringLength(100, ErrorMessage = "O Bairro dever ter no máximo 100 caracteres")]
        public string bairro { get; set; }

        [Display(Name = "Número da Residência")]
        [Required(ErrorMessage = "O NÚMERO DA RESIDÊNCIA NÃO PODE SER NULO!")]
        [StringLength(10, ErrorMessage = "O Número da Residência deve ter no máximo 10 caracteres")]
        public string numeroResidenia { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP NÃO PODE SER NULO!")]
        [StringLength(9, ErrorMessage = "O CEP deve ter no máximo 10 caracteres")]
        public string cep { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A DESCRIÇÃO NÃO PODE SER NULO!")]
        [StringLength(300, ErrorMessage = "A Descrição deve ter no máximo 300 caracteres")]
        public string descricao { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O EMAIL NÃO PODE SER NULO!")]
        [StringLength(150, ErrorMessage = "O E-mail para contato deve ter no máximo 150 caracteres")]
        public string email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O TELEFONE NÃO PODE SER NULO!")]
        [StringLength(20, ErrorMessage = "O Telefone deve ter no máximo 20 caracteres")]
        public string telefone { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(150, ErrorMessage = "O Complemento deve ter no máximo 150 caracteres")]
        public string complemento { get; set; }

        [Display(Name = "Data Início")]
        [Required(ErrorMessage = "A DATA DE INÍCIO NÃO PODE SER NULO!")]
        public DateTime dataInicio { get; set; }

        [Display(Name = "Data Termino")]
        [Required(ErrorMessage = "A DATA DE TERMINO NÃO PODE SER NULO!")]
        public DateTime dataTermino { get; set; }

        [Display(Name = "Tipo de Evento")]
        [Required(ErrorMessage = "O TIPO DE EVENTO NÃO PODE SER NULO!")]
        [StringLength(2, ErrorMessage = "O Tipo de Evento deve ter no máximo 2 caracteres", MinimumLength = 2)]
        public string tipoEvento { get; set; }

        [Display(Name = "Valor do Ingresso")]
        public decimal valorEvento { get; set; }

        [Display(Name = "Foto do Evento")]
        [StringLength(500, ErrorMessage = "O email deve ter no máximo 500 caracteres")]
        public string FotoEvento { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O CLIENTE NÃO PODE SER NULO!")]
        public int idCliente { get; set; }

        [ForeignKey("idCliente")]
        public Clientes Clientes { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A CIDADE NÃO PODE SER NULO!")]
        public int idCidade { get; set; }

        [ForeignKey("idCidade")]
        public Cidade Cidade { get; set; }


    }
}
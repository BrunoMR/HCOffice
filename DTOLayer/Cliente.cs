using System.ComponentModel.DataAnnotations;

namespace DTOLayer
{
    using ServiceStack.DataAnnotations;

    public class Cliente
    {
        [AutoIncrement]
        public int? Id { get; set; }

        [Alias("TIPO_PESSOA")]
        [References(typeof(TipoPessoa))]
        public char TipoPessoaId { get; set; }

        [Reference]
        public TipoPessoa TipoPessoa { get; set; }

        public string Nome { get; set; }

        [Alias("NOME_FANTASIA")]
        public string NomeFantasia { get; set; }

        [Alias("CPF_CNPJ")]
        public string CpfCnpj { get; set; }

        public string Rg { get; set; }

        [Alias("INSCRICAO_MUNICIPAL")]
        public string IncricaoMunicipal { get; set; }
        
        [Alias("ENDERECO_ELETRONICO")]
        public string EnderecoEletronico { get; set; }

        public string Email { get; set; }

        [Alias("NOME_CONTATO")]
        public string NomeContato { get; set; }

        [Alias("TELEFONE_CONTATO")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "(99) 99999-9999")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string TelefoneContato { get; set; }

        [Alias("TELEFONE_EMPRESA")]
        public string TelefoneEmpresa { get; set; }

        [Alias("TELEFONE_FAX")]
        public string TelefoneFax { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Observacao { get; set; }

        [Alias("MAX_LICENCAS")]
        public int MaxLicencas { get; set; }
    }
}

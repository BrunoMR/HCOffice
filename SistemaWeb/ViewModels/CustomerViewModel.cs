namespace SistemaWeb.ViewModels
{
    using System.Collections.Generic;

    public class CustomerViewModel
    {
        public int? Id { get; set; }

        public char TipoPessoaId { get; set; }

        public PersonTypeViewModel TipoPessoa { get; set; }

        public string Nome { get; set; }

        public string NomeFantasia { get; set; }

        public string CpfCnpj { get; set; }

        public string Rg { get; set; }

        public string IncricaoMunicipal { get; set; }

        public string EnderecoEletronico { get; set; }

        public string Email { get; set; }

        public string NomeContato { get; set; }

        public string TelefoneContato { get; set; }

        public string TelefoneEmpresa { get; set; }

        public string TelefoneFax { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Observacao { get; set; }

        public int MaxLicencas { get; set; }

        public IEnumerable<UserViewModel> Operators { get; set; }

        public string CustomerPassword { get; set; }
    }
}
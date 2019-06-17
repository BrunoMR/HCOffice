namespace SistemaWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using BusinessLayer;
    using DTOLayer;
    using Extensions;
    using Identity.Configuration;
    using Identity.Dto;
    using Identity.Model;
    using Microsoft.AspNet.Identity;
    using Security;
    using Toastr;
    using ViewModels;

    [RoutePrefix("Cliente")]
    public class ClienteController : Controller
    {
        readonly IClienteNegocio _clienteNegocio = new ClienteNegocio();

        private readonly IUserRepository _userRepository;
        private readonly OperatorUserManager _operatorUserManager;
        private readonly ApplicationUserManager _userManager;

        public ClienteController(IUserRepository userRepository, OperatorUserManager operatorUserManager, ApplicationUserManager userManager)
        {
            this._userManager = userManager;
            this._userRepository = userRepository;
            this._operatorUserManager = operatorUserManager;
        }

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            var listCliente = await _clienteNegocio.GetAllAsync();
            return View(listCliente);
        }

        // GET: Cliente/Details/5
        [Route("Detalhe/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var cliente = await _clienteNegocio.FindByIdLoadAsync(id);
                var customerViewModel = Mapper.Map<CustomerViewModel>(cliente);
                
                if (User.HasAnyRoles("Admin"))
                {

                }
                if (customerViewModel.Id != null)
                {
                    var operators = this._userRepository.GetByCustomerId((int)customerViewModel.Id);
                    var operatorsViewModel = Mapper.Map<IEnumerable<UserViewModel>>(operators);

                    customerViewModel.Operators = operatorsViewModel;
                }

                BuildDropDownListTipoPessoa(cliente);

                return View("Edit", customerViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BuildDropDownListTipoPessoa(Cliente cliente)
        {
            ITipoPessoaNegocio tipoPessoaNegocio = new TipoPessoaNegocio();

            ViewBag.TipoPessoas = new SelectList(
                tipoPessoaNegocio.GetAll(),
                "Tipo",
                "Descricao",
                cliente?.TipoPessoa?.Tipo
                );
        }

        [HttpPost]
        public async Task<ActionResult> Save(CustomerViewModel customerViewModel)
        {
            try
            {
                var cliente = MapperCliente(customerViewModel);
                var clienteId = _clienteNegocio.AddOrUpdate(cliente).Id;

                var user = new ApplicationUser { UserName = customerViewModel.Email, Email = customerViewModel.Email, CustomerId = clienteId};
                var result = await _userManager.UpsertAsync(user, customerViewModel.CustomerPassword); //"123456");

                if (result.Succeeded)
                    this.AddToastMessage("Cliente", "Registro salvo com sucesso!", ToastType.Success);
                else
                    this.AddToastMessage("Cliente", "Cliente não salvo com sucesso!", ToastType.Warning);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Cliente", "Erro ao salvar registro!", ToastType.Error);
                return RedirectToAction("Index");
            }

        }

        [Route("Novo")]
        public ActionResult Create()
        {
            BuildDropDownListTipoPessoa(new Cliente());
            return View("Edit");
        }

        private async Task<int> GetCustomerIdAsync()
        {
            return await this._operatorUserManager.GetCustomerId(this.User.Identity.GetUserId());
        }

        private Cliente MapperCliente(CustomerViewModel clientScreen)
        {
            return new Cliente()
            {
                Email = clientScreen.Email,
                Bairro = clientScreen.Bairro,
                TelefoneContato = clientScreen.TelefoneContato,
                Uf = clientScreen.Uf,
                Id = clientScreen.Id,
                Cep = clientScreen.Cep,
                Cidade = clientScreen.Cidade,
                CpfCnpj = clientScreen.CpfCnpj,
                Endereco = clientScreen.Endereco,
                EnderecoEletronico = clientScreen.EnderecoEletronico,
                IncricaoMunicipal = clientScreen.IncricaoMunicipal,
                MaxLicencas = clientScreen.MaxLicencas,
                Nome = clientScreen.Nome,
                NomeContato = clientScreen.NomeContato,
                NomeFantasia = clientScreen.NomeFantasia,
                Observacao = clientScreen.Observacao,
                Rg = clientScreen.Rg,
                TelefoneEmpresa = clientScreen.TelefoneEmpresa,
                TelefoneFax = clientScreen.TelefoneFax,
                TipoPessoaId = clientScreen.TipoPessoaId
            };
        }
    }
}

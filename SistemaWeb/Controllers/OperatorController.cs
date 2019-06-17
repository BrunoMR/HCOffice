namespace SistemaWeb.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using Extensions;
    using Identity.Configuration;
    using Identity.Dto;
    using Identity.Model;
    using Microsoft.AspNet.Identity;
    using Security;
    using ViewModels;

    /// <summary>
    /// The operator controller.
    /// </summary>
    [Authorize(Roles = "Root, Customer")]
    [RoutePrefix("Operador")]
    public class OperatorController : Controller
    {
        /// <summary>
        /// The sign in manager.
        /// </summary>
        private readonly ApplicationSignInManager signInManager;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly OperatorUserManager userManager;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        public OperatorController(
            OperatorUserManager userManager, 
            ApplicationSignInManager signInManager,
            IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        [Route(Name = "Index")]
        public async Task<ActionResult> Index()
        {
            var operators = this.userRepository.GetByCustomerId(await GetCustomerIdAsync());

            var operatorsViewModel = Mapper.Map<IEnumerable<UserViewModel>>(operators);

            return this.View(operatorsViewModel);
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("Registrar")]
        public ActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Registrar")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerId = await GetCustomerIdAsync();

                var user = new ApplicationUser
                               {
                                   UserName = model.Email,
                                   Email = model.Email,
                                   EmailConfirmed = true,
                                   CustomerId = customerId
                               };

                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index");
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Route("Excluir")]
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindById(id);
                var custmerId = user.CustomerId;

                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Edit", "Cliente", new {id = custmerId});
                }
                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View("Index");
        }

        /// <summary>
        /// The add errors.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        /// The get customer id async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<int> GetCustomerIdAsync()
        {
            return await userManager.GetCustomerId(User.Identity.GetUserId());
        }
    }
}
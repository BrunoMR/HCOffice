using System.ComponentModel.DataAnnotations;

namespace Identity.Model
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da função")]
        public string Name { get; set; }
    }
}
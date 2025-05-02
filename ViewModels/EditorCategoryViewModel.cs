using System.ComponentModel.DataAnnotations;

namespace BlogWeb.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O slug é obrigatório")]
        [MinLength(40)]
        public string Slug { get; set; }
    }
}

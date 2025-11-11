using System.ComponentModel.DataAnnotations;


namespace UmbracoCMS.ViewModels;

public class QuestionFormViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please write your question")]
    [Display(Name = "Question")]
    [StringLength(1000, ErrorMessage = "Question can't be longer than 1000 characters.")]
    public string Question { get; set; } = null!;

   

}

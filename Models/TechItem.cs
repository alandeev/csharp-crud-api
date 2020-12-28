

using System.ComponentModel.DataAnnotations;

namespace CrudApi.Models
{
    public class TechItem {

      [Editable(false)]
      [DataType("int")]
      [Key]
      public int id { get; protected set; }

      [Required]
      [MinLength(2, ErrorMessage = "Esse campo precisa ter no minimo 2 caracteres")]
      [MaxLength(20, ErrorMessage = "Esse campo precisa ter no maximo 20 caracteres")]
      public string name { get; set; }
    }
}
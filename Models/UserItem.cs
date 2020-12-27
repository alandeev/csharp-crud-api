using System;
using System.ComponentModel.DataAnnotations;

namespace CrudApi.Models
{
  public class UserItem {


    [Editable(false)]
    [DataType("int")]
    [Key]
    public int id { get; protected set; }
    
    [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 10 caracteres")]
    [MaxLength(10, ErrorMessage = "Este campo deve conter entre 2 e 10 caracteres")]
    [DataType("string")]
    public string name { get; set; }

    [Range(18, 60, ErrorMessage = "Você precisa ter de 18 a 60 anos para cadastrar-se")]
    [DataType("int")]
    public int age { get; set; }
  }
}
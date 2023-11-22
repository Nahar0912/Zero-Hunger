using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zero_Hunger.DTO
{
    public class RestaurantLoginDTO
    {
        // GET: RestaurantLoginDTO
        public int Id { get; set; }
        [Required]
        [RegularExpression("^(?=.{1,64}$)[a-z][a-z0-9.-]*[a-z0-9]@[a-z]{2,}(?:\\.[a-z0-9]+)*\\.[a-z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
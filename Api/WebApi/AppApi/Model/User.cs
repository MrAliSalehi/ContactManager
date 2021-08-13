using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppApi.Model.User
{
    public partial class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(650)]
        public string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Cant Be Null")]
        [StringLength(250)]
        [EmailAddress(ErrorMessage ="Bad Email Format")]
        public string Email { get; set; }
        public string Note { get; set; }
    }
}

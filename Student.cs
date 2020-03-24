using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab3
{
    public class Student
    { 
        [Key] 
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
       
        public string Imie { get; set; }
        [Required] 
        [MaxLength(255)]
        public string Nazwisko { get; set; }
    }
}

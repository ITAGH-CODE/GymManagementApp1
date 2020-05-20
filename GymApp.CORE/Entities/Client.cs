using GymApp.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymApp.CORE.Entities
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("First name")]
        public string FirstName { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Last name")]
        public string LastName { get; set; }


        [Column(TypeName = "varchar(10)")]
        [DisplayName("ICN")]
        public string ICN { get; set; }


        [Column(TypeName = "varchar(50)")]
        [DisplayName("Phone")]
        public string Phone { get; set; }


        [EmailAddress]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Email")]
        public string Email { get; set; }


        public int IdPaymentOnGoing { get; set; }

        [NotMapped]
        public bool IsPaymentOk { get; set; }

        public List<Payment> Payments { get; set; }

    }
}

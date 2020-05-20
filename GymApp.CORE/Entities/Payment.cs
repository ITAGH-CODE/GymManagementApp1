using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymApp.CORE.Entities
{
    public class Payment
    {
        [Key]
        public int IdPayment { get; set; }


        public int Id { get; set; }


        [Column(TypeName ="decimal(10,2)")]
        public decimal Amount { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


        public DateTime NextPaymentDate { get; set; }


        public Boolean IsOk { get; set; }
    }
}

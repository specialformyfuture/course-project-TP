using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Course_Project_TP_6.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string ExpirationDate { get; set; }
        public string CVC { get; set; }
    }
}
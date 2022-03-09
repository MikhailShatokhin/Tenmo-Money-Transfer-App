using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int transferTypeId { get; set; } = 2;
        public int transferStatusId { get; set; } = 2;
        public int accountFrom { get; set; }
        public int accountTo { get; set; }
        public decimal amount { get; set; }
    }
}

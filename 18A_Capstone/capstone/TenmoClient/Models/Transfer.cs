using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int transferId { get; set; }
        public int transferTypeId { get; set; }
        public int transferStatusId { get; set; }
        public int accountFrom { get; set; }
        public int accountTo { get; set; }
        public decimal amount { get; set; }
        public string stringTransferStatus { get; set; }
        public string stringTransferType { get; set; }
        public string stringAccountFrom { get; set; }
        public string stringAccountTo { get; set; }
    }
}

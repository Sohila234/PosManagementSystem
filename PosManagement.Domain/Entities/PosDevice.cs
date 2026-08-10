using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Domain.Entities
{
    public class PosDevice
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;

        public int ModelId { get; set; }
        public Model? Model { get; set; }

        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }
    }
}

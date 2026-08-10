using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<PosDevice> PosDevices { get; set; }=new List<PosDevice>();

    }
}

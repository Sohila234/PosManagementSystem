using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PosManagement.Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]

        public ICollection<PosDevice> PosDevices { get; set; }=new List<PosDevice>();

    }
}

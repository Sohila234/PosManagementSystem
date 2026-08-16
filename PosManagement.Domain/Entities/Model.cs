using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PosManagement.Domain.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        [JsonIgnore]

        public ICollection<PosDevice> PosDevices { get; set; }=new List<PosDevice>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PosManagement.Domain.Entities
{
    public class Model
    {
        public int Id { get;private set; }
        public string Name { get;private set; } = string.Empty;
        public int ManufacturerId { get; private set; }
        public Manufacturer? Manufacturer { get; private set; }
        [JsonIgnore]

        public ICollection<PosDevice> PosDevices { get; private set; }=new List<PosDevice>();
        private Model()
        {
            
        }
        internal Model(string Name)
        {
            this.Name = Name;
        }
        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Model name cannot be empty.");

            Name = name;
        }
        public void Delete()
        {
            if (PosDevices.Any())
                throw new InvalidOperationException(
                    "Cannot delete a model that has POS devices.");
        }
    }
}

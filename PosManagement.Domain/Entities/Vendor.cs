using System;
using System.Text.Json.Serialization;

namespace PosManagement.Domain.Entities
{
    public class Vendor
    {
        private readonly List<PosDevice> _posDevices = new();

        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        [JsonIgnore]
        public IReadOnlyCollection<PosDevice> PosDevices
            => _posDevices.AsReadOnly();

        private Vendor()
        {
        }

        public Vendor(string name)
        {
            ChangeName(name);
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Vendor name cannot be empty.");

            Name = name;
        }
    }
}
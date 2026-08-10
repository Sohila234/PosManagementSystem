using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PosManagement.Domain.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Model> Models { get; set; }=new List<Model>();
    }
}

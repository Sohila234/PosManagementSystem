using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PosManagement.Domain.Entities
{
    public class Manufacturer
    {
        private readonly List<Model> _models = new();
        public int Id { get;private set; }
        public string Name { get;private set; } = string.Empty;

        public IReadOnlyCollection<Model> Models => _models.AsReadOnly();

        private Manufacturer()
        {
            
        }
        public Manufacturer( string Name)
        {
            ChangeName(Name);
        }
        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Manufacturer name cannot be empty.", nameof(name));
            Name = name;
        }
        public void Delete()
        {
            if (_models.Any())
                throw new InvalidOperationException(
                    "Cannot delete a manufacturer that has models.");
        }
        public Model AddModel(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
                throw new ArgumentException(
                    "Model name cannot be empty.");

            bool modelExists = _models.Any(m =>
                m.Name.Equals(
                    modelName,
                    StringComparison.OrdinalIgnoreCase));

            if (modelExists)
                throw new InvalidOperationException(
                    "A model with this name already exists.");

            var model = new Model(modelName);

            _models.Add(model);

            return model;
        }
        public void UpdateModel(int modelId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Model name cannot be empty.");

            var model = _models.FirstOrDefault(m => m.Id == modelId);

            if (model == null)
                throw new InvalidOperationException("Model not found.");

            bool modelExists = _models.Any(m =>
                m.Id != modelId &&
                m.Name.Equals(
                    newName,
                    StringComparison.OrdinalIgnoreCase));

            if (modelExists)
                throw new InvalidOperationException(
                    "A model with this name already exists.");

            model.ChangeName(newName);
        }
        
    }
}


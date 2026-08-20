using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Domain.Entities
{
    public class PosDevice
    {
        public int Id { get; private set; }
        public string SerialNumber { get; private set; } = string.Empty;
        public int ModelId { get; private set; }
        public Model? Model { get; private set; }

        public int VendorId { get; private set; }
        public Vendor? Vendor { get; private set; }
        public PosDevice(
             string serialNumber,
                int modelId,
                 int vendorId)
        {
            ChangeSerialNumber(serialNumber);

            if (modelId <= 0)
                throw new ArgumentException("Invalid model.");

            if (vendorId <= 0)
                throw new ArgumentException("Invalid vendor.");

            ModelId = modelId;
            VendorId = vendorId;
        }
        public void ChangeSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new ArgumentException(
                    "Serial number cannot be empty.");

            SerialNumber = serialNumber;
        }
        public void ChangeModelAndVendor(int modelId, int vendorId)
        {
            if (modelId <= 0)
                throw new ArgumentException("Invalid model.");

            if (vendorId <= 0)
                throw new ArgumentException("Invalid vendor.");

            ModelId = modelId;
            VendorId = vendorId;
        }
    }
}

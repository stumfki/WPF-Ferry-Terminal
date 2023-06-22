using System.ComponentModel;
using System;


namespace FerryTerminalNamespace
{
    public class Vehicle : INotifyPropertyChanged
    {
        public int VehicleId { get; set; } // Property for the vehicle ID
        public string Name { get; set; } // Property for the vehicle name
        private double _fuelLevel; // Private field for fuel level
        private bool _needsRefueling; // Private field indicating if the vehicle needs refueling
        private bool _needsCustomsCheck; // Private field indicating if the vehicle needs customs check


        public double FuelLevel
        {
            get { return _fuelLevel; }
            set
            {
                _fuelLevel = value;
                OnPropertyChanged(nameof(FuelLevel));
            }
        } // Property for the fuel level

        public bool NeedsRefueling
        {
            get { return _needsRefueling; }
            set
            {
                _needsRefueling = value;
                OnPropertyChanged(nameof(NeedsRefueling));
            }
        } // Property indicating if the vehicle needs refueling

        public bool NeedsCustomsCheck
        {
            get { return _needsCustomsCheck; }
            set
            {
                _needsCustomsCheck = value;
                OnPropertyChanged(nameof(NeedsCustomsCheck));
            }
        } // Property indicating if the vehicle needs customs check

        public event PropertyChangedEventHandler PropertyChanged;
        private static int nextId = 1; // Variable to keep track of the next vehicle ID
        public Vehicle()
        {
            Random random = new Random(); // Create a random number generator
            string[] vehicleNames = { "Car", "Van", "Bus", "Truck" }; // Array of vehicle names
            double fuelLevel = random.Next(1, 100); // Generate a random fuel level between 1 and 100
            int vehicleId = nextId; // Assign the next available vehicle ID
            string randomVehicleName = vehicleNames[random.Next(0, 4)]; // Select a random vehicle name from the array
            bool needsCustomsCheck = randomVehicleName == "Truck" || randomVehicleName == "Van"; // Check if the vehicle needs a customs check

            Name = randomVehicleName;
            FuelLevel = fuelLevel;
            VehicleId = vehicleId;
            NeedsRefueling = fuelLevel < 20;
            NeedsCustomsCheck = needsCustomsCheck;

            nextId++; // Increment the next available vehicle ID
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

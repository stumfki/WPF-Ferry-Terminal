using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace FerryTerminalNamespace
{
    public class FerryTerminal : INotifyPropertyChanged
    {
        private const int MaxSmallFerrySize = 8; // Maximum size of the small ferry
        private const int MaxLargeFerrySize = 6; // Maximum size of the large ferry

        public ObservableCollection<string> SmallFerryVehicles { get; set; } // Collection of small ferry vehicles to display them on the app
        public ObservableCollection<string> LargeFerryVehicles { get; set; } // Collection of large ferry vehicles to display them on the app
        public ObservableCollection<Vehicle> Vehicles { get; set; } // Collection of the vehicles
        private string _lastLocation; // The last location of the vehicle
        public string LastLocation
        {
            get { return _lastLocation; }
            set
            {
                _lastLocation = value;
                OnPropertyChanged(nameof(LastLocation));
            }
        }

        public double _totalEarnings; // Total earnings of the terminal
        public double TotalEarnings
        {
            get { return _totalEarnings; }
            set
            {
                _totalEarnings = value;
                OnPropertyChanged(nameof(TotalEarnings));
            }
        }
        public double _employeeIncome; // Employee earnings 
        public double EmployeeIncome
        {
            get { return _employeeIncome; }
            set
            {
                _employeeIncome = value;
                OnPropertyChanged(nameof(EmployeeIncome));
            }
        }

        public FerryTerminal()
        {
            Vehicles = new ObservableCollection<Vehicle>(); // Initialize the vehicle collection
            SmallFerryVehicles = new ObservableCollection<string>(); // Initialize the small ferry vehicle collection
            LargeFerryVehicles = new ObservableCollection<string>(); // Initialize the large ferry vehicle collection
        }


        public void AddRandomVehicle()
        {
            if (Vehicles.Count <= 0)
            {
                Vehicle newVehicle = new Vehicle(); // Create a new vehicle object
                Vehicles.Add(newVehicle); // Add the new vehicle to the collection
                LastLocation = "Location: At the terminal"; // Update the location
            }
        }

        public void ProcessNextVehicle()
        {
            if (Vehicles.Count > 0)
            {
                Vehicle nextVehicle = Vehicles.First(); // Get the first vehicle in the collection

                if (!nextVehicle.NeedsCustomsCheck && nextVehicle.FuelLevel >= 20)
                {
                    string vehicleName = nextVehicle.Name; // Get the name of the vehicle
                    string vehicleIdToString = nextVehicle.VehicleId.ToString(); // Convert the vehicle ID to a string

                    if (vehicleName == "Car" || vehicleName == "Van")
                    {
                        SmallFerryVehicles.Add($"ID: {vehicleIdToString} Type: {vehicleName}"); // Add the vehicle to the small ferry vehicle collection
                        LastLocation = "Location: Small Ferry";
                    }
                    else if (vehicleName == "Bus" || vehicleName == "Truck")
                    {
                        LargeFerryVehicles.Add($"ID: {vehicleIdToString} Type: {vehicleName}"); // Add the vehicle to the large ferry vehicle collection
                        LastLocation = "Location: Large Ferry";
                    }

                    
                    CalculateEarnings(vehicleName); // Calculate the earnings based on the vehicle type
                    UpdateFerrySizes(); // Update the sizes of the ferry collections
                    UpdateFerryTexts(); // Update the texts displaying the ferry sizes
                    CalculateEmployeeIncome(); // Calculate the employee income based on the total earnings
                    Vehicles.Remove(Vehicles.First()); // Remove the vehicle
                }
            }
        }

        public void RefuelCurrentVehicle()
        {
            if (Vehicles.Count > 0 && Vehicles.First().NeedsRefueling)
            {
                Vehicles.First().FuelLevel = 100; // Refuel the first vehicle in the collection
                Vehicles.First().NeedsRefueling = false; // Update the refueling flag
                LastLocation = "Location: Pump";
            }
        }

        public void ToggleGate()
        {
            if (Vehicles.Count > 0 && (Vehicles.First().Name == "Van" || Vehicles.First().Name == "Truck"))
            {
                Vehicles.First().NeedsCustomsCheck = false; //Update the needs customs flag
                LastLocation = "Location: Customs";
            }
        }

        private void CalculateEarnings(string vehicleName)
        {
            double vehicleEarnings = 0;

            if (vehicleName == "Car" || vehicleName == "Van")
            {
                vehicleEarnings = (vehicleName == "Car") ? 3 : 4; // Calculate earnings for a car or a van
            }
            else if (vehicleName == "Bus" || vehicleName == "Truck")
            {
                vehicleEarnings = (vehicleName == "Bus") ? 5 : 6; // Calculate earnings for a bus or a truck
            }

            TotalEarnings += vehicleEarnings; // Add the vehicle earnings to the total earnings
        }

        private void UpdateFerrySizes()
        {
            if (SmallFerryVehicles.Count >= MaxSmallFerrySize)
            {
                SmallFerryVehicles.Clear(); // Clear the small ferry vehicle collection if it reaches its maximum size
            }

            if (LargeFerryVehicles.Count >= MaxLargeFerrySize)
            {
                LargeFerryVehicles.Clear(); // Clear the large ferry vehicle collection if it reaches its maximum size
            }
        }

        private void CalculateEmployeeIncome()
        {
            EmployeeIncome = 0.1 * TotalEarnings; // Calculate the employee income as 10% of the total earnings
        }

        private void UpdateFerryTexts()
        {
            OnPropertyChanged(nameof(SmallFerryText)); // Update the property indicating the size of the small ferry vehicle collection
            OnPropertyChanged(nameof(LargeFerryText)); // Update the property indicating the size of the large ferry vehicle collection
        }

        public string SmallFerryText => $"Small Ferry Size {SmallFerryVehicles.Count}/{MaxSmallFerrySize}"; // Property indicating the size of the small ferry vehicle collection
        public string LargeFerryText => $"Large Ferry Size {LargeFerryVehicles.Count}/{MaxLargeFerrySize}"; // Property indicating the size of the large ferry vehicle collection

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

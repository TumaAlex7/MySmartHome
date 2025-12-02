using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices
{
    public class Heater(string name) : ISmartDevice
    {
        public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
        public EventLogger? Logger { get; set; }
        private int minTemperature = 10;
        private bool isOn;

        public void TemperatureChanged(int temperature)
        {
            if (temperature < minTemperature && !isOn)
            {
                isOn = true;
                Console.WriteLine("Heater turned on.");
                Logger?.Log("Heater turned on.");
            }
            else if (temperature > minTemperature && isOn)
            {
                isOn = false;
                Console.WriteLine("Heater turned off.");
                Logger?.Log("Heater turned off.");
            }
            else
            {
                Console.WriteLine("Heater temperature is normal.");
                Logger?.Log("Heater temperature is normal.");
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("MinTemperature"))
            {
                minTemperature = (int)settings["MinTemperature"];
            }
            Console.WriteLine($"Heater configured: MinTemperature={minTemperature}°C.");
            Logger?.Log($"Heater configured: MinTemperature={minTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Heater manually turned on.");
                Logger?.Log("Heater manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Heater manually turned off.");
                Logger?.Log("Heater manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Heater");
                Logger?.Log("Invalid command for Heater");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices
{
    public class AirConditioner(string name) : ISmartDevice
    {
        public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
        public EventLogger? Logger { get; set; }
        private int minTemperature = 18;
        private int maxTemperature = 25;
        private bool isOn;

        public void TemperatureChanged(int temperature)
        {
            if (temperature > maxTemperature && !isOn)
            {
                isOn = true;
                Console.WriteLine("Air Conditioner turned on (High Temperature).");
                Logger?.Log("Air Conditioner turned on (High Temperature).");
            }
            else if (temperature < minTemperature && isOn)
            {
                isOn = false;
                Console.WriteLine("Air Conditioner turned off (Low Temperature).");
                Logger?.Log("Air Conditioner turned off (Low Temperature).");
            }
            else
            {
                Console.WriteLine("Air Conditioner temperature is normal.");
                Logger?.Log("Air Conditioner temperature is normal.");
            }
        }


        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("MinTemperature"))
            {
                minTemperature = (int)settings["MinTemperature"];
            }
            if (settings.ContainsKey("MaxTemperature"))
            {
                maxTemperature = (int)settings["MaxTemperature"];
            }
            Console.WriteLine($"Air Conditioner configured: Min={minTemperature}°C, Max={maxTemperature}°C.");
            Logger?.Log($"Air Conditioner configured: MinTemperature={minTemperature}°C, MaxTemperature={maxTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Air Conditioner manually turned on.");
                Logger?.Log("Air Conditioner manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Air Conditioner manually turned off.");
                Logger?.Log("Air Conditioner manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Air Conditioner.");
                Logger?.Log("Invalid command for Air Conditioner.");
            }
        }
    }
}

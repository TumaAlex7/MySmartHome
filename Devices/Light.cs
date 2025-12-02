using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices
{
    public class Light(string name) : ISmartDevice
    {
        public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
        public EventLogger? Logger { get; set; }
        private int brightness = 100;
        private bool isOn;

        public void DayTimeChanged(string timeOfDay)
        {
            timeOfDay = timeOfDay.ToLower().Trim();
            if (timeOfDay == "morning")
            {
                isOn = true;
                Console.WriteLine("Light turned on (Morning).");
                Logger?.Log("Light turned on (Morning).");
            }
            else if (timeOfDay == "night")
            {
                isOn = false;
                Console.WriteLine("Light turned off (Night).");
                Logger?.Log("Light turned off (Night).");
            }
            else
            {
                Console.WriteLine("Light time is normal.");
                Logger?.Log("Light time is normal.");
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("Brightness"))
            {
                brightness = (int)settings["Brightness"];
            }
            Console.WriteLine($"Light configured: Brightness={brightness}%.");
            Logger?.Log($"Light configured: Brightness={brightness}%.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Light manually turned on.");
                Logger?.Log("Light manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Light manually turned off.");
                Logger?.Log("Light manually turned off.");
            }
            Console.WriteLine("Invalid command for Light");
            Logger?.Log("Invalid command for Light");
        }
    }
}

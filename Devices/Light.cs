using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Light : ISmartDevice
    {
        private int brightness = 100;
        private bool isOn;

        public void DayTimeChanged(string timeOfDay)
        {
            timeOfDay = timeOfDay.ToLower().Trim();
            if (timeOfDay == "morning")
            {
                isOn = true;
                Console.WriteLine("Light turned on (Morning).");
            }
            else if (timeOfDay == "night")
            {
                isOn = false;
                Console.WriteLine("Light turned off (Night).");
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("Brightness"))
                brightness = (int)settings["Brightness"];
            Console.WriteLine($"Light configured: Brightness={brightness}%.");
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Light manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Light manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Light");
            }
        }
    }
}

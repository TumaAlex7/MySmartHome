using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Light : ISmartDevice
    {
        private int brightness = 100;
        private bool isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if(eventType == "DayTimeChanged")
            {
                string timeOfDay = (string)eventData;
                if(timeOfDay == "Morning")
                {
                    isOn = true;
                    Console.WriteLine("Light turned on (Morning).");
                }
                else if(timeOfDay == "Night")
                {
                    isOn = false;
                    Console.WriteLine("Light turned off (Night).");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if(settings.ContainsKey("Brightness"))
                brightness = (int)settings["Brightness"];
            Console.WriteLine($"Light configured: Brightness={brightness}%.");
        }

        public void ExecuteCommand(string command)
        {
            if(command == "On")
            {
                isOn = true;
                Console.WriteLine("Light manually turned on.");
            }
            else if(command == "Off")
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

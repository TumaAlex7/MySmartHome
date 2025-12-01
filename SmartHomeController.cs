using System;
using System.Collections.Generic;
using MySmartHome.Devices;

namespace SmartHomeSystem
{
    public class SmartHomeController
    {
        public event Action<string> OnDayTimeChanged;
        public event Action<int> OnTemperatureChanged;
        public event Action OnMotionDetected;

        private readonly List<ISmartDevice> devices = new List<ISmartDevice>();
        private readonly EventLogger logger = new EventLogger();

        public void RegisterDevice(ISmartDevice device)
        {
            if(device != null)
            {
                devices.Add(device);
                Console.WriteLine($"Device {device.GetType().Name} registered successfully.");
            }
            else
            {
                Console.WriteLine("Device registration failed.");
            }
        }

        public void ChangeDayTime(string timeOfDay)
        {
            Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");
            OnDayTimeChanged?.Invoke(timeOfDay);
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}°C.");
            logger.Log($"Temperature changed to {temperature}°C.");
            OnTemperatureChanged?.Invoke(temperature);
        }

        public void DetectMotion()
        {
            Console.WriteLine("Event: Motion detected.");
            logger.Log("Motion detected.");
            OnMotionDetected?.Invoke();
        }

        public void TriggerDevice(string deviceName, string command)
        {
            foreach(var device in devices)
            {
                if(device.GetType().Name == deviceName)
                {
                    device.ExecuteCommand(command);
                    // Console message is not needed because the device will print its own message.
                    logger.Log($"Command {command} send to device {deviceName} successfully for execution.");
                    return;
                }
            }
            Console.WriteLine($"Device {deviceName} not found.");
            logger.Log($"Command {command} not send to device {deviceName} because device not found.");
        }

        public void ShowLog()
        {
            logger.ShowLog();
            Console.WriteLine();
        }
    }
}

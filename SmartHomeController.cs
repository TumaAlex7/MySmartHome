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
            if (device == null)
            {
                Console.WriteLine("Device registration failed because device is null.");
                logger.Log("Device registration failed because device is null.");
                return;
            }
            if (!devices.Contains(device))
            {
                devices.Add(device);
                Console.WriteLine($"Device {device.Name} registered successfully.");
                logger.Log($"Device {device.Name} registered successfully.");
            }
            else
            {
                Console.WriteLine("Device registration failed.");
                logger.Log($"Device {device.Name} registration failed because device already registered.");
            }
        }

        public void ChangeDayTime(string timeOfDay)
        {
            Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");
            if(OnDayTimeChanged == null)
            {
                Console.WriteLine("Event called but no handlers are registered.");
                logger.Log("Event called but no handlers are registered.");
                return;
            }
            foreach (var handler in OnDayTimeChanged.GetInvocationList())
            {
                try
                {
                    ((Action<string>)handler)(timeOfDay);
                    logger.Log($"Event handled by {handler.Method.Name} successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in event handler: {ex.Message}");
                    logger.Log($"Error in event handler: {ex.Message}. Handler: {handler.Method.Name}");
                }
            }
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}°C.");
            logger.Log($"Temperature changed to {temperature}°C.");
            if(OnTemperatureChanged == null)
            {
                Console.WriteLine("Event called but no handlers are registered.");
                logger.Log("Event called but no handlers are registered.");
                return;
            }
            foreach (var handler in OnTemperatureChanged.GetInvocationList())
            {
                try
                {
                    ((Action<int>)handler)(temperature);
                    logger.Log($"Event handled by {handler.Method.Name} successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in event handler: {ex.Message}");
                    logger.Log($"Error in event handler: {ex.Message}. Handler: {handler.Method.Name}");
                }
            }
        }

        public void DetectMotion()
        {
            Console.WriteLine("Event: Motion detected.");
            logger.Log("Motion detected.");
            if(OnMotionDetected == null)
            {
                Console.WriteLine("Event called but no handlers are registered.");
                logger.Log("Event called but no handlers are registered.");
                return;
            }
            foreach (var handler in OnMotionDetected.GetInvocationList())
            {
                try
                {
                    ((Action)handler)();
                    logger.Log($"Event handled by {handler.Method.Name} successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in event handler: {ex.Message}");
                    logger.Log($"Error in event handler: {ex.Message}. Handler: {handler.Method.Name}");
                }
            }
        }

        public void TriggerDevice(string deviceName, string command)
        {
            var device = devices.FirstOrDefault(d => d.Name == deviceName);
            if(device == null)
            {
                Console.WriteLine($"Device {deviceName} not found.");
                logger.Log($"Command {command} not send to device {deviceName} because device not found.");
                return;
            }
            device.ExecuteCommand(command);
            // Console message is not needed because the device will print its own message.
            logger.Log($"Command {command} send to device {deviceName} successfully for execution.");
        }

        public void ShowLog()
        {
            logger.ShowLog();
            Console.WriteLine();
        }
    }
}

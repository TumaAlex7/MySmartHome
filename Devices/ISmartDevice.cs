using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices
{
    public interface ISmartDevice
    {
        public string Name { get; set; }
        public EventLogger? Logger { get; set; }
        void Configure(Dictionary<string, object> settings);
        void ExecuteCommand(string command);
    }
}

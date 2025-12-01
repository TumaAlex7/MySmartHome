using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public interface ISmartDevice
    {
        public string Name { get; set; }
        void Configure(Dictionary<string, object> settings);
        void ExecuteCommand(string command);
    }
}

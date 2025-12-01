using System;
using System.Collections.Generic;

namespace SmartHomeSystem
{
    public class EventLogger
    {
        private readonly List<string> log = new List<string>();

        public void Log(string message)
        {
            log.Add($"{DateTime.Now}: {message}");
        }

        public void ShowLog()
        {
            Console.WriteLine("Event Log:");
            foreach(var message in log)
            {
                Console.WriteLine(message);
            }
        }
    }
}

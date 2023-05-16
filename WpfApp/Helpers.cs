using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    internal class Helpers
    {
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
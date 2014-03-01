using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temperature_Reader
{
    class TemperatureSensor
    {
        private HidDevice _device;
        
        public TemperatureSensor() {
            _device = HidDevices.Enumerate().Single(x => x.Attributes.VendorId == 3141 && x.Description == "HID-compliant device");

        }

        public double GetTemp() {
            var data = _device.Read().Data;

            double temp = (double)data[3] + (data[4] / 255.0);
            return temp;
        }

    }
}

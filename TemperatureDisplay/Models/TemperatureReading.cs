using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temperature_Display.Models
{
    public class TemperatureReading
    {
        public int TemperatureReadingId { get; set;  }
        public string sensorName { get; set; }
        public DateTime Date { get; set; }
        public decimal temperature { get; set; }
    }
}
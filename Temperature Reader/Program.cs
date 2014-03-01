using HidLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Temperature_Reader
{
    class Program
    {

        public static void Main(string[] args)
        {
            HidLibrary.HidDevice TempPerDevice;

            var tempSensor = new TemperatureSensor(); 

    
            while(true) {
                var currentTemp = tempSensor.GetTemp();

                Console.WriteLine("{0:00.00}", tempSensor.GetTemp());

                using(var wb = new WebClient()) {
                    var data = new NameValueCollection();
                    data["sensorName"] = System.Environment.MachineName;
                    data["temperature"] = string.Format("{0:00.00}",currentTemp);
                    try {
                        var response = wb.UploadValues("http://officetemperature-ppa.azurewebsites.net/temperature", "POST", data);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }

            Console.WriteLine("Done");

            Console.ReadKey();

        }


    }
}

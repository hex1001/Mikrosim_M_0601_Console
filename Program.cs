using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using EasyModbus;
using System.Threading.Tasks;

namespace Mikrosim_M_0601_Console
{
    class Program
    {
        static string port = "";
        static byte ID_dev = 1;
            
        static ModbusClient mb;
        static void Main(string[] args) {
            port = args[0];
            ID_dev = Byte.Parse(args[1]);
            Reading();
        }

        public static void Reading() {
            String[] str = new String[2];
            float kg = 0;
            try {
                
                //serial = new SerialPort(port);
                mb = new ModbusClient(port);
                mb.UnitIdentifier = ID_dev;
                mb.Baudrate = 9600;
                mb.Parity = Parity.None;
                mb.StopBits = StopBits.One;
                mb.ConnectionTimeout = 500;
                mb.Connect();
                string ssss = (mb.ReadHoldingRegisters(2, 1)[0] / 100.0).ToString();
                //Console.WriteLine(ssss);
                //Console.ReadLine();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(ssss, Directory.GetCurrentDirectory() + "\\natija.txt"))) {
                    outputFile.WriteLine(ssss);
                }
               
                mb.Disconnect();


            } catch (Exception ex) {
                mb.Disconnect();
                Console.WriteLine(ex);
            }
        }
    }
}

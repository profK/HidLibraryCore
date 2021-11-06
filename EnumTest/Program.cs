// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HidLibrary;

namespace Enumtest
{
    class Program
    {
        static void Main(string[] argv)
        {
            
            foreach (var dev in HidDevices.Enumerate().ToArray())
            {
                Console.WriteLine(dev.Name.Trim()+":"+dev.Capabilities.UsagePage);
                Console.WriteLine("Button Count: "+dev.Capabilities.NumberInputButtonCaps);
                
                foreach (var button in dev.Buttons.buttons)
                {
                    foreach(var name in button.Names)
                        if (name!=null)
                            Console.WriteLine("  "+name);
                }
                
            }
        }
    }
}
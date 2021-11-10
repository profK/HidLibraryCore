// See https://aka.ms/new-console-template for more information

using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HidLibrary;

namespace Enumtest
{
    class Program
    {
        static void PrintCollections(int nodeIdx,HidDevice dev,string indent)
        {
            HidCollectionNode node = dev.Collections[nodeIdx];
            Console.WriteLine(indent+Enum.ToObject(typeof(HIDUsages.Desktop),node.Usage));
            string newIndent = indent + "    ";
            foreach (var hidButton in dev.Buttons.buttons)
            {
                if (hidButton.CollectionID() == nodeIdx)
                {
                    foreach (var usage in hidButton.Usages)
                    {
                        Console.WriteLine(indent+
                                          Enum.ToObject(typeof(HIDUsages.UsagePages),hidButton.UsagePage).ToString()+
                                          ":"+ 
                                          Enum.ToObject(typeof(HIDUsages.Desktop),usage).ToString());
                    }
                }
            }

            if (node.FirstChild!=0)
                PrintCollections(node.FirstChild,dev,newIndent);
            if (node.NextSibling!=0)
                PrintCollections(node.NextSibling,dev,indent);
        }
        static void Main(string[] argv)
        {
            
            foreach (var dev in HidDevices.Enumerate().ToArray())
            {
                if (dev.Name.Contains("aitek"))
                {
                    Console.WriteLine(dev.Name.Trim()+":"+dev.Capabilities.Usage);
                    PrintCollections(0,dev,"    ");

                }
               

            }
        }
    }
}
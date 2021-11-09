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
        static void PrintCollection(HidCollectionNode node,int indent=0)
        {
            char[] cInd = new char[indent];
            Array.Fill<char>(cInd, ' ');
            string ind = new string(cInd);
            Console.WriteLine(ind+Enum.ToObject(typeof(HIDUsages.Desktop),node.Usage));
            foreach (var child in node.Children)
            {
                PrintCollection(child,indent+4);
            }
        }
        static void Main(string[] argv)
        {
            
            foreach (var dev in HidDevices.Enumerate().ToArray())
            {
                Console.WriteLine(dev.Name.Trim()+":"+dev.Capabilities.Usage);
                foreach (var node in dev.Collection.RootNodes)
                {
                    PrintCollection(node,4);
                }
                    
            }
        }
    }
}
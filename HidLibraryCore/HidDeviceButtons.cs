using System;
using System.Collections.Generic;
using System.Text;

namespace HidLibrary
{

    public class HidButton
    {
        private NativeMethods.HIDP_BUTTON_CAPS buttonCaps;

        internal HidButton(NativeMethods.HIDP_BUTTON_CAPS hidpButtonCaps)
        {
            buttonCaps = hidpButtonCaps;
        }

        public string[] Names
        {
            get
            {
                if (buttonCaps.IsRange)
                {
                    int usageMin = buttonCaps.Range.UsageMin;
                    int usageMax = buttonCaps.Range.UsageMax;
                    string[] arr = new string[usageMax - usageMin + 1];
                    for (var usage = usageMin; usage <= usageMax; usage++)
                    {
                        arr[usage-usageMin] = 
                            Enum.GetName(typeof(HIDUsages.Desktop),
                                usage);
                    }

                    return arr;
                }
                else
                {
                    string name = Enum.GetName(typeof(HIDUsages.Desktop),
                        buttonCaps.NotRange.Usage);
                    string[] arr = new string[1];
                    arr[0] = name;
                    return arr;
                }
            }
        }

        public ushort[] Usages
        {
            get
            {
                if (buttonCaps.IsRange)
                {
                    List<ushort> usageList = new List<ushort>();
                    for (ushort b = buttonCaps.Range.UsageMin;
                        b <= buttonCaps.Range.UsageMax;
                        b++)
                    {
                        usageList.Add(b);
                    }

                    return usageList.ToArray();
                }
                else
                {
                    ushort[] result = new ushort[1];
                    result[0] = buttonCaps.NotRange.Usage;
                    return result;
                }
            }
        }
        
    }
    public class HidDeviceButtons
    {
        public HidButton[] buttons;
        internal HidDeviceButtons()
        {
            buttons = new HidButton[0];
        }
        internal HidDeviceButtons(
            NativeMethods.HIDP_BUTTON_CAPS[] hidpButtonCapsArray)
        {
            buttons = new HidButton[hidpButtonCapsArray.Length];
            for (int idx=0;idx<buttons.Length;idx++)
            {
                buttons[idx] = new HidButton(hidpButtonCapsArray[idx]);
            }
        }
    }
    
    
}
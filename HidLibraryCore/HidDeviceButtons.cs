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
                List<string> names = new List<string>();
                foreach (var usage in Usages)
                {
                   
                    var usageEnum = Enum.ToObject(typeof(HIDUsages.Desktop), usage);
                    if (Enum.IsDefined(typeof(HIDUsages.Desktop), usageEnum))
                    {
                        names.Add(usageEnum.ToString());
                    }
                }

                return names.ToArray();
            }
        }
        
        public ushort UsagePage
        {
            get
            {
                return buttonCaps.UsagePage;
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
        
      

        public int CollectionID()
        {
            return buttonCaps.LinkCollection;
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
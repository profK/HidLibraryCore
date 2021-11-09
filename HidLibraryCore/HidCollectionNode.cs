using System.Collections.Generic;

namespace HidLibrary
{
   
    public class HidCollectionNode
    {
        public ushort FirstChild { get; private set; }

        public ushort UsagePage { get; private set; } 
        public ushort Usage { get; private set; } 
        
        public ushort Parent { get; private set; }
        public uint CollectionType { get; private set; }
        public ushort NextSibling { get; private set; }

        internal HidCollectionNode(
            NativeMethods.HIDP_LINK_COLLECTION_NODE node)
        {
           
            UsagePage = node.LinkUsagePage;
            Usage = node.LinkUsage;
            Parent = node.Parent;
            CollectionType = node.CollectionType;
            FirstChild = node.FirstChild;
            NextSibling = node.NextSibling;
            
        }

       
    }
}
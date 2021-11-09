using System.Collections.Generic;

namespace HidLibrary
{
   
    public class HidCollectionNode
    {
        public ushort UsagePage { get; private set; } 
        public ushort Usage { get; private set; } 
        
        public HidCollectionNode? Parent { get; private set; }
        public uint CollectionType { get; private set; }
        public HidCollectionNode[] Children { get; private set; }

        internal HidCollectionNode(
            NativeMethods.HIDP_LINK_COLLECTION_NODE[] nodes,
            int index,
            HidCollectionNode parent): this(nodes,index)
        {
            Parent = parent;
        }
        internal HidCollectionNode(
            NativeMethods.HIDP_LINK_COLLECTION_NODE[] nodes,
            int nodeIndex)
        {
            var node = nodes[nodeIndex];
            UsagePage = node.LinkUsagePage;
            Usage = node.LinkUsage;
            Parent = null;
            CollectionType = node.CollectionType;
            List<HidCollectionNode> childList = new List<HidCollectionNode>();
            int currentChild = node.FirstChild;
            do
            {
                childList.Add(new HidCollectionNode(nodes, currentChild,
                    this));
                currentChild = nodes[currentChild].NextSibling;
            } while (currentChild != 0);
            Children = childList.ToArray();
        }
    }
}
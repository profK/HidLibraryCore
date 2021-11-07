using System.Collections.Generic;

namespace HidLibrary
{
    public class HidCollectionNode
    {
        public ushort UsagePage { get; private set; } 
        public ushort Usage { get; private set; } 
        
        public HidCollectionNode? Parent { get; private set; }
        public ushort CollectionType { get; private set; }

        internal HidCollectionNode(int nodeIndex,
            NativeMethods.HIDP_LINK_COLLECTION_NODE[] nodes,
            HidCollectionNode parent): this(nodeIndex, nodes)
        {
            Parent = parent;
        }
        internal HidCollectionNode(int nodeIndex,
            NativeMethods.HIDP_LINK_COLLECTION_NODE[] nodes)
        {
            var node = nodes[nodeIndex];
            UsagePage = node.LinkUsagePage;
            Usage = node.LinkUsage;
            Parent = null;
            CollectionType = node.CollectionType;
            List<HidCollectionNode> childList = new List<HidCollectionNode>();
            for (int childNum = 0;
                childNum < node.NumberOfChildren;
                childNum++)
            {
                childList.Add(new HidCollectionNode(
                    node.FirstChild+childNum,nodes,this));
            }
        }
    }
}
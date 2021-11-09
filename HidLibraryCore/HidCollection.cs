using System.Collections.Generic;

namespace HidLibrary
{
    public class HidCollection
    {
        public HidCollectionNode[] RootNodes { get; private set; }

        internal HidCollection(NativeMethods.HIDP_LINK_COLLECTION_NODE[] nodes)
        {
            List<HidCollectionNode> nlist = new List<HidCollectionNode>();
            int currentRoot = 0;
            do
            {
                nlist.Add(new HidCollectionNode(nodes,currentRoot));
                currentRoot = nodes[currentRoot].NextSibling;
            } while (currentRoot != 0);

            RootNodes = nlist.ToArray();
        }
    }
}
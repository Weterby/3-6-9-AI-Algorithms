using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Six_Nine.PNS.Data;

namespace Three_Six_Nine.PNS
{
    class ProofNumberSearch
    {
        private Node root;
        //expandNode
        //pns algorithm
        //selectMostProvingNode
        //setProofAndDisproofNumbers
        //updateAncestors
        //evaluateNode
        public ProofNumberSearch()
        {

        }

        private void ExpandNode(Node node)
        {
            node.GenerateChildren();
            foreach(Node child in node.Children)
            {
                //EvaluateNode(child);
                //SetProofAndDisproofNumbers(child);
                if(node.Type == NodeType.And)
                {
                    if(child.Disproof == 0) break;
                }
                if(node.Type == NodeType.Or)
                {
                    if (child.Proof == 0) break;
                }
            }

            node.IsExpanded = true;
        }

        private void SetProofAndDisproofNumbers(Node node)
        {
            if (node.IsExpanded)
            {
                if (node.Type == NodeType.And)
                {
                    node.Proof = 0;
                    node.Disproof = Int32.MaxValue;
                    foreach (Node child in node.Children)
                    {
                        node.Proof += child.Proof;
                        node.Disproof = Math.Min(node.Disproof, child.Disproof);
                    }
                }

                if (node.Type == NodeType.Or)
                {
                    node.Proof = Int32.MaxValue;
                    node.Disproof = 0;
                    foreach (Node child in node.Children)
                    {
                        node.Disproof += child.Disproof;
                        node.Proof = Math.Min(node.Proof, child.Proof);
                    }
                }
            }
            else
            {
                switch (node.Value)
                {
                    case NodeValue.Win:
                        node.Proof = 0;
                        node.Disproof = Int32.MaxValue;
                        break;
                    case NodeValue.Lose:
                        node.Proof = Int32.MaxValue;
                        node.Disproof = 0;
                        break;
                    case NodeValue.Unknown:
                        node.Proof = 1;
                        node.Disproof = 1;
                        break;
                }
            }
        }
    }
}

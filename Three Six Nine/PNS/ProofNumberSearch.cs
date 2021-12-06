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
        private Resource resource;
        public ProofNumberSearch()
        {
            resource = new Resource();
        }

        private void ExpandNode(Node node)
        {
            node.GenerateChildren();
            foreach(Node child in node.Children)
            {
                EvaluateNode(child);
                SetProofAndDisproofNumbers(child);
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
                        if (isIntValueExceeded(node.Proof, child.Proof))
                        {
                            node.Proof = Int32.MaxValue;
                        }
                        else node.Proof += child.Proof;
                        node.Disproof = Math.Min(node.Disproof, child.Disproof);
                    }
                }
                else
                {
                    node.Proof = Int32.MaxValue;
                    node.Disproof = 0;
                    foreach (Node child in node.Children)
                    {
                        if (isIntValueExceeded(node.Disproof, child.Disproof))
                        {
                            node.Disproof = Int32.MaxValue;
                        }
                        else node.Disproof += child.Disproof;
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
        
        private Node SelectMostProvingNode(Node node)
        {
            Node best = null;

            while (node.IsExpanded)
            {
                int value = Int32.MaxValue;
                if (node.Type == NodeType.And)
                {
                    foreach(Node child in node.Children)
                    {
                        if(value > child.Disproof)
                        {
                            best = child;
                            value = child.Disproof;
                        }
                    }
                }
                if (node.Type == NodeType.Or)
                {
                    foreach (Node child in node.Children)
                    {
                        if (value > child.Proof)
                        {
                            best = child;
                            value = child.Proof;
                        }
                    }
                }
                node = best;
            }

            return node;
        }
    
        private Node UpdateAncestors(Node node, Node root)
        {
            int oldProof;
            int oldDisproof;

            while(node != root)
            {
                oldProof = node.Proof;
                oldDisproof = node.Disproof;

                SetProofAndDisproofNumbers(node);
                if (node.Proof == oldProof && node.Disproof == oldDisproof)
                {
                    return node;
                }

                node = node.Parent;
            }
            SetProofAndDisproofNumbers(root);

            return root;
        }
    
        private Node Search(Node root)
        {
            EvaluateNode(root);
            SetProofAndDisproofNumbers(root);
            Node current = root;

            while(root.Proof != 0 && root.Disproof != 0 && resource.IsAvailable())
            {
                Node mostProving = SelectMostProvingNode(current);
                ExpandNode(mostProving);
                current = UpdateAncestors(mostProving, root);
            }

            Node best = GetBestMove(root);
            return best;
        }

        private void EvaluateNode(Node node)
        {
            int playerScore = node.Board.P1Score;
            int aiScore = node.Board.P2Score;

            if (aiScore - playerScore > 0) node.Value = NodeValue.Win;
            else if (aiScore - playerScore < 0) node.Value = NodeValue.Lose;
            else node.Value = NodeValue.Unknown;
        }

        private Node GetBestMove(Node root)
        {
            if (root.Children.Count == 0)
                return null;

            float value = 0;
            Node best = null;

            foreach(Node child in root.Children)
            {
                float childValue = (float)child.Disproof / (float)child.Proof;
                if (childValue > value)
                {
                    value = childValue;
                    best = child;
                }
            }

            return best;
        }

        private bool isIntValueExceeded(int num1, int num2)
        {
            return ((long)num1) + ((long)num2) >= Int32.MaxValue;
        }
    }
}

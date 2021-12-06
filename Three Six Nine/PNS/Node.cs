using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Six_Nine.PNS.Data;

namespace Three_Six_Nine.PNS
{
    class Node
    {
        private Board board;

        private NodeType type;
        private NodeValue value;

        private int proof;
        private int disproof;

        private bool isExpanded;
        private Node parent;

        private List<Node> children;

        public Node(Board board, NodeType type, Node parent)
        {
            this.board = board;
            this.type = type;
            this.parent = parent;

            InitializeValues();
        }

        private void InitializeValues()
        {
            value = NodeValue.Unknown;
            isExpanded = false;
            proof = 0;
            disproof = 0;
        }

        private void GenerateChildren()
        {
            foreach (int index in board.GetAllEmptyCellsIndexes(board.BoardTable))
            {
                Board boardCopy = new Board();
                boardCopy.BoardTable = board.DeepCopy();

                Node child = new Node(boardCopy, (type == NodeType.And ? NodeType.Or : NodeType.And), this);

                children.Add(child);
            }
        }
    }
}

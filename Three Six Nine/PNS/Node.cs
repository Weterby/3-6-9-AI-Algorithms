﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Six_Nine.PNS.Data;

namespace Three_Six_Nine.PNS
{
    class Node
    {
        #region Private Fields
        private Board board;

        private NodeType type;
        private NodeValue value;

        private int proof;
        private int disproof;

        private bool isExpanded;
        private Node parent;

        private List<Node> children;

        #endregion

        #region Public Properties
        internal Board Board { get => board; set => board = value; }
        internal NodeType Type { get => type; set => type = value; }
        internal NodeValue Value { get => value; set => this.value = value; }
        public int Proof { get => proof; set => proof = value; }
        public int Disproof { get => disproof; set => disproof = value; }
        public bool IsExpanded { get => isExpanded; set => isExpanded = value; }
        internal Node Parent { get => parent; set => parent = value; }
        internal List<Node> Children { get => children; set => children = value; }





        #endregion

        public Node(Board board, NodeType type, Node parent)
        {
            this.Board = board;
            this.Type = type;
            this.Parent = parent;

            InitializeValues();
        }

        private void InitializeValues()
        {
            Value = NodeValue.Unknown;
            IsExpanded = false;
            Proof = 0;
            Disproof = 0;
        }

        public void GenerateChildren()
        {
            foreach (int index in Board.GetAllEmptyCellsIndexes(Board.BoardTable))
            {
                Board boardCopy = board.DeepCopy();
                boardCopy.BoardTable[index] = 1;

                if (Type == NodeType.Or) boardCopy.P2Score += boardCopy.CalculatePoints(boardCopy.BoardTable, index);
                else boardCopy.P1Score += boardCopy.CalculatePoints(boardCopy.BoardTable, index);

                Node child = new Node(boardCopy, (Type == NodeType.And ? NodeType.Or : NodeType.And), this);

                Children.Add(child);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class Algorithm
    {
        protected int depth;
        protected bool isMaximizing;
        int Depth { get => depth; set => depth = value; }
        bool IsMaximizing { get => isMaximizing; set => isMaximizing = value; }
        public virtual int CalculateMove()
        {
            return 0;
        }
    }
}

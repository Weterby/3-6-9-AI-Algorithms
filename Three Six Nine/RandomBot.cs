using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Six_Nine
{
    class RandomBot : Algorithm
    {
        public RandomBot(Board board) : base(board)
        {

        }
        public override int CalculateMove()
        {
            List<int> indexes = board.GetAllEmptyCellsIndexes(board.BoardTable);
            Random rand = new Random();
            int move = indexes[rand.Next(indexes.Count)];
            return move;
        }
    }
}

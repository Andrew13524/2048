using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Coordinate
    {
        public int X { get => x; }
        private readonly int x;

        public int Y { get => y; }
        private readonly int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}

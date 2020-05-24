using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disply
{
    public enum Team { friend, enemy, neutral}
    public class Unit
    {
        public char c = 'w';
        public Team team = Team.neutral;
        public Coordinate Pos = new Coordinate(0, 0);
        public Coordinate Speed = new Coordinate(0, 0);

        void Live() { 
            Map.Write(this); 
        }
        public Unit()
        {
            Library.units.Add(this);
            Program.Step += Live;
        }
        public Unit(Coordinate a) : base()
        {
            Pos = a.Clone();
            Library.units.Add(this);
            Program.Step += Live;
        }
        public void Die()
        {
            Program.Step -= Live;
            Library.units.Remove(this);
        }
    }
}

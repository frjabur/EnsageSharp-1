using Ensage;
using SharpDX;

namespace LoneDruidSharpRewrite.Utilities
{
    public class Move
    {
        private readonly Sleeper sleeper;

        private readonly Unit unit;

        public Move(Unit unit)
        {
            sleeper = new Sleeper();
            this.unit = unit;
        }

        public void Pause(float duration)
        {
            sleeper.Sleep(duration);
        }

        public void ToPosition(Vector3 position)
        {
            Execute(position);
        }

        private void Execute(Vector3 position)
        {
            if (sleeper.Sleeping)
            {
                return;
            }
            unit.Move(position);
            sleeper.Sleep(200);
        }

    }
}

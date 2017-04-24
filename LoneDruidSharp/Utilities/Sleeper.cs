namespace LoneDruidSharpRewrite.Utilities
{
    class Sleeper
    {
        private float lastSleepTickCount;

        public Sleeper()
        {
            lastSleepTickCount = 0;
        }

        public bool Sleeping
        {
            get
            {
                return Variable.TickCount < lastSleepTickCount;
            }
        }

        public void Sleep(float duration)
        {
            lastSleepTickCount = Variable.TickCount + duration;
        }





    }
}

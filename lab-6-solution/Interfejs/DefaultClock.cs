namespace lab_6.Interfejs
{
    public class DefaultClock : IClockProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
        public DateTime Epoch()
        {
            return DateTime.UnixEpoch;
        }
    }

}

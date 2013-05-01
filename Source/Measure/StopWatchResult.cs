using System;

namespace Measure
{
    public class StopWatchResult<T>
    {
        public T Result { get; set; }

        public TimeSpan ElapsedTime { get; set; }
    }
}
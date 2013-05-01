using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Measure
{
    public class StopWatch
    {
        public static StopWatchResult<T> MeasureElapsedTime<T>(Expression<Func<T>> func)
        {
            var compiled = func.Compile();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            T result;

            try
            {
                result = compiled();
            }
            finally
            {
                stopWatch.Stop();
            }

            return new StopWatchResult<T> { ElapsedTime = stopWatch.Elapsed, Result = result };
        }

        public static TimeSpan MeasureElapsedTime(Expression<Action> action)
        {
            var compiled = action.Compile();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                compiled();
            }
            finally
            {
                stopWatch.Stop();
            }

            return stopWatch.Elapsed;
        }
    }
}

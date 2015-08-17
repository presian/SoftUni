namespace EventsInRange
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Wintellect.PowerCollections;

    class EventsInRangeMain
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var events = new OrderedMultiDictionary<DateTime, string>(true);
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] eventEntry = Console.ReadLine()
                    .Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => e.Trim())
                    .ToArray();
                events.Add(DateTime.Parse(eventEntry[1]), eventEntry[0]);
            }

            var rangeCount = int.Parse(Console.ReadLine());
            var ranges = new List<DateTime[]>();
            for (int i = 0; i < rangeCount; i++)
            {
                var currentRanges = Console.ReadLine()
                    .Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => DateTime.Parse(r.Trim()))
                    .ToArray();
                ranges.Add(currentRanges);
            }

            for (int i = 0; i < ranges.Count; i++)
            {
                
            }
        }
    }
}

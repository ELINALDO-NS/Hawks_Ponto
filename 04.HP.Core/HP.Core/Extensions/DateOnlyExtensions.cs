using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Extensions
{
    public static class DateOnlyExtensions
    {
        public static DateTimeOffset ToDateTimeOffset( this DateOnly date, TimeOnly? time = null, TimeSpan? offset = null)
        {
            var targetTime = time ?? TimeOnly.MinValue;
            var targetOffset = offset ?? TimeSpan.Zero;
            return new DateTimeOffset(date.ToDateTime(targetTime), targetOffset);
        }
    }
}

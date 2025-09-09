using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.ValueObjects
{
    public static class GenericMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source is null) return default!;

            var dest = new TDestination();
            var sourceProps = typeof(TSource).GetProperties();
            var destProps = typeof(TDestination).GetProperties().ToDictionary(p => p.Name);

            foreach (var srcProp in sourceProps)
            {
                if (destProps.TryGetValue(srcProp.Name, out var destProp) && destProp.CanWrite)
                {
                    var value = srcProp.GetValue(source);
                    destProp.SetValue(dest, value);
                }
            }

            return dest;
        }

        public static List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList)
            where TDestination : new()
        {
            return sourceList.Select(src => Map<TSource, TDestination>(src)).ToList();
        }
    }


}

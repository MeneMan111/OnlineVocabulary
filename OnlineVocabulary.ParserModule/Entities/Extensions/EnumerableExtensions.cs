﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsAnyOf<T>(this T obj, params T[] list)
        {
            for (var idx = list.Length - 1; idx >= 0; idx--)
                if (list[idx].Equals(obj))
                    return true;

            return false;
        }

        public static IEnumerable<T> Prefer<T>(this IEnumerable<T> seq, Func<T, bool> flag)
        {
            return seq.OrderByDescending(x => flag(x) ? 1 : 0);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> seq)
        {
            return seq == null || !seq.Any();
        }
    }
}

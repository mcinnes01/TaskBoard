using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Extensions
{
    public static class StringExtensions
    {
        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}

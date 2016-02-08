using JetBrains.Annotations;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(StringExtensions));

        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}

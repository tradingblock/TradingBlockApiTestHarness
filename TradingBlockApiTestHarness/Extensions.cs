using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBlockApiTestHarness
{
    internal static class Extensions
    {
        /// <summary>
        /// Formats a List to a string in the format:
        /// 
        /// "{ Count=2; Item={0={item1},1={item2}} }"
        /// 
        /// This should be used instead of the default ToString and instead of
        /// creating your own custom ToString() method.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToStringExtended<T>(this IList<T> list)
        {
            StringBuilder sb = new StringBuilder("{ Count=");
            sb.Append(list.Count);
            sb.Append("; Items=");
            sb.Append('{');
            for (int i = 0; i < list.Count; ++i)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(i).Append("={");
                try
                {
                    sb.Append(list[i]);
                }
                catch (Exception ex)
                {
                    sb.Append(ex);
                }
                sb.Append('}');
            }
            sb.Append("} }");
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    static public class StockLinqExtension
    {
        public static IEnumerable<IEnumerable<T>> EachCons<T>(this IEnumerable<T> source, int span)
        {
            for(int i = 0; i <= source.Count() - span; i++)
            {
                yield return source.Skip(i).Take(span);
            }
        }
        //元の配列と同じ要素数の配列を返す為にspan-1のnullを追加する
        public static IEnumerable<IEnumerable<T>> MapIndicator<T>(this IEnumerable<T> source, int span)
        {
            for(int i = 0; i < span-1; i++)
            {
                yield return null;
            }
            foreach(var result in source.EachCons(span))
            {
                yield return result;
            }
        }
        //移動平均
        public static IEnumerable<double?> MovingAverage<T>(this IEnumerable<T> source, int span)
        {
            foreach(var vals in source.MapIndicator(span))
            {
                if(vals == null)
                {
                    yield return null;
                    continue;
                }
                double? total = 0;
                foreach(var val in vals)
                {
                    total += (dynamic)val;
                }
                yield return total / span;
            }
        }
        //区間高値
        public static IEnumerable<double?> Highs<T>(this IEnumerable<T> source, int span)
        {
            foreach (var vals in source.MapIndicator(span))
            {
                if (vals == null)
                {
                    yield return null;
                    continue;
                }
                double? high = (dynamic)vals.FirstOrDefault();
                foreach (var val in vals)
                {
                    high = (high < (dynamic)val) ? (dynamic)val : high;
                }
                yield return high;
            }
        }
        //区間安値
        public static IEnumerable<double?> Lows<T>(this IEnumerable<T> source, int span)
        {
            foreach (var vals in source.MapIndicator(span))
            {
                if (vals == null)
                {
                    yield return null;
                    continue;
                }
                double? low = (dynamic)vals.FirstOrDefault();
                foreach (var val in vals)
                {
                    low = (low > (dynamic)val) ? (dynamic)val : low;
                }
                yield return low;
            }
        }
    }
}

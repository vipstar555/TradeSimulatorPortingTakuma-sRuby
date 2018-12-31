using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeSimulator.check
{
    static public class ArrayCheck
    {
        static public void ArrayCheckMain()
        {
            var array = new int[]{ 100, 97, 111, 115, 116, 123, 121, 119, 115, 110 };
            MessageBox.Show(array.Sum().ToString()); // 1127
            MessageBox.Show(array.Average().ToString()); // 112.7
                        
            MessageBox.Show(string.Join(", ", array.MovingAverage(4))); // [nil, nil, nil, 105.75, 109.75, 116.25, 118.75, 119.75, 119.5, 116.25]
            MessageBox.Show(string.Join(", ", array.Highs(3))); // [nil, nil, 111, 115, 116, 123, 123, 123, 121, 119]
            MessageBox.Show(string.Join(", ", array.Lows(3))); // [nil, nil, 97, 97, 111, 115, 116, 119, 115, 110]

            //3区間高値と安値の中間(nullオブジェクトパターン適応したい？)
            var middles = new List<double?>();
            foreach (var vals in array.MapIndicator(3))
            {
                if(vals == null)
                {
                    middles.Add(null);
                    continue;
                }
                middles.Add((vals.Max() + vals.Min()) / 2.0);
            }
            MessageBox.Show(string.Join(", ", middles)); // [nil, nil, 104.0, 106.0, 113.5, 119.0, 119.5, 121.0, 118.0, 114.5]
            //前日との増減
            var changes = new List<int?>();
            foreach (var vals in array.MapIndicator(2))
            {
                if (vals == null)
                {
                    changes.Add(null);
                    continue;
                }
                changes.Add(vals.Last() - vals.First() );
            }
            MessageBox.Show(string.Join(", ", changes)); // [nil, -3, 14, 4, 1, 7, -2, -2, -4, -5]
            //3区間の増減の平均
            var average_changes = changes.MovingAverage(3);
            MessageBox.Show(string.Join(", ", average_changes)); //  [nil, nil, nil, 5.0, 6.333333333333333, 4.0, 2.0, 1.0,  -2.6666666666666665, -3.6666666666666665]
            //指数移動平均（Exponential Moving Average）
            var span = 4;
            var alpha = 2.0 / (span + 1);
            List<double?> ema_array = new List<double?>();
            double? ema = null;
            foreach (var vals in array.MapIndicator(span))
            {
                if (vals == null)
                {
                    ema_array.Add(null);
                    continue;
                }
                if(ema.HasValue)
                {
                    ema += alpha * (vals.Last() - ema);
                }
                else
                {
                    ema = vals.Average();
                    
                }
                ema_array.Add(ema);
            }
            MessageBox.Show(string.Join(", ", ema_array)); //[nil, nil, nil, 105.75, 109.85, 115.11, 117.466, 118.0796, 116.84776, 114.108656]
        }
    }
}

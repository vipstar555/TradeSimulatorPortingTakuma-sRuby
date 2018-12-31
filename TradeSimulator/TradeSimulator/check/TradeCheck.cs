using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.lib;
using System.Windows.Forms;

namespace TradeSimulator.check
{
    static public class TradeCheck
    {
        static public void TradeCheckMain()
        {
            var trade = new Trade(
                stockCode: 8604,
                tradeType: Trade.TradeTypeEnum.Long,
                entryDate: DateTime.Parse("2011/11/14"),
                entryPrice: 251,
                entryTime: Trade.TimeEnum.Open,
                volume: 100
                );
            MessageBox.Show(trade.StockCode.ToString());    //8604
            MessageBox.Show(trade.EntryDate.ToString());    //2011/11/14
            MessageBox.Show(trade.EntryPrice.ToString());   //251
            MessageBox.Show(trade.IsLong().ToString());     //true
            MessageBox.Show(trade.IsShort().ToString());    //false
            MessageBox.Show(trade.Closed().ToString());     //false

            trade.FirstStop = 241;
            trade.Stop = 241;
            trade.Length = 1;

            MessageBox.Show(trade.FirstStop.ToString());    //241
            MessageBox.Show(trade.Stop.ToString());         //241
            MessageBox.Show(trade.R().ToString());          //10
            MessageBox.Show(trade.Length.ToString());       //1

            trade.Length += 1;

            MessageBox.Show(trade.Length.ToString());       //2

            trade.Exit(
                date: DateTime.Parse("2011/11/15"),
                price: 255,
                time: Trade.TimeEnum.InSession
                );

            MessageBox.Show(trade.Closed().ToString());     //true
            MessageBox.Show(trade.ExitDate.ToString());     //2011/11/15
            MessageBox.Show(trade.Profit().ToString());     //400
            MessageBox.Show(trade.PercentageResult().ToString());   //1.593625498007968
            MessageBox.Show(trade.RMultiple().ToString());  //0.4
        }
    }
}

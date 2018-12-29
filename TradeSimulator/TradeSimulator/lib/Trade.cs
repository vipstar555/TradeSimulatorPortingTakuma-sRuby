using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.lib
{
    public class Trade : AbstractTrade
    {
        //仕掛ける　コンストラクタ
        public Trade(int stockCode, TradeTypeEnum tradeType, DateTime entryDate, double entryPrice, long volume, TimeEnum entryTime)
        {
            StockCode = stockCode;
            TradeType = tradeType.ToString();
            EntryDate = entryDate;
            EntryPrice = entryPrice;
            Volume = volume;
            EntryTime = entryTime.ToString();
            Length = 1;

            ExitDate = null;
            ExitPrice = null;
        }
        //手仕舞う
        public override void Exit(DateTime date, double price, TimeEnum time)
        {
            ExitDate = date;
            ExitPrice = price;
            ExitTime = time.ToString();
        }
        //手仕舞い済みかどうか
        public override bool Closed()
        {
            return ( ExitDate.HasValue && ExitPrice.HasValue );
        }
        //買いトレードかどうか
        public override bool IsLong()
        {
            return TradeType == TradeTypeEnum.Long.ToString();
        }
        //売りトレードかどうか
        public override bool IsShort()
        {
            return TradeType == TradeTypeEnum.Short.ToString();
        }
        //損益金額
        public override double? Profit()
        {
            return PlainResult() * Volume;
        }
        //%損益
        public override double? PercentageResult()
        {
            return (PlainResult() / EntryPrice)*100;
        }
        //R
        public override double? R()
        {
            if(FirstStop.HasValue == false)
            {
                return null;
            }
            if(IsLong())
            {
                return EntryPrice - FirstStop;
            }
            if(IsShort())
            {
                return FirstStop - EntryPrice;
            }
            return null;
        }
        //R倍数
        public override double? RMultiple()
        {
            if (FirstStop.HasValue == false)
            {
                return null;
            }
            if(R() == 0)
            {
                return 0;
            }
            return PlainResult() / R();
        }
        //株数を掛けない損益
        private double? PlainResult()
        {
            if(IsLong())
            {
                return ExitPrice - EntryPrice;
            }
            else if(IsShort())
            {
                return EntryPrice - ExitPrice;
            }
            return null;
        }
    }
}

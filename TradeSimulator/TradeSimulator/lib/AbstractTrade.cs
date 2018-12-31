using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.lib
{
    public abstract class AbstractTrade
    {
        //買い売り判断用
        public enum TradeTypeEnum
        {
            Long,
            Short
        }
        //寄り引けザラ場どこで仕掛けたか判断用
        public enum TimeEnum
        {
            Open,
            InSession,
            Close
        }
        //トレードプロパティ
        public int StockCode { get; set; }
        public string TradeType { get; set; }
        public DateTime EntryDate { get; set; }
        public double EntryPrice { get; set; }
        public string EntryTime { get; set; }
        public long Volume { get; set; }
        public DateTime? ExitDate { get; set; }
        public double? ExitPrice { get; set; }
        public string ExitTime { get; set; }
        public int Length { get; set; }
        public double? FirstStop { get; set; }
        public double? Stop { get; set; }
        //手仕舞う
        public abstract void Exit(DateTime date, double price, TimeEnum time );
        //手仕舞い済みかどうか
        public abstract bool Closed();
        //買いトレードかどうか
        public abstract bool IsLong();
        //売りトレードかどうか
        public abstract bool IsShort();
        //損益金額
        public abstract double? Profit();
        //%損益
        public abstract double? PercentageResult();
        //R
        public abstract double? R();
        //R倍数
        public abstract double? RMultiple();
        //株数を掛けない損益　private
    }
}

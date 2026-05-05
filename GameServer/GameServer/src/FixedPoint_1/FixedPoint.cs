using System;
using System.Runtime.CompilerServices;

namespace ChuFixedPoint
{
    [Serializable]
    public struct FixedPoint
    {
        public long raw;
        
        public const int Multiplier = 1<<Precision;//1 << 16  = 65536
        /// <summary>
        /// 小数的位数
        /// </summary>
        public const int Precision = 16;
        
        public const int TotalBitCount = 64;
        
        public const int IntBitCount = TotalBitCount - Precision;//48
        /// <summary>
        /// 小数掩码
        /// </summary>
        public const long DecimalsMask = (long)(ulong.MaxValue >> IntBitCount);//2 ^ 16 - 1 = 65535

        /// <summary>
        /// 整数部分掩码 -1L表示所有位都是1(补码表示法) 
        /// </summary>
        public const long IntMask = (-1L & ~DecimalsMask);
        /// <summary>
        /// 小数部分取值范围 0 ~ 65536
        /// </summary>
        public const long DecimalsRange = DecimalsMask + 1;//2^16 

        /// <summary>
        ///  long 的范围右移16位   去掉小数部分，只保留整数部分的范围
        /// </summary>
        public const long MinValue = long.MinValue >> Precision;
        public const long MaxValue = long.MaxValue >> Precision;
                
        public static FixedPoint Zero = new FixedPoint(0);
        public static FixedPoint One = new FixedPoint(1);
        public static FixedPoint Pi = new FixedPoint(3.1416f);
        public static FixedPoint Pi2 = Pi * 2;
        public static FixedPoint One_Div_Pi2 = 1 / Pi2;
        
        private FixedPoint(long raw)
        {
            this.raw = raw;
        }

        public FixedPoint(int v)
        {
            raw = v * Multiplier; //乘以 2^16 相当于左移16位
        }
        
        public FixedPoint(float v)
        {
            raw = (long)Math.Round(v * Multiplier);
        }

        #region 运算符重载

        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator +(FixedPoint a, FixedPoint b)
        {
            a.raw+=b.raw;
            return a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator +(FixedPoint a, int b)
        {
            a.raw+= (long)b << Precision;
            return a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator +(int a, FixedPoint b)
        {
            return b + a;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator -(FixedPoint a, FixedPoint b)
        {
            a.raw-=b.raw;
            return a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator -(FixedPoint a, int b)
        {
            a.raw-= (long)b << Precision;
            return a;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator -(int a, FixedPoint b)
        {
            return new FixedPoint(a) - b;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedPoint operator *(FixedPoint a, FixedPoint b)
        {
            
            
            return FixedPoint.One;
        }
        
        #endregion
                
    }
}

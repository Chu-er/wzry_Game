using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ChuFixedPoint
{
    [Serializable]
    public struct FixedPoint
    {
        public long raw;

        public const int Multiplier = 1 << Precision;//1 << 16  = 65536
        /// <summary>
        /// 小数的位数
        /// </summary>
        public const int Precision = 16;

        public const int TotalBitCount = 64;

        public const int IntBitCount = TotalBitCount - Precision;//48
        /// <summary>
        /// 小数掩码 2 ^ 16 - 1 = 65535
        /// </summary>
        public const long DecimalsMask = (long)(ulong.MaxValue >> IntBitCount);

        /// <summary>
        /// 整数部分掩码 -1L表示所有位都是1(补码表示法)
        /// </summary>
        public const long IntMask = ~DecimalsMask;
        /// <summary>
        /// 小数部分取值范围 0 ~ 65535 (共65536个档)  ,2^16 = 65536
        /// </summary>
        public const long DecimalsRange = DecimalsMask + 1;

        /// <summary>
        ///  long 的范围右移16位   去掉小数部分，只保留整数部分的范围
        /// </summary>
        public const long MinValue = long.MinValue >> Precision;
        public const long MaxValue = long.MaxValue >> Precision;

        public static FixedPoint Zero = new FixedPoint(0);
        public static FixedPoint One = new FixedPoint(1);
        public static FixedPoint Pi = new FixedPoint(3.1416f);
        public static FixedPoint Pi2 = Pi * 2;
        public static FixedPoint One_Div_Pi2 = One / Pi2;

        private FixedPoint(long raw)
        {
            this.raw = raw;
        }

        public FixedPoint(int v)
        {
            // 必须先升到 long：int * int 超过 ±32767 就会 32 位溢出
            raw = (long)v << Precision;// * Multiplier      
        }
        public FixedPoint(float v)
        {
            raw = (long)Math.Round(v * Multiplier);
        }


        public int ToInt()
        {
            return (int)((raw + (DecimalsRange >> 1)) >> Precision);
        }


        public float ToFloat()
        {
            return raw * 1f / Multiplier;
        }
        #region 运算符重载
        public static FixedPoint operator +(FixedPoint a, FixedPoint b)
        {
            a.raw += b.raw;
            return a;
        }

        public static FixedPoint operator +(FixedPoint a, int b)
        {
            a.raw += (long)b << Precision;
            return a;
        }

        public static FixedPoint operator +(int a, FixedPoint b)
        {
            return b + a;
        }


        public static FixedPoint operator -(FixedPoint a, FixedPoint b)
        {
            a.raw -= b.raw;
            return a;
        }

        public static FixedPoint operator -(FixedPoint a, int b)
        {
            a.raw -= (long)b << Precision;
            return a;
        }

        public static FixedPoint operator -(int a, FixedPoint b)
        {
            b.raw = ((long)a << Precision) - b.raw;
            return b;
        }


        public static FixedPoint operator *(FixedPoint a, FixedPoint b)
        {
            // a.raw * b.raw 两个已经放大2^16的数相乘,小数被放大了两次,结果相当于真实值的2^32倍
            //最后的右移16位等于除以65536,低16位被扔掉,扔掉之前加 32768(1/2)
            //例如:
            //100000 / 65536 ≈ 1.526
            // 100000 >> 16                    = 1      // 直接截断
            // (100000 + 32768) >> 16 = 132768 >> 16 = 2  // 更接近 1.526
            long raw = ((a.raw * b.raw) + (DecimalsRange >> 1)) >> Precision;
            return new FixedPoint(raw);
        }

        public static FixedPoint operator *(FixedPoint a, int b)
        {
            // int 没有小数位，直接乘 raw 即可
            a.raw *= b;
            return a;
        }

        public static FixedPoint operator *(int a, FixedPoint b)
        {
            return b * a;
        }

        /// <summary>
        /// 和乘法正好相反：乘法多乘了一次 2^16，要 >> 16；除法少乘了一次 2^16，要 << 16。
        /// </summary>
        public static FixedPoint operator /(FixedPoint a, FixedPoint b)
        {
            long result = ((a.raw << Precision)) / b.raw;
            return new FixedPoint(result);
        }


        public static FixedPoint operator >>(FixedPoint a, int b)
        {
            return new FixedPoint(a.raw >> b);
        }

        public static FixedPoint operator <<(FixedPoint a, int b)
        {
            return new FixedPoint(a.raw << b);
        }

        public static FixedPoint operator -(FixedPoint a)
        {
            return new FixedPoint(-a.raw);
        }

        //取模
        public static FixedPoint operator %(FixedPoint a, FixedPoint b)
        {
            return new FixedPoint(a.raw % b.raw);
        }

        public static FixedPoint operator %(FixedPoint a, int b)
        {
            a.raw %= (long)b << Precision;
            return a;
        }

        public static FixedPoint operator %(int a, FixedPoint b)
        {
            b.raw = ((long)a << Precision) % b.raw;
            return b;
        }

        #endregion

        #region 比较运算符重载
        public static bool operator ==(FixedPoint a, FixedPoint b)
        {
            return a.raw == b.raw;
        }

        public static bool operator !=(FixedPoint a, FixedPoint b)
        {
            return a.raw != b.raw;
        }

        public static bool operator >(FixedPoint a, FixedPoint b)
        {
            return a.raw > b.raw;
        }

        public static bool operator <(FixedPoint a, FixedPoint b)
        {
            return a.raw < b.raw;
        }

        public static bool operator >=(FixedPoint a, FixedPoint b)
        {
            return a.raw >= b.raw;
        }

        public static bool operator <=(FixedPoint a, FixedPoint b)
        {
            return a.raw <= b.raw;
        }
        #endregion

        #region 类型转换



        public static explicit operator int(FixedPoint a)
        {
            return a.ToInt();
        }
        public static implicit operator FixedPoint(int a)
        {
            return new FixedPoint(a);
        }

        public static explicit operator FixedPoint(float a)
        {
            return new FixedPoint(a);
        }

        
        public static explicit operator float(FixedPoint a)
        {
            return a.ToFloat();
        }

        public static explicit operator long(FixedPoint a) 
        {
            return a.raw;
        }

        public static explicit operator FixedPoint(long a)
        {
            return new FixedPoint(a);
        }
        #endregion


        #region override System.Object


        public override readonly bool Equals(object? obj)
        {
            return obj is FixedPoint fp && fp == this;
        }

        public override readonly int GetHashCode()
        {
            return raw.GetHashCode();
        }

        public override string ToString()
        {
            return ToFloat().ToString();
            // return $"{raw / Multiplier}.{raw % Multiplier:F4}";
        }

        #endregion
    }
}

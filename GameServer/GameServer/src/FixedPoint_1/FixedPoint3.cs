using System;

namespace ChuFixedPoint
{
    public struct FixedPoint3
    {
        public FixedPoint x;
        public FixedPoint y;
        public FixedPoint z;

        public static FixedPoint3 Zero = new FixedPoint3(0f, 0f, 0f);
        public static FixedPoint3 One = new FixedPoint3(1f, 1f, 1f);
        public static FixedPoint3 Half = One / (FixedPoint)2f;

        public static FixedPoint3 Up = new FixedPoint3(0f, 1f, 0f);
        public static FixedPoint3 Down = new FixedPoint3(0f, -1f, 0f);
        public static FixedPoint3 Left = new FixedPoint3(-1f, 0f, 0f);
        public static FixedPoint3 Right = new FixedPoint3(1f, 0f, 0f);
        public static FixedPoint3 Forward = new FixedPoint3(0f, 0f, 1f);
        public static FixedPoint3 Backward = new FixedPoint3(0f, 0f, -1f);


        public FixedPoint3(FixedPoint x, FixedPoint y, FixedPoint z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public FixedPoint3(float x, float y, float z)
        {
            this.x = (FixedPoint)x;
            this.y = (FixedPoint)y;
            this.z = (FixedPoint)z;
        }

        #region 向量操作

        public FixedPoint unsafeSqrMagnitude => x * x + y * y + z * z;
        public FixedPoint magnitude => FMath.Sqrt(unsafeSqrMagnitude);

        public FixedPoint3 normalized
        {
            get
            {
                FixedPoint length = magnitude;
                if (length == 0)
                {
                    return Zero;
                }
                FixedPoint reciprocal = 1 / length;
                return new FixedPoint3(x * reciprocal, y * reciprocal, z * reciprocal);
            }
        }

        public static FixedPoint Dot(FixedPoint3 a, FixedPoint3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static long DotLong(FixedPoint3 a, FixedPoint3 b)
        {
            long result = (long)a.x * (long)b.x + (long)a.y * (long)b.y + (long)a.z * (long)b.z;
            return result;
        }

        public static FixedPoint3 Cross(FixedPoint3 a, FixedPoint3 b)
        {
            return new FixedPoint3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static FixedPoint Distance(FixedPoint3 a, FixedPoint3 b)
        {
            FixedPoint3 diff = a - b;
            return diff.magnitude;
        }

        // public static float Angle(Vector3 from, Vector3 to)
        // {
        //     float num = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        //     if (num < 1E-15f)
        //     {
        //         return 0f;
        //     }
        //     float num2 = Mathf.Clamp(Dot(in from, in to) / num, -1f, 1f);
        //     return (float)Math.Acos(num2) * 57.29578f;
        // }
        
        /// <summary>
        /// 返回两个向量之间的角度，单位为弧度 TODO:
        /// </summary>
        public static Factor AngleFactor(FixedPoint3 a, FixedPoint3 b)
        {
            FixedPoint dot = Dot(a, b);
            FixedPoint magnitude = a.magnitude * b.magnitude;
            FixedPoint cos = dot / magnitude;
            return FMath.Acos(cos);
        }

        /// <summary>
        /// 返回两个向量之间的角度，单位为度 TODO:
        /// </summary>
        public static float Angle(FixedPoint3 a, FixedPoint3 b)
        {
            FixedPoint dot = Dot(a, b);
            FixedPoint magnitude = a.magnitude * b.magnitude;
            FixedPoint cos = dot / magnitude;
            return FMath.Acos(cos).ToDegrees();
        }
        public static FixedPoint UnsafeSqrMagnitude(FixedPoint3 a)
        {
            return a.unsafeSqrMagnitude;
        }


        public void Normalize()
        {
            FixedPoint length = magnitude;
            if (length == 0)
            {
                return;
            }
            FixedPoint reciprocal = 1 / length;
            x *= reciprocal;
            y *= reciprocal;
            z *= reciprocal;
        }


        #endregion

        #region 运算符重载
        public static FixedPoint3 operator +(FixedPoint3 a, FixedPoint3 b)
        {
            return new FixedPoint3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static FixedPoint3 operator -(FixedPoint3 a, FixedPoint3 b)
        {
            return new FixedPoint3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        //f3 * f3 
        public static FixedPoint3 operator *(FixedPoint3 a, FixedPoint3 b)
        {
            return new FixedPoint3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        //f3 * f
        public static FixedPoint3 operator *(FixedPoint3 a, FixedPoint b)
        {
            return new FixedPoint3(a.x * b, a.y * b, a.z * b);
        }

        public static FixedPoint3 operator /(FixedPoint3 a, FixedPoint3 b)
        {
            return new FixedPoint3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static FixedPoint3 operator /(FixedPoint3 a, FixedPoint b)
        {
            return new FixedPoint3(a.x / b, a.y / b, a.z / b);
        }

        // 取负
        public static FixedPoint3 operator -(FixedPoint3 a)
        {
            return new FixedPoint3(-a.x, -a.y, -a.z);
        }

        // // 取模
        // public static FixedPoint3 operator %(FixedPoint3 a, FixedPoint3 b)
        // {
        //     return new FixedPoint3(a.x % b.x, a.y % b.y, a.z % b.z);
        // }
        // // f3 % f
        // public static FixedPoint3 operator %(FixedPoint3 a, FixedPoint b)
        // {
        //     return new FixedPoint3(a.x % b, a.y % b, a.z % b);
        // }
        #endregion

        #region 比较运算符重载
        public static bool operator ==(FixedPoint3 a, FixedPoint3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(FixedPoint3 a, FixedPoint3 b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        #endregion

        #region Override System.Object

        public override bool Equals(object? obj)
        {
            if (obj is FixedPoint3 other)
            {
                return this == other;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
        #endregion
    }
}

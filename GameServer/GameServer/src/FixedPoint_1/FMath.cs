using System;

namespace ChuFixedPoint
{
    public static class FMath
    {

        // public static FixedPoint SqrtMagnitude(FixedPoint3 value)
        // {
        //     return Sqrt(value.unsafeSqrMagnitude);
        // }
        
        public static FixedPoint Sqrt(FixedPoint unsafeSqrMagnitude)
        {
            throw new Exception("Not implemented");
        }

        public static Factor Acos(FixedPoint cos)
        {
            throw new Exception("Not implemented");
        }

        public static FixedPoint Abs(FixedPoint value)
        {
            return value < FixedPoint.Zero ? -value : value;
        }
            

    }
}

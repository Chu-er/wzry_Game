using System;

namespace ChuFixedPoint
{
    [Serializable]
    public struct FixedPoint
    {
        public long raw;
        
        public const int Multiplier = 100000;
        
        private FixedPoint(long raw)
        {
            this.raw = raw;
        }

        public FixedPoint(int v)
        {
            raw = v * Multiplier;
        }
        
        
        
    }
}

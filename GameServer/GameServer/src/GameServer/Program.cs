using ChuFixedPoint;

namespace GameServer;
class Program
{
    static void Main(string[] args)
    {
        string maskStr =  Convert.ToString(FixedPoint.DecimalsMask, 2).PadLeft(64, '0');
        Console.WriteLine("掩码"+FixedPoint.DecimalsMask.ToString("B24").PadLeft(64,'0'));
        Console.WriteLine("掩码"+maskStr);
        Console.WriteLine("ulong.MaxValue 2进制:"+ulong.MaxValue.ToString("B").PadLeft(64,'0'));
        
        FixedPoint fp = new FixedPoint(1);//65536
        Console.WriteLine($"fp 2进制 =  {fp.raw.ToString("B").PadLeft(64,'0')} fp.raw = {fp.raw}");
        
        
        
        
    }
}

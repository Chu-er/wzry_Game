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

        int v1 = 777;        
        float v2 = 0.777f;
        FixedPoint f1 = new FixedPoint(v1);
        FixedPoint f2 = new FixedPoint(v2);
        Console.WriteLine($"f1 = {f1.ToFloat()} f2 = {f2.ToFloat()}");
        Console.WriteLine($"--------------------------------");

        Console.WriteLine($"运算加法 Source:{v1+v2}  FixedPoint:{(f1+f2).ToString()}");
        Console.WriteLine($"运算减法 Source:{v1 - v2}  FixedPoint:{(f1 - f2).ToString()}");
        Console.WriteLine($"运算乘法 Source:{v1 * v2}  FixedPoint:{(f1 * f2).ToString()}");
        Console.WriteLine($"运算除法 Source:{v1 / v2}  FixedPoint:{(f1 / f2).ToString()}");
        Console.WriteLine($"运算取负 Source:{-v1}  FixedPoint:{(-f1).ToString()}");
        Console.WriteLine($"运算取模 Source:{v1 % 2}  FixedPoint:{(f1 % 2).ToString()}");

        Console.WriteLine($"运算右移V1右移2位 Source:{v1 >> 2}  FixedPoint:{(f1 >> 2).ToString()}");
        Console.WriteLine($"运算左移V1左移2位 Source:{v1 << 2}  FixedPoint:{(f1 << 2).ToString()}");

        Console.WriteLine($"运算减法重载 Fixed - int Source:{v1 - 2}  FixedPoint:{(f1 - 2).ToString()}");
        Console.WriteLine($"运算减法重载 int - Fixed Source:{10 - v1}  FixedPoint:{(10 - f1).ToString()}");
        Console.WriteLine($"--------------------------------");
        Console.WriteLine($"运算相等比较 {f1 == new FixedPoint(777)} ");
        Console.WriteLine($"运算不相等比较 {f1 != new FixedPoint(777)} | {f1 != new FixedPoint(776)} | {f1 != f2} ");
        Console.WriteLine($"运算大于比较 {f1 > new FixedPoint(776)} | {f1 > new FixedPoint(777)} ");
        Console.WriteLine($"运算小于比较 {f1 < new FixedPoint(778)} | {f1 < new FixedPoint(777)} ");
    }
}

using ChuFixedPoint;
using Xunit.Abstractions;


namespace ChuFixedPoint.Tests;

public class FixedPointTests
{
    private readonly ITestOutputHelper _output;

    public FixedPointTests(ITestOutputHelper output)
    {
        _output = output;
    }


    [Fact]
    public void IntConstructor_ShiftsByPrecision()
    {
        var one = new FixedPoint(1);

        Assert.Equal(1 << FixedPoint.Precision, one.raw);
        Assert.Equal(65536, one.raw);

    }

    [Fact]
    public void Add_TwoFixedPoints()
    {
        var result = new FixedPoint(1) + new FixedPoint(2);

        Assert.Equal(new FixedPoint(3), result);
    }

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(1, 1, 1)]
    [InlineData(-2, 4, -8)]
    public void Multiply_Integers(int a, int b, int expected)
    {
        var result = new FixedPoint(a) * new FixedPoint(b);

        Assert.Equal(new FixedPoint(expected), result);
    }

    [Fact]
    public void Divide_OneByTwo_IsHalf()
    {
        var result = new FixedPoint(1) / new FixedPoint(2);

        Assert.Equal(0.5f, result.ToFloat());
    }

    [Fact]
    public void Compare_GreaterThan()
    {
        Assert.True(new FixedPoint(2) > new FixedPoint(1));
        Assert.False(new FixedPoint(1) > new FixedPoint(2));
    }

    [Theory]
    [InlineData(1.4f,8000)]
    [InlineData(199.9f,236546)]
    public void Test_类型转换(float input,int intInput)
    {
        FixedPoint fp = (FixedPoint)input;
        _output.WriteLine($"fp: {fp}");

        // Q16 步长 ≈ 0.000015，最多保证约 4 位小数；precision: 6 仍会失败
        float error = Math.Abs(input - fp.ToFloat());
        float maxError = 0.5f / FixedPoint.Multiplier;
        _output.WriteLine($"error={error}, maxError={maxError}");
        Assert.True(error <= maxError, $"误差 {error} 超过半个 Q16 格 {maxError}");

        FixedPoint fp2 = intInput;

        Assert.Equal(intInput, (int)fp2);
        Assert.Equal(intInput, fp2);

    }


}

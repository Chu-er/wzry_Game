using ChuFixedPoint;
using Xunit.Abstractions;
namespace ChuFixedPoint.Tests;

public class FixedPoint3Tests
{

    private readonly ITestOutputHelper _output;

    public FixedPoint3Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(1.0f, 2.0f, 3.0f)]
    [InlineData(.1f, .2f, .3f)]
    public void Test_FixedPoint3(float x, float y, float z)
    {
        var fp3 = new FixedPoint3(x, y, z);
        _output.WriteLine($"fp3: {fp3/3}");


        
        FixedPoint3 up = FixedPoint3.Up;
        FixedPoint3 down = FixedPoint3.Down;
        FixedPoint3 left = FixedPoint3.Left;
        FixedPoint3 right = FixedPoint3.Right;
        FixedPoint3 forward = FixedPoint3.Forward;
        FixedPoint3 backward = FixedPoint3.Backward;

        Assert.Equal(up.x, 0);
        Assert.Equal(up.y, 1);
        Assert.Equal(up.z, 0);


        Assert.True(right == FixedPoint3.Right);
        Assert.False(right != FixedPoint3.Right);

        // Assert.Equal(left.x, (FixedPoint)-1f);
        // Assert.Equal(left.y, (FixedPoint)0f);
        // Assert.Equal(x, fp3.x.ToFloat());


    }

    /// <summary>
    /// 测试FixedPoint3 的 长度 和 归一化
    /// </summary>
    [Theory]
    [InlineData(1.0f, 2.0f, 3.0f)]
    [InlineData(.1f, .2f, .3f)]
    public void Test_FixedPoint3_Length_And_Normalize(float x, float y, float z)
    {
        var fp3 = new FixedPoint3(x, y, z);
        float expectedSqrMagnitude = x * x + y * y + z * z;

        FixedPoint sqrMagnitude = fp3.unsafeSqrMagnitude;

        FixedPoint subtract = sqrMagnitude - (FixedPoint)expectedSqrMagnitude;
        FixedPoint abs = FMath.Abs(subtract);

        FixedPoint maxError = (FixedPoint)(3f/FixedPoint.Multiplier);
        _output.WriteLine($"F3SqrMagnitude: {sqrMagnitude} Expected: {expectedSqrMagnitude}");
        _output.WriteLine($"Subtract: {subtract} "+
            $"Abs:{abs} MaxError:{maxError}");

        // Assert.True(FMath.);

        Assert.True( abs <= maxError, $"SqrMagnitude: {sqrMagnitude} Expected: {expectedSqrMagnitude}");
    }



}

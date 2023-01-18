using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

[MemoryDiagnoser]
public class BenchClass
{
    readonly uint exampleValue = 100_000_123;

    [Benchmark]
    public void reverseBitsMath()
    {
        Solutions.reverseBitsMath(exampleValue);
    }

    [Benchmark]
    public void reverseBitsBitWise()
    {
        Solutions.reverseBitsBitWise(exampleValue);
    }

    [Benchmark]
    public void reverseBitsString()
    {
        Solutions.reverseBitsString(exampleValue);
    }
}

static class Solutions
{
    public static uint reverseBitsMath(uint n)
    {
        uint output = 0;
        uint currentNumber = n;
        for(int i = 0; i < 32; i++)
        {
            output *= 2;
            bool isOne = (currentNumber % 2) == 1;
            if(isOne) output += 1;
            currentNumber /= 2;
        }
        return output;
    }

    public static uint reverseBitsBitWise(uint n)
    {
        uint output = 0;
        for(int i = 0; i < 32; i++)
        {
            output <<= 1;
            bool isOne = ((n >> i) % 2) == 1;
            if(isOne) output += 1;
        }
        return output;
    }

    public static uint reverseBitsString(uint n)
    {
        var binaryRepresentation = Convert.ToString(n, 2).PadLeft(32, '0');
        var stack = new Stack<char>();
        foreach(char c in binaryRepresentation) stack.Push(c);
        var builder = new StringBuilder();
        while(stack.Count > 0) builder.Append(stack.Pop());
        var invertedBinaryRepresentation = builder.ToString();
        var output = Convert.ToUInt32(invertedBinaryRepresentation, 2);
        return output;
    }
}
using FluentAssertions;
using Xunit;

namespace Day18.Tests;

public class NumberOperationsShould
{
    [Fact]
    public void AddTwoNumbers1()
    {
        var num = new BranchNumber(new TipNumber(1), new TipNumber(1));
        var result = new BranchNumber(num, num);
        num.Add(num).Should().Be(result);
    }

    [Fact]
    public void AddTwoNumbers2()
    {
        var first = NumberParser.ParseNumber("[[[[4,3],4],4],[7,[[8,4],9]]]");
        var second = NumberParser.ParseNumber("[1,1]");
        var result = NumberParser.ParseNumber("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
        first.Add(second).Should().Be(result);
    }
}
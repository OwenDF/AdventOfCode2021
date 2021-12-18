using System.Diagnostics.CodeAnalysis;

namespace Day18;

public static class NumberOperations
{
    public static Number Add(this Number left, Number right)
        => new BranchNumber(left, right).Reduce();

    public static BranchNumber Reduce(this BranchNumber num)
    {
        while (true)
        {
            var next = (BranchNumber)Explode(num, 1).result;
            if (next == num)
            {
                next = (BranchNumber)num.Split();
                if (next == num) return next;
            }

            num = next;
        }
    }

    public static (Number result, (int l, int r) toExplode) Explode(this BranchNumber num, int currentDepth)
    {
        if (currentDepth is 5) return (new TipNumber(0), (((TipNumber)num.Left).Value, ((TipNumber)num.Right).Value));

        if (num.Left is BranchNumber lbn)
        {
            var (result, toExplode) = Explode(lbn, currentDepth + 1);
            if (result != num.Left)
            {
                var addToRight = num.Right.AddToLeft(toExplode.r);
                return addToRight != num.Right
                    ? (num with { Left = result, Right = addToRight }, (toExplode.l, 0))
                    : (num with { Left = result }, toExplode);
            }
        }

        if (num.Right is BranchNumber rbn)
        {
            var (result, toExplode) = Explode(rbn, currentDepth + 1);
            if (result != num.Right)
            {
                var addToLeft = num.Left.AddToRight(toExplode.l);
                return addToLeft != num.Left
                    ? (num with { Right = result, Left = addToLeft}, (0, toExplode.r))
                    : (num with { Right = result }, toExplode);
            }
        }

        return (num, (0, 0));
    }

    public static Number Split(this Number num) => num switch
    {
        TipNumber { Value: > 9 } x => x.Split(),
        BranchNumber bn => bn.Split(),
        _ => num
    };
    
    public static BranchNumber Split(this TipNumber tip)
        => new(new TipNumber(tip.Value / 2), new TipNumber((tip.Value + 1) / 2));

    public static Number Split(this BranchNumber num)
    {
        var splitLeft = num.Left.Split();
        if (splitLeft != num.Left) return num with { Left = splitLeft };
        var splitRight = num.Right.Split();
        if (splitRight != num.Right) return num with { Right = splitRight };
        return num;
    }

    public static Number AddToRight(this Number num, int toAdd)
    {
        switch (num)
        {
            case TipNumber tn:
                return tn with { Value = tn.Value + toAdd };
            case BranchNumber bn:
            {
                var right = bn.Right.AddToRight(toAdd);
                if (bn.Right != right) return bn with { Right = right };
                return bn with { Left = bn.Left.AddToRight(toAdd) };
            }
            default:
                throw new Exception();
        }
    }
    
    public static Number AddToLeft(this Number num, int toAdd)
    {
        switch (num)
        {
            case TipNumber tn:
                return tn with { Value = tn.Value + toAdd };
            case BranchNumber bn:
            {
                var left = bn.Left.AddToLeft(toAdd);
                if (bn.Left != left) return bn with { Left = left };
                return bn with { Right = bn.Right.AddToLeft(toAdd) };
            }
            default:
                throw new Exception();
        }
    }
}
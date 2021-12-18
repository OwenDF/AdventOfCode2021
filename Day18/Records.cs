namespace Day18;

public interface Number {}

public record TipNumber(int Value) : Number
{
    public override string ToString() => Value.ToString();
}

public record BranchNumber(Number Left, Number Right) : Number
{
    public override string ToString() => $"[{Left.ToString()},{Right.ToString()}]";
}
namespace Day16;

public interface Packet
{
    int Version { get; }
    long Value { get; }
}

public interface OperatorPacket : Packet
{
    Packet[] Subpackets { get; }
}

public record LiteralPacket(int Version, long Value) : Packet;

public record SumPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets.Sum(x => x.Value);
}

public record ProductPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets.Aggregate(1L, (c, n) => c * n.Value);
}

public record MinimumPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets.Min(x => x.Value);
}

public record MaximumPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets.Max(x => x.Value);
}

public record GreaterThanPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets[0].Value > Subpackets[1].Value ? 1 : 0;
}

public record LessThanPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets[0].Value < Subpackets[1].Value ? 1 : 0;
}

public record EqualToPacket(int Version, Packet[] Subpackets) : OperatorPacket
{
    public long Value => Subpackets[0].Value == Subpackets[1].Value ? 1 : 0;
}
namespace Day16;

internal static class Extensions
{
    public static int SumVersionNumber(this Packet packet) => packet switch
    {
        LiteralPacket lp => lp.Version,
        OperatorPacket op => op.Version + op.Subpackets.Sum(SumVersionNumber)
    };
}
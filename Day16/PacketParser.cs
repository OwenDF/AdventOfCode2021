using System.Text;

namespace Day16;

public interface Packet
{
    int Version { get; }
}

public record LiteralPacket(int Version, long Value) : Packet;

public record OperatorPacket(int Version, Packet[] Subpackets) : Packet;

public static class PacketParser
{
    public static Packet ReadPacketMessage(string message) => ParsePacket(message.ToCharArray()).packet;
    
    private static (Packet packet, ReadOnlyMemory<char> remaining) ParsePacket(ReadOnlyMemory<char> sequence)
    {
        var version = Convert.ToInt32(new string(sequence.Slice(0, 3).Span), 2);
        var type = Convert.ToInt32(new string(sequence.Slice(3, 3).Span), 2);

        switch (type)
        {
            case 4:
                var value = ParseValue(sequence.Slice(6));
                return (new LiteralPacket(version, value.value), value.remaining);
            default:
                var subpackets = CreateOperatorSubpackets(sequence.Slice(6));
                return (new OperatorPacket(version, subpackets.packets), subpackets.remaining);
        }
    }

    private static (long value, ReadOnlyMemory<char> remaining) ParseValue(ReadOnlyMemory<char> sequence)
    {
        var intString = new StringBuilder();
        var remaining = sequence;
        while (true)
        {
            intString.Append(remaining.Slice(1, 4));
            if (remaining.Span[0] == '0') break;
            remaining = remaining.Slice(5);
        }
        
        return (Convert.ToInt64(intString.ToString(), 2), remaining.Slice(5));
    }

    private static (Packet[] packets, ReadOnlyMemory<char> remaining) CreateOperatorSubpackets(
        ReadOnlyMemory<char> sequence)
        => sequence.Span[0] switch
        {
            '0' => ParseTotalLengthOperatorPackets(sequence.Slice(1)),
            '1' => ParseTotalPacketsOperatorPackets(sequence.Slice(1))
        };

    private static (Packet[] packets, ReadOnlyMemory<char> remaining) ParseTotalLengthOperatorPackets(
        ReadOnlyMemory<char> sequence)
    {
        var length = Convert.ToInt32(new string(sequence.Slice(0, 15).Span), 2);
        var packets = new List<Packet>();
        var remaining = sequence.Slice(15);
        var startLength = remaining.Length;
        var lengthUsed = 0;
        while (lengthUsed < length)
        {
            var result = ParsePacket(remaining);
            packets.Add(result.packet);
            lengthUsed = startLength - result.remaining.Length;
            remaining = result.remaining;
        }

        return (packets.ToArray(), remaining);
    }

    private static (Packet[] packets, ReadOnlyMemory<char> remaining) ParseTotalPacketsOperatorPackets(
        ReadOnlyMemory<char> sequence)
    {
        var length = Convert.ToInt32(new string(sequence.Slice(0, 11).Span), 2);
        var packets = new Packet[length];
        var remaining = sequence.Slice(11);
        for (var i = 0; i < length; i++)
        {
            var result = ParsePacket(remaining);
            packets[i] = result.packet;
            remaining = result.remaining;
        }

        return (packets, remaining);
    }
}
using FluentAssertions;
using Xunit;

namespace Day16.Tests;

public class PacketParserShould
{
    [Fact]
    public void ParseSingleLiteralPacket()
    {
        var packet = PacketParser.ReadPacketMessage("110100101111111000101000");
        packet.Version.Should().Be(6);
        packet.Should().BeOfType<LiteralPacket>();
        ((LiteralPacket)packet).Value.Should().Be(2021);
    }
    
    [Fact]
    public void ParseSingleLiteralPacket2()
    {
        var packet = PacketParser.ReadPacketMessage("11010001010");
        packet.Should().BeOfType<LiteralPacket>();
        ((LiteralPacket)packet).Value.Should().Be(10);
    }
    
    [Fact]
    public void ParseSingleLiteralPacket3()
    {
        var packet = PacketParser.ReadPacketMessage("0101001000100100");
        packet.Should().BeOfType<LiteralPacket>();
        ((LiteralPacket)packet).Value.Should().Be(20);
    }
    

    [Fact]
    public void ParseFixedWidthOperator()
    {
        var packet = PacketParser.ReadPacketMessage("00111000000000000110111101000101001010010001001000000000");
        packet.Version.Should().Be(1);
        packet.Should().BeOfType<OperatorPacket>();
        var opPacket = (OperatorPacket)packet;
        opPacket.Subpackets.Should().HaveCount(2);
        opPacket.Subpackets[0].Should().BeOfType<LiteralPacket>().Which.Value.Should().Be(10);
        opPacket.Subpackets[1].Should().BeOfType<LiteralPacket>().Which.Value.Should().Be(20);
    }

    [Fact]
    public void ParseDefinedPacketNumberOperator()
    {
        var packet = PacketParser.ReadPacketMessage("11101110000000001101010000001100100000100011000001100000");
        packet.Version.Should().Be(7);
        packet.Should().BeOfType<OperatorPacket>();
        var opPacket = (OperatorPacket)packet;
        opPacket.Subpackets.Should().HaveCount(3);
        opPacket.Subpackets[0].Should().BeOfType<LiteralPacket>().Which.Value.Should().Be(1);
        opPacket.Subpackets[1].Should().BeOfType<LiteralPacket>().Which.Value.Should().Be(2);
        opPacket.Subpackets[2].Should().BeOfType<LiteralPacket>().Which.Value.Should().Be(3);
    }
}
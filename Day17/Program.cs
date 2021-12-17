Area targetArea = new(new(156, 202), new(-110, -69));

var highestY = int.MinValue;
var count = 0;

int lowerXBound;
var lowerXBoundList = new List<int> { 0 };

for (var i = 1;; i++)
{
    lowerXBoundList.Add(i + lowerXBoundList[i - 1]);
    if (lowerXBoundList[i] >= targetArea.X.Lower)
    {
        lowerXBound = i;
        break;
    }
}

for (var xVector = targetArea.X.Upper + 1; xVector >= lowerXBound; xVector--)
for (var yVector = targetArea.Y.Lower - 1; yVector < 200; yVector++)
{
    var path = Fire(new(xVector, yVector), targetArea).ToList();
    if (path.Any(x => IsInArea(targetArea, x)))
    {
        count++;
        highestY = Math.Max(highestY, path.Max(x => x.Y));
    }
}

Console.WriteLine(highestY);
Console.WriteLine(count);

IEnumerable<Position> Fire(Vector initialVector, Area targetArea)
{
    var currentPosition = new Position(0, 0);
    yield return currentPosition;

    foreach (var vector in YieldDegradingVectors(initialVector))
    {
        currentPosition = Move(currentPosition, vector);
        yield return currentPosition;
        if (currentPosition.X > targetArea.X.Upper || currentPosition.Y < targetArea.Y.Lower)
            yield break;
    }
}

IEnumerable<Vector> YieldDegradingVectors(Vector initial)
{
    var current = initial;
    while (true)
    {
        current = Degrade(current);
        yield return current;
    }
}

Position Move(Position initial, Vector delta) => new(initial.X + delta.X, initial.Y + delta.Y);

Vector Degrade(Vector initial)
    => new(initial.X switch { > 0 => initial.X - 1, < 0 => initial.X + 1, 0 => 0 }, initial.Y - 1);

bool IsInRange(InclusiveRange range, int position) => position >= range.Lower && position <= range.Upper;
bool IsInArea(Area area, Position position) => IsInRange(area.X, position.X) && IsInRange(area.Y, position.Y);

internal readonly record struct Vector(int X, int Y);
internal readonly record struct Position(int X, int Y);
internal readonly record struct InclusiveRange(int Lower, int Upper);
internal readonly record struct Area(InclusiveRange X, InclusiveRange Y);

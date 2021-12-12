var connections = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.Split('-'))
    .Select(ToConnection)
    .ToList();

var caveType = new Dictionary<string, CaveType>();
var caves = new Dictionary<string, HashSet<string>>();

foreach (var connection in connections)
{
    if (caves.ContainsKey(connection.First.Name))
    {
        var set = caves[connection.First.Name];
        set.Add(connection.Second.Name);
    }
    else caves[connection.First.Name] = new HashSet<string> { connection.Second.Name };
    
    if (caves.ContainsKey(connection.Second.Name))
    {
        var set = caves[connection.Second.Name];
        set.Add(connection.First.Name);
    }
    else caves[connection.Second.Name] = new HashSet<string> { connection.First.Name };

    if (!caveType.ContainsKey(connection.First.Name))
        caveType[connection.First.Name] = connection.First.Name.ToUpper() == connection.First.Name ?
            CaveType.Large :
            CaveType.Small;
    
    if (!caveType.ContainsKey(connection.Second.Name))
        caveType[connection.Second.Name] = connection.Second.Name.ToUpper() == connection.Second.Name ?
            CaveType.Large :
            CaveType.Small;
}

var routes = CalculateRoutes("start", new HashSet<string>(), false);

int CalculateRoutes(string currentCave, HashSet<string> visitedSmallCaves, bool doubleVisit)
{
    if (visitedSmallCaves.Contains(currentCave))
    {
        if (!doubleVisit && currentCave != "start")
        {
            doubleVisit = true;
        }
        else return 0;
    }

    if (currentCave == "end") return 1;

    if (caveType[currentCave] == CaveType.Small)
    {
        visitedSmallCaves = new HashSet<string>(visitedSmallCaves) { currentCave };
    }

    return caves[currentCave].Sum(x => CalculateRoutes(x, visitedSmallCaves, doubleVisit));
}

Console.WriteLine(routes);

Connection ToConnection(string[] arr)
    => new(
        arr[0].ToUpper() == arr[0] ? new BigCaveName(arr[0]) : new SmallCaveName(arr[0]),
        arr[1].ToUpper() == arr[1] ? new BigCaveName(arr[1]) : new SmallCaveName(arr[1]));

public interface ICaveName
{
    public string Name { get; }
}

public enum CaveType { Undefined, Small, Large }
public record BigCaveName(string Name) : ICaveName;
public record SmallCaveName(string Name) : ICaveName;
public record Connection(ICaveName First, ICaveName Second);

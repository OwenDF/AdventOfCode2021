var connections = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.Split('-'))
    .Select(ToConnection)
    .ToList();

var caveType = new Dictionary<string, CaveType>();
var caves = new Dictionary<string, HashSet<string>>();

foreach (var connection in connections)
{
    if (caves.ContainsKey(connection.First))
    {
        var set = caves[connection.First];
        set.Add(connection.Second);
    }
    else caves[connection.First] = new HashSet<string> { connection.Second };
    
    if (caves.ContainsKey(connection.Second))
    {
        var set = caves[connection.Second];
        set.Add(connection.First);
    }
    else caves[connection.Second] = new HashSet<string> { connection.First };

    if (!caveType.ContainsKey(connection.First))
        caveType[connection.First] = connection.First.ToUpper() == connection.First ?
            CaveType.Large :
            CaveType.Small;
    
    if (!caveType.ContainsKey(connection.Second))
        caveType[connection.Second] = connection.Second.ToUpper() == connection.Second ?
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

Connection ToConnection(string[] arr) => new(arr[0], arr[1]);

public enum CaveType { Undefined, Small, Large }
public record Connection(string First, string Second);

string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

int[][] map = new int[data.Length][];
for (int i = 0; i < data.Length; ++i)
{
    map[i] = data[i].Select(c => int.Parse(c.ToString())).ToArray();
}

List<int> basins = new List<int>();
int risk = 0;
for (int i = 0; i < map.Length; ++i)
{
    for (int j = 0; j < map[i].Length; ++j)
    {
        if ((i == 0 || map[i][j] < map[i-1][j]) && 
            (j == 0 || map[i][j] < map[i][j-1]) &&
            (i == map.Length - 1 || map[i][j] < map[i+1][j]) &&
            (j == map[i].Length - 1 || map[i][j] < map[i][j+1]))
        {
            risk += map[i][j] + 1;
            basins.Add(GetBasin(map, i, j));
        }
    }
}
basins.OrderByDescending(b => b).Where((b, i) => i <=2)
    .Aggregate(1, (total, b) => total * b)
    .Dump();

int GetBasin(int[][] map, int i, int j)
{
    bool[,] marked = new bool[map.Length, map[0].Length];
    return GetBasinPoints(map, i, j, marked).Count();
}

IEnumerable<(int I, int J)> GetBasinPoints(int[][] map, int i, int j, bool[,] marked)
{
    if (map[i][j] == 9) yield break;
    yield return (i, j);
    marked[i,j] = true;
    
    if (i != 0 && !marked[i-1, j])
        foreach (var point in GetBasinPoints(map, i-1, j, marked))
            yield return point;
    if (j != 0 && !marked[i, j-1])
        foreach (var point in GetBasinPoints(map, i, j-1, marked))
            yield return point;
    if (i != map.Length - 1 && !marked[i+1, j])
        foreach (var point in GetBasinPoints(map, i+1, j, marked))
            yield return point;
    if (j != map[i].Length - 1 && !marked[i, j+1])
        foreach (var point in GetBasinPoints(map, i, j+1, marked))
            yield return point;            
}
string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

List<string> caves = new List<string>();
List<(int, int)> edgeList = new List<(int, int)>();
foreach (string line in data)
{
    string[] parts = line.Split('-');
    int c1 = AddCave(parts[0], caves);
    int c2 = AddCave(parts[1], caves);
    edgeList.Add((c1, c2));
}

int N = caves.Count;
List<int>[] adjList = new List<int>[N];
foreach (var (c1, c2) in edgeList)
{
    if (adjList[c1] == null)
        adjList[c1] = new List<int>();
    adjList[c1].Add(c2);
    if (adjList[c2] == null)
        adjList[c2] = new List<int>();
    adjList[c2].Add(c1);
}
int start = caves.IndexOf("start"), end = caves.IndexOf("end");
HashSet<int> big = new HashSet<int>();
for (int i = 0; i < N; ++i)
{
    if (char.IsUpper(caves[i][0]))
        big.Add(i);
}
int paths = 0;
foreach (var path in BFS(adjList, start, end, big))
{
    //Console.WriteLine(string.Join(",", path.Select(v => caves[v])));
    paths++;
}
Console.WriteLine(paths);

IEnumerable<IEnumerable<int>> BFS(List<int>[] graph, int start, int end, HashSet<int> big)
{
    Queue<List<int>> q = new Queue<List<int>>();
    q.Enqueue(new List<int> { start });
    while (q.Count != 0)
    {
        List<int> path = q.Dequeue();
        int v = path[^1];
        if (v == end)
        {
            yield return path;
            continue;
        }
        if (adjList[v] == null) continue;
        foreach (int w in adjList[v])
        {
            var smallCaves = path.Where(c => !big.Contains(c));
            if (w != start && (
                big.Contains(w) || !path.Contains(w) || smallCaves.Count() == smallCaves.Distinct().Count())
                )
            {
                var newPath = new List<int>(path){ w };
                q.Enqueue(newPath);
            }
        }
    }
}


int AddCave(string cave, List<string> caves)
{
    int c = caves.IndexOf(cave);
    if (c == -1)
    {
        c = caves.Count;
        caves.Add(cave);
    }
    return c;
}
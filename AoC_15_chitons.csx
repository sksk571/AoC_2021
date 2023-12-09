var data = File.ReadAllLines(@"c:\temp\input_ex.txt");

int[,] map = new int[data.Length, data[0].Length];
for (int i = 0; i < data.Length; ++i)
{
    for (int j = 0; j < data[i].Length; ++j)
        map[i,j] = int.Parse(data[i][j].ToString());
}

HashSet<int> q = new HashSet<int>();

int N = (riskMap.Length - 1) * (riskMap[0].Length - 1);
int[] dist = new int[N];
int[] prev = new int[N];

for (int v = 0; v <= N; ++v)
{
    dist[v] = int.MaxValue;
    prev[v] = -1;
    q.Add(v);
}

int source = 0;
dist[source] = 0;

while (q.Count != 0)
{
    int u = q.First(v => dist[v] == dist.Min());
    q.Remove(u);
}




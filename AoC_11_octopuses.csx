string[] data = File.ReadAllLines("c:\\temp\\input.txt");
int N = 10;

int[][] grid = data.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

int total = 0;
for (int step = 1; step <= 500; ++step)
{
    int flashes = PerformStep(grid);
    if (flashes == 100)
        Console.WriteLine(step);
    total += flashes;
}

Console.WriteLine(total);

int PerformStep(int[][] grid)
{
    bool[,] flashed = new bool[N, N];
    for (int i = 0; i < N; ++i)
    {
        for (int j = 0; j < N; ++j)
        {
            Inc(grid, i, j, flashed);
        }
    }
    return flashed.OfType<bool>().Count(b => b);
}

void Inc(int[][] grid, int i, int j, bool[,] flashed)
{
    if (i < 0 || j < 0 || i >= N || j >= N || flashed[i,j])
        return;
        
    grid[i][j]++;
    if (grid[i][j] > 9)
    {
        grid[i][j] = 0;
        flashed[i,j] = true;
        Inc(grid, i-1, j-1, flashed);
        Inc(grid, i-1, j, flashed);
        Inc(grid, i-1, j+1, flashed);
        Inc(grid, i, j-1, flashed);
        Inc(grid, i, j+1, flashed);
        Inc(grid, i+1, j-1, flashed);
        Inc(grid, i+1, j, flashed);
        Inc(grid, i+1, j+1, flashed);
    }
   
}
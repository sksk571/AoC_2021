string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

long [] fish = new long[9];
foreach (var g in data[0].Split(',').Select(int.Parse).GroupBy(d => d))
{
    fish[g.Key] = g.Count();
}

int days = 256;
while (--days >= 0)
{
    long newborn = fish[0];
    
    for (int i = 1; i <= 8; ++i)
    {
        fish[i-1] = fish[i];
    }
    fish[6] += fish[8] = newborn;
}
Console.WriteLine(fish.Sum());
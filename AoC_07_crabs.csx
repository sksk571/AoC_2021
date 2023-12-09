string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

int[] positions = data[0].Split(',').Select(int.Parse).ToArray();
long min = long.MaxValue;
for (int pos = positions.Max(); pos >= positions.Min(); --pos)
{
    long total = 0;
    for (int i = 0; i < positions.Length; ++i)
    {
        int tmp = Math.Abs(positions[i] - pos);
        total += (1 + tmp) * tmp / 2;
    }
    if (total < min)
    {
        min = total;
    }
}
Console.WriteLine(min);
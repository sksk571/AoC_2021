using System.Diagnostics;

var data = File.ReadAllLines("c:\\temp\\input.txt");

int index = 0;
HashSet<(int, int)> field = new HashSet<(int, int)>();
foreach (string line in data)
{
    if (!string.IsNullOrEmpty(line))
    {
        if (!line.StartsWith("fold along"))
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            field.Add((coords[0], coords[1]));
        }
        else
        {
            File.WriteAllText($"c:\\temp\\result{index++}.pbm", Draw(field));
            int arg = int.Parse(line.Split('=')[1]);
            field = Fold(field, line[11], arg);
        }
    }
}

File.WriteAllText("c:\\temp\\result999.pbm", Draw(field));

HashSet<(int X, int Y)> Fold(HashSet<(int X, int Y)> field, char axis, int position)
{
    HashSet<(int X, int Y)> result = new HashSet<(int X, int Y)>();
    
    foreach ((int x, int y) in field)
    {
        if (axis == 'x' && x > position)
        {
            result.Add((x - (x - position) * 2, y));
        }
        else if (axis == 'y' && y > position)
        {
            result.Add((x, y - (y - position) * 2));
        }
        else result.Add((x, y));
    }
    
    return result;
}

string Draw(HashSet<(int X, int Y)> field)
{
    StringBuilder sb = new StringBuilder();
    int maxX = field.Max(c => c.X);
    int maxY = field.Max(c => c.Y);
    
    sb.AppendLine($"P1");
    sb.AppendLine($"{maxX+1}, {maxY+1}");
    
    for (int y = 0; y <= maxY; ++y)
    {
        for (int x = 0; x <= maxX; ++x)
            sb.Append(field.Contains((x, y)) ? "1 " : "0 ");
        sb.AppendLine();
    }
    return sb.ToString();
}
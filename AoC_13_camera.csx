string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

List<int[]> points = new List<int[]>();
List<Func<bool[,], bool[,]>> folds = new List<Func<bool[,], bool[,]>>();
bool instr = false;
foreach (string line in data)
{
    if (string.IsNullOrEmpty(line))
    {
        instr = true;
    }
    if (!instr)
    {
        int[] point = line.Split(',').Select(int.Parse).ToArray();
        points.Add(point);
    }
    else if (line.StartsWith("fold along x="))
        folds.Add(field => FoldAlongX(field, int.Parse(line.Substring(13, line.Length - 13))));
    else if (line.StartsWith("fold along y="))
        folds.Add(field => FoldAlongY(field, int.Parse(line.Substring(13, line.Length - 13))));
        
}
int maxX = points.Select(p => p[0]).Max();
int maxY = points.Select(p => p[1]).Max();

bool [,] field = new bool[maxX+1, maxY+1];
foreach (var point in points)
{
    field[point[0], point[1]] = true;
}

var f = field;
foreach (var fold in folds)
{
    f = fold(f);
}
Display(f);
//f.OfType<bool>().Count(c => c).Dump();

bool[,] FoldAlongX(bool[,] field, int foldX)
{
    int xLength = field.GetLength(0);
    int yLength = field.GetLength(1);
    bool[,] result = new bool[xLength/2,yLength];
        
    for (int x = 0; x < xLength; ++x)
    {
        if (x == foldX) continue;
        int newX = x < foldX ? x : xLength - x - 1;
        
        for (int y = 0; y < yLength; ++y)
        {
            if (field[x,y])
                result[newX, y] = true;
        }
    }
    return result;
}
bool[,] FoldAlongY(bool[,] field, int foldY)
{
    int xLength = field.GetLength(0);
    int yLength = field.GetLength(1);
    bool[,] result = new bool[xLength,yLength/2];
    
    for (int x = 0; x < xLength; ++x)
    {       
        for (int y = 0; y < yLength; ++y)
        {
            if (y == foldY) continue;
            int newY = y < foldY ? y : yLength - y - 1;
            
            if (field[x,y])
                result[x, newY] = true;
        }
    }
    return result;
}

void Display(bool[,] field)
{
    StringBuilder sb = new StringBuilder();
    for (int y = 0; y < field.GetLength(1); ++y)
    {
        for (int x = 0; x < field.GetLength(0); ++x)
        {
            sb.Append(field[x,y] ? " #" : " .");
        }
        sb.AppendLine();
    }
    Console.WriteLine(sb.ToString());
}
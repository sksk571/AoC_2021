string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

int score = 0;
List<long> completionScores = new List<long>();
foreach (var line in data)
{
    var (illegal, openingBrackets) = MatchBrackets(line);
    if (illegal != -1)
        score += line[illegal] switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => throw new InvalidOperationException()
        };
    else
    {
        long total = 0;
        string completion = Complete(openingBrackets);
        foreach (char c in completion)
        {
            total = total * 5 + c switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => throw new InvalidOperationException()
            };
        }
        completionScores.Add(total);
    }
}
completionScores.Dump();
completionScores.Sort();
Console.WriteLine(completionScores[completionScores.Count / 2]);

(int, Stack<char>) MatchBrackets(string line)
{
    Stack<char> openingBrackets = new Stack<char>();
    for (int i = 0; i < line.Length; ++i)
    {
        char c = line[i];
        if (c == '(' || c == '[' || c == '{' || c == '<')
            openingBrackets.Push(c);
        else
        {
            if (openingBrackets.Count == 0) return (i, openingBrackets);
            char cc = openingBrackets.Pop();
            if (cc == '(' && c != ')' || 
                cc == '[' && c != ']' ||
                cc == '{' && c != '}' ||
                cc == '<' && c != '>')
                return (i, openingBrackets);
        }
    }
    return (-1, openingBrackets);
}

string Complete(Stack<char> openingBrackets)
{
    StringBuilder sb = new StringBuilder();
    while (openingBrackets.Count != 0)
    {
        char c = openingBrackets.Pop();
        sb.Append(c switch 
        {
            '(' => ')',
            '[' => ']',
            '{' => '}',
            '<' => '>',
            _ => throw new InvalidOperationException()
        });
    }
    return sb.ToString();
}
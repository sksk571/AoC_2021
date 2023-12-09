using System.Collections;

var data = File.ReadAllLines("c:\\temp\\input.txt");

Dictionary<(char L, char R), char> rules = new Dictionary<(char L, char R), char>();

for (int i = 2; i < data.Length; ++i)
{
    rules.Add((data[i][0], data[i][1]), data[i][6]);
}
Stats.SetRules(rules);

string template = data[0];
var stats = Stats.ProcessPair((template[0], template[1]));
for (int i = 2; i < template.Length; ++i)
{
    stats = stats.Combine(Stats.ProcessPair((template[i-1], template[i])));
}
Console.WriteLine(stats.Max(s => s) - stats.Min(s => s));

class Stats : IEnumerable<long>
{
    const int _maxDepth = 40;
    static Dictionary<(char L, char R), char> _rules = new Dictionary<(char L, char R), char>();
    static readonly Dictionary<(char L, char R, int Depth), Stats> _cache = new Dictionary<(char L, char R, int Depth), Stats>();
    readonly char _l, _r;
    Dictionary<char, long> _inner;
    
    public Stats(char l, char r)
    {
        _inner = r != l ? new Dictionary<char, long> { {l, 1L}, {r, 1L} } : new Dictionary<char, long> { {l, 2L} };
        _l = l;
        _r = r;
    }
    
    public static void SetRules(Dictionary<(char L, char R), char> rules)
    {
        _rules = rules;
    }
    
    public static Stats ProcessPair((char L, char R) pair, int depth = 0)
    {
        if (_cache.TryGetValue((pair.L, pair.R, depth), out var result))
            return result;
            
        if (depth == _maxDepth || !_rules.TryGetValue(pair, out char insert))
            return new Stats(pair.L, pair.R);
            
        var stats1 = ProcessPair((pair.L, insert), depth + 1);
        var stats2 = ProcessPair((insert, pair.R), depth + 1);
        result = stats1.Combine(stats2);
        
        _cache.Add((pair.L, pair.R, depth), result);
        return result;
    }
    
    public Stats Combine(Stats stats2)
    {
        Dictionary<char, long> result = new Dictionary<char, long>();
        foreach (char key in _inner.Keys.Union(stats2._inner.Keys))
        {
            _inner.TryGetValue(key, out long c1);
            stats2._inner.TryGetValue(key, out long c2);
            result.Add(key, c1 + c2);
        }
        result[_r]--;
        return new Stats(_l, stats2._r) { _inner = result };
    }

    public IEnumerator<long> GetEnumerator()
    {
        return _inner.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
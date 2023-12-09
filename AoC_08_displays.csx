string[] data = File.ReadAllLines("c:\\temp\\input.txt").ToArray();

int r = 0;
foreach (string line in data)
{
    string[] parts = line.Split('|');
    string[] examples = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] digits = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var mapping = GetMapping(examples);
    int number = GetNumber(digits, mapping);
    r += number;
}
r.Dump();

Dictionary<string, int> GetMapping(string[] examples)
{
    string one = examples.Single(d => d.Length == 2);
    string four = examples.Single(d => d.Length == 4);
    string seven = examples.Single(d => d.Length == 3);
    string eight = examples.Single(d => d.Length == 7);
    string nine = examples.Single(d => d != eight && d != four && d.Intersect(four).Count() == 4);
    string two = examples.Single(d => d.Except(nine).Count() == 1 && d.Length == 5);
    string three = examples.Single(d => d.Except(seven).Count() == 2 && d != four);
    string five = examples.Single(d => d.Intersect(one).Count() == 1 && d.Length == 5 && d != two);
    string six = examples.Single(d => d.Except(five).Count() == 1 && d.Length == 6 && d != nine);
    string zero = examples.Single(d => !new[] { one, two, three, four, five, six, seven, eight, nine }.Contains(d));

    var r = new Dictionary<string, int>();
    Add(r, one, 1);
    Add(r, two, 2);
    Add(r, three, 3);
    Add(r, four, 4);
    Add(r, five, 5);
    Add(r, six, 6);
    Add(r, seven, 7);
    Add(r, eight, 8);
    Add(r, nine, 9);
    Add(r, zero, 0);
    return r;
}

void Add(Dictionary<string, int> mapping, string digit, int value)
{
    mapping.Add(string.Join("", digit.OrderBy(d => d)), value);
}

int GetNumber(string[] digits, Dictionary<string, int> mapping)
{
    int r = 0;
    foreach (string digit in digits)
    {
        r *= 10;
        r += mapping[string.Join("", digit.OrderBy(d => d))];
    }
    return r;
}
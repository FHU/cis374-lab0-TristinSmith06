
// [ "ryan", "beau", "caleb", "rye", 
// "beautiful", "cale", "cephas", "rhino", "cervid", "cecily"
// "ethan" , "ethel"]

/// <summary>
/// WordSet implementation using HashSet. 
/// Exact lookups are fast, but ordered/prefix operations scan and sort.
/// </summary>
public sealed class HashWordSet : IWordSet
{

    public int Count => words.Count();
    private HashSet<string> words = new();

    public bool Add(string word)
    {
        return words.Add(word);
    }

    public bool Contains(string word)
    {
        return words.Contains(word);
    }

    public bool Remove(string word)
    {
        return words.Remove(word);
    }

    /// TODO
    public string? Prev(string word)
    {
        string? best = null;

        // look for a better best
        foreach(var w in words)
        {
            if( word.CompareTo(w) > 0
                && (best is null || w.CompareTo(best) > 0))
            {
                best = w;
            }
        }

        return best;
    }

    public string? Next(string word)
    {
        string? best = null;

        // look for a better best
        foreach(var w in words)
        {
            // word < w && w < best
            if( word.CompareTo(w) < 0
                && (best is null || w.CompareTo(best) < 0))
            {
                best = w;
            }
        }

        return best;
    }

    public IEnumerable<string> Prefix(string prefix, int k)
    {
        var results = new List<string>();

        foreach(var word in words)
        {
            if(word.StartsWith(prefix))
            {
                results.Add(word);
            }
        }

        results.Sort();

        return results.Slice(0, Math.Min(k, results.Count));
    }

    /// TODO
    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var results = new List<string>();
        System.Console.WriteLine($"Checking for words between {lo} and {hi}");

        foreach (var word in words)
        {
            System.Console.WriteLine($"Checking {word}");
            if (word.CompareTo(lo) >= 0 && word.CompareTo(hi) <= 0)
            {
                results.Add(word);
                System.Console.WriteLine($"{word} was between them, added");
            }
        }

        results.Sort();

        foreach (var item in results)
        {
            System.Console.WriteLine($"{item} in final list");
        }

        return results.Slice(0, Math.Min(k, results.Count));

    }


    public string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return string.Empty;
        }
        return word.Trim().ToLowerInvariant();
    }
}

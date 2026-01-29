namespace Lab0;

public class SortedWordSet : IWordSet
{
    public int Count => words.Count();

    private SortedSet<string> words = [];

    public bool Add(string word)
    {
        var norm = Normalize(word);
        if (norm.Length == 0)
        {
            return false;
        }
        return words.Add(word);
    }

    public bool Contains(string word)
    {
        var norm = Normalize(word);
        if (norm.Length == 0)
        {
            return false;
        }
        return words.Contains(word);
    }

    public string? Next(string word)
    {
        var norm = Normalize(word);
        if (norm.Length == 0 || words.Count == 0)
        {
            return null;
        }

        var wordsInRange = words.GetViewBetween(norm, MAX_STRING);

        foreach (var item in wordsInRange)
        {
            if (item.CompareTo(norm) > 0)
            {
                return item;
            }
        }

        return null;
    }

    public IEnumerable<string> Prefix(string prefix, int k)
    {
        if (k <= 0 || words.Count == 0)
        {
            return new List<string>();
        }

        var results = new List<string>();

        var norm_pre = Normalize(prefix);
        
        int count = 0;

        var wordRange = words.GetViewBetween(norm_pre, norm_pre + "\uFFFF");

        foreach (var item in wordRange)
        {
            results.Add(item);
            count++;
            if (count >= k)
            {
                return results;
            }

        }

        return results;

    }

    public string? Prev(string word)
    {
        var norm = Normalize(word);
        if (norm.Length == 0 || words.Count == 0)
        {
            return null;
        }

        var wordsInRange = words.GetViewBetween("!", norm);

        foreach (var item in wordsInRange.Reverse())
        {
            if (item.CompareTo(norm) < 0)
            {
                return item;
            }
        }

        return null;

    }

    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        if (k <= 0 || words.Count == 0)
        {
            return new List<string>();
        }
        var lo_norm = Normalize(lo);
        var hi_norm = Normalize(hi);

        var results = new List<string>();

        var wordRange = words.GetViewBetween(lo_norm, hi_norm);

        var count = 0;

        foreach (var item in wordRange)
        {
            results.Add(item);
            count++;
            if (count >= k)
            {
                return results;
            }

        }

        return results;
    }

    public bool Remove(string word)
    {
        var norm = Normalize(word);
        if (norm.Length == 0)
        {
            return false;
        }
        return words.Remove(word);
    }

    public string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return string.Empty;
        }
        return word.Trim().ToLowerInvariant();
    }

    private const string MAX_STRING = "\uFFFF\uFFFF\uFFFF";
}
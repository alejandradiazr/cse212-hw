using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var pairs = new HashSet<string>();

        foreach (var word in words)
        {
            // Ignorar palabras como "aa"
            if (word[0] == word[1])
                continue;

            string reverse = string.Concat(word[1], word[0]);

            if (seen.Contains(reverse))
            {
                pairs.Add($"{word} & {reverse}");
            }

            seen.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            string degree = fields[3];

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var letters1 = new Dictionary<char, int>();
        var letters2 = new Dictionary<char, int>();

        foreach (char letter in word1.ToLower())
        {
            if (letter == ' ')
                continue;

            if (letters1.ContainsKey(letter))
                letters1[letter]++;
            else
                letters1[letter] = 1;
        }

        foreach (char letter in word2.ToLower())
        {
            if (letter == ' ')
                continue;

            if (letters2.ContainsKey(letter))
                letters2[letter]++;
            else
                letters2[letter] = 1;
        }

        if (letters1.Count != letters2.Count)
            return false;

        foreach (var letter in letters1)
        {
            if (!letters2.ContainsKey(letter.Key))
                return false;

            if (letters2[letter.Key] != letter.Value)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Read earthquake JSON data.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);

        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var earthquakes = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            earthquakes.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
        }

        return earthquakes.ToArray();
    }
}
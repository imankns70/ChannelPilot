using System.Text;
using ChannelPilot.Application.DTOs.Novels;

namespace ChannelPilot.Application.Novels;

public sealed class NovelEpisodeSplitter
{
    public const int DefaultMinChars = 3_500;
    public const int DefaultMaxChars = 7_500;

    public NovelEpisodeSplitter(int minChars = DefaultMinChars, int maxChars = DefaultMaxChars)
    {
        if (minChars <= 0 || maxChars < minChars)
        {
            throw new ArgumentOutOfRangeException(nameof(minChars), "Character limits must be positive and max must be at least min.");
        }

        MinChars = minChars;
        MaxChars = maxChars;
    }

    public int MinChars { get; }

    public int MaxChars { get; }

    public IReadOnlyList<NovelEpisodeDraftDto> Split(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        var normalized = text.Replace("\r\n", "\n", StringComparison.Ordinal).Trim();
        var paragraphs = normalized.Split(
            "\n\n",
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var pieces = paragraphs.SelectMany(SplitOversizedParagraph).ToList();
        var episodes = BuildEpisodes(pieces);

        return episodes
            .Select((content, index) => new NovelEpisodeDraftDto(
                index + 1,
                $"Episode {index + 1}",
                content,
                null))
            .ToList();
    }

    private IReadOnlyList<string> BuildEpisodes(IReadOnlyList<string> pieces)
    {
        var episodes = new List<string>();
        var current = new StringBuilder();

        foreach (var piece in pieces)
        {
            var separatorLength = current.Length == 0 ? 0 : 2;
            if (current.Length > 0 && current.Length + separatorLength + piece.Length > MaxChars)
            {
                episodes.Add(current.ToString());
                current.Clear();
            }

            if (current.Length > 0)
            {
                current.AppendLine().AppendLine();
            }

            current.Append(piece);
        }

        if (current.Length > 0)
        {
            episodes.Add(current.ToString());
        }

        if (episodes.Count > 1 && episodes[^1].Length < MinChars)
        {
            var merged = $"{episodes[^2]}\n\n{episodes[^1]}";
            if (merged.Length <= MaxChars)
            {
                episodes[^2] = merged;
                episodes.RemoveAt(episodes.Count - 1);
            }
        }

        return episodes;
    }

    private IEnumerable<string> SplitOversizedParagraph(string paragraph)
    {
        var remaining = paragraph.Trim();

        while (remaining.Length > MaxChars)
        {
            var splitAt = remaining.LastIndexOf(' ', MaxChars, MaxChars);
            if (splitAt <= 0)
            {
                splitAt = MaxChars;
            }

            yield return remaining[..splitAt].Trim();
            remaining = remaining[splitAt..].Trim();
        }

        if (remaining.Length > 0)
        {
            yield return remaining;
        }
    }
}

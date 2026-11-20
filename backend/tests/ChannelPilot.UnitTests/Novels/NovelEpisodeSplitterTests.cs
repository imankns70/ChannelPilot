using ChannelPilot.Application.Novels;

namespace ChannelPilot.UnitTests.Novels;

public sealed class NovelEpisodeSplitterTests
{
    [Fact]
    public void Split_returns_no_episodes_for_blank_text()
    {
        var splitter = new NovelEpisodeSplitter(minChars: 10, maxChars: 20);

        var result = splitter.Split("   ");

        Assert.Empty(result);
    }

    [Fact]
    public void Split_uses_paragraph_boundaries_and_numbers_episodes()
    {
        var splitter = new NovelEpisodeSplitter(minChars: 10, maxChars: 20);
        var text = $"{new string('a', 12)}\n\n{new string('b', 12)}";

        var result = splitter.Split(text);

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].EpisodeNumber);
        Assert.Equal("Episode 1", result[0].Title);
        Assert.Equal(new string('a', 12), result[0].Content);
        Assert.Equal(2, result[1].EpisodeNumber);
        Assert.Equal(new string('b', 12), result[1].Content);
    }

    [Fact]
    public void Split_hard_splits_a_paragraph_that_exceeds_the_maximum()
    {
        var splitter = new NovelEpisodeSplitter(minChars: 10, maxChars: 20);

        var result = splitter.Split(new string('x', 45));

        Assert.Equal(3, result.Count);
        Assert.All(result, episode => Assert.InRange(episode.Content.Length, 1, 20));
    }
}

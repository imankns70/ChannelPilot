using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Application.Novels;

public sealed class NovelCaptionBuilder
{
    public string Build(string novelTitle, NovelEpisode episode)
    {
        var caption = $"{novelTitle}\nقسمت {episode.EpisodeNumber}\n\n{episode.Content}";

        if (!string.IsNullOrWhiteSpace(episode.EndingTeaser))
        {
            caption += $"\n\n{episode.EndingTeaser}";
        }

        return $"{caption}\n\nادامه دارد...";
    }
}

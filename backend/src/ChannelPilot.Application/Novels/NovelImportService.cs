using ChannelPilot.Application.DTOs.Novels;
using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Application.Novels;

public sealed class NovelImportService
{
    public Novel CreateNovel(NovelImportDto import)
    {
        return new Novel
        {
            Title = import.Title,
            Author = import.Author,
            Description = import.Description,
            Genre = import.Genre,
            Status = NovelStatus.Draft
        };
    }
}

using Id3;

namespace MP3Tagger;

internal record Tags(string Artist, string Album,
    int Year, string Title, int TrackNumber);

internal class Tagger
{
    public static void SetTags(string filename, Tags tags)
    {
        using var mp3 = new Mp3(filename, Mp3Permissions.ReadWrite);
        Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
        tag.Title = tags.Title;
        tag.Track = tags.TrackNumber;
        tag.Album = tags.Album;
        tag.Artists.Value.Add(tags.Artist);
        tag.Year = tags.Year;
        mp3.WriteTag(tag, WriteConflictAction.Replace);
    }
}

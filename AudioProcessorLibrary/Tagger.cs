using Id3;

namespace AudioProcessorLibrary;

public class Tagger
{
    public string TagFile(string fileName, TrackInfo info)
    {
        using var mp3 = new Mp3(fileName, Mp3Permissions.ReadWrite);
        Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
        tag.Title = info.Title;
        tag.Track = info.TrackNumber;
        tag.Album = info.Album;
        tag.Artists.Value.Add(info.Artist);
        tag.Year = info.Year;
        mp3.WriteTag(tag, WriteConflictAction.Replace);
        return fileName;
    }
}

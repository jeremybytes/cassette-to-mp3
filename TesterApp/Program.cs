using AudioProcessorLibrary;

namespace TesterApp;

internal class Program
{
    static void Main(string[] args)
    {
        Splitter splitter = new();
        string inputFile = """D:\MusicProcessing\Recordings\Youth_Choir_Side1.wav""";

        TrackInfo info = new("Youth Choir", "Youth Choir", 1985, "Someone's Calling", 1);

        string filePath = splitter.ExtrackWAVTrack(
            inputFile,
            TimeSpan.Parse($"00:00:12"),
            TimeSpan.Parse($"00:04:41"),
            info);

        Console.WriteLine($"Track WAV path: {filePath}");

        MP3Converter converter = new();

        string mp3Path = converter.ConvertFile(filePath);
        Console.WriteLine($"Track MP3 path: {mp3Path}");

        Tagger tagger = new();
        string taggedFilePath = tagger.TagFile(mp3Path, info);
        Console.WriteLine($"Tagged File: {taggedFilePath}");
    }
}
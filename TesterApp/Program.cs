using AudioProcessorLibrary;

namespace TesterApp;

internal class Program
{
    static void Main(string[] args)
    {
        // Input for raw recording
        string inputFile = """C:\Recordings\Youth_Choir_Side1.wav""";

        TrackInfo info = new("Test Band", "Test Album", 1985, "Someone's C?all>ing", 1);

        // Split out single track (to WAV)
        string wavFilePath = Splitter.ExtractWAVTrack(
            inputFile,
            TimeSpan.Parse($"00:00:12"),
            TimeSpan.Parse($"00:04:41"),
            info);

        Console.WriteLine($"Track WAV path: {wavFilePath}");

        // Convert WAV to MP3
        string mp3Path = MP3Converter.ConvertFile(wavFilePath);
        Console.WriteLine($"Track MP3 path: {mp3Path}");

        // Delete WAV
        File.Delete(wavFilePath);

        // Add tags to MP3
        string taggedFilePath = Tagger.TagFile(mp3Path, info);
        Console.WriteLine($"Tagged File: {taggedFilePath}");
    }
}
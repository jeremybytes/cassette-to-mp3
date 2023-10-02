﻿using AudioProcessorLibrary;

namespace TesterApp;

internal class Program
{
    static void Main(string[] args)
    {
        Splitter splitter = new();

        // Input for raw recording
        string inputFile = """D:\MusicProcessing\Recordings\Youth_Choir_Side1.wav""";

        TrackInfo info = new("Youth Choir", "Youth Choir", 1985, "Someone's Calling", 1);

        // Split out single track (to WAV)
        string wavFilePath = splitter.ExtrackWAVTrack(
            inputFile,
            TimeSpan.Parse($"00:00:12"),
            TimeSpan.Parse($"00:04:41"),
            info);

        Console.WriteLine($"Track WAV path: {wavFilePath}");

        // Convert WAV to MP3
        MP3Converter converter = new();

        string mp3Path = converter.ConvertFile(wavFilePath);
        Console.WriteLine($"Track MP3 path: {mp3Path}");

        // Delete WAV
        File.Delete(wavFilePath);

        // Add tags to MP3
        Tagger tagger = new();
        string taggedFilePath = tagger.TagFile(mp3Path, info);
        Console.WriteLine($"Tagged File: {taggedFilePath}");
    }
}
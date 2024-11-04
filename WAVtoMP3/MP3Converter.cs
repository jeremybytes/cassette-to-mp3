using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace WAVtoMP3;

internal class MP3Converter
{
    public static void ConvertFiles(string inputName, List<string> trackNames)
    {
        var inputFolder = """D:\MusicProcessing\SplitFiles""";

        var outputFolder = Path.Combine("""D:\MusicProcessing\ConvertedMP3s""", inputName);
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        for (int i = 0; i < trackNames.Count; i++)
        {
            if (i == 0) continue;
            var inputFilePath = Path.Combine(inputFolder, $"{inputName}_Track{i}.wav");

            var outputFilePath = Path.Combine(outputFolder, $"{i:D2} {trackNames[i]}.mp3");
            ConvertFile(inputFilePath, outputFilePath);
        }
    }

    private static void ConvertFile(string inputFilePath, string outputFilePath)
    {
        using var reader = new WaveFileReader(inputFilePath);
        MediaFoundationEncoder.EncodeToMp3(reader,
                outputFilePath, 48000);
    }
}

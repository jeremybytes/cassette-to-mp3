using NAudio.Wave;

namespace AudioProcessorLibrary;

public class Splitter
{
    public string ExtractWAVTrack(string filePath, TimeSpan start, TimeSpan end, 
        TrackInfo info)
    {
        string inputPath = Path.GetDirectoryName(filePath)!;
        string outputFileName = $"""{info.TrackNumber:D2} {info.Title}.wav""";
        string outputFilePath = Path.Combine(inputPath, 
            ReplaceForbiddenCharacters(info.Artist), 
            ReplaceForbiddenCharacters(info.Album), 
            ReplaceForbiddenCharacters(outputFileName));

        if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath)!);

        TrimWavFile(filePath, outputFilePath, start, end);

        return outputFilePath;
    }

    private string ReplaceForbiddenCharacters(string input)
    {
        foreach(char c in """<>:"/\|?*""")
            input = input.Replace(c, '_');
        return input;
    }

    private void TrimWavFile(string inPath, string outPath, TimeSpan startPoint, TimeSpan endPoint)
    {
        using (WaveFileReader reader = new WaveFileReader(inPath))
        {
            using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
            {
                int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                int startPos = (int)startPoint.TotalMilliseconds * bytesPerMillisecond;
                startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                int endPos = (int)endPoint.TotalMilliseconds * bytesPerMillisecond;
                endPos = endPos - endPos % reader.WaveFormat.BlockAlign;

                TrimWavFile(reader, writer, startPos, endPos);
            }
        }
    }

    private void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
    {
        reader.Position = startPos;
        byte[] buffer = new byte[1024];
        while (reader.Position < endPos)
        {
            int bytesRequired = (int)(endPos - reader.Position);
            if (bytesRequired > 0)
            {
                int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                int bytesRead = reader.Read(buffer, 0, bytesToRead);
                if (bytesRead > 0)
                {
                    writer?.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}

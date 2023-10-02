using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace WAVSplitter;

internal class Splitter
{
    public void SplitFile(string fileName, List<TimeSpan> breakPoints)
    {
        var inputFolder = """D:\MusicProcessing\Recordings""";
        var inputFilePath = Path.Combine(inputFolder, fileName+".wav");

        var outputFolder = """D:\MusicProcessing\SplitFiles""";
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        for (int i = 0; i < breakPoints.Count; i++)
        {
            string outfileName = $"{fileName}_Track{i}";
            var outputFilePath = Path.Combine(outputFolder, outfileName+".wav");
            TimeSpan startTime = i switch
            {
                0 => new TimeSpan(0),
                _ => breakPoints[i-1],
            };
            TimeSpan endTime = breakPoints[i];
            TrimWavFile(inputFilePath, outputFilePath, startTime, endTime);
        }
    }

    public void TrimWavFile(string inPath, string outPath, TimeSpan startPoint, TimeSpan endPoint)
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

    //public void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
    //{
    //    using (WaveFileReader reader = new WaveFileReader(inPath))
    //    {
    //        using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
    //        {
    //            int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

    //            int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
    //            startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

    //            int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
    //            endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
    //            int endPos = (int)reader.Length - endBytes;

    //            TrimWavFile(reader, writer, startPos, endPos);
    //        }
    //    }
    //}

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
                    writer.WriteData(buffer, 0, bytesRead);
                }
            }
        }
    }
}

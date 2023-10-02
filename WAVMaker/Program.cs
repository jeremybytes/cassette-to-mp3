using NAudio.Wave;

namespace WAVMaker;

internal class Program
{
    static void Main(string[] args)
    {
        WaveInEvent? recorder;
        BufferedWaveProvider? bufferedWaveProvider;
        SavingWaveProvider? savingWaveProvider;
        WaveOutEvent? player;

        var outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recordings");
        Directory.CreateDirectory(outputFolder);
        var outputFilePath = Path.Combine(outputFolder, "recorded.wav");

        recorder = new WaveInEvent();
        recorder.DeviceNumber = 3;
        recorder.WaveFormat = new WaveFormat(rate: 44100, bits: 16, channels: 2);

        bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
        savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, outputFilePath);

        recorder.DataAvailable += (s, waveInEventArgs) =>
        {
            bufferedWaveProvider?.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        };

        player = new WaveOutEvent();
        player.Init(savingWaveProvider);

        player.Play();
        recorder.StartRecording();

        Console.WriteLine("Press any key to stop recording");
        Console.ReadKey();

        recorder.StopRecording();
        player.Stop();
        savingWaveProvider.Dispose();

        Console.WriteLine("Done");
    }
}
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WAVRecorder;

public partial class MainWindow : Window
{
    private WaveInEvent? recorder;
    private BufferedWaveProvider? bufferedWaveProvider;
    private SavingWaveProvider? savingWaveProvider;
    private WaveOutEvent? player;
    private int selectedDevice = 0;
    private string fileName = "";
    private DispatcherTimer recordingTime;
    private DateTime startTime;

    public MainWindow()
    {
        InitializeComponent();
        recordingTime = new();
        recordingTime.Interval = new TimeSpan(0, 0, 0, 0, 300);
        recordingTime.Tick += new EventHandler(TimerTick);

        PopulateInputDevices();
        SetFileName();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        TimeSpan elapsed = DateTime.Now - startTime;
        RecordingTime.Content = elapsed.ToString(@"mm\:ss");
    }

    private void SetFileName()
    {
        FileName.Text = fileName;
    }

    private void PopulateInputDevices()
    {
        int deviceCount = WaveIn.DeviceCount;
        List<KeyValuePair<int, string>> sortList = new();

        for (int i = 0; i < deviceCount; i++)
        {
            WaveInCapabilities capabilities = WaveIn.GetCapabilities(i);
            sortList.Add(new KeyValuePair<int, string>(i, capabilities.ProductName));
        }

        InputDevices.ItemsSource = sortList;
        InputDevices.SelectedIndex = selectedDevice;
    }

    private void InputDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedDevice = InputDevices.SelectedIndex;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        startTime = DateTime.Now;
        recordingTime.Start();

        var outputFilePath = fileName;

        recorder = new WaveInEvent();
        recorder.DeviceNumber = selectedDevice;
        recorder.WaveFormat = new WaveFormat(rate: 44100, bits: 16, channels: 2);
        recorder.DataAvailable += (s, waveInEventArgs) =>
        {
            bufferedWaveProvider?.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        };


        bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
        savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, outputFilePath);

        player = new WaveOutEvent();
        player.Init(savingWaveProvider);

        // begin playback & record
        player.Play();
        recorder.StartRecording();
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        recordingTime.Stop();
        recorder?.StopRecording();
        player?.Stop();
        savingWaveProvider?.Dispose();
    }

    private void FileName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.InitialDirectory = """C:\Recordings""";
        dialog.FileName = "Recording.wav"; // Default file name
        dialog.DefaultExt = ".wav"; // Default file extension
        dialog.Filter = "WAV Files (.wav)|*.wav"; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            fileName = dialog.FileName;
            FileName.Text = fileName;
        }
    }
}

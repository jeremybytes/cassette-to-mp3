using AudioProcessorLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RecordingToMP3;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Go_Click(object sender, RoutedEventArgs e)
    {
        // Input for raw recording
        string inputFile = InputName.Text;
        int baseTrack = int.Parse(TrackStart.Text);

        TrackInfo baseInfo = new(
            Artist.Text,
            Album.Text,
            int.Parse(Year.Text),
            "",
            0);

        List<TimeSpan> editPoints = GetEditPoints(EditPoints.Text);
        List<string> trackNames = GetTrackNames(TrackNames.Text);

        for (int i = 1; i < trackNames.Count; i++)
        {
            TimeSpan start = editPoints[i - 1];
            TimeSpan end = editPoints[i];
            int trackNumber = baseTrack + i - 1;
            TrackInfo trackInfo = baseInfo with { TrackNumber = trackNumber, Title = trackNames[i] };

            Splitter splitter = new();

            // Split out single track (to WAV)
            string wavFilePath = splitter.ExtractWAVTrack(
                inputFile,
                start,
                end,
                trackInfo);

            // Convert WAV to MP3
            MP3Converter converter = new();

            string mp3Path = converter.ConvertFile(wavFilePath);

            // Delete WAV
            File.Delete(wavFilePath);

            // Add tags to MP3
            Tagger tagger = new();
            tagger.TagFile(mp3Path, trackInfo);
        }

        MessageBox.Show("Done");
    }

    private void InputName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.InitialDirectory = """C:\Recordings""";
        dialog.FileName = ""; // Default file name
        dialog.DefaultExt = ".wav"; // Default file extension
        dialog.Filter = "WAV Files (.wav)|*.wav"; // Filter files by extension

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            string filename = dialog.FileName;
            InputName.Text = filename;
        }
    }

    private List<TimeSpan> GetEditPoints(string text)
    {
        string[] times = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        List<TimeSpan> editPoints = new();
        foreach (string item in times)
            editPoints.Add(TimeSpan.Parse($"00:{item}"));
        return editPoints;
    }

    private List<string> GetTrackNames(string text)
    {
        List<string> tracks = new() { "EMPTY" };
        string[] data = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var item in data)
            tracks.Add(item);
        return tracks;
    }
}

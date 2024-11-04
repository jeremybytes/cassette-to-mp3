using System.Collections.Generic;
using System;
using System.Windows;

namespace WAVtoMP3;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Go_Click(object sender, RoutedEventArgs e)
    {
        string inputName = InputName.Text;
        List<string> trackNames = GetTrackNames(TrackNames.Text);

        MP3Converter.ConvertFiles(inputName, trackNames);
    }

    private static List<string> GetTrackNames(string text)
    {
        List<string> tracks = ["EMPTY"];
        string[] data = text.Split('\n', StringSplitOptions.TrimEntries);
        foreach (var item in data)
            tracks.Add(item);
        return tracks;
    }
}

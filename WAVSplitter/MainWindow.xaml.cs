using System;
using System.Collections.Generic;
using System.Windows;

namespace WAVSplitter;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Go_Click(object sender, RoutedEventArgs e)
    {
        string fileName = FileName.Text;
        List<TimeSpan> editPoints = ParseEditPoints(EditPoints.Text);

        Splitter.SplitFile(fileName, editPoints);
        //List<TimeSpan> breakpoints = new()
        //{
        //    new TimeSpan(0, 0, 12),
        //    new TimeSpan(0, 4, 41),
        //    new TimeSpan(0, 7, 20),
        //    new TimeSpan(0, 11, 9),
        //    new TimeSpan(0, 14, 43),
        //    new TimeSpan(0, 19, 15),
        //};
        //splitter.SplitFile("Youth_Choir_Side1", breakpoints);
    }

    private static List<TimeSpan> ParseEditPoints(string text)
    {
        string[] times = text.Split('\n', StringSplitOptions.TrimEntries);
        List<TimeSpan> editPoints = new();
        foreach (string item in times)
            editPoints.Add(TimeSpan.Parse($"00:{item}"));
        return editPoints;
    }
}

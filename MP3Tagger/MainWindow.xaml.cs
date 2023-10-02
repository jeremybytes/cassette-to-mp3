using System.IO;
using System.Windows;

namespace MP3Tagger;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Go_Click(object sender, RoutedEventArgs e)
    {
        Tagger tagger = new();

        string inputName = "TestFolder";
        var inputFolder = Path.Combine("""D:\MusicProcessing\ConvertedMP3s""", inputName);

        var outputFolder = Path.Combine("""D:\MusicProcessing\TaggedMP3s""", inputName);
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        Tags tags = new("Youth Choir", "Youth Choir", 1982, "First Track", 1);

        string inputFilePath = Path.Combine(inputFolder, "First_Track.mp3");

        tagger.SetTags(inputFilePath, tags);
    }
}

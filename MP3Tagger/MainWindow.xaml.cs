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

        string inputName = "Youth_Choir_Side1";
        var inputFolder = Path.Combine("""D:\MusicProcessing\ConvertedMP3s""", inputName);

        Tags tags = new("Youth Choir", "Youth Choir", 1982, "Someone's Calling", 1);

        string inputFilePath = Path.Combine(inputFolder, "01 Someone's Calling.mp3");

        tagger.SetTags(inputFilePath, tags);
    }
}

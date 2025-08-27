using Terminal.Gui;
using Terminal.Gui.Configuration;
using Terminal.Gui.App;
using Terminal.Gui.Drawing;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;
using LibVLCSharp.Shared;

Application.Run<ExampleWindow> ().Dispose ();
Application.Shutdown ();

public class ExampleWindow : Window {
    public static string StreamCode { get; set; }
    public static string StreamUrl {get; set;}

    private static LibVLC _libVLC;
    private static MediaPlayer _mediaPlayer;

    public ExampleWindow(){

        Core.Initialize();
        _libVLC = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVLC);


        Title = $"Example ({Application.QuitKey} To Exit)";

        var InputCode = new TextField
        {
            X = 2,Y = 1,

            // Fill remaining horizontal space
            Width = 10
        };
        var BtnStream = new Button
        {
            Text = "Stream",
            Y = Pos.Bottom (InputCode) + 1,

            // center the login button horizontally
            X = 2,
            IsDefault = true
        };
        BtnStream.Accepting += (s, e) =>
        {
            StreamCode = InputCode.Text;
            StreamUrl = $"https://radio.garden/api/ara/content/listen/{StreamCode}/channel.mp3";
            StreamMp3Url(StreamUrl);
            Thread.Sleep(500);
            MessageBox.Query ("Starting Stream...", $"Streaming {StreamCode}", "Ok");
            e.Handled = true;
        };
    Add (InputCode,BtnStream);
    }

    public static void StreamMp3Url(string url)
    {
        if (_libVLC == null || _mediaPlayer == null)
            throw new InvalidOperationException("VLC not initialized. Call InitializeVLC first.");

        using var media = new Media(_libVLC, url, FromType.FromLocation);
        _mediaPlayer.Play(media);
    }
}

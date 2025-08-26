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

    public ExampleWindow(){

        Core.Initialize();
        using var libVLC = new LibVLC();
        using var mediaPlayer = new MediaPlayer(libVLC);


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
            MessageBox.Query ("Starting Stream...", $"Streaming {StreamCode}", "Ok");
            StreamUrl = $"https://radio.garden/api/ara/content/listen/{StreamCode}/channel.mp3";

            using var media = new Media(libVLC, StreamUrl, FromType.FromLocation);
            mediaPlayer.Play(media);

            Console.WriteLine (StreamUrl);
            e.Handled = true;
        };
    Add (InputCode,BtnStream);
    }
}

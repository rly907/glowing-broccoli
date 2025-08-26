using Terminal.Gui;
using Terminal.Gui.Configuration;
using Terminal.Gui.App;
using Terminal.Gui.Drawing;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

Application.Run<ExampleWindow> ().Dispose ();
Application.Shutdown ();

public class ExampleWindow : Window {
    public ExampleWindow(){
        Title = $"Example ({Application.QuitKey} To Exit)";

        var InputCode = new TextField
        {
            X = 2,Y = 1,

            // Fill remaining horizontal space
            Width = 10
        };
    Add (InputCode);
    }

}

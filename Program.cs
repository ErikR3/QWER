using SFML.Graphics;
using SFML.System;
using SFML.Window;

var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "My SFML Window");
window.Closed += (sender, e) => window.Close();

var shape = new CircleShape(50)
{
    FillColor = Color.Green,
    Position = new Vector2f(375, 275)
};

while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Draw(shape);
    window.Display();
}

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;
using Components;
using Systems;

var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "My SFML Window");
window.Closed += (sender, e) => window.Close();
window.SetFramerateLimit(ECSConfig.Framerate);

var c = new Coordinator();
var sm = new SystemManager(c);
var ms = new MovementSystem();
sm.RegisterSystem(ms);

Clock deltaClock = new Clock();

while (window.IsOpen)
{
    Time deltaTime = deltaClock.Restart();
    sm.Update(deltaTime.AsSeconds());

    window.DispatchEvents();
    window.Clear(Color.Black);

    window.Display();
}

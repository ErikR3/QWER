using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;

/* var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "My SFML Window");
window.Closed += (sender, e) => window.Close();

var shape = new CircleShape(50)
{
    FillColor = Color.Red,
    Position = new Vector2f(375, 275)
    }; */

var EntityTest = new EntityManager();
EntityTest.AddEntity(3);
EntityTest.AddEntity(7);
EntityTest.AddEntity(2);
ECSDebugger.PrintSets(EntityTest);

/* while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Draw(shape);
    window.Display();
    } */

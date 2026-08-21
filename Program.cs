using SFML.Graphics;
using SFML.System;
using SFML.Window;
using QWER;

var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "My SFML Window");
window.Closed += (sender, e) => window.Close();

var shape = new CircleShape(50)
{
    FillColor = Color.Red,
    Position = new Vector2f(375, 275)
};

var EntityTest = new EntityManager();
EntityTest.AddEntity();
EntityTest.AddEntity();
EntityTest.AddEntity();
ECSDebugger.PrintSets(EntityTest);
EntityTest.RemoveEntity(1);
ECSDebugger.PrintSets(EntityTest);
EntityTest.AddEntity();
EntityTest.AddEntity();
EntityTest.AddEntity();
ECSDebugger.PrintSets(EntityTest);
EntityTest.RemoveEntity(3);
ECSDebugger.PrintSets(EntityTest);
EntityTest.AddEntity();
ECSDebugger.PrintSets(EntityTest);

// ECSDebugger.PrintDense(EntityTest.GetEntityAt(EntityTest.GetDenseIndex(2)));

while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Draw(shape);
    window.Display();
}

using Godot;
using System;

public partial class GlobalSignals : Node
{
	[Signal]

	public delegate void GlobalBulletFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);


	public void OnEntityFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		EmitSignal(SignalName.GlobalBulletFired, bullet, position, direction);
		GD.Print("glorb");
	}
}

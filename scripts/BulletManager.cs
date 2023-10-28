using Godot;
using System;

public partial class BulletManager : Node2D
{
	public void OnPlayerFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		AddChild(bullet);
		bullet.GlobalPosition = position;
		bullet.SetDirection(direction);
	}
}

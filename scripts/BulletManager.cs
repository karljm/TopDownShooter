using Godot;
using System;

public partial class BulletManager : Node2D
{
	public void OnPlayerFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		GD.Print("bullet manager OnPlayerFired");
		AddChild(bullet);
		bullet.GlobalPosition = position;
		bullet.SetDirection(direction);
	}
	public void OnBulletFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		GD.Print("bullet manager OnBulletFired");
		AddChild(bullet);
		bullet.GlobalPosition = position;
		bullet.SetDirection(direction);
	}
}

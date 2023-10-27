using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] private int _speed = 30;
	private Vector2 direction = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (this.direction != Vector2.Zero)
		{
			Vector2 velocity = direction * _speed;

			GlobalPosition += velocity;
		}
	}

	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
		Rotation += direction.Angle();
	}

	public void OnKillTimerTimeout()
	{
		QueueFree();
	}

	public void OnBodyEntered(CharacterBody2D body)
	{
		// using duck typing
		if (body.HasMethod("HandleHit"))
		{
			body.Call("HandleHit");
			QueueFree();
		}
	}
}

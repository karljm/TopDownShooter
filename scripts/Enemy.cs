using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;

	private float _health = 100;
	public override void _PhysicsProcess(double delta)
	{

	}
	
	public void HandleHit()
	{
		_health -= 20;
		// GD.Print("Enemy hit!");
		if (_health <= 0)
		{
			QueueFree();
		}
	}
}

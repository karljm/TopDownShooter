using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;

	private Health _health;
	// private float _health = 100;
	public override void _Ready()
    {
		_health = GetNode<Health>("Health");
    }
	public override void _PhysicsProcess(double delta)
	{

	}
	
	public void HandleHit()
	{
		_health.Value -= 20;
		// GD.Print("Enemy hit!");
		if (_health.Value <= 0)
		{
			QueueFree();
		}
	}
}

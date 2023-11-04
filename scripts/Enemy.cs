using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export]
	private float _speed = 100;
	[Signal]
	public delegate void EntityFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);
	public const float Speed = 300.0f;

	private Health _health;
	private EnemyAI _enemyAI;
	private Weapon _weapon;

	public override void _Ready()
	{
		_enemyAI = GetNode<EnemyAI>("EnemyAI");
		_weapon = GetNode<Weapon>("Weapon");
		_health = GetNode<Health>("Health");

		// dependency injection
		_enemyAI.Initialise(this, _weapon);
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

	private void OnWeaponFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		// call down, signal up
		EmitSignal(SignalName.EntityFired, bullet, position, direction);
		// GD.Print("OnPlayerWeaponFired");
	}

	public void RotateToward(Vector2 location)
	{
		Rotation = Mathf.Lerp(Rotation, GlobalPosition.DirectionTo(location).Angle(), 0.1f);
	}

	public Vector2 VelocityToward(Vector2 location)
	{
		return GlobalPosition.DirectionTo(location) * _speed;
	}
}

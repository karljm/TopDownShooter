using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	[Export] public const float Speed = 300.0f;

	[Signal]
	public delegate void PlayerFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);
	// [Signal]
	// public delegate void WeaponFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);

	private Health _health;
	private Weapon _weapon;

	// private float _health = 200;

    public override void _Ready()
    {
		_health = GetNode<Health>("Health");
		_weapon = GetNode<Weapon>("Weapon");
    }

    public override void _PhysicsProcess(double delta)
	{
		var movement_direction = Vector2.Zero; // The player's movement vector.

		// Godot 4.0 solution
		// Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		if (Input.IsActionPressed("move_up"))
			movement_direction.Y = -1;
		if (Input.IsActionPressed("move_down"))
			movement_direction.Y = 1;
		if (Input.IsActionPressed("move_left"))
			movement_direction.X = -1;
		if (Input.IsActionPressed("move_right"))
			movement_direction.X = 1;



		if (movement_direction.Length() > 0)
			movement_direction = movement_direction.Normalized() * Speed;
		
		Velocity = movement_direction;
		MoveAndSlide();

		LookAt(GetGlobalMousePosition());
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("click"))
		{
			// Shoot();
			_weapon.Shoot();
			// GD.Print("player shot");
		}
	}

	private void OnPlayerWeaponFired(Bullet bullet, Vector2 position, Vector2 direction)
	{
		// call down, signal up
		EmitSignal(SignalName.PlayerFired, bullet, position, direction);
	}

	public void HandleHit()
	{
		// _health -= 20;
		// _health.UpdateHeal
		_health.Value -= 20;
		GD.Print("Health = " + _health.Value);
	}
}

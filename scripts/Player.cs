using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	[Export] 
	public PackedScene BulletScene { get; set; }
	[Export] public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private Marker2D _muzzle;


    public override void _Ready()
    {
		_muzzle = GetNode<Marker2D>("Muzzle");
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
			Shoot();
		}

	}

	private void Shoot()
	{
		GD.Print("player shot");
		// create new instance of bullet scene
		Bullet bullet = BulletScene.Instantiate<Bullet>();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = _muzzle.GlobalPosition;
		Vector2 target = GetGlobalMousePosition();
		Vector2 direction = bullet.GlobalPosition.DirectionTo(target).Normalized();	// vector from mouse to bullet
		bullet.SetDirection(direction);
	}
}

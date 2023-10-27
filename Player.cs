using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

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
}

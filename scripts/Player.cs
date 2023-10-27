using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	[Export] 
	public PackedScene BulletScene { get; set; }
	[Export] public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	[Signal]
	public delegate void BulletFiredEventHandler(Bullet bullet);

	private Marker2D _muzzle;
	private Marker2D _gunDirection;
	private Timer _attackCooldown;
	private AnimationPlayer _shootAnimation;

	private float _health = 200;

    public override void _Ready()
    {
		_muzzle = GetNode<Marker2D>("Muzzle");
		_gunDirection = GetNode<Marker2D>("GunDirection");
		_attackCooldown = GetNode<Timer>("AttackCooldown");
		_shootAnimation = GetNode<AnimationPlayer>("ShootAnimation");
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
		if (_attackCooldown.IsStopped() == false)
			return;

		// create new instance of bullet scene
		Bullet bullet = BulletScene.Instantiate<Bullet>();
		Vector2 direction = (_gunDirection.GlobalPosition - _muzzle.GlobalPosition).Normalized();
		EmitSignal(SignalName.BulletFired, bullet, _muzzle.GlobalPosition, direction);
		_attackCooldown.Start();
		_shootAnimation.Play("MuzzleFlash");
	}

	public void HandleHit()
	{
		_health -= 20;
		GD.Print("Health = " + _health);
	}
}

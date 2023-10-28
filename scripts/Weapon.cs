using Godot;
using System;

public partial class Weapon : Node2D
{
	[Export] 
	public PackedScene BulletScene { get; set; }
	[Signal]
	public delegate void WeaponFiredEventHandler(Bullet bullet, Vector2 position, Vector2 direction);

	private Marker2D _muzzle;
	private Marker2D _gunDirection;
	private Timer _attackCooldown;
	private AnimationPlayer _shootAnimation;
	
	  public override void _Ready()
    {
		_muzzle = GetNode<Marker2D>("Muzzle");
		_gunDirection = GetNode<Marker2D>("GunDirection");
		_attackCooldown = GetNode<Timer>("AttackCooldown");
		_shootAnimation = GetNode<AnimationPlayer>("ShootAnimation");
    }

	public void Shoot()
	{
		if (_attackCooldown.IsStopped() == false)
			return;
		// GD.Print("weapon firing");
		// create new instance of bullet scene
		Bullet bullet = BulletScene.Instantiate<Bullet>();
		Vector2 direction = (_gunDirection.GlobalPosition - _muzzle.GlobalPosition).Normalized();
		// call down, signal up
		EmitSignal(SignalName.WeaponFired, bullet, _muzzle.GlobalPosition, direction);
		_attackCooldown.Start();
		_shootAnimation.Play("MuzzleFlash");
	}
}

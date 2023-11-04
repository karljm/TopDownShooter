using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Node2D _bulletManager;
	private CharacterBody2D _player;
	// private GlobalSignals _globalSignals;


	public override void _Ready()
	{
		GD.Randomize(); // supposidely is automatically called already
		_bulletManager = GetNode<Node2D>("BulletManager");
		_player = GetNode<CharacterBody2D>("Player");
		// _globalSignals = GetNode<GlobalSignals>("GlobalSignals");
		// Callable bulletFired = new(_bulletManager, "OnBulletFired");
		// _globalSignals.Connect("BulletFired", bulletFired);
		// _globalSignals. += 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GlobalSignals.
	}
}

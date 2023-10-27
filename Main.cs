using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Node2D _bulletManager;
	private CharacterBody2D _player;


	public override void _Ready()
	{
		_bulletManager = GetNode<Node2D>("BulletManager");
		_player = GetNode<CharacterBody2D>("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

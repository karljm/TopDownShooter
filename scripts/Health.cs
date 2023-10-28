using Godot;
using System;

public partial class Health : Node2D
{
	[Export] 
	private int _value = 200;
	[Export] 
	private int _capacity = 200;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int Value
	{
		get => _value;
		set => _value = Math.Clamp(value, 0, _capacity);	// keep value in valid range
	} 
}

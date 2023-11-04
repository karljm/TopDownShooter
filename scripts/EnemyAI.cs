using Godot;
using System;

public partial class EnemyAI : Node2D
{
	[Signal]
	public delegate void StateChangedEventHandler(int _currentState);

	public enum States
	{
		ROAM,
		CHASE,
	}
	private Area2D _playerDetectionZone;

	private States _state;

	private int _currentState;
	private Node2D _entity = null;
	private Node2D _player = null;
	private Weapon _weapon = null;
	public override void _Ready()
	{
		_state = States.ROAM;
		_currentState = (int)States.ROAM;
		_playerDetectionZone = GetNode<Area2D>("PlayerDetectionZone");
	}

	public States State
	{
		get { return _state; }
		set
		{
			if (value == _state)
				return;
			_state = value;
			EmitSignal(SignalName.StateChanged, (int)_state);
		}
	}

	public int CurrentState
	{
		get
		{
			return _currentState;
		}

		set
		{
			if (value == _currentState)
				return;

			_currentState = value;

			EmitSignal(SignalName.StateChanged, _currentState);
		}
	}

	private void SetWeapon(Weapon weapon)
	{
		_weapon = weapon;
	}


	public void Initialise(Node2D entity, Weapon weapon)
	{
		_entity = entity;
		SetWeapon(weapon);
	}

	public void OnPlayerDetectionZoneBodyEntered(Node2D body)
	{
		if (body.IsInGroup("PlayerGroup"))
		{
			GD.Print("player in zone");
			CurrentState = (int)States.CHASE;
			State = States.CHASE;
			_player = body;
		}
	}


	public void OnPlayerDetectionZoneBodyExited(Node2D body)
	{
		if (body.IsInGroup("PlayerGroup"))
		{
			GD.Print("player exits zone");
			CurrentState = (int)States.ROAM;
			State = States.ROAM;
			_player = body;
		}
	}

	private void Chase()
	{
		if (_player == null && _weapon == null)
		{
			GD.Print("Enemy in engage state, but no weapon or player");
			return;
		}
		_entity.Rotation = _entity.GlobalPosition.DirectionTo(_player.GlobalPosition).Angle();
		_weapon.Shoot();
	}

	private void Roam()
	{
		return;
	}

	public override void _Process(double delta)
	{
		// could be more efficient and called in listeners instead
		switch (_state)
		{
			case States.ROAM:
				Roam();
				break;
			case States.CHASE:
				Chase();
				break;
			default:
				GD.Print("Enemy state does not exist");
				break;
		}
	}

}

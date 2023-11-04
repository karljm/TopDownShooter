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
		INVALID,
	}
	private Area2D _playerDetectionZone;
	private Timer _roamTimer;

	private States _state = States.INVALID;
	private int _currentState;
	private Enemy _entity = null;
	private Node2D _player = null;
	private Weapon _weapon = null;

	// ROAM STATE
	private Vector2 _origin;
	private Vector2 _roamLocation = Vector2.Zero;
	private Vector2 _entityVelocity = Vector2.Zero;
	private bool _roamLocationReached = false;
	public override void _Ready()
	{
		_currentState = (int)States.ROAM;
		_playerDetectionZone = GetNode<Area2D>("PlayerDetectionZone");
		_roamTimer = GetNode<Timer>("RoamTimer");
		_origin = Vector2.Zero;
		State = States.ROAM;

	}

	public States State
	{
		get { return _state; }
		set
		{
			if (value == _state)
				return;
			_state = value;

			if (value == States.ROAM)
			{
				_origin = GlobalPosition;
				_roamTimer.Start();
				_roamLocationReached = true;
			}
			EmitSignal(SignalName.StateChanged, (int)_state);
		}
	}

	// public int CurrentState
	// {
	// 	get
	// 	{
	// 		return _currentState;
	// 	}

	// 	set
	// 	{
	// 		if (value == _currentState)
	// 			return;

	// 		_currentState = value;

	// 		EmitSignal(SignalName.StateChanged, _currentState);
	// 	}
	// }

	private void SetWeapon(Weapon weapon)
	{
		_weapon = weapon;
	}


	public void Initialise(Enemy entity, Weapon weapon)
	{
		_entity = entity;
		SetWeapon(weapon);
	}

	public void OnPlayerDetectionZoneBodyEntered(Node2D body)
	{
		if (body.IsInGroup("PlayerGroup"))
		{
			GD.Print("player in zone");
			// CurrentState = (int)States.CHASE;
			State = States.CHASE;
			_player = body;
		}
	}

	public void OnPlayerDetectionZoneBodyExited(Node2D body)
	{
		// if (body.IsInGroup("PlayerGroup"))
		// {
		// 	GD.Print("player exits zone");
		// 	CurrentState = (int)States.ROAM;
		// 	State = States.ROAM;
		// 	_player = null;
		// }
		if ((_player != null) && (body == _player))
		{
			// CurrentState = (int)States.ROAM;
			State = States.ROAM;
			_player = null;
		}
	}

	public void OnRoamTimerTimeout()
	{
		double roamRange = 50;
		float randomX = (float)GD.RandRange(-roamRange, roamRange);
		float randomY = (float)GD.RandRange(-roamRange, roamRange);
		_roamLocation = new Vector2(randomX, randomY) + _origin;
		_roamLocationReached = false;
		_entityVelocity = _entity.VelocityToward(_roamLocation);
		_entity.Velocity = _entityVelocity;

	}

	private void Chase()
	{
		if (_player == null && _weapon == null)
		{
			GD.Print("Enemy in engage state, but no weapon or player");
			return;
		}
		float angleToPlayer = _entity.GlobalPosition.DirectionTo(_player.GlobalPosition).Angle();
		// _entity.Rotation = Mathf.Lerp(_entity.Rotation, angleToPlayer, 0.1f);
		_entity.RotateToward(_player.GlobalPosition);
		if (Mathf.Abs(_entity.Rotation - angleToPlayer) < 0.1)
		{
			_weapon.Shoot();
		}
	}

	private void Roam()
	{
		if (_roamLocationReached)
			return;
		_entity.MoveAndSlide();
		_entity.RotateToward(_roamLocation);
		if (_entity.GlobalPosition.DistanceTo(_roamLocation) < 5)
		{
			_roamLocationReached = true;
			// _entityVelocity = Vector2.Zero;
			_entity.Velocity = Vector2.Zero;
			_roamTimer.Start();
		}



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

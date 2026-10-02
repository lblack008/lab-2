using Godot;
using System;

[GlobalClass]
public partial class Bat : Enemy
{
	[Export]
	private Vector2 _amplitudeRange = new Vector2(50f, 150f);
	
	[Export]
	private float _spawnX = 1300f;
	
	[Export]
	private Vector2 _spawnYRange = new Vector2(0f, 400f);

	[Export]
	private float _verticalSpeed = 2f;

	private float _amplitude;
	private float _offset;

	//начальные параметры мыши
    protected override void Setup()
    {
        _amplitude = (float)GD.RandRange (
			_amplitudeRange.X,
			_amplitudeRange.Y
		);
    }

	//определение точки спавна
	public override Vector2 GetSpawnPosition()
	{
		return new Godot.Vector2(
			_spawnX,
			(float)GD.RandRange(
				_spawnYRange.X,
				_spawnYRange.Y
			)
		);
	}

	//переопределние логики движения (по синусоиде)
	protected override void Move(float delta)
	{
		_offset += _verticalSpeed * delta;
		Position += new Vector2(
			velocity.X * delta,
			Mathf.Sin(_offset) * _amplitude * delta
		);
	}
}

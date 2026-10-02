using Godot;
using System;

public partial class Field : Node2D
{
	private Timer _timer;
	private RandomNumberGenerator _random = new RandomNumberGenerator();

	[Export]
	private Godot.Collections.Array<PackedScene> _objects = new Godot.Collections.Array<PackedScene>();


	//генерация рандомный чисел и получение таймера
    public override void _Ready()
    {
        _random.Randomize();
		_timer = GetNode<Timer>("Timer");
	}
	
	//спавн врагов через определённое время
	private void _On_Spawn_Timer_Timeout()
	{
		SpawnEnemy();
	}

	//спавн врагов случайным образом
	private void SpawnEnemy()
	{
		int RandomIndex = _random.RandiRange(0, _objects.Count-1);

		//сообщение в консоль, что был заспавнен враг
		GD.Print($"Spawn enemy: {_objects[RandomIndex].ResourcePath}");
		DamagingObject obj = (DamagingObject)_objects[RandomIndex].Instantiate();
		
		obj.Position = obj.GetSpawnPosition();
		AddChild(obj);
	}
}

using Godot;
using System;

[GlobalClass]
public partial class Rock : Obstacle
{
	[Export]
	private float _spawnX = 1300f;
	[Export]
	private float _spawnY = 511f;

    //задание точки спавна
    public override Vector2 GetSpawnPosition()
    {
        return new Vector2(_spawnX, _spawnY);
    }
}

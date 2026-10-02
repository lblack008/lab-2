using Godot;
using System;

public partial class Enemy : DamagingObject
{
	[Export]
	protected float Health = 30f;
	public void TakeDamage(float damage)
	{
		Health -= damage;
		//вывод в консоль информации об оставшемся здоровье врага
		GD.Print($"enemy took {damage} damage. Heath: {Health}");

		if (Health <= 0)
		{
			Die();
		}
	}

	//реализация смерти врага
	protected void Die()
	{
		//сообщение в консоль, что враг умер
		GD.Print("Enemy died.");
		QueueFree();
	}
}

using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	private float _damage = 10f;

	//обработка движения пули
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	//обработка столкновений пули с другими объектами
	private void _on_area_2d_area_entered(Area2D area)
	{
		//сообщение в консоль для пояснения, с чем столкнулась пуля
		GD.Print("Bullet collided with: " + area.Name);
		if (area is Enemy)
		{
			Enemy enemy = (Enemy)area;
			enemy.TakeDamage(_damage);
			QueueFree();
		}
		else if (area is DamagingObject)
		{
			QueueFree();
		}
	}

	//удаление пули при выходе за границы экрана
	private void _On_Screen_Leave()
	{
		QueueFree();
	}

	//сеттер для изменения урона пули
	public void SetDamage(float damage)
	{
		_damage = damage;
	}
}

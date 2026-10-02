using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private float _gravity = 980f;
    [Export]
    private float _jumpVelocity {get; set; } = -800f;
    [Export]
    private float _reloadTime {get; set; } = 0.5f;
    [Export]
    private float _bulletSpeed = 500f;
    [Export]
    private float _bulletDamage = 10f;
    private Vector2 _velocity;
    private PackedScene _bulletScene = GD.Load<PackedScene>("res://scenes/Bullet.tscn");

    //реализация прыжков игрока
    public override void _PhysicsProcess(double delta)
    {
        _velocity = Velocity;

        if (!IsOnFloor())
        {
            _velocity.Y += _gravity * (float)delta;

        }

        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            _velocity = Jump(_velocity);
        }
        Velocity = _velocity;
        MoveAndSlide();
    }

    //вычисление вектора для прыжков
    private Vector2 Jump(Vector2 velocity)
    {
        return new Vector2(0, _jumpVelocity);
    }

    //обработка ввода
    public override void _Input(InputEvent @event)
    {
        //выстрел по ЛКМ
        if (@event.IsActionPressed("shoot"))
        {
            Shoot();
        }
        //выход игры по Escape
        if (@event.IsActionPressed("exit"))
        {
            GetTree().Quit();
        }
    }

    //реализация стельбы
    private void Shoot()
    {
        //сообщение в консоль, что игрок стреляет
        GD.Print("Shooting");

        //инстанцирование пули
        Bullet bullet = (Bullet)_bulletScene.Instantiate();
        AddSibling(bullet);

        //задание позиции пули
        bullet.Position = Position + new Vector2(50, 0);
        bullet.Velocity = new Vector2(_bulletSpeed, 0);
        bullet.SetDamage(_bulletDamage);
    }
}

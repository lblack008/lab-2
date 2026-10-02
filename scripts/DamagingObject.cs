using Godot;
using System;

[GlobalClass]
public partial class DamagingObject : Area2D
{
	[Export]
	protected Vector2 velocity = new Vector2(-300f, 0);

	//обработка позиции объекта
    public override void _Process(double delta)
    {
        Move((float)delta);
    }

	//перемещение объекта влево
	protected virtual void Move(float delta)
	{
		Position += velocity * delta;
	}

	//инициализация объекта и подключение коллизии
    public override void _Ready()
    {
		BodyEntered += OnBodyEntered;
		
        VisibleOnScreenNotifier2D notifier = new VisibleOnScreenNotifier2D();
		AddChild(notifier);
		notifier.Connect(
			"screen_exited",
			new Callable(
				this, "_On_Screen_Exited"
			)
		);
		Setup();
    }

	//обработка столкновения с игроком
	 private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
			//вывод в консоль информации о том, с каким объектом столкнулся игрок
            GD.Print($"Player hit by {Name}!");
            //CallDeferred обрабатывает ошибку, возникающую при обычном вызове QueueFree()
			//ReloadCurrentScene прописан для того, чтобы после смерти игрока раннер запускался снова. Так он будет бесконечным
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene); 
        }
    }

	//заглушка
	protected virtual void Setup()
	{}

	//удаление объекта при выходе за пределы экрана
	private async void _On_Screen_Exited()
	{
		Timer timer = new Timer();
		timer.WaitTime = 1f;

		timer.OneShot = true;
		AddChild(timer);
		timer.Start();
		await ToSignal(timer, "timeout");

		//вывод в консоль информации о том, что объект покинул игровое поле
		GD.Print($"{Name} has exited the scene.");
		QueueFree();
	}

	//получение позиции для спавна
	public virtual Vector2 GetSpawnPosition()
	{
		return new Vector2(0, 0);
	}
}

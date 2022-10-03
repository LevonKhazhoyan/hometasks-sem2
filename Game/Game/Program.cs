using Game;

Console.CursorVisible = false;

var game = new Game.Game("..//..//..//Map.txt", Console.Write, Console.SetCursorPosition);

var eventLoop = new EventLoop();
eventLoop.LeftHandler += game.OnLeft;
eventLoop.RightHandler += game.OnRight;
eventLoop.UpHandler += game.OnUp;
eventLoop.DownHandler += game.OnDown;

game.DrawMap();

eventLoop.Run();
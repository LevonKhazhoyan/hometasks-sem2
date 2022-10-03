namespace GameUnitTests;

using Game;
using NUnit.Framework;

public class GameTests
{
    private Game game;

    private readonly bool[,] expectedMap =
    {
        {true, true, false, true, true },
        {false, false, false, false, false },
        {true, false, false, false, true },
        {true, false, false, true, true }
    };

    [SetUp]
    public void Setup()
    {
        game = new Game("..//..//..//TestMap.txt", line => { }, (x, y) => { });
    }

    [Test]
    public void GameInitializeTest()
    {
        Assert.AreEqual(expectedMap, game.BoolMap);
        Assert.AreEqual((2, 2), game.PlayerPosition);
        Assert.AreEqual(5, game.Width);
        Assert.AreEqual(4, game.Height);
    }

    [TestCaseSource(nameof(FilesWithInvalidData))]
    public void GameInitializeShouldThrowExceptionIfMapIsInvalid(string filePath)
    {
        Assert.Throws<ArgumentException>(() => new Game(filePath, line => { }, (x, y) => { }));
    }

    [TestCaseSource(nameof(PlayerMovementDataCases))]
    public void PlayerMovementTest(Action<Game> moveFunction, (int, int) expectedPlayerCoordinates)
    {
        moveFunction(game);
        Assert.AreEqual(expectedPlayerCoordinates, game.PlayerPosition);
    }

    private static IEnumerable<string> FilesWithInvalidData()
    {
        yield return "..//..//..//TestNoPlayer.txt";
        yield return "..//..//..//TestMoreThanOnePlayer.txt";
        yield return "..//..//..//WrongName.txt";
        yield return "..//..//..//EmptyMap.txt";
    }

    private static IEnumerable<object[]> PlayerMovementDataCases()
    {
        yield return new object[] { new Action<Game>(game => game.OnLeft(null, EventArgs.Empty)), (1 ,2) };
        yield return new object[] { new Action<Game>(game => game.OnRight(null, EventArgs.Empty)), (3, 2) };
        yield return new object[] { new Action<Game>(game => game.OnUp(null, EventArgs.Empty)), (2, 1) };
        yield return new object[] { new Action<Game>(game => game.OnDown(null, EventArgs.Empty)), (2, 3) };
        yield return new object[] { new Action<Game>(game => { game.OnLeft(null, EventArgs.Empty);
                                                               game.OnLeft(null, EventArgs.Empty); }), (1, 2) };
        yield return new object[] { new Action<Game>(game => { game.OnDown(null, EventArgs.Empty);
                                                               game.OnDown(null, EventArgs.Empty); }), (2, 0) };
        yield return new object[] { new Action<Game>(game => { game.OnUp(null, EventArgs.Empty);
                                                               game.OnLeft(null, EventArgs.Empty);
                                                               game.OnLeft(null, EventArgs.Empty);
                                                               game.OnLeft(null, EventArgs.Empty); }), (4, 1) };
    }
}
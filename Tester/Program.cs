using Dominó;

namespace Tester
{
   

    class MyProgram
    {
        static void Main(string[] args)
        {
            Game x = new Game(new DoubleSixInToken(), new GetRandomFirstPlayer(), new RadomTokenDistributor(), new CommonEnd(), new List<IPlayer> { new HighScoreDropperPlayer(1), new HighScoreDropperPlayer(2), new HighScoreDropperPlayer(3), new HighScoreDropperPlayer(4) });
            Console.WriteLine(x.Run());
        }
    }
}

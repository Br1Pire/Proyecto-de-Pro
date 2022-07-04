



namespace Dominó
{

    // pa comentar

    public class Selector
    {
        public int FancySelector(string initialMessage, string[] states)
        {
            int stateSelector = 0;
            while (true)
            {
                Console.WriteLine(initialMessage);
                Console.WriteLine(states[stateSelector]);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return stateSelector;
                }
                if (key.Key == ConsoleKey.RightArrow && stateSelector == states.Length - 1)
                {
                    Console.Clear();
                    continue;
                }
                if (key.Key == ConsoleKey.LeftArrow && stateSelector == 0)
                {
                    Console.Clear();
                    continue;
                }

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        stateSelector++;
                        break;

                    case ConsoleKey.LeftArrow:
                        stateSelector--;
                        break;
                }
                Console.Clear();
            }
        }
    }

    
    public class Game
    {
        List<IPlayer> players;
        TokenAmountGenerator tokenGenerator;
        TurnOrderSelector turnSelector;
        TokensDistributor tokenDistributor;
        GameEnder gameEnder;
        List<Token> allTokens;
        List<Token> Table;
        Token lastTokenPlayed;
        bool gameOver;
        int turn;
        public bool GameOver
        {
            get { return gameOver; }
        }

        public string TableStatus()
        {
            string ret = "";
            for (int i = 0; i < Table.Count; i++)
            {
                ret += Table[i].ToString();
            }
            return ret;
        }

        public Game(TokenAmountGenerator tokenGenerator, TurnOrderSelector turnSelector, TokensDistributor tokenDistributor, GameEnder gameEnder, List<IPlayer> players)
        {
            this.players = players;
            this.tokenGenerator = tokenGenerator;
            this.turnSelector = turnSelector;
            this.tokenDistributor = tokenDistributor;
            this.gameEnder = gameEnder;
            this.Table = new List<Token>();
            turn = 0;
            gameOver = false;
            lastTokenPlayed = null;
            allTokens = tokenGenerator.GenerateTokens();
            turnSelector.GetTurnOrder(players);
            tokenDistributor.DistributeTokens(allTokens, players);
        }

        public string Run()
        {
            string ret = "";
            if (turn == players.Count) turn = 0;

            int endGame = gameEnder.CheckIfTheGameIsOver(players);
            if (endGame > 0)
            {
                
                gameOver = true;
                return "El Jugador " + endGame + " ha ganado";
            }

            lastTokenPlayed = players[turn].Play( Table);
            
            if (lastTokenPlayed == null) ret = "Turno del Jugador " + players[turn].GetPlayerNumber + "\n" + "El Jugador se ha pasado " + " \n" + "Tablero: " + TableStatus();

            else ret = "Turno del Jugador " + players[turn].GetPlayerNumber + "\n" + "El Jugador ha jugado " + lastTokenPlayed + " \n" + "Tablero: " + TableStatus();

            turn++;
            return ret;


        }


       



    }
}
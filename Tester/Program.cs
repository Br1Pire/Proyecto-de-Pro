using Dominó;

namespace ConsoleInterface
{
    //En esta parte del codigo va a estar todo lo relacionado con la interfaz del juego
    class VisualInterface
    {
        List<IPlayer> players = new List<IPlayer>();
        TokenAmountGenerator tokenGenerator;
        TurnOrderSelector turnSelector;
        TokensDistributor tokensDistributor;
        GameEnder gameEnder;
        List<Token> allTokens;
        Selector selector = new Selector();
        string[] message = new string[6];
        Game game;

        public void RunVisual()
        {
            InicialiceGame();

            while (!game.GameOver)
            {
                Console.WriteLine(game.Run()+"\n");
                Thread.Sleep(1000);
               // Console.ReadLine();
            }
            Console.WriteLine("\nToque Esc para cerrar la consola");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {

            }
        }

        public void InicialiceGame()
        {
            bool final = false;

            while (!final)
            {
                int selectorValue = selector.FancySelector(PrintMessage() + "\n\n\nSeleccione la configuracion del juego", new string[] 
                {
                    "=>Jugadores\n  Tipo de ficha y juego\n  Selector de Turnos\n  Distribución de fichas\n  Finalización de juego\n  Comenzar Juego" ,
                    "  Jugadores\n=>Tipo de ficha y juego\n  Selector de Turnos\n  Distribución de fichas\n  Finalización de juego\n  Comenzar Juego" ,
                    "  Jugadores\n  Tipo de ficha y juego\n=>Selector de Turnos\n  Distribución de fichas\n  Finalización de juego\n  Comenzar Juego",
                    "  Jugadores\n  Tipo de ficha y juego\n  Selector de Turnos\n=>Distribución de fichas\n  Finalización de juego\n  Comenzar Juego",
                    "  Jugadores\n  Tipo de ficha y juego\n  Selector de Turnos\n  Distribución de fichas\n=>Finalización de juego\n  Comenzar Juego",
                    "  Jugadores\n  Tipo de ficha y juego\n  Selector de Turnos\n  Distribución de fichas\n  Finalización de juego\n=>Comenzar Juego" 
                });

                switch (selectorValue)
                {
                    case 0:
                        SelectPlayers();
                        break;
                    case 1:
                        SelectTokenGenerator();
                        break;
                    case 2:
                        SelectTurnSelector();
                        break;
                    case 3:
                        SelectTokenDistributor();
                        break;
                    case 4:
                        SelectGameEnder();
                        break;
                    case 5:
                        if (players.Count == 0 || tokenGenerator == null || turnSelector == null || tokensDistributor == null || gameEnder == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Faltan opciones por seleccionar");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        game = new Game(tokenGenerator, turnSelector, tokensDistributor, gameEnder, players);
                        final = true;
                        break;
                }
            }


        }

        public void SelectPlayers()
        {

            int selectorValue = selector.FancySelector("Seleccione el número de jugadores que desea que participen en el juego", new string[] { "=>2     4", "  2   =>4" });

            int amountOfPlayers = -1;
            switch (selectorValue)
            {
                case 0:
                    amountOfPlayers = 2;
                    break;

                case 1:
                    amountOfPlayers = 4;
                    break;
            }

            int count = 1;
            string messagee = "Seleccione un tipo de jugador que desee incluir en el juego \n\nYa ha seleccionado:";
            string retMessage = "";
            players = new List<IPlayer>();

            while (count <= amountOfPlayers)
            {
                selectorValue = selector.FancySelector(messagee, new string[] { "=>Bota Gorda     BotaFlaca     Random", "  Bota Gorda   =>BotaFlaca     Random", "  Bota Gorda     BotaFlaca   =>Random" });

                switch (selectorValue)
                {
                    case 0:
                        players.Add(new HighScoreDropperPlayer(count));
                        string x = "\n- Jugador" + count + " Bota Gorda";
                        messagee += x;
                        retMessage += x;
                        count++;
                        break;

                    case 1:
                        players.Add(new LowScoreDropperPlayer(count));
                        string y = "\n- Jugador" + count + " Bota Flaca";
                        messagee += y;
                        retMessage += y;
                        count++;
                        break;

                    case 2:
                        players.Add(new RandomPlayer(count));
                        string z = "\n- Jugador" + count + " Random";
                        messagee += z;
                        retMessage += z;
                        count++;
                        break;

                }
            }
            message[0] = retMessage;
        }

        public void SelectTokenGenerator()
        {
            int selectorValue = selector.FancySelector("Seleccione el tipo de fichas que desea que utilice su juego\n\n", new string[] { "=>Ficha Común     Ficha Animal", "  Ficha Común   =>Ficha Animal" });

            if (selectorValue == 0)
            {
                message[1] = "Ficha Común";
                selectorValue = selector.FancySelector("Seleccione que tipo de juego desea jugar respecto al número de fichas", new string[] { "=>Doble seis     Doble nueve", "  Doble seis   =>Doble nueve" });

                switch (selectorValue)
                {
                    case 0:
                        tokenGenerator = new DoubleSixInToken();
                        message[2] = "Doble seis";
                        break;

                    case 1:
                        tokenGenerator = new DoubleNineInToken();
                        message[2] = "Doble nueve";
                        break;

                }
            }
            else if (selectorValue == 1)
            {
                message[1] = "Ficha Animal";
                selectorValue = selector.FancySelector("Seleccione que tipo de juego desea jugar respecto al número de fichas", new string[] { "=>Doble seis     Doble nueve", "  Doble seis   =>Doble nueve" });

                switch (selectorValue)
                {
                    case 0:
                        tokenGenerator = new DoubleSixAnimalToken();
                        message[2] = "Doble seis";
                        break;

                    case 1:
                        tokenGenerator = new DoubleNineAnimalToken();
                        message[2] = "Doble nueve";
                        break;

                }
            }
        }

        public void SelectTurnSelector()
        {
            int selectorValue = selector.FancySelector("Seleccione el método para asignar los turnos", new string[] { "=>Primer jugador Random     Todos los jugadores Random", "  Primer jugador Random   =>Todos los jugadores Random" });

            switch (selectorValue)
            {
                case 0:
                    turnSelector = new GetRandomFirstPlayer();
                    message[3] = "Primer jugador Random";
                    break;

                case 1:
                    turnSelector = new GetAllPLayersRandom();
                    message[3] = "Todos los jugadores Random";
                    break;
            }
        }

        public void SelectTokenDistributor()
        {
            int selectroValue = selector.FancySelector("Seleccione la manera de repartir las fichas", new string[] { "=>Random     Por Seleccion", "  Random   =>Por Seleccion" });

            switch (selectroValue)
            {
                case 0:
                    tokensDistributor = new RadomTokenDistributor();
                    message[4] = "Random";
                    break;

                case 1:
                    tokensDistributor = new SelectedTokenDistributor();
                    message[4] = "Por Seleccion";
                    break;
            }
        }

        public void SelectGameEnder()
        {
            int selectorValue = selector.FancySelector("Seleccione commo desea que finalice cada partida", new string[] { "=>Común     Por expulsion", "  Común   =>Por expulsion" });
            switch (selectorValue)
            {
                case 0:
                    gameEnder = new CommonEnd();
                    message[5] = "Común";
                    break;

                case 1:
                    gameEnder = new ExpelEnd();
                    message[5] = "Por expulsion";
                    break;
            }
        }

        public string PrintMessage()
        {
            return ("Sus selecciones son: \n\nLista de jugaderes:\n" + message[0] + "\n\nTipo de ficha:\n-" + message[1] + "\nTipo de juego:\n-" + message[2] + "\nMétodo para asignar los trunos:\n-" + message[3] + "\nManera de repartir las fichas:\n-" + message[4] + "\nFinalización del juego:\n-" + message[5] + "\n");
        }
    }

    class Printer
    {

        static void Main(string[] args)
        {
            VisualInterface x = new VisualInterface();
            x.RunVisual();
        }
    }
}

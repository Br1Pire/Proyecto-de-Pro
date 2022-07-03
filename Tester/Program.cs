using Dominó;

namespace ConsoleInterface
{




    class Printer
    {
        public static int FancySelector(string initialMessage, string[] states)
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
        static void Main(string[] args)
        {



            bool process = true;
            int number = 1;
            List<IPlayer> players = new List<IPlayer>();
            string[] states = { "=>Bota Gorda     BotaFlaca     Random     Finalizar", "  Bota Gorda   =>BotaFlaca     Random     Finalizar", "  Bota Gorda     BotaFlaca   =>Random     Finalizar", "  Bota Gorda     BotaFlaca     Random   =>Finalizar" };

            int stateSelector = -1;
            while (process)
            {
                stateSelector = FancySelector("Añada un jugador", states);
                //while (true)
                //{
                //    Console.WriteLine("Añada un jugador");
                //    Console.WriteLine(states[stateSelector]);
                //    ConsoleKeyInfo key = Console.ReadKey();
                //    if (key.Key == ConsoleKey.Enter)
                //    {
                //        Console.Clear();
                //        break;
                //    }
                //    if (key.Key == ConsoleKey.RightArrow && stateSelector == 3)
                //    {
                //        Console.Clear();
                //        continue;
                //    }
                //    if (key.Key == ConsoleKey.LeftArrow && stateSelector == 0)
                //    {
                //        Console.Clear();
                //        continue;
                //    }

                //    switch (key.Key)
                //    {
                //        case ConsoleKey.RightArrow:
                //            stateSelector++;
                //            break;

                //        case ConsoleKey.LeftArrow:
                //            stateSelector--;
                //            break;
                //    }
                //    Console.Clear();
                //}


                switch (stateSelector)
                {
                    case 0:
                        players.Add(new HighScoreDropperPlayer(number));
                        number++;
                        break;

                    case 1:
                        players.Add(new LowScoreDropperPlayer(number));
                        number++;
                        break;

                    case 2:
                        players.Add(new RandomPlayer(number));
                        number++;
                        break;

                    case 3:
                        process = false;
                        break;

                }

            }


            int tokenType = FancySelector("Seleccione el tipo de Ficha que desea usar", new string[] { "=>Ficha Común     Ficha Animal", "  Ficha Común   =>Ficha Animal" });
            TokenAmountGenerator generator=null;

            if (tokenType == 0)
            {
                stateSelector = FancySelector("Seleccione que tipo de juego desea jugar respecto al número de fichas", new string[] { "=>Doble seis     Doble nueve", "  Doble seis   =>Doble nueve" });

                switch (stateSelector)
                {
                    case 0:
                        generator = new DoubleSixInToken();
                        break;

                    case 1:
                        generator = new DoubleNineInToken();
                        break;

                }
            }
            else if (tokenType == 1)
            {
                stateSelector = FancySelector("Seleccione que tipo de juego desea jugar respecto al número de fichas", new string[] { "=>Doble seis     Doble nueve", "  Doble seis   =>Doble nueve" });

                switch (stateSelector)
                {
                    case 0:
                        generator = new DoubleSixAnimalToken();
                        break;

                    case 1:
                        generator = new DoubleNineAnimalToken();
                        break;

                }
            }

            stateSelector = FancySelector("Seleccione el método para asignar los turnos", new string[] { "=>Primer jugador Random     Todos los jugadores Random", "  Primer jugador Random   =>Todos los jugadores Random" });
            TurnOrderSelector turnSelector=null;
            switch (stateSelector)
            {
                case 0:
                    turnSelector = new GetRandomFirstPlayer();
                    break;

                case 1:
                    turnSelector = new GetAllPLayersRandom();
                    break;
            }

            stateSelector = FancySelector("Seleccione la manera de repartir las fichas", new string[] { "=>Random     Por Seleccion", "  Random   =>Por Seleccion" });
            TokensDistributor tokensDistributor=null;
            switch (stateSelector)
            {
                case 0:
                    tokensDistributor = new RadomTokenDistributor();
                    break;

                case 1:
                    tokensDistributor = new SelectedTokenDistributor();
                    break;
            }

            stateSelector = FancySelector("Seleccione commo desea que finalice cada partida", new string[] { "=>Común     Por expulsion", "  Común   =>Por expulsion" });
            GameEnder gameEnder=null;
            switch (stateSelector)
            {
                case 0:
                    gameEnder = new CommonEnd();
                    break;

                case 1:
                    gameEnder = new ExpelEnd();
                    break;
            }


            Console.WriteLine("Su juego comenzara cuando toque enter");
            Console.ReadLine();

            Game game = new Game(generator, turnSelector, tokensDistributor, gameEnder, players);

           while (!game.GameOver)
            {
                Console.WriteLine(game.Run());
                Console.ReadLine();
            }

            










        }
    }
}

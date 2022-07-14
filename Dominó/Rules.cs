
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    // pa commentar menos generadores de Fichas

    #region(Generadores de Fichas)
    public interface TokenAmountGenerator
    {
        public List<Token> GenerateTokens();


    }

    public class DoubleSixInToken : TokenAmountGenerator
    {


        public List<Token> GenerateTokens()
        {
            List<Token> ret = new List<Token>();
            int index = 0;
            for (int i = 0; i <= 6; i++)
            {
                for (int j = index; j <= 6; j++)
                {
                    ret.Add(new InToken(i, j));
                }

                index++;
            }
            return ret;

        }
    }

    public class DoubleSixAnimalToken : TokenAmountGenerator
    {
        public List<Token> GenerateTokens()
        {
            List<Token> ret = new List<Token>();
            int index = 0;
            for (int i = 0; i <= 6; i++)
            {
                for (int j = index; j <= 6; j++)
                {
                    ret.Add(new AnimalToken(i, j));
                }

                index++;
            }
            return ret;

        }
    }

   public class DoubleNineInToken : TokenAmountGenerator
    {


        public List<Token> GenerateTokens()
        {
            List<Token> ret = new List<Token>();
            int index = 0;
            for (int i = 0; i <= 6; i++)
            {
                for (int j = index; j <= 9; j++)
                {
                    ret.Add(new InToken(i, j));
                }

                index++;
            }
            return ret;

        }
    }

   public class DoubleNineAnimalToken : TokenAmountGenerator
    {
        public List<Token> GenerateTokens()
        {
            List<Token> ret = new List<Token>();
            int index = 0;
            for (int i = 0; i <= 9; i++)
            {
                for (int j = index; j <= 6; j++)
                {
                    ret.Add(new AnimalToken(i, j));
                }

                index++;
            }
            return ret;

        }
    }

    #endregion

    #region(Selector de Turnos)
    public interface TurnOrderSelector
    {
        public void GetTurnOrder(List<IPlayer> players);

    }

    public class GetRandomFirstPlayer : TurnOrderSelector
    {
        public void GetTurnOrder(List<IPlayer> players)
        {
            List<IPlayer> ret = ((IPlayer[])players.ToArray().Clone()).ToList();
            Random x = new Random();
            Random y = new Random(x.Next());

            int index = y.Next(players.Count);
            int count = 0;

            for (int i = index; i < ret.Count; i++)
            {
                players[count] = ret[i];
                count++;
            }
            for (int i = 0; i < index; i++)
            {
                players[count] = ret[i];
                count++;
            }

            
        }
    }

    public class GetAllPLayersRandom : TurnOrderSelector
    {
        public void GetTurnOrder(List<IPlayer> players)
        {
            List<IPlayer> ret = ((IPlayer[])players.ToArray().Clone()).ToList();

            Random x = new Random();
            Random y = new Random(x.Next());


            for (int i = 0; i < players.Count; i++)
            
            {
                int index = y.Next(ret.Count);
                players[i]=(ret[index]);
                ret.RemoveAt(index);

            }
        }
    }

    #endregion

    #region(Repartidor de tokens)
    public interface TokensDistributor
    {
        public void DistributeTokens(List<Token> tokens, List<IPlayer> players);
    }

    public class RadomTokenDistributor : TokensDistributor
    {
        public void DistributeTokens(List<Token> tokens, List<IPlayer> players)
        {
            List<Token> aux = ((Token[])tokens.ToArray().Clone()).ToList();

            Random x = new Random();
            Random y = new Random(x.Next());
            int maxAmountTokens = tokens[tokens.Count - 1].Higher() + 1;

            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < maxAmountTokens; j++)
                {
                    int index = y.Next(aux.Count);
                    players[i].GetHand.Add(aux[index]);
                    aux.RemoveAt(index);
                }
            }
        }
    }

    public class SelectedTokenDistributor : TokensDistributor 
    {
        public void DistributeTokens(List<Token> tokens, List<IPlayer> players)
        {
            int maxAmountTokens= tokens[tokens.Count - 1].Higher() + 1;
            List<Token> aux = ((Token[])tokens.ToArray().Clone()).ToList();
            Selector selector = new Selector();

            for (int i = 0; i <players.Count; i++)
            {
                int count = 0;
                string status = "Fichas seleccionadas para el Jugador " + players[i].GetPlayerNumber+":\n\n";
                while (count < maxAmountTokens)
                {
                    //Console.WriteLine(status);
                    int index = selector.FancySelector(status+"\n\n"+"Seleccione una de las siguientes Fichas para Player " + players[i].GetPlayerNumber, CreateStates(aux));
                    players[i].GetHand.Add(aux[index]);
                    status+=aux[index]+"  ";
                    aux.RemoveAt(index);
                    count++;
                }
            
            
            }
        }
        string[] CreateStates(List<Token>tokens)
        {
            string[] ret= new string[tokens.Count];
            for (int i = 0; i < ret.Length; i++)
            {
                for (int j = 0; j < ret.Length; j++)
                {
                    if (i == j) ret[i] += "=>" + tokens[j] + "\n";

                    else ret[i] += "  " + tokens[j] + "\n";
                }
            }
            return ret;
        }
    }
    
    #endregion

    #region(Finalizacion del juego)
    public interface GameEnder
    {
        public int CheckIfTheGameIsOver(List<IPlayer> players);


    }

    public class CommonEnd : GameEnder
    {
        public int CheckIfTheGameIsOver(List<IPlayer> players)
        {
            int count = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetHand.Count() == 0) return players[i].GetPlayerNumber;
                if (players[i].GetContinuesTimesPassed > 0) count++;
            }
            if (count == players.Count)
            {
                int index = 0;
                int memory = int.MinValue;
                for (int i = 0; i < players.Count; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < players[i].GetHand.Count; j++)
                    {
                        sum += players[i].GetHand[j].Score;
                    }
                    if (sum > memory)
                    {
                        memory = sum;
                        index = i;
                    }
                }
                return players[index].GetPlayerNumber;
            }
            return 0;
        }
    }

    public class ExpelEnd : GameEnder
    {
        public int CheckIfTheGameIsOver(List<IPlayer> players)
        {
            int amount = 0;
            foreach (IPlayer p in players)
            {
                if (p != null) amount++;
            }

            if (amount == 1) return players[0].GetPlayerNumber;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null) continue;
                if (players[i].GetContinuesTimesPassed == 2)
                {
                    Console.WriteLine("\nEl Jugador" + players[i].GetPlayerNumber + " ha sido expulsado\n");
                    players[i] = null;
                }

            }
            int count = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null) continue;
                if (players[i].GetHand.Count() == 0) return players[i].GetPlayerNumber;
                if (players[i].GetContinuesTimesPassed > 0) count++;
            }
            
            if (count == amount)
            {
                int index = 0;
                int memory = int.MinValue;
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i] == null) continue;
                    int sum = 0;
                    for (int j = 0; j < players[i].GetHand.Count; j++)
                    {
                        sum += players[i].GetHand[j].Score;
                    }
                    if (sum > memory)
                    {
                        memory = sum;
                        index = i;
                    }
                }
                return players[index].GetPlayerNumber;
            }
            return 0;

        }

    }
    #endregion

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{

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
            List<IPlayer> ret = new List<IPlayer>();

            Random x = new Random();
            Random y = new Random(x.Next());


            while (players.Count > 0)
            {
                int index = y.Next(players.Count);
                ret.Add(players[index]);
                players.RemoveAt(index);

            }

            players = ret;
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

    public class SelectedTokenDistributor : TokensDistributor //hay q implementar
    {
        public void DistributeTokens(List<Token> tokens, List<IPlayer> players)
        {
            throw new NotImplementedException();
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
                if (players[i].GetHand.Count() == 0) return i + 1;
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
                return index + 1;
            }
            return 0;
        }
    }

    public class ExpelEnd : GameEnder
    {
        public int CheckIfTheGameIsOver(List<IPlayer> players)
        {
            if (players.Count == 1) return 1;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetContinuesTimesPassed == 2) players.RemoveAt(i);
            }
            int count = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetHand.Count() == 0) return i + 1;
                if (players[i].GetContinuesTimesPassed > 0) count++;
            }
            if (count == players.Count - 1)
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
                return index + 1;
            }
            return 0;

        }

    }
    #endregion

}

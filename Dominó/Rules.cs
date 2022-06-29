
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{

    #region(Generadores de Fichas)
    interface TokenAmountGenerator
    {
        public List<Token> GenerateTokens();
        

    }

    class DoubleSixInToken : TokenAmountGenerator
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

    class DoubleSixAnimalToken : TokenAmountGenerator
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

    class DoubleNineInToken : TokenAmountGenerator
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

    class DoubleNineAnimalToken : TokenAmountGenerator
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
    interface TurnOrderSelector
    {
        public void GetTurnOrder(List<IPlayer> players);

    }

    class GetRandomFirstPlayer : TurnOrderSelector
    {
        public void GetTurnOrder(List<IPlayer> players)
        {
            List<IPlayer> ret = new List<IPlayer>();
            Random x = new Random();
            Random y = new Random(x.Next());

            int index = y.Next(players.Count);

            for (int i = index; i < players.Count; i++)
            {
                ret.Add(players[i]);
            }
            for (int i = 0; i < index; i++)
            {
                ret.Add(players[i]);
            }

            players = ret;
        }
    }

    class GetAllPLayersRandom : TurnOrderSelector
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
    interface TokensDistributor
    {
        public void DistributeTokens(List<Token> tokens, List<List<Token>> playerHands);
    }

    class RadomTokenDistributor : TokensDistributor
    {
        public void DistributeTokens(List<Token> tokens, List<List<Token>> playerHands)
        {
            List<Token> aux= ((Token[])tokens.ToArray().Clone()).ToList();

            Random x = new Random();
            Random y = new Random(x.Next());
            int maxAmountTokens = tokens[tokens.Count - 1].Higher()+1;

            for (int i = 0; i < playerHands.Count; i++)
            {
                for (int j = 0; j < maxAmountTokens; j++)
                {
                    int index = y.Next(aux.Count);
                    playerHands[i].Add(aux[index]);
                    aux.RemoveAt(index);
                }
            }
        }
    }

    class SelectedTokenDistributor : TokensDistributor //hay q implementar
    {
        public void DistributeTokens(List<Token> tokens, List<List<Token>> playerHands)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    interface GameEnding
    {
        public bool CheckIfTheGameIsOver(List<IPlayer> players);
    }




}

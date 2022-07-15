using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{

    public interface IPlayer
    {
        public int GetContinuesTimesPassed
        {
            get;
        }
        public int GetPlayerNumber
        {
            get;
        }
        public List<Token> GetHand
        {
            get;
        }
        Token Play(List<Token> field);

    }
    public class HighScoreDropperPlayer : IPlayer
    {
        int continuesTimesPassed;
        int playerNumber;
        List<Token> hand;

        public HighScoreDropperPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }
        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }

        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }

        public List<Token> GetHand
        {
            get { return hand; }
        }
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }

        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public static void SortHand(List<Token> hand)
        {
            for (int i = 0; i < hand.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Count; j++)
                {
                    Token temp;
                    if (hand[i].Score < hand[j].Score)
                    {
                        temp = hand[i];
                        hand[i] = hand[j];
                        hand[j] = temp;
                    }
                }

            }
        }

        public Token Play(List<Token> field)
        {
            SortHand(hand);
            if (field.Count == 0)
            {
                field.Add(hand[0]);
                Token returner = hand[0];
                hand.RemoveAt(0);
                return returner;
            }
            foreach (Token token in hand)
            {
                if (token.LeftBack == field[field.Count - 1].RightBack || token.RightBack == field[field.Count - 1].RightBack)
                {
                    if (token.RightBack == field[field.Count - 1].RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    continuesTimesPassed = 0;
                    return returner;
                }
                else if (token.RightBack == field[0].LeftBack || token.LeftBack == field[0].LeftBack)
                {
                    if (token.LeftBack == field[0].LeftBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Insert(0, token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    continuesTimesPassed = 0;
                    return returner;
                }
            }
            TurnsPassed();
            return null;
        }
    }

    public class LowScoreDropperPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;
        int playerNumber;

        public LowScoreDropperPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }

        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }

        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }

        public List<Token> GetHand
        {
            get { return hand; }
        }

        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public static void SortHand(List<Token> hand)
        {
            for (int i = 0; i < hand.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Count; j++)
                {
                    Token temp;
                    if (hand[i].Score > hand[j].Score)
                    {
                        temp = hand[i];
                        hand[i] = hand[j];
                        hand[j] = temp;
                    }
                }

            }
        }
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }
        public Token Play(List<Token> field)
        {
            SortHand(hand);
            if (field.Count == 0)
            {
                field.Add(hand[0]);
                Token returner = hand[0];
                hand.RemoveAt(0);
                return returner;
            }
            foreach (Token token in hand)
            {
                if (token.LeftBack == field[field.Count - 1].RightBack || token.RightBack == field[field.Count - 1].RightBack)
                {
                    if (token.RightBack == field[field.Count - 1].RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    continuesTimesPassed = 0;
                    return returner;
                }
                else if (token.RightBack == field[0].LeftBack || token.LeftBack == field[0].LeftBack)
                {
                    if (token.LeftBack == field[0].LeftBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Insert(0, token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    continuesTimesPassed = 0;
                    return returner;
                }
            }
            TurnsPassed();
            return null;
        }
    }

    public class RandomPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;
        int playerNumber;

        public RandomPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }

        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }

        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }

        public List<Token> GetHand
        {
            get { return hand; }
        }

        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }
        public Token Play(List<Token> field)
        {
            
            Random x = new Random();
            Random y = new Random(x.Next());

            if (field.Count == 0)
            {
                int index = y.Next(hand.Count);
                Token returner = hand[index];
                field.Add(returner);
                hand.RemoveAt(index);
                return returner;
            }

            List<Token> aux = ((Token[])hand.ToArray().Clone()).ToList();

            while (aux.Count > 0)
            {
                int index = y.Next(aux.Count);
                Token returner = aux[index];

                if (returner.Contains(field[0].LeftBack))
                {
                    if (returner.LeftBack == field[0].LeftBack) returner.Rotate();

                    field.Insert(0, returner);
                    hand.RemoveAt(index);
                    continuesTimesPassed = 0;
                    return returner;
                }
                if (returner.Contains(field[field.Count - 1].RightBack))
                {
                    if (returner.RightBack == field[field.Count - 1].RightBack) returner.Rotate();

                    field.Add(returner);
                    hand.RemoveAt(index);
                    continuesTimesPassed = 0;
                    return returner;
                }
                aux.RemoveAt(index);
            }
            
            TurnsPassed();
            return null;
        }

    }
}



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
        Token Play(List<Token> field);
        public List<Token> GetHand
        {
            get;
        }
    }
   public class HighScoreDropperPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;
        int playerNumber;
        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }
        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public List<Token> GetHand
        {
            get { return hand; }
        }
        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }
        public HighScoreDropperPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }
        public static void SortHand(List<Token> hand)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                for (int j = 0; j < hand.Count; j++)
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
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }
        public Token Play(List<Token> field)
        {
            SortHand(hand);
            if (field.Count == 0) return null;
            foreach (Token token in hand)
            {
                if (token.LeftBack == field[field.Count - 1].RightBack || token.RightBack == field[field.Count - 1].RightBack)
                {
                    if (token.RightBack == field[field.Count - 1].RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    return returner;
                }
                else if (token.RightBack == field[0].LeftBack || token.LeftBack == field[0].LeftBack)
                {
                    if (token.LeftBack == field[0].LeftBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Insert(0, token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
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
        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }
        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public List<Token> GetHand
        {
            get { return hand; }
        }
        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }
        public RandomPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }
        public Token Play(List<Token> field)
        {
            if (field.Count == 0) return null;
            Token tokenToplay = null;
            List<Token> possibleTokens = new List<Token>();
            List<int> indexesToRemove = new List<int>();
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].LeftBack == field[0].LeftBack || hand[i].LeftBack == field[field.Count - 1].RightBack)
                {
                    possibleTokens.Add(hand[i]);
                    indexesToRemove.Add(i);
                }
                else if (hand[i].RightBack == field[0].LeftBack || hand[i].RightBack == field[field.Count - 1].RightBack)
                {
                    possibleTokens.Add(hand[i]);
                    indexesToRemove.Add(i);
                }
            }
            Random random = new Random();
            int randomIndex = random.Next(possibleTokens.Count - 1);
            tokenToplay = possibleTokens[randomIndex];
            if (tokenToplay.LeftBack == field[field.Count - 1].RightBack || tokenToplay.RightBack == field[field.Count - 1].RightBack)
            {
                if (tokenToplay.RightBack == field[field.Count - 1].RightBack) tokenToplay.Rotate();
                field.Add(tokenToplay);
                Token returner = tokenToplay;
                hand.RemoveAt(randomIndex);
                return returner;
            }
            else if (tokenToplay.RightBack == field[0].LeftBack || tokenToplay.LeftBack == field[0].LeftBack)
            {
                if (tokenToplay.LeftBack == field[0].LeftBack) tokenToplay.Rotate();
                field.Insert(0, tokenToplay);
                Token returner = tokenToplay;
                hand.RemoveAt(randomIndex);
                return returner;
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
        public int GetPlayerNumber
        {
            get { return playerNumber; }
        }
        public void AssignToken(Token token)
        {
            hand.Add(token);
        }
        public List<Token> GetHand
        {
            get { return hand; }
        }
        public int GetContinuesTimesPassed
        {
            get { return continuesTimesPassed; }
        }
        public LowScoreDropperPlayer(int playerNumber)
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
            this.playerNumber = playerNumber;
        }
        public static void SortHand(List<Token> hand)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                for (int j = 0; j < hand.Count; j++)
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
            if (field.Count == 0) return null;
            SortHand(hand);
            foreach (Token token in hand)
            {
                if (token.LeftBack == field[field.Count - 1].RightBack || token.RightBack == field[field.Count - 1].RightBack)
                {
                    if (token.RightBack == field[field.Count - 1].RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    return returner;
                }
                else if (token.RightBack == field[0].LeftBack || token.LeftBack == field[0].LeftBack)
                {
                    if (token.LeftBack == field[0].LeftBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Insert(0, token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    return returner;
                }
            }
            TurnsPassed();
            return null;
        }
    }
}

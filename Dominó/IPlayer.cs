using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{

    interface IPlayer
    {
        public int GetContinuesTimesPassed
        {
            get;
        }
        Token Play(Token tokenAvailable, List<Token> field);
        public List<Token> GetHand
        {
            get;
        }
    }
    class HighScoreDropperPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;

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
        public HighScoreDropperPlayer()
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
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
        public Token Play(Token tokenAvailable, List<Token> field)
        {
            SortHand(hand);
            if (field.Count == 0) return null; 
            foreach (Token token in hand)
            {
                if (token.LeftBack == tokenAvailable.RightBack || token.RightBack == tokenAvailable.RightBack)
                {
                    if (token.RightBack == tokenAvailable.RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token; 
                    hand.RemoveAt(indexToRemove);
                    return returner;
                }
                else if (token.RightBack == tokenAvailable.LeftBack || token.LeftBack == tokenAvailable.LeftBack)
                {
                    if (token.LeftBack == tokenAvailable.LeftBack) token.Rotate();
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
    class RandomPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;

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
        public void TurnsPassed()
        {
            continuesTimesPassed++;
        }
        public Token Play(Token tokenAvailable, List<Token> field)
        {
            if (field.Count == 0) return null;
            Token tokenToplay = null;
            List<Token> possibleTokens = new List<Token>();
            List<int> indexesToRemove = new List<int>();
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].LeftBack == tokenAvailable.LeftBack || hand[i].LeftBack == tokenAvailable.RightBack)
                {
                    possibleTokens.Add(hand[i]);
                    indexesToRemove.Add(i);
                }
                else if (hand[i].RightBack == tokenAvailable.LeftBack || hand[i].RightBack == tokenAvailable.RightBack)
                {
                    possibleTokens.Add(hand[i]);
                    indexesToRemove.Add(i);
                }
            }
            Random random = new Random();
            int randomIndex = random.Next(possibleTokens.Count - 1);
            tokenToplay = possibleTokens[randomIndex];
            if (tokenToplay.LeftBack == tokenAvailable.RightBack || tokenToplay.RightBack == tokenAvailable.RightBack)
            {
                if (tokenToplay.RightBack == tokenAvailable.RightBack) tokenToplay.Rotate();
                field.Add(tokenToplay);
                Token returner = tokenToplay;
                hand.RemoveAt(randomIndex);
                return returner;
            }
            else if (tokenToplay.RightBack == tokenAvailable.LeftBack || tokenToplay.LeftBack == tokenAvailable.LeftBack)
            {
                if (tokenToplay.LeftBack == tokenAvailable.LeftBack) tokenToplay.Rotate();
                field.Insert(0, tokenToplay);
                Token returner = tokenToplay;
                hand.RemoveAt(randomIndex);
                return returner;
            }
            TurnsPassed();
            return null;
        }
    }
    class LowScoreDropperPlayer : IPlayer
    {
        int continuesTimesPassed;
        List<Token> hand;

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
        public LowScoreDropperPlayer()
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
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
        public Token Play(Token tokenAvailable, List<Token> field)
        {
            if (field.Count == 0) return null;
            SortHand(hand);
            foreach (Token token in hand)
            {
                if (token.LeftBack == tokenAvailable.RightBack || token.RightBack == tokenAvailable.RightBack)
                {
                    if (token.RightBack == tokenAvailable.RightBack) token.Rotate();
                    int indexToRemove = hand.IndexOf(token);
                    field.Add(token);
                    Token returner = token;
                    hand.RemoveAt(indexToRemove);
                    return returner;
                }
                else if (token.RightBack == tokenAvailable.LeftBack || token.LeftBack == tokenAvailable.LeftBack)
                {
                    if (token.LeftBack == tokenAvailable.LeftBack) token.Rotate();
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

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
        void Play(Token tokenAvailable, List<Token> field);

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
            get { return continuesTimesPassed;}
        }

        public HighScoreDropperPlayer()
        {
            continuesTimesPassed = 0;
            hand = new List<Token>();
        }

        public void Play(Token tokenAvailable, List<Token> field)
        {
            Token tokenToPlay;
            foreach (Token token in hand)
            {

            }
            hand.myList.IndexOf(tokenToPlay);
            int indexToRemove = hand.myList.IndexOf(tokenToPlay);
            if (tokenToPlay.LeftElement == tokenAvailable.LeftElement)
            {
                field.ToList.Insert(0, tokenToPlay);
            }
            else field.ToList.Add(tokenToPlay);
            hand.myList.RemoveAt(indexToRemove);
        }
    }
    class RandomPlayer : IPlayer
    {
        int continuesTimesPassed;
        ListTokenCollection hand;
        public RandomPlayer()
        {
            continuesTimesPassed = 0;
        }

        public List<Token> GetHand => throw new NotImplementedException();

        public void AssignHand(ListTokenCollection tokenCollection)
        {
            this.hand.myList = tokenCollection.ToList;
        }
        public void Play(TupleToken tokenAvailable, ListTokenCollection field)
        {
            TupleToken tokenToplay = new TupleToken(0, 0);
            TupleToken[] handToArray = hand.myList.ToArray();
            List<TupleToken> possibleTokens = new List<TupleToken>();
            List<int> indexToRemove = new List<int>();
            for (int i = 0; i < handToArray.Length; i++)
            {
                if (handToArray[i].LeftElement == tokenAvailable.LeftElement || handToArray[i].LeftElement == tokenAvailable.RightElement)
                {
                    possibleTokens.Add(handToArray[i]);
                    indexToRemove.Add(i);
                }
                else if (handToArray[i].RightElement == tokenAvailable.LeftElement || handToArray[i].RightElement == tokenAvailable.RightElement)
                {
                    possibleTokens.Add(handToArray[i]);
                    indexToRemove.Add(i);
                }
            }
            Random random = new Random();
            int randomIndex = random.Next(possibleTokens.Count - 1);
            tokenToplay = possibleTokens[randomIndex];
            if (tokenToplay.LeftElement == tokenAvailable.LeftElement)
            {
                field.ToList.Insert(0, tokenToplay);
            }
            else field.ToList.Add(tokenToplay);
            hand.myList.RemoveAt(indexToRemove[randomIndex]);
        }
    }
    class SmartPlayer : IPlayer
    {
        ListTokenCollection hand;
        int continuesTimesPassed;
        public SmartPlayer()
        {
            int continuesTimesPassed = 0;
        }

        public List<Token> GetHand => throw new NotImplementedException();

        public void AssignHand(ListTokenCollection tokenCollection)
        {
            this.hand.myList = tokenCollection.ToList;
        }
        public void Play(TupleToken tokenAvailable, ListTokenCollection field)
        {
            throw new NotImplementedException();
        }
    }
}

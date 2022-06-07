using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    interface IToken<T>
    {
        T LeftElement { get; }
        T RightElement { get; }
    }

    interface ITokensCollection<T> where T : IToken<int> //duda de xq tengo q poner IToken<int> y no IToken<T>
    {
        T Largest { get; }
        T Smallest { get; }

        List<T> ToList { get; }
    }

    class TupleToken : IToken<int>
    {
        (int, int) myToken;
        public TupleToken(int a, int b)
        {
            myToken = (a, b);
        }
        public int LeftElement { get { return myToken.Item1; } }
        public int RightElement { get { return myToken.Item2; } }
    }

    class ListTokenCollection : ITokensCollection<TupleToken>
    {
        public List<TupleToken> myList;

        public ListTokenCollection(List<TupleToken> list)
        {
            myList = list;
        }

        public TupleToken Largest => throw new NotImplementedException();

        public TupleToken Smallest => throw new NotImplementedException();

        public List<TupleToken> ToList => throw new NotImplementedException();
    }
    interface IPlayer
    {
        public void AssignHand(ListTokenCollection tokenCollection);

        void Play(TupleToken tokenAvailable, ListTokenCollection field);
    }
    class HighScoreDropperPlayer : IPlayer
    {
        ListTokenCollection hand;
        public HighScoreDropperPlayer()
        {
        }
        public void AssignHand(ListTokenCollection tokenCollection)
        {
            this.hand.myList = tokenCollection.ToList;
        }
        public void Play(TupleToken tokenAvailable, ListTokenCollection field)
        {
            TupleToken tokenToplay = new TupleToken(0, 0);
            TupleToken[] handToArray = hand.myList.ToArray();
            int indexToRemove = 0;
            int HStoken = 0;
            for (int i = 0; i < handToArray.Length; i++)
            {
                if ((handToArray[i].LeftElement > HStoken) &&
                    (handToArray[i].LeftElement == tokenAvailable.LeftElement || handToArray[i].LeftElement == tokenAvailable.RightElement))
                {
                    tokenToplay = handToArray[i];
                    indexToRemove = i;
                }
                if ((handToArray[i].RightElement > HStoken) &&
                    (handToArray[i].RightElement == tokenAvailable.LeftElement || handToArray[i].RightElement == tokenAvailable.RightElement))
                {
                    tokenToplay = handToArray[i];
                    indexToRemove = i;
                }
            }
            if (tokenToplay.LeftElement == tokenAvailable.LeftElement)
            {
                field.ToList.Insert(0, tokenToplay);
            }
            else field.ToList.Add(tokenToplay);
            hand.myList.RemoveAt(indexToRemove);
        }
    }
    class RandomPlayer : IPlayer
    {
        ListTokenCollection hand;

        public RandomPlayer()
        {
        }
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

        public SmartPlayer()
        {
        }
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

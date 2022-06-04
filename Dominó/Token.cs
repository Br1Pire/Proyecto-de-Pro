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
        public int LeftElement
        {
            get
            {
                return myToken.Item1;
            }

        }
        public int RightElement { get { return myToken.Item2; } }

        public int Total { get { return myToken.Item1 + myToken.Item2; } }
    }



    class ListTokenCollection : ITokensCollection<TupleToken>
    {
        List<TupleToken> myList;

        public ListTokenCollection(List<TupleToken> list)
        {
            myList = list;
        }

        public TupleToken Largest
        {
            get
            {
                int highest = -1;
                int j = 0;

                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].Total > highest)
                    {
                        highest=myList[i].Total;
                        j=i;
                    }
                }

                return myList[j];
            }
        }
        public TupleToken Smallest
        {
            get
            {
                int lowest = int.MaxValue;
                int j = 0;

                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].Total < lowest)
                    {
                        lowest = myList[i].Total;
                        j = i;
                    }
                }

                return myList[j];
            }
        }

        public List<TupleToken> ToList { get { return myList; } }
    }
}

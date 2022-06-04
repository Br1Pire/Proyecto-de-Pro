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
    }



    class ListTokenCollection : ITokensCollection<TupleToken>
    {
        List<TupleToken> myList;

        public ListTokenCollection(List<TupleToken> list)
        {
            myList = list;
        }

        public TupleToken Largest => throw new NotImplementedException();

        public TupleToken Smallest => throw new NotImplementedException();

        public List<TupleToken> ToList => throw new NotImplementedException();
    }
}

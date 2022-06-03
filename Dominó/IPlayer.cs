using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    internal interface IPlayer
    {
        void Play(List<(int, int)> hand, int token1, int token2);
    }
}

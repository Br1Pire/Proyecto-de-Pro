
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    interface IAmountOfTokens
    {
        ListTokenCollection GameTokens();
    }

    interface IDropMode
    {
        void DropTokens(ListTokenCollection gameTokens);
    }

    //interface ITurnSelection
    //{
    //    void SelectTurns(List<Player> players);
    //}
}

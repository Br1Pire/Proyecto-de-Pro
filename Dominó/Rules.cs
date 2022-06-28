
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


    interface TurnSelector
    {

    }
}

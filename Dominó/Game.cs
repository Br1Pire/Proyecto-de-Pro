

namespace Dominó
{
    //public class Token
    //{
    //    public int leftItem;
    //    public int rightItem;
    //    public int score;
        

    //    public Token(int leftItem, int rightItem)
    //    {
    //        this.leftItem = leftItem;
    //        this.rightItem = rightItem;
            
    //        this.score = leftItem + rightItem;
    //    }

    //    public int GetHigher()
    //    {
    //        if (leftItem < rightItem) return rightItem;
    //        else return leftItem;
    //    }

    //    public int GetLower()
    //    {
    //        if (leftItem > rightItem) return rightItem;
    //        else return leftItem;
    //    }

        

        
    //    public bool Contain(int x)
    //    {
    //        return leftItem == x|| rightItem==x;
    //    }

    //    public void Rotate()
    //    {
    //        int aux = leftItem;
    //        leftItem=rightItem;
    //        rightItem=aux;
    //    }

    //    public override string ToString()
    //    {
    //        return "[ " + leftItem + " | " + rightItem + " ]";
    //    }
        
    //}

    public class Game
    {
        List<int> players = new List<int>();
        List<Token> tokens = new List<Token>();
        List<Token> table = new List<Token>();
        List<List<Token>> playersHands = new List<List<Token>>();
        Token lastPLayed;
        int turn;
        int amountOfTokensPerPlayer;
        int timesPassed=0;

        public Game(int players, int maxToken)
        {
            for (int i = 1; i <= players; i++)
            {
                this.players.Add(i);
                playersHands.Add(new List<Token>());
            }
            GenerateTokens(maxToken);
            GetPlayersTurnsRandom();
            this.amountOfTokensPerPlayer = maxToken+1;
            AssignTokens();
        }

        public void Run()
        {
            for ( ;  ;turn ++)
            {
                if (turn > playersHands.Count)
                {
                    turn = 0;
                    continue;
                }

                Play();
                if (CheckEndGame())
                {
                    Console.WriteLine("Ha ganado el Player "+ SelectWinner());
                    break;
                }
                Console.WriteLine(PrintStatus());
                Console.ReadLine();
            }
        }

        public void GetPlayersTurnsRandom()
        {
            Random y = new Random();
            Random x = new Random(y.Next());
           turn= x.Next(1,players.Count+1);
        }

        public void GenerateTokens(int max)
        {
            List<Token> ret = new List<Token>();
            int index = 0;
            for (int i = 0; i <= max; i++)
            {
                for (int j = index; j <= max; j++)
                {
                    ret.Add(new Token(i, j));
                }
                index++;
            }
            tokens = ret;
        }

        public void AssignTokens()
        {
             List<Token> aux = ((Token[])tokens.ToArray().Clone()).ToList();

            foreach (var item in playersHands)
            {
                int count=0;
                while (count < amountOfTokensPerPlayer)
                {
                    Random x = new Random();
                    Random y = new Random(x.Next());

                    int index = y.Next(aux.Count);
                    item.Add(aux[index]);
                    aux.RemoveAt(index);
                    count++;
                }
                SortByHighers(item);
            }
        }

        public void Play()
        {
            
            if (table.Count == 0)
            {
                table.Add(playersHands[turn - 1][0]);
                lastPLayed = playersHands[turn - 1][0];
                playersHands[turn - 1].RemoveAt(0);
                return;
            }
            for (int i = 0; i < playersHands[turn - 1].Count; i++)
            {
                if (playersHands[turn - 1][i].Contain(table[0].leftItem))
                {
                    if (playersHands[turn - 1][i].rightItem == table[0].leftItem)
                    {
                        timesPassed = 0;
                        table.Insert(0, playersHands[turn - 1][i]);
                        lastPLayed=playersHands[turn - 1][i];
                        playersHands[turn - 1].RemoveAt(i);

                        return;
                    }
                    else
                    {
                        timesPassed = 0;
                        playersHands[turn - 1][i].Rotate();
                        table.Insert(0, playersHands[turn - 1][i]);
                        lastPLayed = playersHands[turn - 1][i];
                        playersHands[turn - 1].RemoveAt(i);

                        return;
                    }
                }

                if (playersHands[turn - 1][i].Contain(table[table.Count - 1].rightItem))
                {
                    if (playersHands[turn - 1][i].leftItem == table[table.Count - 1].rightItem)
                    {
                        timesPassed = 0;
                        table.Add(playersHands[turn - 1][i]);
                        lastPLayed = playersHands[turn - 1][i];
                        playersHands[turn - 1].RemoveAt(i);

                        return;
                    }
                    else
                    {
                        timesPassed = 0;
                        playersHands[turn - 1][i].Rotate();
                        table.Add(playersHands[turn - 1][i]);
                        lastPLayed = playersHands[turn - 1][i];
                        playersHands[turn - 1].RemoveAt(i);

                        return;
                    }

                }
            }

            timesPassed++;

        }
        #region(Auxiliares de Play)

        public static void SortByHighers(List<Token> playerHand)
        {

            for (int i = 0; i < playerHand.Count; i++)
            {
                for (int j = 0; j < playerHand.Count-1; j++)
                {
                    if (playerHand[j].score < playerHand[j + 1].score)
                    {
                        Token aux=playerHand[j];
                        playerHand[j]=playerHand[j+1];
                        playerHand[j+1]=aux;
                        
                    }
                }
            }
           
        }

        #endregion

        public bool CheckEndGame()
        {
            return playersHands[turn - 1].Count == 0 || timesPassed == playersHands.Count;
        }

        public int SelectWinner()
        {
            int index=0;
            int score = int.MaxValue;
            for (int i = 0; i < playersHands.Count; i++)
            {
                int aux = CalculateHandScore(playersHands[i]);
                if (aux < score)
                {
                    score=aux;
                    index=i;
                }
            }

            return index+1;
        }

        public static int CalculateHandScore(List<Token> hand)
        {
            int ret = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                ret+=hand[i].score;
            }
            return ret;
        }

        public string PrintStatus()
        {
            string t = "";
            for (int i = 0; i < table.Count; i++)
            {
                t += table[i].ToString() + "  ";
            }

            string s = "Turno de Player " + (turn) + "\n\nJugo " + lastPLayed.ToString() + "\n\n" + t;
            return s;
        }


        public List<int> Players
        {
            get { return players; }
        }

        public List<Token> Tokens
        {
            get { return tokens; }
        }

        public List<List<Token>> PlayersHands
        {
            get { return playersHands;}
        }


    }
}
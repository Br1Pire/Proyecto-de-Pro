using Dominó;

namespace Tester
{
   class A
    {
        int p;
        int q;

        public A(int p, int q)
        {
            this.p = p;
            this.q = q;
        }

        public int P { get { return p; } }
        public int Q { get { return q; } }
    }

    class B : A
    {
        int z;
        public B(int p, int q, int z):base ( p,  q) 
        {
            this.z = z;
        }
        public int Z { get { return z; } }
    }

    class MyProgram
    {
        static void Main(string[] args)
        {
            A x = new A(5,6);

            B y = (B)x;

            Console.WriteLine(y.Z);

        }
    }
}

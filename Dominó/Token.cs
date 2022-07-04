using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    //pa comentar

    public class Token
    {

        int leftBack;
        int rightBack;


        public Token(int leftBack, int rightBack)
        {
            this.leftBack = leftBack;
            this.rightBack = rightBack;
        }

        public int LeftBack
        {
            get { return leftBack; }
        }
        public int RightBack
        {
            get { return rightBack; }
        }

        public int Score
        {
            get { return leftBack + rightBack; }
        }

        virtual public int Higher()
        {
            if (leftBack < rightBack) return rightBack;
            return leftBack;
        }


        virtual public int Lower()
        {
            if (leftBack > rightBack) return rightBack;
            return leftBack;
        }

        public void Rotate()
        {
            int aux = leftBack;
            leftBack = rightBack;
            rightBack = aux;
        }

        virtual public bool Contains(int value)
        {
            return leftBack == value || rightBack == value;
        }

        public override string ToString()
        {
            return "[ " + leftBack + " | " + rightBack + " ]";
        }

    }

    public class InToken : Token
    {
       
        public  InToken(int leftBack, int rightBack) : base(leftBack, rightBack) { }
        
    }

     public class Animal
    {
        string name;

        public Animal(string name)
        {
            this.name = name;
        }

        public override bool Equals(object? x)
        {
            if (this.name == ((Animal)x).name) return true;
            return false;
        }

        public override string ToString()
        {
            return name;
        }
    }

     public class AnimalToken : Token
    {
        int leftBack;
        int rightBack;
        Animal[] guide;

        public AnimalToken(int leftBack, int rightBack) : base(leftBack, rightBack)
        {
            this.leftBack = leftBack;
            this.rightBack = rightBack;
            guide = new Animal[] { new Animal("Lion"), new Animal("Snake"), new Animal("Dog"), new Animal("Cat"), new Animal("Elephant"), new Animal("Mouse"), new Animal("Ant"), new Animal("Falcon"), new Animal("Duck"), new Animal("Worm") };
        }

        //public Animal LeftElement
        //{
        //    get { return guide[leftBack]; }
        //}
        //public Animal RightElement
        //{
        //    get { return guide[rightBack]; }
        //}


        public override bool Contains(int value)
        {

            return leftBack == value || rightBack == value;

        }

        public override int Higher()
        {
            if (leftBack > rightBack) return leftBack;
            return rightBack;
        }

        public override int Lower()
        {
            if (leftBack < rightBack) return leftBack;
            return rightBack;
        }

        public override string ToString()
        {
            return "[ " + guide[LeftBack].ToString() + " | " + guide[RightBack].ToString() + " ]";
        }

    }
}



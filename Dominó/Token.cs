using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominó
{
    //Aqui definimos la clase Token
    public class Token
    {
        //esta va a tener solo dos valores o dos caras que es lo mismo 
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
        
        //Score va a devolver la suma de los valores de la cara de la ficha
        public int Score 
        {
            get { return leftBack + rightBack; }
        }

        //Higher va a devolver el valor máximo entre los valores de la cara de la ficha  
        virtual public int Higher()
        {
            if (leftBack < rightBack) return rightBack;
            return leftBack;
        }

        //Lower va a devolver el valor mínimo entre los valores de la cara de la ficha
        virtual public int Lower()
        {
            if (leftBack > rightBack) return rightBack;
            return leftBack;
        }

        //Rotate va a intercambiar los valores de la cara de ficha
        public void Rotate()
        {
            int aux = leftBack;
            leftBack = rightBack;
            rightBack = aux;
        }

        //Contains va a preguntar si cierto valor esta contenido en la ficha
        virtual public bool Contains(int value)
        {
            return leftBack == value || rightBack == value;
        }

        //ToString esta redefinido en este caso para que imprima la ficha con sus caracteristicas
        public override string ToString()
        {
            return "[ " + leftBack + " | " + rightBack + " ]";
        }

    }

    //InToken va a ser la ficha de tipo numeros, una ficha con valores numericos en sus caras
    public class InToken : Token
    {
        public  InToken(int leftBack, int rightBack) : base(leftBack, rightBack) { }
    }

    //Aqui definimos la clase Animal
     public class Animal
    {
        //este va a ser el nomre de dicho animal
        string name;

        public Animal(string name)
        {
            this.name = name;
        }

        //Equals esta redefinido para preguntar si el valor de una cara es igual al de otra o lo que es lo mismo si un animal es igual a otro
        public override bool Equals(object? x)
        {
            if (this.name == ((Animal)x).name) return true;
            return false;
        }

        //ToString esta redefinido en este caso para que devuelva el nombre del animal
        public override string ToString()
        {
            return name;
        }
    }

    //AnimalToken va a ser la ficha de tipo animales, una ficha con nombres de animales en sus caras
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

        //Contains va a preguntar si cierto valor esta contenido en la ficha
        public override bool Contains(int value)
        {

            return leftBack == value || rightBack == value;

        }

        //Higher va a devolver el valor máximo entre los valores de la cara de la ficha  
        public override int Higher()
        {
            if (leftBack > rightBack) return leftBack;
            return rightBack;
        }

        //Lower va a devolver el valor mínimo entre los valores de la cara de la ficha
        public override int Lower()
        {
            if (leftBack < rightBack) return leftBack;
            return rightBack;
        }

        //ToString esta redefinido en este caso para que imprima la ficha con sus caracteristicas
        public override string ToString()
        {
            return "[ " + guide[LeftBack].ToString() + " | " + guide[RightBack].ToString() + " ]";
        }

    }
}



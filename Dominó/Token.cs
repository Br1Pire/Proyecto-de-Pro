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
        public int Higher()
        {
            if (leftBack < rightBack) return rightBack;
            return leftBack;
        }

        //Lower va a devolver el valor mínimo entre los valores de la cara de la ficha
        public int Lower()
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
        public bool Contains(int value)
        {
            return leftBack == value || rightBack == value;
        }

        //ToString esta redefinido en este caso para que imprima la ficha con sus valores
        public override string ToString()
        {
            return "[ " + leftBack + " | " + rightBack + " ]";
        }

    }

    //InToken va a ser la ficha de tipo números, una ficha con valores numéricos en sus caras
    public class InToken : Token
    {
        public  InToken(int leftBack, int rightBack) : base(leftBack, rightBack) { }
    }

    //Aqui definimos la clase Animal
     public class Animal
    {
        //este va a ser el nombre de dicho animal
        string name;

        public Animal(string name)
        {
            this.name = name;
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
        
        Animal[] guide;

        public AnimalToken(int leftBack, int rightBack) : base(leftBack, rightBack)
        {
            guide = new Animal[] { new Animal("Lion"), new Animal("Snake"), new Animal("Dog"), new Animal("Cat"), new Animal("Elephant"), new Animal("Mouse"), new Animal("Ant"), new Animal("Falcon"), new Animal("Duck"), new Animal("Worm") };
        }
        
        //ToString esta redefinido en este caso para que imprima la ficha con sus caracteristicas
        public override string ToString()
        {
            return "[ " + guide[LeftBack].ToString() + " | " + guide[RightBack].ToString() + " ]";
        }

    }
}



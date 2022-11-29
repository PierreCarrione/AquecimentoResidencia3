using System;

public class Interface
{
	public void imprimeNumsArmstrong(int valorLimite)
	{
        for (int i = 0; i < valorLimite; i++)
        {
            if (i.IsArmstrong())
            {
                Console.WriteLine(i);
            } 
        }
    }
}

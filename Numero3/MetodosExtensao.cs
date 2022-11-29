using System;

public static class MetodosExtensao
{
	public static Boolean IsArmstrong(this int num)
	{
        string aux = num.ToString();
        int soma = 0;
        for (int i = 0; i < aux.Length; i++)
        {
            soma = soma + (int)Math.Pow(aux[i] - '0', aux.Length);
        }
        return soma == num;
    }
}

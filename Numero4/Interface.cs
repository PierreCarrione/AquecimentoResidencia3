using System;

public class Interface
{
	public void IniciarTestes()
	{
        List<string> listStrings = new List<string>();
        List<Cliente> clientes = new List<Cliente>();
        List<int> inteiros = new List<int>();
        List<float> flutuantes = new List<float>();

        listStrings.Add("abc");
        listStrings.Add("lkm");
        listStrings.Add("abc");
        listStrings.Add("ll");

        clientes.Add(new Cliente("12345678910", "pierre"));
        clientes.Add(new Cliente("12345678910", "emanuele"));
        clientes.Add(new Cliente("12345678911", "pierre"));

        inteiros.Add(10);
        inteiros.Add(5);
        inteiros.Add(9);
        inteiros.Add(10);

        flutuantes.Add(10.5F);
        flutuantes.Add(5.6F);
        flutuantes.Add(9.1F);
        flutuantes.Add(10.5F);

        listStrings = listStrings.RemoveRepetidos();
        clientes = clientes.RemoveRepetidos();
        inteiros = inteiros.RemoveRepetidos();
        flutuantes = flutuantes.RemoveRepetidos();

        foreach (var item in listStrings)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("----------------------------------------------");
        foreach (var item in clientes)
        {
            Console.WriteLine("Nome: " + item.nome + " cpf: " + item.CPF);
        }
        Console.WriteLine("----------------------------------------------");
        foreach (var item in inteiros)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("----------------------------------------------");
        foreach (var item in flutuantes)
        {
            Console.WriteLine(item);
        }
    }
}

using System;
using System.Reflection.PortableExecutable;

public class Cliente
{
	public string CPF { get; set; }
	public string nome { get; set; }

	public Cliente(string cpf, string nome)
	{
		CPF = cpf;
		this.nome = nome;
	}
}

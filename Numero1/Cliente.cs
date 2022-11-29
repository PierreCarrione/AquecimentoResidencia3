using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public class Cliente
{
    public string Nome { set; get; }
    public long Cpf { set; get; }
    public DateTime DataNasc { set; get; }
    public float RendaMensal { set; get; }
    public char EstCivil { set; get; }
    public int Dependentes { set; get; }
    public Cliente() { }

    public Cliente(string nome, long cpf, DateTime dt_nascimento, float renda_mensal, char estado_civil, int dependentes)
    {
        this.Nome = nome;
        this.Cpf = cpf;
        this.DataNasc = dt_nascimento;
        this.RendaMensal = renda_mensal;
        this.EstCivil = estado_civil;
        this.Dependentes = dependentes;
    }
}
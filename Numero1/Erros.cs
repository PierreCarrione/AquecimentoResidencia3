using System;
using System.Net;

public enum tiposErros { nome, cpf, dt_nascimento , renda_mensal , estado_civil , dependentes}

public class Erros
{
	public Cliente dados { get; set; }
	public Dictionary<tiposErros,String> ErrosGerados { get; set; }

    public Erros(Cliente cliente, Dictionary<tiposErros, String> erro)
	{
		dados = cliente;
        ErrosGerados = erro;
	}
}

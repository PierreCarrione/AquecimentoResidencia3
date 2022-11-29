using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Interface
{
    public string nomeLeitura { set; get; }
    public string nomeEscrita { set; get; }
    public string nome { set; get; }
    public string cpf { set; get; }
    public string dt_nascimento { set; get; }
    public string renda_mensal { set; get; }
    public string estado_civil { set; get; }
    public string dependentes { set; get; }
    //Propriedade que irão receber os dados do arquivo.Criei essas propriedades intermediárias pois os dados estão em formato string

    List<Interface> clts { get; set; }
    public List<Cliente> clientes { get; set; }
    Validador valida = new Validador();

    public Interface()
    {
    }
    public Interface(string nomeLeitura)
    {
        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + nomeLeitura))
        {
            File.Create(AppDomain.CurrentDomain.BaseDirectory + nomeLeitura).Close();
        }
        this.nomeLeitura = AppDomain.CurrentDomain.BaseDirectory + nomeLeitura;
    }

    public void lerDados()
    {
        string jsonFile = File.ReadAllText(nomeLeitura);
        clts = JsonSerializer.Deserialize<List<Interface>>(jsonFile)!;
    }
    public void checkDatas()//Verifica se as datas são válidas
    {
        DateTime aux;
        for (int i = 0; i < clts.Count; i++)
        {
            try
            {
                aux = DateTime.ParseExact(clts[i].dt_nascimento, "d", new CultureInfo("pt-BR"));
            }
            catch (Exception e)
            {
                clts[i].dt_nascimento = "01/01/0001";//Escolhi essa data pois quando usei o tryparseexact quando a data não é valida ele retorna essa data padrão
                //Além disso, quando é uma data inválida ele gera erro e fecha o programa, colocando essa data "padrão" sei que a data lida é inválida
            }
        }
    }
    public void converterDados()//Converte os dados de string para o formato de cliente com seus respectivos tipos.
    {
        checkDatas();
        clientes = clts.ConvertAll(x => new Cliente
        {
            Nome = x.nome,
            Cpf = long.Parse(x.cpf),
            DataNasc = DateTime.ParseExact(x.dt_nascimento, "d", new CultureInfo("pt-BR")),
            RendaMensal = float.Parse(x.renda_mensal),
            EstCivil = char.Parse(x.estado_civil),
            Dependentes = int.Parse(x.dependentes)
        });
    }

    public void validarDados()
    {
        for (int i = 0; i < clientes.Count; i++)
        {
            valida.writeErros(clientes[i]);
        }
    }

    public void armazenarErros(string nomeArquivo)
    {
        string jsonString = JsonSerializer.Serialize<List<Erros>>(valida.erros);
        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + nomeArquivo, jsonString);
    }

    public void imprimiDados(List<Cliente> clientes)
    {
        foreach (var clts in clientes)
        {
            Console.WriteLine("Nome : " + (clts.Nome) + "\nCpf : " + (clts.Cpf) + "\nData de Nascimento : " + (clts.DataNasc) + "\nRenda Mensal : " + (clts.RendaMensal) +
                "\nEstado Civil : " + (clts.EstCivil) + "\nDependentes : " + (clts.Dependentes));
            Console.WriteLine("--------------------------------------");
        }
    }

    public void runPadrao()
    {
        lerDados();
        converterDados();
        validarDados();
        imprimiDados(clientes);
        armazenarErros(nomeEscrita);
    }
}

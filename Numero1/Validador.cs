using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.Metadata;

public class Validador
{
    public List<Erros> erros { set; get; }

    public Validador()
    {
        erros = new List<Erros>();
    }

    //Função que verifica se o cpf é válido
    public Boolean verificaCpf(Cliente cliente)
    {
        string auxCpf = cliente.Cpf.ToString();
        int freq = auxCpf.Count(f => (f == auxCpf[0]));
        int dvJ = int.Parse(auxCpf[0].ToString()) * 10 + int.Parse(auxCpf[1].ToString()) * 9 +
            int.Parse(auxCpf[2].ToString()) * 8 + int.Parse(auxCpf[3].ToString()) * 7 +
            int.Parse(auxCpf[4].ToString()) * 6 + int.Parse(auxCpf[5].ToString()) * 5 +
            int.Parse(auxCpf[6].ToString()) * 4 + int.Parse(auxCpf[7].ToString()) * 3 + int.Parse(auxCpf[8].ToString()) * 2;
        int dvK = int.Parse(auxCpf[0].ToString()) * 11 + int.Parse(auxCpf[1].ToString()) * 10 +
            int.Parse(auxCpf[2].ToString()) * 9 + int.Parse(auxCpf[3].ToString()) * 8 +
            int.Parse(auxCpf[4].ToString()) * 7 + int.Parse(auxCpf[5].ToString()) * 6 +
            int.Parse(auxCpf[6].ToString()) * 5 + int.Parse(auxCpf[7].ToString()) * 4 +
            int.Parse(auxCpf[8].ToString()) * 3 + int.Parse(auxCpf[9].ToString()) * 2;

        //Se cpf for com todos os numeros iguais ou de tamanho diferente de 11
        if (cliente.Cpf.ToString().Length != 11 || freq == 11)
        {
            return false;
        }
        //Verifica se o digito J ou K para o resto da divisão entre 0 e 1 é diferente de 0.
        if (((dvJ % 11 == 0 || dvJ % 11 == 1) && int.Parse(auxCpf[9].ToString()) != 0) || ((dvK % 11 == 0 || dvK % 11 == 1) && int.Parse(auxCpf[10].ToString()) != 0))
        {
            return false;
        }
        //Verifica se o resto da divisão está entre 2 a 10 e se o valor J ou K é igual a 11-resto
        if ((dvJ % 11 > 10 || dvJ % 11 < 0) || (dvK % 11 > 10 || dvK % 11 < 0) || (11 - dvJ % 11 != int.Parse(auxCpf[9].ToString())) || (11 - dvK % 11 != int.Parse(auxCpf[10].ToString())))
        {
            return false;
        }

        return true;
    }

    //Função que irá armazenar os logs dos erros em json
    public void writeErros(Cliente cliente)
    {
        Dictionary<tiposErros, String> auxErros = new Dictionary<tiposErros, String>();

        if (!Regex.IsMatch(cliente.Nome, @"^\w{5,}(\s?\w+)*$"))
        {
            auxErros.Add(tiposErros.nome, " Nome nao atende ao requisito de ter pelo menos 5 caracteres");
        }
        if (!verificaCpf(cliente))
        {
            auxErros.Add(tiposErros.cpf, "Cpf nao e valido");
        }
        if (((DateTime.Now - cliente.DataNasc).Days / 365 < 18) || DateTime.Compare(cliente.DataNasc, DateTime.Parse("01/01/0001")) == 0)
        {
            auxErros.Add(tiposErros.dt_nascimento, "A data inserida e invalida ou nao atende ao requisito de ter pelo menos 18 anos");
        }
        if (!Regex.IsMatch(cliente.RendaMensal.ToString(), @"^\d{2}\.\d$"))
        {
            auxErros.Add(tiposErros.renda_mensal, "Renda mensal nao atende ao requisito de ter duas casas decimais e virgula decimal");
        }
        if (!Regex.IsMatch(cliente.EstCivil.ToString(), @"[s]|[v]|[d]|[c]", RegexOptions.IgnoreCase))
        {
            auxErros.Add(tiposErros.estado_civil, "E aceito somente as letras: C, S, V ou D(Maiusculo ou Minusculo)");
        }
        if (cliente.Dependentes > 10 || cliente.Dependentes < 0)
        {
            auxErros.Add(tiposErros.dependentes, "Nao atende ao requisito de ter somente valores de 0 a 10");
        }

        if (auxErros.Count != 0)//Se tiver algum erro ele armazena os dados
        {
            erros.Add(new Erros(cliente, auxErros));
        }
    }
}
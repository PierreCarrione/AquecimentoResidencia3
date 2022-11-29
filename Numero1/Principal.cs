using System;
using System.Globalization;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

public class Principal
{
    public static void Main(String[] args)
    {
        Interface inter = new Interface("clientes.json");
        inter.nomeEscrita = "erros.json";
        inter.runPadrao();
    }
}

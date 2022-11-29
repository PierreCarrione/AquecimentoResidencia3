using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class fileError : Exception
{
    public fileError(String message)
        : base(message) { }
}

public class Palavras
{
	public string word { get; set; }
	public List<int>? indices { get; set; }
    public int quantidade = 0;	

    public void addValor(int valor)
	{
		if (indices == null)
		{
            indices = new List<int>();
            indices.Add(valor);
        }
		else
		{
			indices.Add(valor);	
		}
	}
}

public class IndiceRemissivo
{
    public string[] linesTXT;
    public string[] linesIgnore;
	Regex rg = new Regex(@"^\w{1,}$");
	public List<Palavras> palavras { get; set; }

    public IndiceRemissivo()
	{
		palavras = new List<Palavras>();
	}

	public void indiceRemissivo(string pathTXT, string? pathIgnore)
	{
		if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory+pathTXT))
		{
			throw new fileError("Arquivo não existe no diretório");	
		}
		if (!pathIgnore.Equals(null))
		{
            linesIgnore = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + pathIgnore);
		}
		linesTXT = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + pathTXT);
	}

	public void contagemPalavras()
	{
        Palavras auxPalavra;
        string[] splitPalavras;

        for (int i = 0; i < linesTXT.Length; i++)//for que irá percorrer cada linha do texto
        {
            splitPalavras = Regex.Split(linesTXT[i], @"\W\s?");//Separa a frase de cada linha por palavras, a condição de separação é qualquer símbolo ou espaço
            var aux = splitPalavras.ToList();
            aux.RemoveAll(x => x.Equals(""));//Retirando palavras "vazias" que estão no vetor
            splitPalavras = aux.ToArray();

            for (int y = 0; y < splitPalavras.Length; y++)//Percore o vetor de palavras por linha
            {
                if (!palavras.Any(x => x.word.Equals(splitPalavras[y])) && !linesIgnore.Contains(splitPalavras[y]) )//Se a palavra ainda não estiver no array de palavras e não
                {                                                                                               //estiver no ignore.txt
                    auxPalavra = new Palavras();
                    auxPalavra.word = splitPalavras[y];
                    auxPalavra.addValor(i + 1);
                    auxPalavra.quantidade++;
                    palavras.Add(auxPalavra);
                }
                else if(!linesIgnore.Contains(splitPalavras[y]))//Verifica se a palavra analisada não está no ignore.txt
                {
                    //A palavra já está no array, com isso verifica se é outra palavra igual só que em linha diferente, pois se for na mesma linha nao adiciona novamente a mesma linha
                    if (!palavras[palavras.FindIndex(x => x.word.Equals(splitPalavras[y]))].indices.Exists(x => x == (i + 1)))
                    {
                        palavras[palavras.FindIndex(x => x.word.Equals(splitPalavras[y]))].addValor(i + 1);
                        palavras[palavras.FindIndex(x => x.word.Equals(splitPalavras[y]))].quantidade++;
                    }
                    else
                    {
                        palavras[palavras.FindIndex(x => x.word.Equals(splitPalavras[y]))].quantidade++;
                    }
                }
            }
        }
    }

	public void imprime()
	{
        List<Palavras> aux = palavras.OrderBy(x => x.word).ToList();

        for (int i = 0; i < aux.Count; i++)
        {
            Console.Write(aux[i].word.ToUpper() + " (" + aux[i].quantidade + ") ");
            for (int y = 0; y < aux[i].indices.Count; y++)
            {
                Console.Write(aux[i].indices[y] + ", ");
            }
            Console.WriteLine("");
            Console.WriteLine("//-------------------- // -------------------- // -------------------- // --------------------//");
        }
    }

	public static void Main(String[] args)
	{
        IndiceRemissivo indice = new IndiceRemissivo();
        indice.indiceRemissivo("texto.txt","ignore.txt");
        indice.contagemPalavras();
        indice.imprime();
    }
}

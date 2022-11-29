using System;
using System.Runtime.CompilerServices;

public static class MetodosExtensao
{
    public static List<TEntity> RemoveRepetidos<TEntity>(this List<TEntity> entidade)
    {
        if (typeof(TEntity).Name.Equals("Cliente"))
        {
            List<Cliente> aux = entidade.Cast<Cliente>().ToList();

            for (int i = 0; i < aux.Count - 1; i++)
            {
                for (int y = i + 1; y < aux.Count; y++)
                {
                    if (aux[i].CPF.Equals(aux[i].CPF))
                    {
                        aux.RemoveAt(y);
                    }
                }
            }
            return aux.Cast<TEntity>().ToList();
        }
        return entidade.Distinct().ToList();
    }
}

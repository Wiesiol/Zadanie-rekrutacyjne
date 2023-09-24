using System;
using System.Collections.Generic;
using System.Linq;

public static class LinqExtension
{
    public static T Random<T>(this IEnumerable<T> enumerable, Func<T, bool> condition = null)
    {
        if (enumerable == null)
        {
            throw new System.ArgumentNullException(nameof(enumerable));
        }

        // Uwaga: Tworzenie nowej instancji Random za każdym razem może nie być odpowiednie w pewnych sytuacjach.
        // Rozważ użycie statycznej instancji, jeśli potrzebujesz losowości w różnych miejscach w kodzie.
        System.Random r = new System.Random();
        var list = enumerable as IList<T> ?? enumerable.ToList();

        if (condition != null)
        {
            list = list.Where(condition).ToList();
        }

        if (list.Count == 0)
        {
            return default(T);
        }

        int randomIndex = r.Next(0, list.Count);
        return list[randomIndex];
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagerLibrary
{
 
    /// <summary>
    /// klassen är Generic som tillåter olika typer såsom int, string,char osv i parameter.  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class QuickSort<T> where T : IComparable
    {
       /// <summary>
       /// Quicksort funktion som har pivot i mittpunkten. Den tar emot en listan, en vänster punkt och en höger.
       /// Där den försöker ordna de olika element genom att jämföra med pivot. De element som har lägre värde än pivot kommer
       /// att hamna på vänster sida medan de som har större värde står i höger sida.
       /// </summary>
       /// <param name="list">listan som vill bli sorterad</param> 
       /// <param name="left">Left eller start av listan vilket är 0</param>
       /// <param name="right">Right av list vilket är list.Count-1</param>
        public static void SortQuick(ref List<T> list, int left, int right)
        {
            // Start and end variabler är använda för att ha koll när det behövs göra swap
            int start = left;
            int end = right;
            
            // Definiera en pivot för att jämföra med start och end element. Här väljs pivot i mitten
            T pivot = list[(left + right) / 2];

            if (list == null)
            {
                throw new NullReferenceException("The list cannot be sorted by null");
            }

            if (left < 0 || right > list.Count - 1)
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the list");
            }

            // While loop gäller när start och end inte korsar varandra. Om de korsar varandra, kommer funktionen anropa sig själv(rekursivt)
            while (start <= end)
            {
                // Inkrementera start tills element på vänster sida är större än eller lika med pivot
                while (list[start].CompareTo(pivot)<0 ) 
                {
                    start++;

                }

                // Dekrementera end tills element på höger sida är mindre än eller lika med pivot
                while (list[end].CompareTo(pivot)>0) 
                {
                    end--;
                }

                // Gör en swap av start och end och fortsätta inkrementa start och decrementa end
                if (start <= end) 
                {
                    Swap(list, start, end);
                    
                    start++;
                    end--;
                }
            }


            // Anropar rekursivt både på vänster sida och höger sida tills listan blir sorterad.
            if (left < end)
                SortQuick(ref list, left, end);
            if (start < right)
                SortQuick(ref list, start, right);
        }
        
       /// <summary>
       /// Swap funktion
       /// </summary>
       /// <param name="list"></param>
       /// <param name="a"></param>
       /// <param name="b"></param>
        private static void Swap(List<T>list, int a, int b)
        {
            T t = list[a];
            list[a] = list[b];
            list[b] = t;
        }
    }

}

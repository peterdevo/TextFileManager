using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagerLibrary
{
    public static class QuickSort<T>where T:IComparable
    {
        //Quick sort algorithm 
        public static void SortQuick(List<T>list, int left, int right)
        {
            //start and end variabler är använda för att ha koll när behövs göra swap
            int start = left;
            int end = right;
            
            //Definiera en pivot för att jämföra med start och end element.Här väljs pivot i mitten
            T pivot = list[(left + right) / 2];

           //while loop gäller när start och end inte korsar varandra. Om de korsar varandra, kommer funktionen anropa sig sjäv (recursivt) 
            while (start <= end)
            {
                
                while (list[start].CompareTo(pivot)<0 )// inkrement start tills element på vänster sida är större än eller lika med pivot
                {
                    start++;

                }

                while (list[end].CompareTo(pivot)>0)// dekrement end tills element på höger sida är mindre än eller lika med pivot
                {
                    end--;
                }
                if (start <= end)// Gör en swap av start och end och fortsätta inkrementa start och decrementa end
                {
                    Swap(list, start, end);
                    
                    start++;
                    end--;
                }
            }


            // Recursivt anropar både på vänster sida och höger sida
            if (left < end)
                SortQuick(list, left, end);
            if (start < right)
                SortQuick(list, start, right);
        }
      //swap function
        private static void Swap(List<T>list, int a, int b)
        {
            T t = list[a];
            list[a] = list[b];
            list[b] = t;
        }
    }

}

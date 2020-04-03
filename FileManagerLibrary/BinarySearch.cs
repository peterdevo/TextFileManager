using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagerLibrary
{
    public static class BinarySearch 
    {
        
        // Rekursiv sökalgoritm
        private static int Search(List<string> list, int start, int count, string key)
        {
            if (count < start)
                return -1;

            // Sätter index till mitten av listan
            int mid = (start + count) / 2;

            // Returnerar index om elementet är lika med key
            if (list[mid].CompareTo(key) == 0)
                return mid;

            // Om elementet är större än key så kollar man vänstra halvan av listan
            if (list[mid].CompareTo(key) == 1)
                return Search(list, start, mid - 1, key);
            
            // Kommer man hit så är elementet mindre än key och man vill kolla högra halvan av listan 
            return Search(list, mid + 1, count, key);
        }

        // Extension metod så man kan skriva t.ex list.CountOccurencesOf(key)
        public static int CountOccurencesOf(this List<string> source, string key)
        {
            // Använder sök metoden för att hitta indexet av key om det finns
            int index = Search(source, 0, source.Count - 1, key);

            // Om index är -1 så betyder det att sök metoden inte hittade något, då returnerar den 0
            if(index == -1)
            {
                return 0;
            }

            // För att hålla koll på original ordet
            string originalWord = source[index];

            int count = 1;
            int left = index - 1;

            // Kollar till vänster om ordet om det finns mer av samma
            while (left >= 0 && source[left].CompareTo(originalWord) == 0)
            {
                count++;
                left--;
            }

            // Kollar till höger
            int right = index + 1;
            while (right < source.Count && source[right].Equals(originalWord))
            {
                count++;
                right++;
            }

            return count;
        }
    }
}

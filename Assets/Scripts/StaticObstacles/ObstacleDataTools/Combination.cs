using System.Collections.Generic;
using System.Linq;

namespace StaticObstacles.ObstacleDataTools
{
    public class Combination<T>
    {
        private List<T> items;
        private List<List<T>> resultList;
        private T[] current;
        private int length;
        
        private Combination(List<T> aItems, int aLength)
        {
            items = aItems;
            length = aLength;
            resultList = new List<List<T>>();
            current = new T[aLength];
        }
        
        public static List<List<T>> GetCombinations(List<T> aItems, int aLength)
        {
            if (aItems == null || aItems.Count < aLength)
                return new List<List<T>>();
            var context = new Combination<T>(aItems, aLength);
         
            context.GetCombinations(0, 0);
            return context.resultList;
        }
        
        private void GetCombinations(int aStart, int aDepth)
        {
            if (aDepth >= length)
                return;
            int c = items.Count + aDepth - length + 1;
            for (int i = aStart; i < c; i++)
            {
                current[aDepth] = items[i];
                if (aDepth == length-1)
                    resultList.Add(current.ToList());
                else
                    GetCombinations(i+1, aDepth + 1);
            }
        }
    }
}
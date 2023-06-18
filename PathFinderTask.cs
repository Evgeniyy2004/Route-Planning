// Вставьте сюда финальное содержимое файла PathFinderTask.cs
// Вставьте сюда финальное содержимое файла PathFinderTask.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            List<int[]> result = new List<int[]>();
            int[] bestOrder = MakeTrivialPermutation(checkpoints.Length);
            int[] startOrder = new int[checkpoints.Length];
            result.Add(MakeTrivialPermutation(checkpoints.Length));
            FindTheWay(result, checkpoints, startOrder, bestOrder, 0);
            result.Sort((x, y) => CountLength(checkpoints, x).CompareTo(CountLength(checkpoints, y)));
            return result[0];
        }

        private static int[] MakeTrivialPermutation(int size)
        {
            var bestOrder = new int[size];
            for (int i = 0; i < bestOrder.Length; i++)
                bestOrder[i] = i;
            return bestOrder;
        }

        public static void FindTheWay(List<int[]> massiv, Point[] checkpoints, int[] waynow, int[] maybe, int start)
        {
            if (waynow[0] != 0)
            {
                return;
            }
            if (start == checkpoints.Length)
            {
                if (CountLength(checkpoints, waynow) > CountLength(checkpoints, maybe))
                {
                    return;
                }
                int[] maybe2 = new int[checkpoints.Length];
                waynow.CopyTo(maybe2, 0);
                massiv.Add(maybe2);
                return;
            }
            for (int i = 0; i < checkpoints.Length; i++)
            {
                var index = Array.IndexOf(waynow, i, 0, start);
                if (index != -1)
                    continue;
                waynow[start] = i;
                FindTheWay(massiv, checkpoints, waynow, maybe, start + 1);
            }
        }

        public static double CountLength(Point[] checkpoints, int[] order)
        {
            double sum = 0;
            for (int i = 0; i < order.Length - 1; i++)
            {
                var ab = Math.Pow(checkpoints[order[i + 1]].X - checkpoints[order[i]].X, 2);
                var bc = Math.Pow(checkpoints[order[i + 1]].Y - checkpoints[order[i]].Y, 2);
                sum += Math.Sqrt(ab + bc);
            }
            return sum;
        }
    }
}

using System;
using System.Collections.Generic;

namespace MathLibrary
{
    /// <summary>
    /// Random 클래스
    /// </summary>
    static class Random
    {
        private static System.Random random;
        static Random()
        {
            random = new System.Random();
        }

        /// <summary>
        /// 50% 확률로 참, 거짓을 리턴합니다
        /// </summary>
        /// <returns></returns>
        public static bool NextBoolean()
        {
            return random.Next(2) == 0;
        }

        /// <summary>
        /// 특정 확률에 대한 참, 거짓을 리턴합니다
        /// </summary>
        /// <param name="percent">확률</param>
        public static bool NextBoolean(float percent)
        {
            return random.NextDouble() <= percent;
        }

        /// <summary>
        /// 특정 확률에 대한 참, 거짓을 리턴합니다
        /// </summary>
        /// <param name="percent">확률</param>
        public static bool NextBoolean(double percent)
        {
            return random.NextDouble() <= percent;
        }

        /// <summary>
        /// 0부터 n-1까지의 난수를 리턴합니다
        /// </summary>
        /// <param name="maxValue">n-1의 최댓값</param>
        public static int Range(int maxValue)
        {
            return random.Next(maxValue);
        }

        /// <summary>
        /// 특정 범위에 의해 난수를 리턴합니다
        /// </summary>
        /// <param name="minValue">최솟값</param>
        /// <param name="maxValue">최댓값</param>
        public static int Range(int minValue, int maxValue)
        {
            return minValue + random.Next(maxValue - minValue + 1);
        }

        /// <summary>
        /// 특정 범위에 의해 난수를 리턴합니다
        /// </summary>
        /// <param name="minValue">최솟값</param>
        /// <param name="maxValue">최댓값</param>
        public static float Range(float minValue, float maxValue)
        {
            return minValue + ((float)random.NextDouble() * (maxValue - minValue));
        }

        /// <summary>
        /// 특정 범위에 의해 난수를 리턴합니다
        /// </summary>
        /// <param name="minValue">최솟값</param>
        /// <param name="maxValue">최댓값</param>
        public static double Range(double minValue, double maxValue)
        {
            return minValue + random.NextDouble() * (maxValue - minValue);
        }

        public static double NextDouble() {
            return random.NextDouble();
        }
    }
    public class MathUtils
    {
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static bool CheckInRange(float x, float min, float max)
        {
            return min <= x && x <= max;
        }
    }
}

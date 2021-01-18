using UnityEngine;

namespace Assets.Scripts
{
    internal class NoiseGenerator
    {
        private readonly float[,] matrix;
        public float[,] GetNoise(int width, int height, float scale)
        {
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; i < height; i++)
                {
                    var xSize = width/scale;
                    var ySize = height/scale;

                    float noise = Mathf.PerlinNoise(xSize, ySize);
                    matrix[width, height] = noise;
                }
            }

            return matrix;
        }
    }
}

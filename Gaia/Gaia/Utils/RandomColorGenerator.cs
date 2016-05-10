using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaia.Utils
{
    public static class RandomColorGenerator
    {

        private static Dictionary<int, Color> generationColors;

        public static void Initialize()
        {
            generationColors = new Dictionary<int, Color>();
        }

        private static Color RandomColor(Random random)
        {
            byte red = (byte)random.Next(0, 255);
            byte green = (byte)random.Next(0, 255);
            byte blue = (byte)random.Next(0, 255);
            return new Color(red, green, blue);
        }

        public static Color GenerationColor(Random random, int generation)
        {
            if (!generationColors.ContainsKey(generation))
            {
                //Esta geração ainda não existe no dictionary, gerá-la
                Color cor = RandomColor(random);
                generationColors.Add(generation, cor);
                return cor;
            }
            else
            {
                Color cor;
                generationColors.TryGetValue(generation, out cor);
                return cor;
            }
        }

        public static int NumGenerations()
        {
            return generationColors.Count;
        }

    }
}

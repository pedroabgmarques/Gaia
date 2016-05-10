using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaia.Biome
{
    public class Terreno
    {

        public float[,] comida;
        Texture2D pixel;
        Vector2 pos;
        Random random;

        public Terreno(int width, int heigth, Texture2D pixel, Random random)
        {
            comida = new float[width, heigth];
            this.pixel = pixel;
            this.random = random;

            for (int i = 0; i < comida.GetLength(0); i++)
            {
                for (int j = 0; j < comida.GetLength(1); j++)
                {
                    comida[i, j] = (random.Next(0, 500) / 255f);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < comida.GetLength(0); i++)
            {
                for (int j = 0; j < comida.GetLength(1); j++)
                {
                    comida[i, j] += ((float)random.NextDouble() / random.Next(5, 10)) / 255f;
                    if (comida[i, j] >= 6)
                    {
                        comida[i, j] = 0.3f;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < comida.GetLength(0); i++){
                for (int j = 0; j < comida.GetLength(1); j++)
                {
                    pos.X = i;
                    pos.Y = j;
                    spriteBatch.Draw(this.pixel, pos, (Color.YellowGreen * comida[i, j]));
                }
            }
        }

    }
}

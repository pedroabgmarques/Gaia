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
            comida = new float[width / Game1.escala, heigth / Game1.escala];
            this.pixel = pixel;
            this.random = random;

            for (int i = 0; i < comida.GetLength(0); i++)
            {
                for (int j = 0; j < comida.GetLength(1); j++)
                {
                    comida[i, j] = (random.Next(0, 250) / 255f);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < comida.GetLength(0); i++)
            {
                for (int j = 0; j < comida.GetLength(1); j++)
                {
                    comida[i, j] += (random.Next(1, 5) / 255f) / 40f;
                    //if (comida[i, j] >= 6)
                    //{
                    //    comida[i, j] = 0.3f;
                    //}
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < comida.GetLength(0) * Game1.escala; i+= Game1.escala)
            {
                for (int j = 0; j < comida.GetLength(1) * Game1.escala; j += Game1.escala)
                {
                    pos.X = i;
                    pos.Y = j;
                    spriteBatch.Draw(this.pixel, pos, null, (Color.YellowGreen * comida[i / Game1.escala, j / Game1.escala]), 0f, Vector2.Zero, Game1.escala, SpriteEffects.None, 0f);
                }
            }
        }

    }
}

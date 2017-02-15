using Gaia.Biome;
using Gaia.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaia.Entities
{
    public class LiveEntity
    {

        private float fome;
        public float Fome
        {
            get { return fome; }
            set { fome = value; }
        }

        private LiveEntity pai;
        public LiveEntity Pai
        {
            get { return pai; }
            set { pai = value; }
        }
        

        private Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        private int idade;
        public int Idade
        {
            get { return idade; }
            set { idade = value; }
        }

        private int maxIdade;
        public int MaxIdade
        {
            get { return maxIdade; }
            set { maxIdade = value; }
        }

        private int maxFome;
        public int MaxFome
        {
            get { return maxFome; }
            set { maxFome = value; }
        }

        private int maxFomeConfortavel;
        public int MaxFomeConfortavel
        {
            get { return maxFomeConfortavel; }
            set { maxFomeConfortavel = value; }
        }
        

        private int maxContadorReproducoes;
        public int MaxContadorReproducoes
        {
            get { return maxContadorReproducoes; }
            set { maxContadorReproducoes = value; }
        }

        private int maxContadorDirecao;
        public int MaxContadorDirecao
        {
            get { return maxContadorDirecao; }
            set { maxContadorDirecao = value; }
        }

        private float comidaConsumida;
        public float ComidaConsumida
        {
            get { return comidaConsumida; }
            set { comidaConsumida = value; }
        }

        private float taxaFome;
        public float TaxaFome
        {
            get { return taxaFome; }
            set { taxaFome = value; }
        }

        private string causaMorte;
        public string CausaMorte
        {
            get { return causaMorte; }
            set { causaMorte = value; }
        }

        private int numFilhos;
        public int NumFilhos
        {
            get { return numFilhos; }
            set { numFilhos = value; }
        }
        
        
        
        

        private int contadorReproducao;
        public int ContadorReproducao
        {
            get { return contadorReproducao; }
            set { contadorReproducao = value; }
        }

        private int generation;
        public int Generation
        {
            get { return generation; }
            set { generation = value; }
        }
        

        private Color cor;
        public Color Cor
        {
            get { return cor; }
            set { cor = value; }
        }
        
        private bool vivo;
        public bool Vivo
        {
            get { return vivo; }
            set { vivo = value; }
        }

        private int width;
        private int height;
        private int contadorDirecao;
        private int direcao;

        private LiveEntity entityTemp;

        Texture2D pixel;
        
        public LiveEntity(Random random, 
            Texture2D pixel, 
            int width, 
            int height, 
            int posX, 
            int posY, 
            int generation, 
            int maxIdade,
            int maxFome,
            int maxContadorReproducoes,
            int maxContadorDirecao,
            float comidaConsumida,
            float taxaFome,
            LiveEntity pai,
            int maxFomeConfortavel)
        {
            fome = 0;
            this.maxFome = maxFome;
            pos = new Vector2(posX, posY);
            this.pixel = pixel;
            this.width = width / Game1.escala;
            this.height = height / Game1.escala;
            vivo = true;
            idade = 0;
            contadorReproducao = 0;
            this.generation = generation;
            this.cor = RandomColorGenerator.GenerationColor(random, generation);
            contadorDirecao = 0;
            direcao = 0;
            this.maxIdade = maxIdade;
            this.numFilhos = 0;
            this.causaMorte = "";
            this.pai = pai;

            int num;
            this.comidaConsumida = 0.5f;
            //if (comidaConsumida > 0.2f)
            //{
            //    num = random.Next(0, 2);
            //    if (num == 0)
            //    {
            //        this.comidaConsumida = comidaConsumida - 0.01f;
            //    }
            //    else
            //    {
            //        this.comidaConsumida = comidaConsumida + 0.01f;
            //    }
            //}
            //else
            //{
            //    this.comidaConsumida = comidaConsumida + 0.01f;
            //}

            /*
            if (maxFomeConfortavel > 2f)
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxFomeConfortavel = maxFomeConfortavel - 1;
                }
                else
                {
                    this.maxFomeConfortavel = maxFomeConfortavel + 1;
                }
            }
            else
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxFomeConfortavel = 1;
                }
                else
                {
                    this.maxFomeConfortavel = maxFomeConfortavel + 1;
                }
            }
            */

            if (maxContadorReproducoes > 2f)
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxContadorReproducoes = maxContadorReproducoes - 1;
                }
                else
                {
                    this.maxContadorReproducoes = maxContadorReproducoes + 1;
                }
            }
            else
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxContadorReproducoes = 1;
                }
                else
                {
                    this.maxContadorReproducoes = maxContadorReproducoes + 1;
                }
            }

            this.taxaFome = taxaFome;
            //if (taxaFome > 0.3f)
            //{
            //    int num = random.Next(0, 2);
            //    if (num == 0)
            //    {
            //        this.taxaFome = taxaFome - 0.1f;
            //    }
            //    else
            //    {
            //        this.taxaFome = taxaFome + 0.1f;
            //    }
            //}
            //else
            //{
            //    int num = random.Next(0, 2);
            //    if (num == 0)
            //    {
            //        this.taxaFome = 0.2f;
            //    }
            //    else
            //    {
            //        this.taxaFome = taxaFome + 0.1f;
            //    }
            //}

            if (maxContadorDirecao > 2f)
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxContadorDirecao = maxContadorDirecao - 1;
                }
                else
                {
                    this.maxContadorDirecao = maxContadorDirecao + 1;
                }
            }
            else
            {
                num = random.Next(0, 2);
                if (num == 0)
                {
                    this.maxContadorDirecao = 1;
                }
                else
                {
                    this.maxContadorDirecao = maxContadorDirecao + 1;
                }
            }
        }

        public void Update(GameTime gameTime, Random random, Terreno terreno, List<LiveEntity> entidades)
        {


            if (fome > taxaFome)
            {
                //tem fome, comer
                if (terreno.comida[(int)pos.X, (int)pos.Y] >= comidaConsumida)
                {
                    fome -= comidaConsumida;
                    terreno.comida[(int)pos.X, (int)pos.Y] -= comidaConsumida;
                }
                else if (terreno.comida[(int)pos.X, (int)pos.Y] >= taxaFome)
                {
                    fome -= terreno.comida[(int)pos.X, (int)pos.Y];
                    terreno.comida[(int)pos.X, (int)pos.Y] = 0;
                }
                else
                {
                    {
                        //Não há comida aqui, andar
                        Andar(random);
                    }

                }
            }
            else
            {
                Andar(random);
            }
            
            
            idade++;
            contadorReproducao++;
            

            //Verificar morte!
            if (fome > maxFome || idade > maxIdade) //2 minutos * 60 fps
            {
                vivo = false;
                if (fome > maxFome)
                {
                    this.causaMorte = "Fome";
                }
                else
                {
                    this.causaMorte = "Velhice";
                }
            }

            if (this.idade > maxIdade / 4)
            {
                entityTemp = entidades.Find(x => x.Pos.X == this.pos.X &&
                x.Pos.Y == this.pos.Y && x != this);

                if (entityTemp != null)
                {

                    if (entidades.Find(x => x == entityTemp &&
                    x.Idade > x.maxIdade / 4 &&
                    x.Vivo &&
                    x != this.pai &&
                    x.Pai != this) != null)
                    {
                        vivo = false;
                        this.causaMorte = "Violencia";
                    }
                }
            }
            
        }

        private void Andar(Random random)
        {

            if (contadorDirecao <= 0 || contadorDirecao > maxContadorDirecao)
            {
                direcao = random.Next(0, 4);
                contadorDirecao = 1;
            }

            int num = random.Next(0, 2);
            switch (direcao)
            {
                case 0:
                    if (pos.X > 0)
                    {
                        pos.X--;
                    }
                    
                    if (num == 0)
                    {
                        int num2 = random.Next(0, 2);
                        if (num2 == 0)
                        {
                            if (pos.Y > 0)
                            {
                                pos.Y--;
                            }
                        }
                        else
                        {
                            if (pos.Y < height - 1)
                            {
                                pos.Y++;
                            }
                        }
                    }
                    break;
                case 1:
                    if (pos.X < width - 1)
                    {
                        pos.X++;
                    }
                    if (num == 0)
                    {
                        int num2 = random.Next(0, 2);
                        if (num2 == 0)
                        {
                            if (pos.Y > 0)
                            {
                                pos.Y--;
                            }
                        }
                        else
                        {
                            if (pos.Y < height - 1)
                            {
                                pos.Y++;
                            }
                        }
                    }
                    break;
                case 2:
                    if (pos.Y > 0)
                    {
                        pos.Y--;
                    }
                    if (num == 0)
                    {
                        int num2 = random.Next(0, 2);
                        if (num2 == 0)
                        {
                            if (pos.X > 0)
                            {
                                pos.X--;
                            }
                        }
                        else
                        {
                            if (pos.X < width - 1)
                            {
                                pos.X++;
                            }
                        }
                    }
                    break;
                case 3:
                    if (pos.Y < height - 1)
                    {
                        pos.Y++;
                    }
                    if (num == 0)
                    {
                        int num2 = random.Next(0, 2);
                        if (num2 == 0)
                        {
                            if (pos.X > 0)
                            {
                                pos.X--;
                            }
                        }
                        else
                        {
                            if (pos.X < width - 1)
                            {
                                pos.X++;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            fome += taxaFome;
            contadorDirecao++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this.pixel, pos, cor);
            spriteBatch.Draw(this.pixel, new Vector2(pos.X * Game1.escala, pos.Y * Game1.escala), null, cor, 0f, Vector2.Zero, Game1.escala, SpriteEffects.None, 0f);
            //spriteBatch.Draw(this.pixel, pos, (cor * ((255f - ((fome * 255 / this.maxFome)) ) / 255f)));
            //spriteBatch.Draw(this.pixel, pos, (cor * ((255f - ((fome * 255 / this.maxFome))) / 255f)));
        }

    }
}
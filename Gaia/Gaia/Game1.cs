using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Gaia.Biome;
using Gaia.Entities;
using System.Text;
using Gaia.Utils;
using System.IO;

namespace Gaia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        Random random;
        Terreno terreno;
        List<LiveEntity> entidades;
        List<LiveEntity> entidadesMortas;
        List<LiveEntity> entidadesEmbriao;
        SpriteFont arial12;
        StringBuilder stringBuilder;
        int contadorReproducoes;
        int contadorMortes;
        StreamWriter w;
        bool desenhar;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = false;
            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 200;
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = false;           
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            random = new Random();
            entidades = new List<LiveEntity>();
            entidadesMortas = new List<LiveEntity>();
            entidadesEmbriao = new List<LiveEntity>();
            stringBuilder = new StringBuilder();
            contadorReproducoes = 0;
            contadorMortes = 0;
            RandomColorGenerator.Initialize();
            w = new StreamWriter("dados.txt");
            desenhar = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = Content.Load<Texture2D>("Textures/pixel");
            arial12 = Content.Load<SpriteFont>("Fonts/arial_12");
            terreno = new Terreno(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height,
                pixel,
                random);

            for (int i = 0; i < 5; i++)
            {
                entidades.Add(new LiveEntity(random, 
                    pixel, 
                    GraphicsDevice.Viewport.Width, 
                    GraphicsDevice.Viewport.Height,
                    random.Next(0, (GraphicsDevice.Viewport.Width - 1)), 
                    random.Next(0, (GraphicsDevice.Viewport.Height - 1)), 
                    0,          //generation
                    2000,       //maxIdade
                    400,        //maxFome
                    500,        //maxContadorReproducoes
                    5,          //maxContadorDirecao
                    0.5f,       //comidaConsumida
                    0.3f,       //taxaFome
                    null,       //Pai
                    50          //maxFomeConfortavel
                    ));
            }

            var title = String.Format("Generation; Fome; MaxFome; ComidaConsumida; MaxIdade; Idade; MaxContadorDirecao; MaxContadorReproducoes; NumFilhos; CausaMorte; MaxFomeConfortavel; Pop");
            w.WriteLine(title);
            w.Flush();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                desenhar = !desenhar;
            }

            foreach (LiveEntity entidade in entidades)
            {
                entidade.Update(gameTime, random, terreno, entidades);
                if (!entidade.Vivo)
                {
                    entidadesMortas.Add(entidade);
                    contadorMortes++;
                }
                //Reproduzir as que estão prontas
                if (entidade.Fome < entidade.MaxFome / 4 && 
                    entidade.Idade > entidade.MaxIdade / 6 && 
                    //entidade.Idade < entidade.MaxIdade - (entidade.MaxIdade / 6) && 
                    entidade.ContadorReproducao > entidade.MaxContadorReproducoes)
                {
                    entidade.ContadorReproducao = 0;
                    entidadesEmbriao.Add(new LiveEntity(random,
                        pixel,
                        GraphicsDevice.Viewport.Width,
                        GraphicsDevice.Viewport.Height,
                        (int)entidade.Pos.X, 
                        (int)entidade.Pos.Y,
                        entidade.Generation + 1,
                        random.Next(entidade.MaxIdade - 20, entidade.MaxIdade + 21),
                        random.Next(entidade.MaxFome - 5, entidade.MaxFome + 6),
                        entidade.MaxContadorReproducoes,
                        entidade.MaxContadorDirecao,
                        entidade.ComidaConsumida,
                        entidade.TaxaFome,
                        entidade,
                        entidade.MaxFomeConfortavel
                        ));
                    contadorReproducoes++;
                    entidade.NumFilhos++;
                }
            }
            foreach (LiveEntity entidade in entidadesEmbriao)
            {
                entidades.Add(entidade);
            }
            entidadesEmbriao.Clear();

            //remover as mortas
            foreach (LiveEntity entidade in entidadesMortas)
            {
                entidades.Remove(entidade);
                
                var line = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                    entidade.Generation.ToString(),
                    ((double)entidade.Fome).ToString(),
                    entidade.MaxFome.ToString(),
                    ((double)entidade.ComidaConsumida).ToString(),
                    entidade.MaxIdade.ToString(),
                    entidade.Idade.ToString(),
                    entidade.MaxContadorDirecao.ToString(),
                    entidade.MaxContadorReproducoes.ToString(),
                    entidade.NumFilhos.ToString(),
                    entidade.CausaMorte,
                    entidade.MaxFomeConfortavel,
                    entidades.Count.ToString()
                    );
                w.WriteLine(line);
                w.Flush();
            }
            entidadesMortas.Clear();

            terreno.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            spriteBatch.Begin();

            if (desenhar)
            {
                //Desenhar terreno
                terreno.Draw(spriteBatch);

                foreach (LiveEntity entidade in entidades)
                {
                    entidade.Draw(spriteBatch);
                }
            }
            

            stringBuilder.Clear();
            stringBuilder.Append("Entidades: ");
            stringBuilder.Append(entidades.Count);
            stringBuilder.AppendLine();
            stringBuilder.Append("Reproducoes: ");
            stringBuilder.Append(contadorReproducoes);
            stringBuilder.AppendLine();
            stringBuilder.Append("Mortes: ");
            stringBuilder.Append(contadorMortes);
            stringBuilder.AppendLine();
            stringBuilder.Append("Generations: ");
            stringBuilder.Append(RandomColorGenerator.NumGenerations());
            
            spriteBatch.DrawString(arial12, stringBuilder, Vector2.Zero, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

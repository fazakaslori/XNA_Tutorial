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

namespace Tutorial
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D xnaImageTexture;
        Texture2D xnaTransparentTxture;

        Vector2 xnaImagePos;
        Vector2 xnaTransparentImagePos;

        float xnaImageMoveSpeed = 4f;
        float xnaTransparentImageMoveSpeed = 3f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            // TODO: use this.Content to load your game content here
            xnaImageTexture = Content.Load<Texture2D>(@"Images/logo");
            xnaTransparentTxture = Content.Load<Texture2D>(@"Images/logo_trans");

            xnaImagePos = Vector2.Zero;
            xnaTransparentImagePos = new Vector2(Window.ClientBounds.Width - xnaImageTexture.Width, Window.ClientBounds.Height - xnaImageTexture.Height);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //moving around
            xnaImagePos.X += xnaImageMoveSpeed;
            xnaTransparentImagePos.Y += xnaTransparentImageMoveSpeed;

            //collision detection
            if (xnaImagePos.X > Window.ClientBounds.Width - xnaImageTexture.Width || xnaImagePos.X < 0)
            {
                xnaImageMoveSpeed *= -1;
            }

            if (xnaTransparentImagePos.Y > Window.ClientBounds.Height - xnaTransparentTxture.Height || xnaTransparentImagePos.Y < 0)
            {
                xnaTransparentImageMoveSpeed *= -1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend); //makes use of Z layering

            spriteBatch.Draw(xnaImageTexture, xnaImagePos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(xnaTransparentTxture, xnaTransparentImagePos, null, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 1);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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

namespace AnimatedSprites
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Texxture variables
        Texture2D animatedRingTexture;
        Texture2D animatedSkullTexture;

        //animation variables
        Point animatedRingFrameSize = new Point(75, 75);
        Point currentRingFrame = new Point(0, 0);
        Point animatedRingSpriteSheetSize = new Point(6, 8);
        SpriteAinationTime ringAnimation;

        Point animatedSkullFrameSize = new Point(75, 75);
        Point currentSkullFrame = new Point(0, 0);
        Point animatedSkullSpriteSheetSize = new Point(6, 8);
        SpriteAinationTime skullAnimation;

        //variables for movespeeds and positioning
        Vector2 ringMoveSpeed;
        Vector2 animatedRingPosition;

        Vector2 skullMoveSpeed;
        Vector2 animatedSkullPosition;

        struct SpriteAinationTime
        {
            public int TimeSinceLastUpdate { get; set; }
            public int FramesPerSecond { get; set; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //chaging the frame rate!
            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 50); //20fps
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ringAnimation = new SpriteAinationTime();
            ringAnimation.FramesPerSecond = 60;
            ringAnimation.TimeSinceLastUpdate = 0;

            animatedRingPosition = Vector2.Zero;
            ringMoveSpeed = new Vector2(5, 3);

            skullAnimation = new SpriteAinationTime();
            skullAnimation.FramesPerSecond = 60;
            skullAnimation.TimeSinceLastUpdate = 0;

            animatedSkullPosition = Vector2.Zero;
            skullMoveSpeed = new Vector2(2, 2);

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

            //loading sprites
            animatedRingTexture = Content.Load<Texture2D>(@"images\threerings");
            animatedSkullTexture = Content.Load<Texture2D>(@"images\skullball");
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

            //animate the ring image - a.k.a increse the index of the image in the image matrix
            ringAnimation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (ringAnimation.TimeSinceLastUpdate > 1000 / ringAnimation.FramesPerSecond)
            {
                ringAnimation.TimeSinceLastUpdate -= 1000 / ringAnimation.FramesPerSecond;

                ++currentRingFrame.X;
                if (currentRingFrame.X >= animatedRingSpriteSheetSize.X)
                {
                    currentRingFrame.X = 0;
                    ++currentRingFrame.Y;
                    if (currentRingFrame.Y >= animatedRingSpriteSheetSize.Y)
                        currentRingFrame.Y = 0;
                }
            }

            //animate the skull image - a.k.a increse the index of the image in the image matrix
            skullAnimation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (skullAnimation.TimeSinceLastUpdate > 1000 / skullAnimation.FramesPerSecond)
            {
                skullAnimation.TimeSinceLastUpdate -= 1000 / skullAnimation.FramesPerSecond;

                ++currentSkullFrame.X;
                if (currentSkullFrame.X >= animatedSkullSpriteSheetSize.X)
                {
                    currentSkullFrame.X = 0;
                    ++currentSkullFrame.Y;
                    if (currentSkullFrame.Y >= animatedSkullSpriteSheetSize.Y)
                        currentSkullFrame.Y = 0;
                }
            }

            //move the ring along the X AXIS
            animatedRingPosition.X += ringMoveSpeed.X;
            if (animatedRingPosition.X > Window.ClientBounds.Width - animatedRingFrameSize.X || animatedRingPosition.X < 0)
            {
                ringMoveSpeed.X *= -1;
            }

            //move the ring along the Y AXIS
            animatedRingPosition.Y += ringMoveSpeed.Y;
            if (animatedRingPosition.Y >= Window.ClientBounds.Height - animatedRingFrameSize.Y || animatedRingPosition.Y < 0)
            {
                ringMoveSpeed.Y *= -1;
            }

            //move the ring along the X AXIS
            animatedSkullPosition.X += skullMoveSpeed.X;
            if (animatedSkullPosition.X > Window.ClientBounds.Width - animatedSkullFrameSize.X || animatedSkullPosition.X < 0)
            {
                skullMoveSpeed.X *= -1;
            }

            //move the ring along the Y AXIS
            animatedSkullPosition.Y += skullMoveSpeed.Y;
            if (animatedSkullPosition.Y >= Window.ClientBounds.Height - animatedSkullFrameSize.Y || animatedSkullPosition.Y < 0)
            {
                skullMoveSpeed.Y *= -1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(animatedRingTexture, animatedRingPosition, 
                new Rectangle(currentRingFrame.X * animatedRingFrameSize.X, currentRingFrame.Y * animatedRingFrameSize.Y, animatedRingFrameSize.X, animatedRingFrameSize.Y), 
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.Draw(animatedSkullTexture, animatedSkullPosition,
                new Rectangle(currentSkullFrame.X * animatedSkullFrameSize.X, currentSkullFrame.Y * animatedSkullFrameSize.Y, animatedSkullFrameSize.X, animatedSkullFrameSize.Y),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

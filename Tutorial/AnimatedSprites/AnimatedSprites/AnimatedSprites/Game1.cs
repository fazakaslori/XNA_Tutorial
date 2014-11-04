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

        //mouse helpers
        MouseState prevMouseState;

        //Texxture variables
        Texture2D ringTexture;
        Texture2D skullTexture;
        Texture2D plusTexture;

        //animation variables
        Point ringFrameSize = new Point(75, 75);
        Point currentRingFrame = new Point(0, 0);
        Point ringSpriteSheetSize = new Point(6, 8);
        SpriteAinationTime ringAnimation;

        Point skullFrameSize = new Point(75, 75);
        Point currentSkullFrame = new Point(0, 0);
        Point skullSpriteSheetSize = new Point(6, 8);
        SpriteAinationTime skullAnimation;

        Point plusFrameSize = new Point(75, 75);
        Point currentPlusFrame = new Point(0, 0);
        Point plusSpriteSheetSize = new Point(6, 4);
        SpriteAinationTime plusAnimation;

        //variables for movespeeds and positioning
        Vector2 ringMoveSpeed;
        Vector2 ringPosition;

        Vector2 skullMoveSpeed;
        Vector2 skullPosition;

        Vector2 plusMoveSpeed;
        Vector2 plusPosition;

        int ringCollisionRectOffset = 10;
        int skullCollisionRectOffset = 10;
        int plusCollisionRectOffset = 10;

        struct SpriteAinationTime
        {
            public int TimeSinceLastUpdate { get; set; }
            public int FramesPerSecond { get; set; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

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
            //initializing the rings object
            ringAnimation = new SpriteAinationTime();
            ringAnimation.FramesPerSecond = 60;
            ringAnimation.TimeSinceLastUpdate = 0;

            ringPosition = Vector2.Zero;
            ringMoveSpeed = new Vector2(5, 3);

            //initializing the skull object
            skullAnimation = new SpriteAinationTime();
            skullAnimation.FramesPerSecond = 60;
            skullAnimation.TimeSinceLastUpdate = 0;

            skullPosition = Vector2.Zero;
            skullMoveSpeed = new Vector2(2, 2);

            //initializing the plus object
            plusAnimation = new SpriteAinationTime();
            plusAnimation.FramesPerSecond = 60;
            plusAnimation.TimeSinceLastUpdate = 0;

            plusPosition = Vector2.Zero;
            plusMoveSpeed = new Vector2(4, 6);

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
            ringTexture = Content.Load<Texture2D>(@"images\threerings");
            skullTexture = Content.Load<Texture2D>(@"images\skullball");
            plusTexture = Content.Load<Texture2D>(@"images\plus");
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

            var keyboardState = Keyboard.GetState();

            //animate the ring image - a.k.a increse the index of the image in the image matrix
            ringAnimation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (ringAnimation.TimeSinceLastUpdate > 1000 / ringAnimation.FramesPerSecond)
            {
                ringAnimation.TimeSinceLastUpdate -= 1000 / ringAnimation.FramesPerSecond;

                ++currentRingFrame.X;
                if (currentRingFrame.X >= ringSpriteSheetSize.X)
                {
                    currentRingFrame.X = 0;
                    ++currentRingFrame.Y;
                    if (currentRingFrame.Y >= ringSpriteSheetSize.Y)
                        currentRingFrame.Y = 0;
                }
            }

            //animate the skull image - a.k.a increse the index of the image in the image matrix
            skullAnimation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (skullAnimation.TimeSinceLastUpdate > 1000 / skullAnimation.FramesPerSecond)
            {
                skullAnimation.TimeSinceLastUpdate -= 1000 / skullAnimation.FramesPerSecond;

                ++currentSkullFrame.X;
                if (currentSkullFrame.X >= skullSpriteSheetSize.X)
                {
                    currentSkullFrame.X = 0;
                    ++currentSkullFrame.Y;
                    if (currentSkullFrame.Y >= skullSpriteSheetSize.Y)
                        currentSkullFrame.Y = 0;
                }
            }

            //animate the plus image - a.k.a increse the index of the image in the image matrix
            plusAnimation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (plusAnimation.TimeSinceLastUpdate > 1000 / plusAnimation.FramesPerSecond)
            {
                plusAnimation.TimeSinceLastUpdate -= 1000 / plusAnimation.FramesPerSecond;

                ++currentPlusFrame.X;
                if (currentPlusFrame.X >= plusSpriteSheetSize.X)
                {
                    currentPlusFrame.X = 0;
                    ++currentPlusFrame.Y;
                    if (currentPlusFrame.Y >= plusSpriteSheetSize.Y)
                        currentPlusFrame.Y = 0;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ringPosition.X += ringMoveSpeed.X;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ringPosition.X -= ringMoveSpeed.X;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ringPosition.Y -= ringMoveSpeed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ringPosition.Y += ringMoveSpeed.Y;
            }

            //move the ring along the X AXIS
            //animatedRingPosition.X += ringMoveSpeed.X;
            if (ringPosition.X >= Window.ClientBounds.Width - ringFrameSize.X)
            {
                ringPosition.X = Window.ClientBounds.Width - ringFrameSize.X;
            }
            if (ringPosition.X <= 0)
            {
                ringPosition.X = 0;
            }

            //move the ring along the Y AXIS
            //animatedRingPosition.Y += ringMoveSpeed.Y;
            if (ringPosition.Y >= Window.ClientBounds.Height - ringFrameSize.Y)
            {
                ringPosition.Y = Window.ClientBounds.Height - ringFrameSize.Y;
            }
            if (ringPosition.Y <= 0)
            {
                ringPosition.Y = 0;
            }

            //move the skull along the X AXIS
            skullPosition.X += skullMoveSpeed.X;
            if (skullPosition.X > Window.ClientBounds.Width - skullFrameSize.X || skullPosition.X <= 0)
            {
                skullMoveSpeed.X *= -1;
            }

            //move the skull along the Y AXIS
            skullPosition.Y += skullMoveSpeed.Y;
            if (skullPosition.Y >= Window.ClientBounds.Height - skullFrameSize.Y || skullPosition.Y <= 0)
            {
                skullMoveSpeed.Y *= -1;
            }

            //move the plus along the X AXIS
            plusPosition.X += plusMoveSpeed.X;
            if (plusPosition.X > Window.ClientBounds.Width - plusFrameSize.X || plusPosition.X <= 0)
            {
                plusMoveSpeed.X *= -1;
            }

            //move the plus along the Y AXIS
            plusPosition.Y += plusMoveSpeed.Y;
            if (plusPosition.Y >= Window.ClientBounds.Height - plusFrameSize.Y || plusPosition.Y <= 0)
            {
                plusMoveSpeed.Y *= -1;
            }

            MouseState mouseState = Mouse.GetState();
            if (mouseState.X != prevMouseState.X || mouseState.Y != prevMouseState.Y)
            {
                ringPosition = new Vector2(mouseState.X, mouseState.Y);
            }
            prevMouseState = mouseState;

            if (Collide())
            {
                Exit();
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

            spriteBatch.Draw(ringTexture, ringPosition, 
                new Rectangle(currentRingFrame.X * ringFrameSize.X, currentRingFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), 
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.Draw(skullTexture, skullPosition,
                new Rectangle(currentSkullFrame.X * skullFrameSize.X, currentSkullFrame.Y * skullFrameSize.Y, skullFrameSize.X, skullFrameSize.Y),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.Draw(plusTexture, plusPosition,
                new Rectangle(currentPlusFrame.X * plusFrameSize.X, currentPlusFrame.Y * plusFrameSize.Y, plusFrameSize.X, plusFrameSize.Y),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool Collide()
        {
            Rectangle ringsRect = new Rectangle((int)ringPosition.X + ringCollisionRectOffset, (int)ringPosition.Y + ringCollisionRectOffset,
                ringFrameSize.X - (ringCollisionRectOffset * 2), ringFrameSize.Y - (ringCollisionRectOffset * 2));
            Rectangle skullRect = new Rectangle((int)skullPosition.X + skullCollisionRectOffset, (int)skullPosition.Y + skullCollisionRectOffset,
                skullFrameSize.X - (skullCollisionRectOffset * 2), skullFrameSize.Y - (skullCollisionRectOffset * 2));
            Rectangle plusRect = new Rectangle((int)plusPosition.X + plusCollisionRectOffset, (int)plusPosition.Y + plusCollisionRectOffset,
                plusFrameSize.X - (plusCollisionRectOffset * 2), plusFrameSize.Y - (plusCollisionRectOffset * 2));

            return ringsRect.Intersects(skullRect) || ringsRect.Intersects(plusRect);
        }
    }
}

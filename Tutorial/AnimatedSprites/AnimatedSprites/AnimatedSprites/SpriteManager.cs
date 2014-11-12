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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControlledSprite player; //could also be a list...
        List<Sprite> automatedSprites = new List<Sprite>();

        private int EnemySpawnMinMilliseconds { get { return 1000; } }
        private int EnemySpawnMaxMilliseconds { get { return 2000; } }

        private int EnemySpawnMinSpeed { get { return 2; } }
        private int EnemySpawnMaxSpeed { get { return 6; } }

        private int NextSpawnTime { get; set; }

        public SpriteManager(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Images/threerings"), Vector2.Zero, new Point(75, 75), 12, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6));

 	        base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            ResetSpawnTime();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //update player
            player.Update(gameTime, Game.Window.ClientBounds);

            NextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextSpawnTime <= 0)
            {
                SpanwEnemy();
                ResetSpawnTime();
            }

            //update Sprites
            for (int i = 0; i < automatedSprites.Count; ++i)
            {
                automatedSprites[i].Update(gameTime, Game.Window.ClientBounds);

                //check for collision detection
                if (automatedSprites[i].collisionRect.Intersects(player.collisionRect))
                {
                    // Play collision sound
                    if (automatedSprites[i].CollisionCueName != null)
                    {
                        ((Game1)Game).PlayCue(automatedSprites[i].CollisionCueName);
                    }
                    // Remove the automated Sprite
                    automatedSprites.RemoveAt(i);
                    --i;
                }

                //remove irrelevant objects
                else if (automatedSprites[i].IsOutOfBounds(Game.Window.ClientBounds))
                {
                    automatedSprites.RemoveAt(i);
                    --i;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // Draw the player
            player.Draw(gameTime, spriteBatch);

            // Draw all sprites
            foreach (Sprite sprite in automatedSprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }
                
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetSpawnTime()
        {
            NextSpawnTime = ((Game1)Game).Randomizer.Next(EnemySpawnMinMilliseconds, EnemySpawnMaxMilliseconds);
        }

        private void SpanwEnemy()
        {
            var currentFrameX = ((Game1)Game).Randomizer.Next(0, 5);
            var currentFrameY = ((Game1)Game).Randomizer.Next(0, 7);

            Vector2 position = Vector2.Zero;
            Vector2 speed = Vector2.Zero;

            Point frameSize = new Point(75, 75);

            switch (((Game1)Game).Randomizer.Next(4))
            {
                //Left to Right
                case 0:
                    position = new Vector2(-frameSize.X, ((Game1)Game).Randomizer.Next(0, Game.GraphicsDevice.PresentationParameters.BackBufferHeight - frameSize.Y));

                    speed = new Vector2(((Game1)Game).Randomizer.Next(EnemySpawnMinSpeed, EnemySpawnMaxSpeed), 0);
                    break;
                //Up to Down
                case 1:
                    position = new Vector2(((Game1)Game).Randomizer.Next(0, Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X), -frameSize.Y);

                    speed = new Vector2(0, ((Game1)Game).Randomizer.Next(EnemySpawnMinSpeed, EnemySpawnMaxSpeed));
                    break;
                //Right to Left
                case 2:
                    position = new Vector2(Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        ((Game1)Game).Randomizer.Next(0, Game.GraphicsDevice.PresentationParameters.BackBufferHeight - frameSize.Y));

                    speed = new Vector2(-((Game1)Game).Randomizer.Next(EnemySpawnMinSpeed, EnemySpawnMaxSpeed), 0);
                    break;
                //Down to Up
                case 3:
                    position = new Vector2(((Game1)Game).Randomizer.Next(0, Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);

                    speed = new Vector2(0, -((Game1)Game).Randomizer.Next(EnemySpawnMinSpeed, EnemySpawnMaxSpeed));
                    break;
            }

            var newEnemy = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Images\skullball"), position, frameSize, 12,
                new Point(currentFrameX, currentFrameY), new Point(6, 8), speed, "skullcollision");

            automatedSprites.Add(newEnemy);
        }
    }
}

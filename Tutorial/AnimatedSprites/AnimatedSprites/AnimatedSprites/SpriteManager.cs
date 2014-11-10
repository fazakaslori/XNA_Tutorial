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

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Images/threerings"), Vector2.Zero, new Point(75, 75), 12, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6));

            automatedSprites.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"Images/skullball"), new Vector2(150, 150), new Point(75, 75), 12,
                new Point(0, 0), new Point(6, 8), Vector2.Zero, "skullcollision"));
            automatedSprites.Add(new BouncingSprite(Game.Content.Load<Texture2D>(@"Images/skullball"), new Vector2(300, 150), new Point(75, 75), 12,
                new Point(0, 0), new Point(6, 8), new Vector2(1, 2), "skullcollision"));
            automatedSprites.Add(new BouncingSprite(Game.Content.Load<Texture2D>(@"Images/plus"), new Vector2(150, 300), new Point(75, 75), 12,
                new Point(0, 0), new Point(6, 4), new Vector2(1, 1), "skullcollision"));
            automatedSprites.Add(new BouncingSprite(Game.Content.Load<Texture2D>(@"Images/plus"), new Vector2(600, 400), new Point(75, 75), 12,
                new Point(0, 0), new Point(6, 4), new Vector2(2, 2), "skullcollision"));

 	        base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

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
                        ((Game)Game).PlayCue(automatedSprites[i].CollisionCueName);
                    }
                    // Remove the automated Sprite
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
    }
}

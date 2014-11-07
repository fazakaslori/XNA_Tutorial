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


namespace Escape_The_Objects.Sprites
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpritesManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch SpriteBatch { get; set; }

        private UserControlledSprite user;
        private List<SelfMovingSprite> selfMovingSprites;
        private List<StaticSprite> staticSprites;

        public SpritesManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            selfMovingSprites = new List<SelfMovingSprite>();
            staticSprites = new List<StaticSprite>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            staticSprites.Add(new StaticSprite(Game.Content.Load<Texture2D>("Images/skullball"), new Point(6, 8), new Point(75, 75), 60, new Point(0, 0),
                new Vector2(150, 100)));
            staticSprites.Add(new StaticSprite(Game.Content.Load<Texture2D>("Images/skullball"), new Point(6, 8), new Point(75, 75), 60, new Point(3, 5),
                new Vector2(550, 100)));
            staticSprites.Add(new StaticSprite(Game.Content.Load<Texture2D>("Images/skullball"), new Point(6, 8), new Point(75, 75), 60, new Point(5, 2),
                new Vector2(150, 300)));
            staticSprites.Add(new StaticSprite(Game.Content.Load<Texture2D>("Images/skullball"), new Point(6, 8), new Point(75, 75), 60, new Point(2, 6),
                new Vector2(550, 300)));

            selfMovingSprites.Add(new SelfMovingSprite(Game.Content.Load<Texture2D>("Images/plus"), new Point(6, 4), new Point(75, 75), 60, new Point(2, 2),
                new Vector2(550, 300), Game.Window.ClientBounds, new Vector2(-4, 4)));
            selfMovingSprites.Add(new SelfMovingSprite(Game.Content.Load<Texture2D>("Images/plus"), new Point(6, 4), new Point(75, 75), 60, new Point(3, 3),
                new Vector2(550, 300), Game.Window.ClientBounds, new Vector2(3, 3)));

            user = new UserControlledSprite(Game.Content.Load<Texture2D>("Images/threerings"), new Point(6, 8), new Point(75, 75), 60, new Point(2, 1),
                new Vector2(350, 200), Game.Window.ClientBounds, new Vector2(-3, 3));

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            foreach (SpriteBase sprite in staticSprites)
            {
                sprite.Update(gameTime);
            }

            foreach (SpriteBase sprite in selfMovingSprites)
            {
                sprite.Update(gameTime);
            }

            user.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            foreach (SpriteBase sprite in staticSprites)
            {
                sprite.Draw(gameTime, SpriteBatch);
            }

            foreach (SpriteBase sprite in selfMovingSprites)
            {
                sprite.Draw(gameTime, SpriteBatch);
            }

            user.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

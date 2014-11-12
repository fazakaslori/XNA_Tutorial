using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites
{
    internal class SpriteAinationTime
    {
        public int TimeSinceLastUpdate { get; set; }
        public int FramesPerSecond { get; set; }

        internal SpriteAinationTime(int framesPerSec)
        {
            FramesPerSecond = framesPerSec;
        }
    }

    public abstract class Sprite
    {
        Texture2D texture;
        
        //animation variables
        protected Point frameSize;
        Point currentFrame;

        Point sheetSize;
        SpriteAinationTime animation;
        const int framesPerSecond = 60;

        //variables for movespeeds and positioning
        protected Vector2 moveSpeed;
        protected Vector2 position;

        int collisionRectOffset;
        public string CollisionCueName { get; private set; }

        public abstract Vector2 Direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle((int)position.X + collisionRectOffset, (int)position.Y + collisionRectOffset, frameSize.X - (collisionRectOffset * 2),
                    frameSize.Y - (collisionRectOffset * 2));
            }
        }

        public Sprite(Texture2D texture, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 moveSpeed, string collisionCueName)
            : this(texture, position, frameSize, collisionOffset, currentFrame, sheetSize, moveSpeed, framesPerSecond, collisionCueName)
        {

        }

        public Sprite(Texture2D texture, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 moveSpeed, int framesPerSecond, string collisionCueName)
        {
            this.texture = texture;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionRectOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.moveSpeed = moveSpeed;
            this.animation = new SpriteAinationTime(framesPerSecond);
            CollisionCueName = collisionCueName;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            animation.TimeSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (animation.TimeSinceLastUpdate > 1000 / animation.FramesPerSecond)
            {
                animation.TimeSinceLastUpdate -= 1000 / animation.FramesPerSecond;

                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public bool IsOutOfBounds(Rectangle rectangle)
        {
            if (position.X < -frameSize.X || position.X > rectangle.Width || position.Y < -frameSize.Y || position.Y > rectangle.Height)
            {
                return true;
            }

            return false;
        }
    }
}

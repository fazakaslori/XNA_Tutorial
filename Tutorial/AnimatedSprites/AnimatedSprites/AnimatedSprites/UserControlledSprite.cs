using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites
{
    class UserControlledSprite : Sprite
    {
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, null)
        { 

        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame, null)
        { 

        }

        public override Vector2 Direction
        {
            get 
            {
                Vector2 inputPosition = Vector2.Zero;

                if (Keyboard.GetState( ).IsKeyDown(Keys.Left))
                    inputPosition.X -= 1;
                if (Keyboard.GetState( ).IsKeyDown(Keys.Right))
                    inputPosition.X += 1;
                if (Keyboard.GetState( ).IsKeyDown(Keys.Up))
                    inputPosition.Y -= 1;
                if (Keyboard.GetState( ).IsKeyDown(Keys.Down))
                    inputPosition.Y += 1;

                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputPosition.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputPosition.Y -= gamepadState.ThumbSticks.Left.Y;

                return inputPosition * moveSpeed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move the sprite based on direction
            position += Direction;

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;

            base.Update(gameTime, clientBounds);
        }
    }
}

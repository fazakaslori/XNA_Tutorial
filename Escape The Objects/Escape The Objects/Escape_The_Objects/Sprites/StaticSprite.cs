using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public class StaticSprite : SpriteBase
    {
        public StaticSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Point collisionRectangle)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, collisionRectangle)
        {
        }
    }
}

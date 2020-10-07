using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHellGame
{
    public class SpriteSheet
    {
        public Texture2D texture { get; set; }
        public int updateSpeed {get; set;} // number of Updates() before animation moves to next frame.
        private int updateCounter;
        public int rows { get; set; }
        public int columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        
        public SpriteSheet(Texture2D texture, int rows, int columns, int updateSpeed=7) 
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.updateSpeed = updateSpeed;
            updateCounter = updateSpeed;
            currentFrame = 0;
            totalFrames = rows * columns;
        }
        public void Update()
        {
            updateCounter--; // number of Updates() left until next sprite frame
            if (updateCounter==0) 
            {
                updateCounter = updateSpeed; //resets time until next frame

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int frameWidth = texture.Width /columns; 
            int frameHeight = texture.Height / rows;
            int currentRow = (int)((float)currentFrame / (float)columns);
            int currentColumn = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(frameWidth* currentColumn, frameHeight * currentRow, frameWidth, frameHeight);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(50, 50), sourceRectangle, Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 1);
            spriteBatch.End();
        }
    }
}

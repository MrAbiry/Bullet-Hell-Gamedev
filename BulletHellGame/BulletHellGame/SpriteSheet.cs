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
        public int updateSpeed { get; set; } // number of Updates() before animation moves to next frame.
        private int updateCounter;
        public int rows { get; set; }
        public int columns { get; set; }
        private int currentFrame;
        public int rowToDraw {get; set;} //zero-based index
        public int frameWidth;
        public int frameHeight;

        public SpriteSheet(Texture2D texture, int rows, int columns, int rowToDraw=0, int updateSpeed=7) 
        {
            this.texture = texture;
            this.rows = rows; // not zero indexed
            this.columns = columns; // not zero indexed
            this.updateSpeed = updateSpeed;
            updateCounter = updateSpeed;
            currentFrame = 0;

            if (rowToDraw > rows - 1) // sets default in case of out of bounds
                this.rowToDraw = 0;
            else this.rowToDraw = rowToDraw;

            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;
        }
        public void Update()
        {
            updateCounter--; // number of Updates() left until next sprite frame
            if (updateCounter==0) 
            {
                updateCounter = updateSpeed; //resets time until next frame

                currentFrame++;
                if (currentFrame == columns - 1) //subtracting 1 because currentFrame is zero-based index. columns is not.
                    currentFrame = rowToDraw*columns; // resets to first frame in the row, if frame reached end of row
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 drawLocation=new Vector2(), int? _rowToDraw=null, Color? _color =null, float rotation=0, Vector2 origin = new Vector2(), float scale=1, SpriteEffects spriteEffects=SpriteEffects.None, float layerDepth=1)
        {
            //in case the color value wasn't set, it sets the color to be white
            Color color; 
            color = _color ?? Color.White;
            rowToDraw = _rowToDraw ?? rowToDraw;

            //int currentRow = (int)((float)currentFrame / (float)columns);
            int currentColumn = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(frameWidth * currentColumn, frameHeight * rowToDraw, frameWidth, frameHeight);
            //Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, drawLocation, sourceRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.End();
        }
    }
}

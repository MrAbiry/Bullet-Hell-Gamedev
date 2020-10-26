using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BulletHellGame
{
    public enum MoveDirection // used to decide which animation to draw
    { //no support for diagonal-movement animations because we dont have any right now
        moveUp,
        moveDown,
        moveLeft,
        moveRight,
        moveIdle
    }
    public class Player
    {
        InputManager inputManager;
        TileMapManager tileMapManager;
        SpriteBatch spriteBatch;
        public Vector2 position { get; set; }
        public int movespeed {get; set;}
        Vector2 moveAngle { get; set; }
        public SpriteSheet[] spriteSheets { get; set; }
        public SpriteSheet spriteSheet;
        public MoveDirection moveDirection { get; set; }
        public Rectangle playerHitboxRect { get; set; }
        public Player(SpriteBatch spriteBatch, InputManager inputManager, TileMapManager tileMapManager, Vector2 position, int movespeed, SpriteSheet spriteSheet)
        {
            this.spriteBatch = spriteBatch;
            this.inputManager = inputManager;
            this.tileMapManager = tileMapManager;
            this.position = position;
            this.movespeed = movespeed;
            this.spriteSheet = spriteSheet;
            this.playerHitboxRect = new Rectangle(position.ToPoint(), new Point(spriteSheet.frameWidth, spriteSheet.frameHeight));
            moveDirection = MoveDirection.moveIdle;
            moveAngle = inputManager.GetPlayerMoveDirection();
        }
        public MoveDirection GetMoveDirection()
        {  
            if (moveAngle.X > 0) return MoveDirection.moveRight;
            if (moveAngle.X < 0) return MoveDirection.moveLeft;
            if (moveAngle.Y < 0) return MoveDirection.moveUp;
            if (moveAngle.Y > 0) return MoveDirection.moveDown;
            return MoveDirection.moveIdle;
        }
        public void MoveTo() // I am returning a vector instead of directly setting the new pos, because later I will run this through a collision checker first
        {
            moveAngle.Normalize();
            Vector2 moveBy = Vector2.Multiply(moveAngle, (float)movespeed);
            Vector2 targetPos = new Vector2(position.X + (int)moveBy.X, position.Y + (int)moveBy.Y);
            //Vector2 targetTileCoord = tileMapManager.getTileCoordFromPos(targetPos);
            if(tileMapManager.CanMoveToTile(playerHitboxRect,targetPos))
            {
                /*int tileIndex = tileMapManager.getTileIndexFromTileCoord(targetTileCoord);
                int tileID = tileMapManager.GetTileIDFromIndex(tileIndex);
                if(tileMapManager.getProperty(tileID,"Collision")=="false")
                {
                    position = targetPos;
                }*/
                //if(tileMapManager.IsLegalMove(targetPos))
                position = targetPos;
            }
        }

        public void Update()
        {
            moveAngle = inputManager.GetPlayerMoveDirection();
            moveDirection = GetMoveDirection();
            MoveTo();
            spriteSheet.Update();
        }
        public void Draw()
        {
            spriteSheet.Draw(spriteBatch, position,(int)moveDirection);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
        SpriteBatch spriteBatch;
        public Vector2 position { get; set; }
        public int movespeed {get; set;}
        Vector2 moveAngle { get; set; }
        public SpriteSheet[] spriteSheets { get; set; }
        public MoveDirection moveDirection { get; set; }
        

        public Player(SpriteBatch spriteBatch, InputManager inputManager, Vector2 position, int movespeed, SpriteSheet moveUp,SpriteSheet moveDown,SpriteSheet moveLeft,SpriteSheet moveRight,SpriteSheet moveIdle)
        {
            this.spriteBatch = spriteBatch;
            this.inputManager = inputManager;
            this.position = position;
            this.movespeed = movespeed;
            spriteSheets = new SpriteSheet[] { moveUp, moveDown, moveLeft, moveRight, moveIdle };
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
        public Vector2 MoveTo() // I am returning a vector instead of directly setting the new pos, because later I will run this through a collision checker first
        {
            Vector2 moveBy = Vector2.Multiply(moveAngle, (float)movespeed);
            return new Vector2(position.X + (int)moveBy.X, position.Y + (int)moveBy.Y);
        }
        public void Update()
        {
            moveAngle = inputManager.GetPlayerMoveDirection();
            moveDirection = GetMoveDirection();
            position = MoveTo();
            spriteSheets[(int)moveDirection].Update();
        }
        public void Draw()
        {
            spriteSheets[(int)moveDirection].Draw(spriteBatch, position);
        }
    }
}

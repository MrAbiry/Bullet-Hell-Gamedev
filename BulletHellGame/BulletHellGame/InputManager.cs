using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletHellGame
{
   public class InputManager
    {
        //current and previous key/mouse press:
        KeyboardState keyboardState;
        KeyboardState keyboardStatePrevious; //used to keep track of whether button is being pressed or held. the keyboardState of previous update
        MouseState mouseState;
        MouseState mouseStatePrevious;
        //movement controls
        Keys upMove;
        Keys downMove;
        Keys leftMove;
        Keys rightMove;
        int idleTime = 0; // time where no key at all has been pressed

        public InputManager()
        {
            keyboardState = Keyboard.GetState();
            keyboardStatePrevious = Keyboard.GetState(); //i called .getState() again because I know with some classes, if you do b=c and then a=b it turns it into a reference? not sure if it's the case here and cba to check
            UseWASDKeys(); //sets default movement keys to WASD;
            
        }
        private void DebugLog()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var key in keyboardState.GetPressedKeys())
                sb.Append("Key: ").Append(key).Append(" pressed ");
            if (mouseState.LeftButton == ButtonState.Pressed)
                sb.Append("Mouse: ").Append(mouseState.LeftButton).Append(" pressed");
        }
        public void Update()
        {
            keyboardStatePrevious = keyboardState;
            keyboardState = Keyboard.GetState();
            mouseStatePrevious = mouseState;
            mouseState = Mouse.GetState();

            DebugLog();
        }
        private bool IsMouseJustPressed()
        {
            return (mouseState.LeftButton == ButtonState.Pressed & mouseState != mouseStatePrevious); //checks to see if key is being pressed, and if this is different to previous Update()
        }
        private bool IsMouseHeld()
        {
            return (mouseState.LeftButton == ButtonState.Pressed & mouseState == mouseStatePrevious); //checks to see if a key is being pressed, and if this is different to previous Update()
        }
        private bool IsMouseReleased()
        {
            return (mouseStatePrevious.LeftButton == ButtonState.Pressed & mouseState.LeftButton == ButtonState.Pressed);
        }
        private bool IsKeyJustPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) & keyboardState != keyboardStatePrevious); //checks to see if key is being pressed, and if this is different to previous Update()
        }
        private bool IsKeyHeld(Keys key)
        {
            return (keyboardState.IsKeyDown(key) & keyboardState == keyboardStatePrevious); //checks to see if a key is being pressed, and if this is different to previous Update()
        }
        private bool IsKeyReleased(Keys key)
        {
            return (keyboardStatePrevious.IsKeyDown(key) & keyboardState.IsKeyDown(Keys.None));
        }
        private bool IsPlayerIdle() // keys track of how long player is idle, and returns whether true
        {
            if (keyboardStatePrevious.IsKeyDown(Keys.None) && keyboardState.IsKeyDown(Keys.None))
            {
                idleTime++;
            }
            else
                idleTime = 0;


            if (idleTime > 4000)
            {
                if (idleTime == 2147483647)// prevents integer overload
                    idleTime = 1000000000;

                return true;
            }
            else return false;
        }

       public Vector2 GetPlayerMoveDirection()
        {
            int moveX = 0;
            int moveY = 0;
            if (keyboardState.IsKeyDown(upMove))
                moveY -= 1;
            if (keyboardState.IsKeyDown(downMove))
                moveY += 1;
            if (keyboardState.IsKeyDown(leftMove))
                moveX += 1;
            if (keyboardState.IsKeyDown(rightMove))
                moveX -= 1;
            return new Vector2(moveX, moveY);

        }
        public void UseWASDKeys() //method to change controls to WASD keys
        {
            upMove = Keys.W;
            downMove = Keys.S;
            leftMove = Keys.A;
            rightMove = Keys.D;
        }
        public void UseArrowKeys() //method to change controls to arrow keys
        {
            upMove = Keys.Up;
            downMove = Keys.Down;
            leftMove = Keys.Left;
            rightMove = Keys.Right;
        }
    }
}

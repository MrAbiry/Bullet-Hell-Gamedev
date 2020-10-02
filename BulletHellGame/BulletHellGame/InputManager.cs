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
        //current and previous key press:
        KeyboardState keyboardState;
        KeyboardState keyboardStatePrevious; //used to keep track of whether button is being pressed or held. the keyboardState of previous update
        //movement controls
        Keys UpMove;
        Keys DownMove;
        Keys LeftMove;
        Keys RightMove;
        //movestates
        enum Movestate
        {
            None,
            Up,
            Down,
            Left,
            Right,
            DiagonalUpRight, //8-way movement
            DiagonalUpLeft,
            DiagonalDownLeft,
            DiagonalDownRight
        }
        public int movestate = (int)Movestate.None;
        //other:
        int idleTime = 0; // time where no key at all has been pressed

        public InputManager()
        {
            keyboardState = Keyboard.GetState();
            keyboardStatePrevious = Keyboard.GetState(); //i called .getState() again because I know with some classes, if you do b=c and then a=b it turns it into a reference? not sure if it's the case here and cba to check
            UseWASDKeys(); //sets default movement keys to WASD;
        }
        public void Update()
        {
            keyboardStatePrevious = keyboardState;
            keyboardState = Keyboard.GetState();
            DetectMoveState();
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
        private void DetectMoveState()
        {
            if (keyboardState.IsKeyDown(Keys.None))
            { 
                movestate = (int)Movestate.None;
                if(IsPlayerIdle())
                { 
                    //raise event here
                }
            }
            bool moveup = keyboardState.IsKeyDown(UpMove);
            bool moveright = keyboardState.IsKeyDown(RightMove);
            bool movedown = keyboardState.IsKeyDown(DownMove);
            bool moveleft = keyboardState.IsKeyDown(LeftMove);
            
            if (moveup)
            {
                if (moveright) //if both up and right keys are pressed at same time
                    movestate = (int)Movestate.DiagonalUpRight;
                else if (moveleft)
                    movestate = (int)Movestate.DiagonalUpLeft;
                else movestate = (int)Movestate.Up;
            }
            else if (movedown)
            {
                if (moveright)
                    movestate = (int)Movestate.DiagonalDownRight;
                else if (moveleft)
                    movestate = (int)Movestate.DiagonalDownLeft;
                else movestate = (int)Movestate.Down;
            }
            else if (moveright)
                movestate = (int)Movestate.Right;
            else if (moveleft)
                movestate = (int)Movestate.Left;
        }

        //
        //
        //
        public int GetMoveState()
        {
            return movestate;
        }
        public void UseWASDKeys() //method to change controls to WASD keys
        {
            UpMove = Keys.W;
            DownMove = Keys.S;
            LeftMove = Keys.A;
            RightMove = Keys.D;
        }
        public void UseArrowKeys() //method to change controls to arrow keys
        {
            UpMove = Keys.Up;
            DownMove = Keys.Down;
            LeftMove = Keys.Left;
            RightMove = Keys.Right;
        }
    }
}

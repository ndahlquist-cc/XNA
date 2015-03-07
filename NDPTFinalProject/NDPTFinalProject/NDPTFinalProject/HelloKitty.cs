/*  HelloKitty.cs
 *  
 *  This class is used to create the hello kitty character
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 *      + http://spritedatabase.net/file/3349 hello kitty sprite sheet
 *      + http://www.customwallgraphics.com/product_images/z/459/Walking_Dead_Zombie_Sticker_20__19316_zoom.png zombie hello kitty
 *  Revision History:
 *  2014-11-10
 *      + added sprite sheet and loops for stationary movement simulation
 *  2014-11-11
 *      + added jumping functionality with spacebar
 *  2014-11-12
 *      + added bounce for bouncing off enemies heads
 *  2014-11-14
 *      +edited hello kitty spritesheet, added zombie kitty, darkened edges on images
 *      
 *  2014-11-14
 *      +Fixed bug where land method was not re-initializing speed as intended
 *      +Added jumping sound effect https://www.freesound.org/people/fins/sounds/146726/
 *  2014-11-29
 *      + adjusted hello kitty spritesheet to improve hitbox, fix in-game dimensions
 *      + adjust gravity and initial jump speed
 *  2014-11-30
 *      + added isPathing and PathRight() for transition scenes
 *      
 */
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


namespace NDPTFinalProject
{
    /// <summary>
    /// This class is used to create the hello kitty character
    /// </summary>
    public class HelloKitty : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //HelloKitty class varaibles
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 initPosition;
        private Rectangle srcRect;
        public Rectangle SrcRect
        {
            get { return srcRect; }
            set { srcRect = value; }
        }

        //Jump variables
        private Vector2 speed;

        public Vector2 Speed
        {
            get { return speed; }
        }
        private const int DEFAULT_JUMP_SPEED = -200;
        private Vector2 bouncePosition;
        private float g;
        private float jumpTime;
        private bool isJumping;
        public bool IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }
        private bool isBounce;
        private bool isFalling;
        public bool IsFalling
        {
            get { return isFalling; }
            set { isFalling = value; }
        }

        private int positionChange;
        private int direction;
        private Vector2 dimension;
        private Vector2 dimensionFacing;
        private List<Rectangle> framesRight;
        private List<Rectangle> framesLeft;
        private List<Rectangle> framesZombieLeft;
        private Rectangle framesFacing;
        private Rectangle framesFacingDead;
        private List<Rectangle> framesFiery;
        private Rectangle zombieFramesFacing;
        private Rectangle framesJumpRight;
        private Rectangle framesJumpLeft;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private KeyboardState oldState;
        private bool isPathing = false;

        private SoundEffect jumpSound;
        private float deathTimer;

        /// <summary>
        /// Constructor for HelloKitty class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="delay">Delay between frame changes</param>
        public HelloKitty(Game game, SpriteBatch spriteBatch, int delay)
            : base(game)
        {
            // Construct child components
            this.spriteBatch = spriteBatch;
            this.tex = game.Content.Load<Texture2D>("Images/helloKittySpriteSheet");
            this.jumpSound = game.Content.Load<SoundEffect>("Sounds/jump");
            initPosition = new Vector2(Shared.stage.X / 4,
                (Shared.stage.Y - 60));
            position = initPosition;
            direction = 0;
            this.delay = delay;
            this.dimension = new Vector2(35, 60);
            this.dimensionFacing = new Vector2(45, 60);
            
            //Initialize jumping physics variables
            this.speed = new Vector2(0, DEFAULT_JUMP_SPEED);
            this.isJumping = false;
            this.isFalling = false;
            this.g = -175;

            // create frames here
            createFramesRight();
            createFramesLeft();
            createFrameFacing();
            createFrameJumpRight();
            createFrameJumpLeft();
            createFrameZombieFacing();
            createFramesZombie();
            createFrameFacingDead();
            createFramesFieryKitty();

            // stop the animation initially
            stop();
        }
        #region Create Frames
        /// <summary>
        /// Method to create frames for hello kitty on fire
        /// </summary>
        private void createFramesFieryKitty()
        {
            framesFiery = new List<Rectangle>();
            for (int i = 4; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int x = j * (int)dimensionFacing.X;
                    int y = i * (int)dimensionFacing.Y;
                    Rectangle ff = new Rectangle(x, y + 2, (int)dimensionFacing.X, (int)dimensionFacing.Y);
                    framesFiery.Add(ff);
                }
            }
        }
        /// <summary>
        /// Method to create frame for when hello kitty isn't moving
        /// </summary>
        private void createFrameFacing()
        {
            int x = 0;
            int y = 2 * (int)dimensionFacing.Y;
            framesFacing = new Rectangle(x, y, (int)dimensionFacing.X, (int)dimensionFacing.Y);
        }
        /// <summary>
        /// Method to create frame for when hello kitty is dead
        /// </summary>
        private void createFrameFacingDead()
        {
            int x = (int)dimensionFacing.X;
            int y = 2 * (int)dimensionFacing.Y;
            framesFacingDead = new Rectangle(x, y, (int)dimensionFacing.X, (int)dimensionFacing.Y);
        }
        /// <summary>
        /// Method to create frame for when hello kitty dies to a zombie
        /// </summary>
        private void createFrameZombieFacing()
        {
            int x = 2 * (int)dimensionFacing.X;
            int y = 2 * (int)dimensionFacing.Y;
            zombieFramesFacing = new Rectangle(x, y, (int)dimensionFacing.X, (int)dimensionFacing.Y);
        }
        /// <summary>
        /// Method to create frame for when hello kitty jumps to the right
        /// </summary>
        private void createFrameJumpRight()
        {
            int x = 5 * (int)dimension.X;
            int y = 0;
            framesJumpRight = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
        }
        /// <summary>
        /// Method to create frame for when hello kitty jumps to the left
        /// </summary>
        private void createFrameJumpLeft()
        {
            int x = 5 * (int)dimension.X;
            int y = (int)dimension.Y;
            framesJumpLeft = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);

        }
        /// <summary>
        /// Method to create frames for hello kitty animation moving right
        /// </summary>
        private void createFramesRight()
        {
            framesRight = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    framesRight.Add(r);
                }
            }
        }

        /// <summary>
        /// Method to create frames for hello kitty animation moving left
        /// </summary>
        private void createFramesLeft()
        {
            framesLeft = new List<Rectangle>();
            for (int i = 1; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle l = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    framesLeft.Add(l);
                }
            }
        }
        /// <summary>
        /// Method to create frames for hello kitty zombie animation moving left
        /// </summary>
        private void createFramesZombie()
        {
            framesZombieLeft = new List<Rectangle>();
            for (int i = 3; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle l = new Rectangle(x, y + 2, (int)dimension.X, (int)dimension.Y);
                    framesZombieLeft.Add(l);
                }
            }
        }
        #endregion
        #region Kitty Actions
        /// <summary>
        /// Method which causes kitty to bounce upwards slightly
        /// </summary>
        /// <param name="landHeight">Heigh which kitty was at when she started to bounce</param>
        public void bounce(float landHeight)
        {

            this.speed = new Vector2(0, DEFAULT_JUMP_SPEED);
            jumpTime = 0;            
            isJumping = true;
            isBounce = true;
            bouncePosition = new Vector2(position.X, landHeight);
            
        }

        /// <summary>
        /// Method used to allow kitty to free fall
        /// </summary>
        public void fall()
        {
            this.speed = new Vector2(0, 0);
            jumpTime = 0;
            isJumping = true;
            isBounce = true;
            isFalling = true;
            bouncePosition = position;
        }
        
        /// <summary>
        /// method to stop hello kitty animation
        /// </summary>
        public void stop()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// method to start hello kitty animation
        /// </summary>
        public void start()
        {
            this.Enabled = true;
            this.Visible = true;
            Shared.isKittyDead = false;
            Shared.isKittyZombified = false;
            Shared.isKittyFried = false;
        }

        /// <summary>
        /// Method for kitty to land on the current surface
        /// </summary>
        /// <param name="landHeight">Height which kitty was at when she landed</param>
        public void land(float landHeight)
        {
            isJumping = false;
            isBounce = true;
            isFalling = false;
            bouncePosition = new Vector2(position.X, landHeight);
            this.speed = new Vector2(0, DEFAULT_JUMP_SPEED);
        }

        /// <summary>
        /// Method to make kitty walk to the right without player input
        /// </summary>
        public void pathRight()
        {
            position.X += Shared.worldSpeed.X/4;
            isPathing = true;
        }
        /// <summary>
        /// method for hello kitty dying to a zombie
        /// </summary>
        public void zombieDeath()
        {
            frameIndex = 0;
            Shared.isKittyDead = true;
            Shared.isKittyZombified = true;
            if (isJumping)
            {
                fall();   
            }       
            
        }

        /// <summary>
        /// Method for hello kitty dying to a fire
        /// </summary>
        public void fry()
        {
            Shared.isKittyDead = true;
            Shared.isKittyFried = true;
            frameIndex = 0;
        }
        #endregion
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Only allow player control of kitty when she is alive and not on the start scene
            if (!Shared.isKittyDead && !Shared.isStartScene)
            {
                KeyboardState ks = Keyboard.GetState();

                //Frames for moving kitty right
                if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && !(oldState.IsKeyDown(Keys.Left) || oldState.IsKeyDown(Keys.A)) || isPathing)
                {
                    delayCounter++;
                    direction = 1;
                    if (delayCounter > delay)
                    {
                        frameIndex++;
                        if (frameIndex > 4)
                        {
                            frameIndex = 0;
                        }

                        delayCounter = 0;
                    }


                }

                //Frame for moving kitty left
                else if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && !(oldState.IsKeyDown(Keys.Right) || oldState.IsKeyDown(Keys.D)) && !isPathing)
                {
                    delayCounter++;
                    direction = -1;
                    if (delayCounter > delay)
                    {
                        frameIndex++;
                        if (frameIndex > 4)
                        {
                            frameIndex = 0;
                        }

                        delayCounter = 0;
                    }
                }

                //Set direction to 0 when kitty is not pathing and not moving left or right due to player
                else if (!isPathing)
                {
                    direction = 0;
                }

                //Hello kitty jumps when pressing space
                if (ks.IsKeyDown(Keys.Space) && isJumping == false && isPathing == false)
                {
                    jumpSound.Play();
                    isJumping = true;
                    jumpTime = 0;
                }
                oldState = ks;
            }

            //Reposition kitty while jumping
            if (isJumping)
            {
                Vector2 deltaPosition = Vector2.Zero;
                jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                deltaPosition.Y = speed.Y * jumpTime - g * jumpTime * jumpTime / 2;                

                if (isBounce)
                {
                    position = bouncePosition + deltaPosition;
                }
                else
                {
                    position = initPosition + deltaPosition;
                }

            }

            //Kitty lands on the ground after a jump
            if (position.Y >= initPosition.Y && isJumping)
            {
                this.land(initPosition.Y);
            }

            // Post-mortem movement frames
            if (Shared.isKittyDead)
            {
                //Fire death frames
                if (Shared.isKittyFried)
                {                    
                    delayCounter++;
                    if (delayCounter > delay)
                    {
                        frameIndex++;
                        if (frameIndex > 4)
                        {
                            stop();
                        }
                        delayCounter = 0;
                    }
                }

                //Zombie death frames
                else if(Shared.isKittyZombified)
                {
                    delayCounter++;
                    if (delayCounter > delay)
                    {
                        position.X -= positionChange;
                        if (position.X + tex.Width < 0)
                        {
                            stop();
                        }
                        frameIndex++;
                        if (frameIndex > 4)
                        {
                            frameIndex = 0;
                        }
                        delayCounter = 0;
                    }
                }
            }  

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when hello kitty should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            srcRect = new Rectangle((int)position.X, (int)position.Y, 35, 60);
            spriteBatch.Begin();
            //Draw frames when kitty is not dead
            if (!Shared.isKittyDead)
            {
                //Kitty standing still
                if (direction == 0 || frameIndex == -1)
                {
                    spriteBatch.Draw(tex, position, framesFacing, Color.White);
                }

                if (frameIndex >= 0)
                {
                    //Kitty moving right frames
                    if (direction == 1)
                    {
                        if (isJumping)
                        {
                            spriteBatch.Draw(tex, position, framesJumpRight, Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(tex, position, framesRight.ElementAt<Rectangle>(frameIndex), Color.White);
                        }
                    }

                    //Kitty moving left frames
                    if (direction == -1)
                    {
                        
                        if (isJumping)
                        {
                            spriteBatch.Draw(tex, position, framesJumpLeft, Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(tex, position, framesLeft.ElementAt<Rectangle>(frameIndex), Color.White);
                        }
                    }
                }
            }

            //Draw frames for when kitty is dead
            else if (Shared.isKittyDead)
            {
                //Kitty died to a spike frames
                if (frameIndex == -1 || (direction == 0 && !Shared.isKittyFried))
                {
                    frameIndex = 0;
                }
                if (!Shared.isKittyFried && !Shared.isKittyZombified)
                {
                    positionChange = 0;
                    spriteBatch.Draw(tex, position, framesFacingDead, Color.White);
                }

                //Kitty died to a zombie frames
                if (Shared.isKittyZombified)
                {
                    deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (deathTimer > 5)
                    {
                        positionChange = 10;
                        spriteBatch.Draw(tex, position, framesZombieLeft.ElementAt<Rectangle>(frameIndex), Color.White);
                    }
                    else
                    {
                        positionChange = 0;
                        spriteBatch.Draw(tex, position, zombieFramesFacing, Color.White);
                    }
                }

                //Kitty died to a fire frames
                if(Shared.isKittyFried)
                {
                    
                    positionChange = 0;
                    spriteBatch.Draw(tex, position, framesFiery.ElementAt<Rectangle>(frameIndex), Color.White);
                }
                
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

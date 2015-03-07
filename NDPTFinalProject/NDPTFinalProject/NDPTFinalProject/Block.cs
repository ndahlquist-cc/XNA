/*  Block.cs
 *  
 *  This game component is used to create drawable block obstacles in the level scenes
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-16 Created:
 *      + declare variables, create draw and update methods
 *      + created dictionary with various block textures to create various blocks using a string key
 *      + added world movement to update
 *  2014-11-17
 *      + adjust x dimension so that fat ass kitty falls off when her feet reach the edge
 *      
 *  2014-11-21
 *      + changed dictionary key to an enum containing block types
 *      + added star block and star block animation
 *     
 *  2014-11-22
 *      + Added animation and sounds for hitting a star block
 *      + Added a randomly generated star count for each block
 *      + Added star animation and points for hitting a block with a star in it
 *      + Added block hit sound effects http://www.freesound.org/people/grunz/sounds/109662/ (star get), https://www.freesound.org/people/pepingrillin/sounds/252173/ (empty block hit)
 *      + added log object
 * 2014-11-23
 *      + Added town objects 
 * 2014-11-24
 *      + added stick pit
 * 2014-11-27
 *      + made fireBlocks array and made it a read-only property
 * 2014-11-29
 *      + adjusted fire on spritesheet for improved hitbox, adjust in-game dimensions
 *      + fixed issue with shared block and star animations not moving
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
    /// Game component class used to create drawable block obstacles in the level scenes
    /// </summary>
    public class Block : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // declare class scope variables
        private SpriteBatch spriteBatch;
        private bool blockHit;
        private SoundEffect blockHitSound;
        private SoundEffect blockEmptySound;
        private bool starAnimation = false;
        private Random starCountRand;
        //private bool isSharedBlock = false;
        private float initHeight;
        private KeyboardState oldState;

        // textures
        private Texture2D blockTex;
        private Texture2D sharedTex;
        private Texture2D townTex;

        // vectors
        private Vector2 dimensions = new Vector2(30, 30);
        private Vector2 speed = Shared.worldSpeed;
        private Vector2 starDeltaPos;

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        // rectangles
        private Rectangle blockFrame;
        private Rectangle sharedFrame;
        private Rectangle townFrame;
        private Rectangle srcRect;

        // integers
        private int starFrameIndex = 0;
        private int delay = 10;
        private int delayCounter = 0;
        private int blockHitTime;
        private int starDelay = 5;
        private int starDelayCounter = 0;
        private int starCount;
        private int starAnimationFrames = 0;

        // arrays
        private Rectangle[] starFrames;
        private Rectangle[] fireFrames;
        private Block.townTypes[] fireBlocks = new Block.townTypes[] { Block.townTypes.FIRE_1, Block.townTypes.FIRE_2, Block.townTypes.FIRE_3, Block.townTypes.FIRE_4, Block.townTypes.FIRE_5 };

        public Block.townTypes[] FireBlocks
        {
            get { return fireBlocks; }
        }

        
        // types
        private blockTypes blockType;

        public blockTypes BlockType
        {
            get { return blockType; }
            set { blockType = value; }
        }
        private sharedTypes sharedType;

        public sharedTypes SharedType
        {
            get { return sharedType; }
            set { sharedType = value; }
        }

        private townTypes townType;

        public townTypes TownType
        {
            get { return townType; }
            set { townType = value; }
        }
        public Rectangle SrcRect
        {
            get { return srcRect; }
            set { srcRect = value; }
        }

        // enums
        public enum sharedTypes
        {
            NOSHARED,
            STAR_BLOCK_1,
            STAR_BLOCK_2,
            STAR_BLOCK_3,
            STAR_BLOCK_EMPTY,
            STAR_1,
            STAR_2,
            STAR_3,
            STAR_4,
            PLATFORM
        }
        public enum townTypes
        {
            NOTOWN, 
            ROOF_LEFT,
            ROOF_MID,
            ROOF_RIGHT,
            WHITE_BLOCK,
            BALCONY_DOOR, 
            BALCONY_RAIL,
            WINDOW,
            SMALL_WINDOW,
            ROUND_WINDOW,
            DOOR, 
            BUSHES, 
            WINDOW_WITH_AWNING, 
            TREE, 
            FENCE, 
            FIRE_1,
            FIRE_2,
            FIRE_3,
            FIRE_4,
            FIRE_5

        }
        public enum blockTypes
        {
            NONE,
            DEFAULT,
            GRASS_RIGHT,
            GRASS_LEFT,
            GRASS_RIGHT_TOP,
            GRASS_LEFT_TOP,
            GRASS_TOP,
            GRASS_LEFT_CORNER,
            GRASS_RIGHT_CORNER,
            GRASS_BOTH_CORNER,
            GRASS_PEAK,
            DEFAULT_LEFT,
            DEFAULT_RIGHT,
            GRASS_TOP_EDGE_RIGHT,
            GRASS_TOP_EDGE_LEFT,
            GRASS_LEFT_CORNER_RIGHT_SIDE,
            GRASS_RIGHT_CORNER_LEFT_SIDE,
            LOG,
            STICK_PIT_SMALL,
            STICK_PIT_LARGE
        }

        // dictionaries
        private Dictionary<blockTypes, Rectangle> blockTexDictionary = new Dictionary<blockTypes, Rectangle>();
        private Dictionary<sharedTypes, Rectangle> sharedTexDictionary = new Dictionary<sharedTypes, Rectangle>();
        private Dictionary<townTypes, Rectangle> townTexDictionary = new Dictionary<townTypes, Rectangle>();

        /// <summary>
        /// Overloaded Block class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="position">position of block</param>
        /// <param name="blockType">town type of blocks</param>
        public Block(Game game, SpriteBatch spriteBatch, Vector2 position, blockTypes blockType = blockTypes.DEFAULT)
            : base(game)
        {
            #region Initialize Block & Shared Texture Dictionary
            // Initialize Block and Shared Texture Dicitonaries
            // Forest Textures
            blockTexDictionary.Add(blockTypes.NONE, new Rectangle(0, 0, 0, 0));
            blockTexDictionary.Add(blockTypes.DEFAULT, new Rectangle(0, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_RIGHT, new Rectangle(30, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_LEFT, new Rectangle(60, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_RIGHT_TOP, new Rectangle(90, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_LEFT_TOP, new Rectangle(120, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_TOP, new Rectangle(150, 0, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_LEFT_CORNER, new Rectangle(0, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_RIGHT_CORNER, new Rectangle(30, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_BOTH_CORNER, new Rectangle(60, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_PEAK, new Rectangle(90, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.DEFAULT_LEFT, new Rectangle(120, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.DEFAULT_RIGHT, new Rectangle(150, 30, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_LEFT_CORNER_RIGHT_SIDE, new Rectangle(0, 90, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_RIGHT_CORNER_LEFT_SIDE, new Rectangle(30, 90, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_TOP_EDGE_RIGHT, new Rectangle(60, 90, 30, 30));
            blockTexDictionary.Add(blockTypes.GRASS_TOP_EDGE_LEFT, new Rectangle(90, 90, 30, 30));
            blockTexDictionary.Add(blockTypes.LOG, new Rectangle(0, 65, 116, 23));
            blockTexDictionary.Add(blockTypes.STICK_PIT_SMALL, new Rectangle(0, 136, 30, 44));
            blockTexDictionary.Add(blockTypes.STICK_PIT_LARGE, new Rectangle(0, 136, 60, 44));

            // Shared Textures
            sharedTexDictionary.Add(sharedTypes.NOSHARED, new Rectangle(0, 0, 0, 0));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_1, new Rectangle(0, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_2, new Rectangle(30, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_3, new Rectangle(60, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_EMPTY, new Rectangle(90, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_1, new Rectangle(0, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_2, new Rectangle(30, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_3, new Rectangle(60, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_4, new Rectangle(90, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.PLATFORM, new Rectangle(120, 0, 60, 20));
            #endregion

            // child components
            this.spriteBatch = spriteBatch;
            this.position = position;
            
            this.blockTex = game.Content.Load<Texture2D>("Images/forestObjects");
            this.sharedTex = game.Content.Load<Texture2D>("Images/sharedObjects");
            this.townTex = game.Content.Load<Texture2D>("Images/townObjects");
                
            
            this.blockFrame = blockTexDictionary[blockType];
            this.sharedFrame = sharedTexDictionary[sharedType];
            //this.townFrame = townTexDictionary[townType];
            this.blockType = blockType;
            this.sharedType = sharedTypes.NOSHARED;
            this.townType = townTypes.NOTOWN;
            this.starCountRand = new Random(DateTime.Now.Millisecond * (int)position.X);
            this.starCount = starCountRand.Next(0, 5);
            this.blockHit = false;
            this.blockHitSound = game.Content.Load<SoundEffect>("Sounds/coinHit");
            this.blockEmptySound = game.Content.Load<SoundEffect>("Sounds/emptyHit");
            this.starFrames = new Rectangle[] { sharedTexDictionary[sharedTypes.STAR_1], sharedTexDictionary[sharedTypes.STAR_2],
                                                sharedTexDictionary[sharedTypes.STAR_3], sharedTexDictionary[sharedTypes.STAR_4]};
            this.starDeltaPos = new Vector2(0, 0);
            this.initHeight = position.Y;
        }

        /// <summary>
        /// Overloaded Block class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="position">Position of blocks</param>
        /// <param name="sharedType">shared type of blocks</param>
        public Block(Game game, SpriteBatch spriteBatch, Vector2 position, sharedTypes sharedType = sharedTypes.STAR_1)
            : base(game)
        {
            #region Initialize Shared Texture Dictionary
            // Initialize shared texture dictionary
            // Shared Textures
            sharedTexDictionary.Add(sharedTypes.NOSHARED, new Rectangle(0, 0, 0, 0));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_1, new Rectangle(0, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_2, new Rectangle(30, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_3, new Rectangle(60, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_EMPTY, new Rectangle(90, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_1, new Rectangle(0, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_2, new Rectangle(30, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_3, new Rectangle(60, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_4, new Rectangle(90, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.PLATFORM, new Rectangle(120, 0, 60, 20));
            #endregion

            // child components
            this.spriteBatch = spriteBatch;
            this.position = position;
            
            this.blockTex = game.Content.Load<Texture2D>("Images/forestObjects");
            this.sharedTex = game.Content.Load<Texture2D>("Images/sharedObjects");
            this.townTex = game.Content.Load<Texture2D>("Images/townObjects");
            this.sharedFrame = sharedTexDictionary[sharedType];
            this.sharedType = sharedType;
            this.blockType = blockTypes.NONE;
            this.townType = townTypes.NOTOWN;
            this.starCountRand = new Random(DateTime.Now.Millisecond * (int)position.X);
            this.starCount = starCountRand.Next(0, 5);
            this.blockHit = false;
            this.blockHitSound = game.Content.Load<SoundEffect>("Sounds/coinHit");
            this.blockEmptySound = game.Content.Load<SoundEffect>("Sounds/emptyHit");
            this.starFrames = new Rectangle[] { sharedTexDictionary[sharedTypes.STAR_1], sharedTexDictionary[sharedTypes.STAR_2],
                                                sharedTexDictionary[sharedTypes.STAR_3], sharedTexDictionary[sharedTypes.STAR_4]};
            this.starDeltaPos = new Vector2(0, 0);
            this.initHeight = position.Y;
        }
        /// <summary>
        /// Overloaded Block class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="position">position of blocks</param>
        /// <param name="townType">town type of blocks</param>
        public Block(Game game, SpriteBatch spriteBatch, Vector2 position, townTypes townType = townTypes.WHITE_BLOCK)
            : base(game)
        {

            #region Initialize Town and Shared Texture Dictionary
            // initialize town and shared texture dictionary
            // Town Textures
            townTexDictionary.Add(townTypes.NOTOWN, new Rectangle(0, 0, 60, 60));
            townTexDictionary.Add(townTypes.BALCONY_DOOR, new Rectangle(60, 60, 60, 60));
            townTexDictionary.Add(townTypes.BALCONY_RAIL, new Rectangle(60, 120, 60, 60));
            townTexDictionary.Add(townTypes.BUSHES, new Rectangle(120, 120, 60, 60));
            townTexDictionary.Add(townTypes.DOOR, new Rectangle(0, 120, 60, 60));
            townTexDictionary.Add(townTypes.ROOF_LEFT, new Rectangle(0, 0, 60, 60));
            townTexDictionary.Add(townTypes.ROOF_MID, new Rectangle(60, 0, 60, 60));
            townTexDictionary.Add(townTypes.WINDOW_WITH_AWNING, new Rectangle(0, 180, 60, 60));
            townTexDictionary.Add(townTypes.ROOF_RIGHT, new Rectangle(120, 0, 60, 60));
            townTexDictionary.Add(townTypes.ROUND_WINDOW, new Rectangle(120, 60, 60, 60));
            townTexDictionary.Add(townTypes.SMALL_WINDOW, new Rectangle(120, 180, 60, 60));
            townTexDictionary.Add(townTypes.WHITE_BLOCK, new Rectangle(60, 180, 60, 60));
            townTexDictionary.Add(townTypes.WINDOW, new Rectangle(0, 60, 60, 60));
            townTexDictionary.Add(townTypes.TREE, new Rectangle(181, 0, 59, 130));
            townTexDictionary.Add(townTypes.FENCE, new Rectangle(240, 0, 60, 60));
            townTexDictionary.Add(townTypes.FIRE_1, new Rectangle(0, 240, 40, 60));
            townTexDictionary.Add(townTypes.FIRE_2, new Rectangle(40, 240, 40, 60));
            townTexDictionary.Add(townTypes.FIRE_3, new Rectangle(80, 240, 40, 60));
            townTexDictionary.Add(townTypes.FIRE_4, new Rectangle(120, 240, 40, 60));
            townTexDictionary.Add(townTypes.FIRE_5, new Rectangle(160, 240, 40, 60)); 

            // Shared Textures
            sharedTexDictionary.Add(sharedTypes.NOSHARED, new Rectangle(0, 0, 0, 0));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_1, new Rectangle(0, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_2, new Rectangle(30, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_3, new Rectangle(60, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_BLOCK_EMPTY, new Rectangle(90, 0, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_1, new Rectangle(0, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_2, new Rectangle(30, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_3, new Rectangle(60, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.STAR_4, new Rectangle(90, 30, 30, 30));
            sharedTexDictionary.Add(sharedTypes.PLATFORM, new Rectangle(120, 0, 60, 20));
            #endregion

            // child components
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.sharedTex = game.Content.Load<Texture2D>("Images/sharedObjects");
            this.townTex = game.Content.Load<Texture2D>("Images/townObjects");
                
            
            this.sharedFrame = sharedTexDictionary[sharedType];
            this.townFrame = townTexDictionary[townType];
            this.townType = townType;
            this.sharedType = sharedTypes.NOSHARED;
            this.blockType = blockTypes.NONE;
            this.starCountRand = new Random(DateTime.Now.Millisecond * (int)position.X);
            this.starCount = starCountRand.Next(0, 5);
            this.blockHit = false;
            this.blockHitSound = game.Content.Load<SoundEffect>("Sounds/coinHit");
            this.blockEmptySound = game.Content.Load<SoundEffect>("Sounds/emptyHit");
            this.starFrames = new Rectangle[] { sharedTexDictionary[sharedTypes.STAR_1], sharedTexDictionary[sharedTypes.STAR_2],
                                                sharedTexDictionary[sharedTypes.STAR_3], sharedTexDictionary[sharedTypes.STAR_4]};
            this.fireFrames = new Rectangle[] { townTexDictionary[townTypes.FIRE_1], townTexDictionary[townTypes.FIRE_2],
                                                townTexDictionary[townTypes.FIRE_3], townTexDictionary[townTypes.FIRE_4], townTexDictionary[townTypes.FIRE_5]};
            
            this.starDeltaPos = new Vector2(0, 0);
            this.initHeight = position.Y;
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {   
            base.Initialize();      
           
        }

        /// <summary>
        /// this method displays the star animation, plays a sound and 
        /// adds to the score when a pink block is hit if it is not empty. 
        /// </summary>
        public void hitBlock()
        {
            if (sharedType != sharedTypes.STAR_BLOCK_EMPTY)
            {
                blockHit = true;
                starAnimation = true;
                starDeltaPos = new Vector2(sharedFrame.Width / 2 - starFrames[starFrameIndex].Width / 2, -20);
                blockHitSound.Play();
                Shared.score += 100;
            }

            else
            {
                blockEmptySound.Play();
            }

            if(starCount > 0)
            {
                starCount--;
            }
            else
            {
                sharedType = sharedTypes.STAR_BLOCK_EMPTY;
                sharedFrame = sharedTexDictionary[sharedType];
                blockHit = false;
            }
            
        }

        /// <summary>
        /// Allows the block class to update itself.  Updates block position
        /// based on key press (using worldspeed)
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            starDelayCounter++;
            KeyboardState ks = Keyboard.GetState();
            if (!Shared.isKittyDead)
            {
                if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && !(oldState.IsKeyDown(Keys.Left) || oldState.IsKeyDown(Keys.A)))
                {
                    position.X -= Shared.worldSpeed.X;
                }

                if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && !(ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)))
                {
                    position.X += Shared.worldSpeed.X;
                }
            }               
            

            //fire block animation
            if (townType != townTypes.NOTOWN)
            {
                if (delayCounter > delay)
                {
                    delayCounter = 0;
                    if (townType == townTypes.FIRE_1)
                    {
                        townType = townTypes.FIRE_2;
                    }

                    else if (townType == townTypes.FIRE_2)
                    {
                        townType = townTypes.FIRE_3;
                    }
                    else if (townType == townTypes.FIRE_3)
                    {
                        townType = townTypes.FIRE_4;
                    }
                    else if (townType == townTypes.FIRE_4)
                    {
                        townType = townTypes.FIRE_5;
                    }

                    else if (townType == townTypes.FIRE_5)
                    {
                        townType = townTypes.FIRE_1;
                    }
                    townFrame = townTexDictionary[townType];
                }
            }
                //Star block animation
                if (delayCounter > delay)
                {
                    delayCounter = 0;
                    if (sharedType == sharedTypes.STAR_BLOCK_1)
                    {
                        sharedType = sharedTypes.STAR_BLOCK_2;
                    }

                    else if (sharedType == sharedTypes.STAR_BLOCK_2)
                    {
                        sharedType = sharedTypes.STAR_BLOCK_3;                        
                    }
                    else if (sharedType == sharedTypes.STAR_BLOCK_3)
                    {
                        sharedType = sharedTypes.STAR_BLOCK_1;
                    }
                    sharedFrame = sharedTexDictionary[sharedType];                    
                }               

                //Star animation
                if (starDelayCounter > starDelay && starAnimation)
                {
                    starDelayCounter = 0;
                    starAnimationFrames++;
                    starDeltaPos += new Vector2(0, -3);
                    if (starAnimationFrames > 8)
                    {
                        starAnimationFrames = 0;
                        starAnimation = false;
                    }

                    starFrameIndex++;
                    if (starFrameIndex > 3)
                    {
                        starFrameIndex = 0;
                    }
                }               
                
                //Hit block animation
                if (blockHit && !(sharedType == sharedTypes.STAR_BLOCK_EMPTY))
                {
                    blockHitTime += gameTime.ElapsedGameTime.Milliseconds;
                    if(blockHitTime <= 125)
                    {
                        position -= new Vector2(0,1);
                    }                   

                    else if (blockHitTime <= 250)
                    {
                        position += new Vector2(0,1);
                    }                

                    else
                    {
                        blockHitTime = 0;
                        blockHit = false;
                        position = new Vector2(position.X, initHeight);
                    }
                    
                }             
                
            
    
            oldState = ks;
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the block should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            if (sharedType == sharedTypes.NOSHARED && townType == townTypes.NOTOWN)
            {
                srcRect = new Rectangle((int)position.X, (int)position.Y, blockFrame.Width, blockFrame.Height);
                spriteBatch.Draw(blockTex, position, blockFrame, Color.White);
            }
            else if (blockType == blockTypes.NONE && townType == townTypes.NOTOWN)
            {
                srcRect = new Rectangle((int)position.X, (int)position.Y, sharedFrame.Width, sharedFrame.Height);
                spriteBatch.Draw(sharedTex, position, sharedFrame, Color.White);
            }
            else if (sharedType == sharedTypes.NOSHARED && blockType == blockTypes.NONE)
            {
                if (townType == townTypes.TREE)
                {
                    srcRect = new Rectangle((int)position.X, (int)position.Y + 5, townFrame.Width, townFrame.Height);
                }
                else
                {
                    srcRect = new Rectangle((int)position.X, (int)position.Y, townFrame.Width, townFrame.Height);
                }
                spriteBatch.Draw(townTex, position, townFrame, Color.White);
            }
            if(starAnimation)
            {
                spriteBatch.Draw(sharedTex, position + starDeltaPos, starFrames[starFrameIndex], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

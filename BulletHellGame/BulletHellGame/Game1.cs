using Aurora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using TiledSharp;

namespace BulletHellGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D playerTexture;
        private SpriteSheet playerSpriteSheet;


        private TmxMap map;
        private Texture2D tileset;

        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWide;
        private int screenWidth;
        private int screenHeight;
        private TileMapManager mapRenderer;
        private Camera cam;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            cam = new Camera();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("Player");
            playerSpriteSheet = new SpriteSheet(playerTexture, 1, 6);

            map = new TmxMap("Content/Map/GroupProject.tmx");
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            mapRenderer = new TileMapManager(_spriteBatch, map, tileset, tilesetTilesWide, tileWidth, tileHeight,cam.transform);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            playerSpriteSheet.Update();
            
            //cam.Follow(Vector2,screenWidth,screenHeight);//Uncomment and replace Vector2 with player position
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapRenderer.Draw();
            playerSpriteSheet.Draw(_spriteBatch,cam.transform);
            base.Draw(gameTime);
        }
    }
}

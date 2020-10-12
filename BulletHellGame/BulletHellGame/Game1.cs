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
        private SpriteSheet playerMoveUp;
        private SpriteSheet playerMoveDown;
        private SpriteSheet playerMoveLeft;
        private SpriteSheet playerMoveRight;
        private SpriteSheet playerMoveIdle;
        private SpriteSheet playerSheet;

        private TmxMap map;
        private Texture2D tileset;
        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWide;
        private TileMapManager mapRenderer;

        public InputManager inputManager;

        public Player player;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSheet = new SpriteSheet(Content.Load<Texture2D>("Player/playerSheet"), 5, 6);

            inputManager = new InputManager();
            Player _player = new Player(_spriteBatch, inputManager, new Vector2(), 3,playerSheet); //I don't know why, but it gives a null error if i directly assign new instance to player... so instead I am pointing player to _player
            player = _player;
            map = new TmxMap("Content/Map/GroupProject.tmx");
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            mapRenderer = new TileMapManager(_spriteBatch, map, tileset, tilesetTilesWide, tileWidth, tileHeight);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            inputManager.Update();
            // TODO: Add your update logic here
            player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapRenderer.Draw();
            player.Draw();
            base.Draw(gameTime);
        }
    }
}

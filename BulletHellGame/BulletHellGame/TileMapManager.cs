using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;
using System.Collections.ObjectModel;
namespace BulletHellGame
{
    public class TileMapManager
    {
        TmxMap map;
        SpriteBatch _spriteBatch;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        
        public TileMapManager(SpriteBatch _spriteBatch, TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            this._spriteBatch = _spriteBatch;
            this.map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }
        public int getTileIndexFromPos(Vector2 absolutePosition) // inputs a location paramater, and returns the index of the tile at that specified point. index is zero-based
        {
            return (int)(absolutePosition.X/tileHeight + absolutePosition.Y/tileHeight * map.Width);
        }
        public int getTileIndexFromPos(int x, int y)
        {
            return x/tileHeight + y/tileHeight * map.Width;
        }
        public int GetTileIDFromIndex(int tileIndex) // returns the gid of first tile that isnt empty (searches layers from top to bottom)
        {
            for (var j = map.TileLayers.Count-1; j == 0; j--)
            {
                if (map.TileLayers[j].Tiles[tileIndex].Gid != 0)
                    return map.TileLayers[j].Tiles[tileIndex].Gid;
            }
            return 0; // if no tile was found at that specific location
        }

        public void Draw()
        {
            _spriteBatch.Begin();
            for (var j = 0; j < map.TileLayers.Count; j++)
            {
                for (var i = 0; i < map.TileLayers[j].Tiles.Count; i++)
                {
                    int gid = map.TileLayers[j].Tiles[i].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1; //tileset id is zero-based index
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (i % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);

                        _spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
            _spriteBatch.End();
        }
    }
}

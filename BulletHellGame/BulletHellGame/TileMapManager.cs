using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;
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
        public void Draw()
        {
            _spriteBatch.Begin();
            for (var j = 0; j < map.TileLayers.Count; j++)
            {
                for (var i = 0; i < map.TileLayers[j].Tiles.Count; i++)
                {
                    int gid = map.TileLayers[j].Tiles[i].Gid;

                    // Empty tile, do nothing
                    if (gid == 0)
                    {

                    }
                    else
                    {
                        int tileFrame = gid - 1;
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TiledSharp;

namespace BulletHellGame
{
    public class TileMapManager
    {
        TmxMap map;
        SpriteBatch _spriteBatch;
        Texture2D tileset;
        int tilesetTilesWide;
        public int tileWidth { get; }
        public int tileHeight { get; }


        public TileMapManager(SpriteBatch _spriteBatch, TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            this._spriteBatch = _spriteBatch;
            this.map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }
        public Vector2 GetTileCoordFromPos(Vector2 absolutePosition) // inputs a location paramater, and returns the index of the tile at that specified point. index is zero-based
        {
            //int index = (int)((double)absolutePosition.X / tileHeight + (double)absolutePosition.Y / tileHeight * map.Width);
            int x = (int)(absolutePosition.X / tileWidth);
            int y = (int)(absolutePosition.Y / tileHeight);
            Console.WriteLine("("+x+" , " + y+")");
            return new Vector2(x,y);
        }
        public int GetTileIndexFromTileCoord(Vector2 TileCoord) // inputs a location paramater, and returns the index of the tile at that specified point. index is zero-based
        {
            int index = (int)TileCoord.Y * map.Width + (int)TileCoord.X;
            return index;
        }
        public int GetTileIDFromPos(Vector2 absolutePosition)
        {
            return GetTileIDFromIndex(GetTileIndexFromTileCoord(GetTileCoordFromPos(absolutePosition)));
        }
        public bool CanMoveToTile(Rectangle player, Vector2 targetPos)
        {
            Rectangle collisionCheck = new Rectangle((int)targetPos.X, (int)targetPos.Y, player.Width, player.Height);
            //map border check
            if (collisionCheck.Right > (map.Width) * tileWidth) //-1 because map coord is zero-indexed
                return false;
            if (collisionCheck.Left < 0)
                return false;
            if (collisionCheck.Bottom > (map.Height - 2)*tileHeight) // -1 because map coord is zero-indexed
                return false; //very minor bug: when in the bottom map border,, if pressing down key, sprite moves up a pixel and coord Y is 1 higher when key is pressed
            if (collisionCheck.Top < 0)
                return false;
            //Console.Write("Debug: LeftX: " + collisionCheck.Left + ", RightX: " + collisionCheck.Right + ", TopY: " + collisionCheck.Top + ", BotY: " + collisionCheck.Bottom);
            //Console.WriteLine("Debug: mapWidth: "+map.Width+"   mapHeight: "+map.Height);
            return true;
        }
        public int GetTileIDFromIndex(int tileIndex) // returns the gid of first tile that isnt empty (searches layers from top to bottom)
        {
            for (var j = map.TileLayers.Count - 1; j >= 0; j--)
            {
                if (map.TileLayers[j].Tiles[tileIndex].Gid != 0)
                    return map.TileLayers[j].Tiles[tileIndex].Gid-1;
            }
            return 0; // if no tile was found at that specific location
        }
        public string GetProperty(int tileID, string propertyName)
        {
            return map.Tilesets[0].Tiles[tileID].Properties.GetValueOrDefault(propertyName);
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.View
{
    class Camera
    {
        float scaleX;
        float scaleY;

        // Used for resizing all textures.
        float overallSize = 1.0f;

        public Camera(Viewport viewPort)
        {
            scaleX = viewPort.Width;
            scaleY = viewPort.Height;

            //if (scaleX < scaleY)
            //{
            //    scaleY = scaleX;
            //}
            //else if (scaleY < scaleX)
            //{
            //    scaleX = scaleY;
            //}

        }

        public Vector2 getVisualCoords(Vector2 logicalCoords, float textureWidth, float textureHeight)
        {

            float visualX = (logicalCoords.X * scaleX) - textureWidth / 2;
            float visualY = (logicalCoords.Y * scaleY) - textureHeight / 2;

            return new Vector2(visualX, visualY);
        }


        public Vector2 getLogicalCoords(Vector2 visualCoords)
        {
            float logicalX = (visualCoords.X) / scaleX;
            float logicalY = (visualCoords.Y) / scaleY;
            //Console.WriteLine(logicalX + " " + logicalY);
            return new Vector2(logicalX, logicalY);
        }

        public float getTextureScale(float textureWidth, float size)
        {
            return scaleX * (size * overallSize) / textureWidth;
        }

        //public Rectangle getGameArea()
        //{
        //    return new Rectangle(0, 0, (int)scaleX, (int)scaleY);
        //}

        //public Vector2 getViewport()
        //{
        //    return new Vector2(scaleX, scaleY);
        //}
    }
}
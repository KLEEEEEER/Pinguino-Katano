using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class NetworkState : State
    {
        protected float lastX;
        protected float lastZ;
        protected float lastY;
        protected float currentX;
        protected float currentY;
        protected float currentZ;

        public void SetStartPosition(float x, float y, float z)
        {
            lastX = x;
            lastY = y;
            lastZ = z;
        }
    }
}
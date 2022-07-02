using UnityEngine;

namespace Utils
{
    public static class RotationHelper
    {
        public static float FixAngle(this float angle)
        {
            if (angle > 180)
            {
                return angle - 360;
            }

            if (angle < -180)
            {
                return angle + 360;
            }

            return angle;
        }

        /// <summary>
        /// Returns clamped Quaternion.
        /// </summary>
        /// <param name="quat">Quaternion</param>
        /// <param name="minValue">MinValue in degrees</param>
        /// <param name="maxValue">MaxValue in degrees</param>
        /// <returns></returns>
        /// 
        public static Quaternion ClampRotationAroundXAxis(this Quaternion quat, float minValue, float maxValue)
        {
            quat.x /= quat.w;
            quat.w = 1.0f;

            float _xRotation = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quat.x);
            _xRotation = Mathf.Clamp(_xRotation, FixAngle(minValue), FixAngle(maxValue));
            quat.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * _xRotation);
            
            return quat;
        }
        
        /// <summary>
        /// Returns clamped Quaternion.
        /// </summary>
        /// <param name="quat">Quaternion</param>
        /// <param name="minValue">MinValue in degrees</param>
        /// <param name="maxValue">MaxValue in degrees</param>
        /// <returns></returns>
        public static Quaternion ClampRotationAroundYAxis(this Quaternion quat, float minValue, float maxValue)
        {
             quat.y /= quat.w;
             quat.w = 1.0f;

             float _yRotation = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quat.y);
             _yRotation = Mathf.Clamp(_yRotation, FixAngle(minValue), FixAngle(maxValue));
             quat.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * _yRotation);

             return quat;
        }
        
        /// <summary>
        /// Returns clamped Quaternion.
        /// </summary>
        /// <param name="quat">Quaternion</param>
        /// <param name="minValue">MinValue in degrees</param>
        /// <param name="maxValue">MaxValue in degrees</param>
        /// <returns></returns>
        public static Quaternion ClampRotationAroundZAxis(this Quaternion quat, float minValue, float maxValue)
        {
            quat.z /= quat.w;
            quat.w = 1.0f;
            
            float _zRotation = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quat.z);
            _zRotation = Mathf.Clamp(_zRotation, FixAngle(minValue), FixAngle(maxValue));
            quat.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * _zRotation);

            return quat;
        }
    }
}
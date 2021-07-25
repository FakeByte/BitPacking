﻿using System.Runtime.CompilerServices;

namespace JamesFrowen.BitPacking
{
    public static class Compression
    {
        /// <summary>
        /// Scales float from minFloat->maxFloat to minUint->maxUint
        /// <para>values out side of minFloat/maxFloat will return either 0 or maxUint</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minFloat"></param>
        /// <param name="maxFloat"></param>
        /// <param name="minUint">should be a power of 2, can be 0</param>
        /// <param name="maxUint">should be a power of 2, for example 1 &lt;&lt; 8 for value to take up 8 bytes</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ScaleToUInt(float value, float minFloat, float maxFloat, uint minUint, uint maxUint)
        {
            // if out of range return min/max
            if (value > maxFloat) { return maxUint; }
            if (value < minFloat) { return minUint; }

            float rangeFloat = maxFloat - minFloat;
            uint rangeUint = maxUint - minUint;

            // scale value to 0->1 (as float)
            float valueRelative = (value - minFloat) / rangeFloat;
            // scale value to uMin->uMax
            float outValue = (valueRelative * rangeUint) + minUint;

            return (uint)outValue;
        }

        /// <summary>
        /// Scales uint from minUint->maxUint to minFloat->maxFloat 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minFloat"></param>
        /// <param name="maxFloat"></param>
        /// <param name="minUint">should be a power of 2, can be 0</param>
        /// <param name="maxUint">should be a power of 2, for example 1 &lt;&lt; 8 for value to take up 8 bytes</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ScaleFromUInt(ulong value, float minFloat, float maxFloat, uint minUint, uint maxUint)
        {
            // if out of range return min/max
            if (value > maxUint) { return maxFloat; }
            if (value < minUint) { return minFloat; }

            float rangeFloat = maxFloat - minFloat;
            uint rangeUint = maxUint - minUint;

            // scale value to 0->1 (as float)
            // make sure divide is float
            float valueRelative = (value - minUint) / (float)rangeUint;
            // scale value to fMin->fMax
            float outValue = (valueRelative * rangeFloat) + minFloat;
            return outValue;
        }
    }
}

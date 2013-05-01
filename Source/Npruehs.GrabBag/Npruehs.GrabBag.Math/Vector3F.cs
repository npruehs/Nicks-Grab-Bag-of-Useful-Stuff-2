// --------------------------------------------------------------------------------
// <copyright company="Nick Pruehs" file="Vector3F.cs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// 
// --------------------------------------------------------------------------------
namespace Npruehs.GrabBag.Math
{
    using System;

    /// <summary>
    /// </summary>
    [CLSCompliant(true)]
    public struct Vector3F : IEquatable<Vector3F>
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static readonly Vector3F One = new Vector3F(1.0f, 1.0f, 1.0f);

        /// <summary>
        /// </summary>
        public static readonly Vector3F Zero = new Vector3F();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="x">
        /// </param>
        /// <param name="y">
        /// </param>
        /// <param name="z">
        /// </param>
        public Vector3F(float x, float y, float z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        public Vector3F(Vector3F v)
            : this()
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public float Length
        {
            get
            {
                return Math2.Sqrt(this.LengthSquared);
            }
        }

        /// <summary>
        /// </summary>
        public float LengthSquared
        {
            get
            {
                return (this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z);
            }
        }

        /// <summary>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// </summary>
        public float Z { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// http://en.wikipedia.org/wiki/Cross_product#Coordinate_notation
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F Cross(Vector3F v1, Vector3F v2)
        {
            return new Vector3F(
                (v1.Y * v2.Z) - (v1.Z * v2.Y), (v1.Z * v2.X) - (v1.X * v2.Z), (v1.X * v2.Y) - (v1.Y * v2.X));
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static float Distance(Vector3F v1, Vector3F v2)
        {
            return Math2.Sqrt(DistanceSquared(v1, v2));
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static float DistanceSquared(Vector3F v1, Vector3F v2)
        {
            var distX = v1.X - v2.X;
            var distY = v1.Y - v2.Y;
            var distZ = v1.Z - v2.Z;
            return (distX * distX) + (distY * distY) + (distZ * distZ);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static float Dot(Vector3F v1, Vector3F v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <param name="l">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F Lerp(Vector3F v1, Vector3F v2, float l)
        {
            if (l <= 0.0f)
            {
                return v1;
            }

            if (l >= 1.0f)
            {
                return v2;
            }

            return v1 + (l * (v2 - v1));
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F Normalize(Vector3F v)
        {
            var lengthSquared = v.LengthSquared;
            if (lengthSquared == 0)
            {
                return v;
            }

            var lengthInverse = 1.0f / Math2.Sqrt(lengthSquared);

            return new Vector3F(v.X * lengthInverse, v.Y * lengthInverse, v.Z * lengthInverse);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F operator +(Vector3F v1, Vector3F v2)
        {
            return new Vector3F(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <param name="f">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F operator /(Vector3F v, float f)
        {
            return new Vector3F(v.X / f, v.Y / f, v.Z / f);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(Vector3F v1, Vector3F v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(Vector3F v1, Vector3F v2)
        {
            return !(v1 == v2);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <param name="f">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F operator *(Vector3F v, float f)
        {
            return f * v;
        }

        /// <summary>
        /// </summary>
        /// <param name="f">
        /// </param>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F operator *(float f, Vector3F v)
        {
            return new Vector3F(v.X * f, v.Y * f, v.Z * f);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3F operator -(Vector3F v1, Vector3F v2)
        {
            return new Vector3F(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public Vector3F Cross(Vector3F v)
        {
            return Cross(this, v);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public float Distance(Vector3F v)
        {
            return Distance(this, v);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public float DistanceSquared(Vector3F v)
        {
            return DistanceSquared(this, v);
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <returns>
        /// </returns>
        public float Dot(Vector3F v)
        {
            return Dot(this, v);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Vector3F && this.Equals((Vector3F)obj);
        }

        /// <summary>
        /// </summary>
        /// <param name="other">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Equals(Vector3F other)
        {
            return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="v">
        /// </param>
        /// <param name="l">
        /// </param>
        /// <returns>
        /// </returns>
        public Vector3F Lerp(Vector3F v, float l)
        {
            return Lerp(this, v, l);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public Vector3F Normalize()
        {
            return Normalize(this);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
        }

        #endregion
    }
}
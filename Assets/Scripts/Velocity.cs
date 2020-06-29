using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity
{
    public float x;
    public float y;
    public float z;

    public Velocity(float Vx, float Vy)
    {
        this.x = Vx;
        this.y = Vy;
    }
    public Velocity(float Vx, float Vy, float Vz)
    {
        this.x = Vx;
        this.y = Vy;
        this.z = Vz;
    }
}

public static class PhyConstants
{
    public const float gravity = 9.8f;
}
public enum TypeOfFluid { SeaWater, Gaslone, Bromine }
public enum TypeOfMaterial { Timber, Gold, Walnut }

public static class FluidDensity
{
    public const float seaWater = 0.1f;
    public const float gasoline = 0.0711f;
    public const float bromine = 0.31f;
}

public static class MaterialDensity
{
    public const float timber = 0.07f;
    public const float rock = 0.16f;
    public const float walnut = 0.04f;
}

public class Position
{
    public float x;
    public float y;
    public float z;

    public Position(float X, float Y)
    {
        this.x = X;
        this.y = Y;
    }
    public Position(float X, float Y, float Z)
    {
        this.x = X;
        this.y = Y;
        this.z = Z;
    }
}

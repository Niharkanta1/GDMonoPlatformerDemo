using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public static class MathUtil
{

    public static bool IsEqualApprox(float a, float b)
    {
        return IsEqualApprox(a, b, 0.00001);
    }

    private static bool IsEqualApprox(double left, double right, double delta)
    {
        return Math.Abs(left - right) < delta;
    }

    // Angles
    public static float DegreeToRadian(float degrees) => (float)(Math.PI / 180) * degrees;
}
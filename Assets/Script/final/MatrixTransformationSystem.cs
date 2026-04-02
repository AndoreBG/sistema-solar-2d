using UnityEngine;

public static class MatrixTransformationSystem
{
    // Compoe matrizes por produto
    public static Matrix4x4 ComposeMatrices(Matrix4x4[] matrices)
    {
        if (matrices == null || matrices.Length == 0)
            return Matrix4x4.identity;

        Matrix4x4 composed = matrices[0];

        for (int i = 1; i < matrices.Length; i++)
            composed = matrices[i] * composed;

        return composed;
    }

    // Matriz de translacao
    public static Matrix4x4 Translation(float tx, float ty)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m03 = tx;
        m.m13 = ty;
        return m;
    }

    // Matriz de rotacao (graus)
    public static Matrix4x4 Rotation(float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = Mathf.Cos(rad);
        m.m01 = -Mathf.Sin(rad);
        m.m10 = Mathf.Sin(rad);
        m.m11 = Mathf.Cos(rad);
        return m;
    }

    // Matriz de escala
    public static Matrix4x4 Scale(float sx, float sy)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = sx;
        m.m11 = sy;
        return m;
    }

    // Aplica matriz ao transform
    public static void Apply(Transform t, Matrix4x4 m)
    {
        t.position = new Vector3(m.m03, m.m13, 0);
        t.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(m.m10, m.m00) * Mathf.Rad2Deg);
        t.localScale = new Vector3(
            Mathf.Sqrt(m.m00 * m.m00 + m.m10 * m.m10),
            Mathf.Sqrt(m.m01 * m.m01 + m.m11 * m.m11),
            1
        );
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Int2
{
    public int x, y;

    public Int2(Int2 i)
    {
        x = i.x;
        y = i.y;
    }
    public Int2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Int2(int x)
    {
        this.x = x;
    }
    public Int2()
    {
    }

    public static implicit operator Vector2(Int2 i)
    {
        return new Vector2(i.x, i.y);
    }

    public static implicit operator Int2(Vector2 i)
    {
        return new Int2((int)i.x, (int)i.y);
    }

    public static implicit operator Vector3(Int2 i)
    {
        return new Vector3(i.x, i.y);
    }

    public static Int2 operator -(Int2 i1, Int2 i2)
    {
        return new Int2(i1.x - i2.x, i1.y - i2.y);
    }

    public static Int2 operator +(Int2 i1, Int2 i2)
    {
        return new Int2(i1.x + i2.x, i1.y + i2.y);
    }

    public static Int2 operator *(Int2 i1, Int2 i2)
    {
        return new Int2(i1.x * i2.x, i1.y * i2.y);
    }

    public static Int2 operator /(Int2 i1, Int2 i2)
    {
        return new Int2(i1.x / i2.x, i1.y / i2.y);
    }

    public static bool operator ==(Int2 i1, Int2 i2)
    {
        return i1.x == i2.x && i1.y == i2.y;
    }

    public static bool operator !=(Int2 i1, Int2 i2)
    {
        return i1.x != i2.x && i1.y != i2.y;
    }
}

public static class Int2Extensions
{
    public static Int2 Abs(this Int2 i)
    {
        return new Int2(Mathf.Abs(i.x), Mathf.Abs(i.y));
    }

    public static Int2 NormalizePerAxis(this Int2 i)
    {
        var r = new Int2();
        r.x = i.x >= 0 ? 1 : -1;
        r.y = i.y >= 0 ? 1 : -1;
        return r;
    }
}

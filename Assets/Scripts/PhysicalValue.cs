using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalValue : IEquatable<PhysicalValue>,IComparable<PhysicalValue>
{
    public static readonly PhysicalValue Zero = new PhysicalValue(0, 0);

    public PhysicalValue(decimal value, int exponent)
    {
        Value = value;
        Exponent = exponent;
        while(Value>=10 || Value <= -10)
        {
            Exponent++;
            Value /= 10;
        }
    }

    public PhysicalValue(double number)
    {
        int exponent = 0;
        while (number >= 10||number<=-10)
        {
            exponent++;
            number /= 10;
        }
        Value = (decimal)number;
        Exponent = exponent;
    }

    public decimal Value { get; private set; }
    public int Exponent { get; private set; }

    public static PhysicalValue Pow(PhysicalValue ph,int exponent)
    {
        return new PhysicalValue(ph.Value, ph.Exponent*exponent);
    }

    public static PhysicalValue Sqrt(PhysicalValue ph)
    {
        decimal value = ph.Value;
        int exponent = ph.Exponent;
        if (exponent % 2 == 1)
        {
            exponent -= 1;
            value *= 10;
        }
        exponent /= 2;
        value = (decimal)Math.Sqrt((double)value);
        return new PhysicalValue(value, exponent);
    }
    public static PhysicalValue Root(PhysicalValue ph, int root)
    {
        decimal value = ph.Value;
        int exponent = ph.Exponent;
        int r = exponent % 3;
        exponent -= r;
        for (int i = 0; i < r; i++)
        {
            value *= 10;
        }

        exponent /= 3;
        value=(decimal)Math.Pow((double)value,1f/root);
        return new PhysicalValue(value, exponent);

    }

    public static PhysicalValue ClampRadians(PhysicalValue ph)
    {
        decimal twoPi = (decimal)(2 * Math.PI);
        decimal x = ph;
        decimal periods = Math.Floor(x / twoPi);
        decimal rest = x - periods * twoPi;
        return new PhysicalValue(rest, 0);
    }
    public static bool operator == (PhysicalValue first, PhysicalValue second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(PhysicalValue first, PhysicalValue second)
    {
        return !first.Equals(second);
    }
    public static PhysicalValue operator + (PhysicalValue ph1, PhysicalValue ph2)
    {
        int diff = ph1.Exponent - ph2.Exponent;

        if (diff==0) return new PhysicalValue(ph1.Value + ph2.Value, ph1.Exponent);

        if (diff>0)
        {
            decimal value = ph1.Value + ph2.Value * (decimal)Math.Pow(10, -diff);
            return new PhysicalValue(value, ph1.Exponent);
        }
        else
        {
            decimal value = ph2.Value + ph1.Value * (decimal)Math.Pow(10, diff);
            return new PhysicalValue(value, ph2.Exponent);
        }
    }
    public static PhysicalValue operator - (PhysicalValue ph1, PhysicalValue ph2)
    {
        int diff = ph1.Exponent - ph2.Exponent;

        if (diff == 0) return new PhysicalValue(ph1.Value - ph2.Value, ph1.Exponent);

        if (diff > 0)
        {
            decimal value = ph1.Value - ph2.Value * (decimal)Math.Pow(10, -diff);
            return new PhysicalValue(value, ph1.Exponent);
        }
        else
        {
            decimal value = ph1.Value * (decimal)Math.Pow(10, diff)- ph2.Value;
            return new PhysicalValue(value, ph2.Exponent);
        }
    }

    public static PhysicalValue operator -(PhysicalValue ph)
    {
        return new PhysicalValue(-ph.Value, ph.Exponent);
    }

    public static PhysicalValue operator *(PhysicalValue ph1, PhysicalValue ph2)
    {
        return new PhysicalValue(ph1.Value * ph2.Value, (ph1.Exponent + ph2.Exponent));
    }

    public static PhysicalValue operator /(PhysicalValue ph1, PhysicalValue ph2)
    {
        return new PhysicalValue(ph1.Value / ph2.Value, (ph1.Exponent - ph2.Exponent));
    }

    public static bool operator <(PhysicalValue ph1, PhysicalValue ph2)
    {
        return ph1.CompareTo(ph2) < 0;
    }
    public static bool operator <=(PhysicalValue ph1, PhysicalValue ph2)
    {
        return ph1.CompareTo(ph2) <= 0;
    }
    public static bool operator >(PhysicalValue ph1, PhysicalValue ph2)
    {
        return ph1.CompareTo(ph2) > 0;
    }
    public static bool operator >=(PhysicalValue ph1, PhysicalValue ph2)
    {
        return ph1.CompareTo(ph2) >= 0;
    }

    public static explicit operator float (PhysicalValue arg)
    {
        return (float)arg.Value * Mathf.Pow(10, arg.Exponent);
    }

    public static explicit operator double(PhysicalValue arg)
    {
        return (double)arg.Value * Math.Pow(10, arg.Exponent);
    }

    public static implicit operator PhysicalValue(double d)
    {
        return new PhysicalValue(d);
    }

    public static implicit operator decimal(PhysicalValue arg)
    {
        return arg.Value* (decimal)Math.Pow(10, arg.Exponent);
    }

    public override string ToString()
    {
        return Value.ToString()+"e"+Exponent;
    }

    public bool Equals(PhysicalValue other)
    {
        return (Value == other.Value && Exponent == other.Exponent);
    }

    public int CompareTo(PhysicalValue other)
    {
        if (Exponent == other.Exponent) return (int)(Value - other.Value);

        return Exponent - other.Exponent;
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() == typeof(PhysicalValue)) return Equals((PhysicalValue)obj);
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

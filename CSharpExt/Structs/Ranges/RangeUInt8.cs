﻿using System;

namespace Noggog
{
    public struct RangeUInt8 : IEquatable<RangeUInt8>
    {
        public readonly byte Min;
        public readonly byte Max;
        public float Average => ((Max - Min) / 2f) + Min;
        public byte Difference => (byte)(this.Max - this.Min);

        public RangeUInt8(byte val1, byte val2)
        {
            if (val1 > val2)
            {
                Max = val1;
                Min = val2;
            }
            else
            {
                Min = val1;
                Max = val2;
            }
        }

        public RangeUInt8(byte? min, byte? max)
            : this(min ?? byte.MinValue, max ?? byte.MaxValue)
        {
        }

        public static RangeUInt8 Parse(string str)
        {
            if (!TryParse(str, out RangeUInt8 rd))
            {
                return default(RangeUInt8);
            }
            return rd;
        }

        public static bool TryParse(string str, out RangeUInt8 rd)
        {
            if (str == null)
            {
                rd = default(RangeUInt8);
                return false;
            }
            string[] split = str.Split('-');
            if (split.Length != 2)
            {
                rd = default(RangeUInt8);
                return false;
            }
            rd = new RangeUInt8(
                byte.Parse(split[0]),
                byte.Parse(split[1]));
            return true;
        }

        public bool IsInRange(byte i)
        {
            if (i > this.Max) return false;
            if (i < this.Min) return false;
            return true;
        }

        public byte PutInRange(byte f, bool throwException = true)
        {
            if (throwException)
            {
                if (f < this.Min)
                {
                    throw new ArgumentException($"Min is out of range: {f} < {this.Min}");
                }
                if (f > this.Max)
                {
                    throw new ArgumentException($"Min is out of range: {f} < {this.Max}");
                }
            }
            else
            {
                if (f > this.Max) return this.Max;
                if (f < this.Min) return this.Min;
            }
            return f;
        }

        public bool IsInRange(RangeUInt8 r)
        {
            if (r.Max > this.Max) return false;
            if (r.Min < this.Min) return false;
            return true;
        }

        public RangeUInt8 PutInRange(RangeUInt8 r, bool throwException = true)
        {
            if (throwException)
            {
                if (r.Min < this.Min)
                {
                    throw new ArgumentException($"Min is out of range: {r.Min} < {this.Min}");
                }
                if (r.Max > this.Max)
                {
                    throw new ArgumentException($"Min is out of range: {r.Max} < {this.Max}");
                }
                return r;
            }
            else
            {
                byte min = r.Min < this.Min ? this.Min : r.Min;
                byte max = r.Max < this.Max ? this.Max : r.Max;
                return new RangeUInt8(min, max);
            }
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is RangeUInt8 rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(RangeUInt8 other)
        {
            return this.Min == other.Min
                && this.Max == other.Max;
        }

        public override int GetHashCode()
        {
            return HashHelper.GetHashCode(Min, Max);
        }

        public override string ToString()
        {
            return Min == Max ? $"({Min.ToString()})" : $"({Min} - {Max})";
        }

        public static bool operator ==(RangeUInt8 c1, RangeUInt8 c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(RangeUInt8 c1, RangeUInt8 c2)
        {
            return !c1.Equals(c2);
        }
    }
}
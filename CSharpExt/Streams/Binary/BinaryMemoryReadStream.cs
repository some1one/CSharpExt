﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noggog
{
    public class BinaryMemoryReadStream : IBinaryReadStream
    {
        internal int _pos;
        internal byte[] _data;
        public int Position
        {
            get => this._pos;
            set => SetPosition(value);
        }
        public int Length => this._data.Length;
        public int Remaining => this._data.Length - this._pos;
        public bool Complete => this._data.Length <= this._pos;

        #region IBinaryReadStream
        long IBinaryReadStream.Position { get => _pos; set => SetPosition(checked((int)value)); }
        long IBinaryReadStream.Length => this._data.Length;
        long IBinaryReadStream.Remaining => this._data.Length - this._pos;
        #endregion

        public BinaryMemoryReadStream(byte[] data)
        {
            this._data = data;
        }

        public int Read(byte[] buffer)
        {
            return Read(buffer, offset: 0, amount: buffer.Length);
        }

        public int Read(byte[] buffer, int offset, int amount)
        {
            var ret = Get(buffer, offset, amount);
            _pos += amount;
            return ret;
        }

        public byte[] ReadBytes(int amount)
        {
            var ret = GetBytes(amount);
            _pos += amount;
            return ret;
        }

        private void SetPosition(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Cannot set to a negative position");
            }
            _pos = value;
        }

        public bool ReadBool()
        {
            return _data[_pos++] > 0;
        }

        public byte ReadUInt8()
        {
            return _data[_pos++];
        }

        public byte ReadByte()
        {
            return _data[_pos++];
        }

        public ushort ReadUInt16()
        {
            _pos += 2;
            return BitConverter.ToUInt16(this._data, _pos - 2);
        }

        public uint ReadUInt32()
        {
            _pos += 4;
            return BitConverter.ToUInt32(this._data, _pos - 4);
        }

        public ulong ReadUInt64()
        {
            _pos += 8;
            return BitConverter.ToUInt64(this._data, _pos - 8);
        }

        public sbyte ReadInt8()
        {
            return (sbyte)_data[_pos++];
        }

        public short ReadInt16()
        {
            _pos += 2;
            return BitConverter.ToInt16(this._data, _pos - 2);
        }

        public int ReadInt32()
        {
            _pos += 4;
            return BitConverter.ToInt32(this._data, _pos - 4);
        }

        public long ReadInt64()
        {
            _pos += 8;
            return BitConverter.ToInt64(this._data, _pos - 8);
        }

        public string ReadString(int amount)
        {
            _pos += amount;
            return BinaryUtility.BytesToString(_data, _pos - amount, amount);
        }

        public float ReadFloat()
        {
            _pos += 4;
            return BitConverter.ToSingle(this._data, _pos - 4);
        }

        public double ReadDouble()
        {
            _pos += 8;
            return BitConverter.ToDouble(this._data, _pos - 8);
        }

        public void Dispose()
        {
            this._data = null;
        }

        public void WriteTo(Stream stream, int amount)
        {
            _pos += amount;
            stream.Write(_data, _pos - amount, amount);
        }

        public ReadOnlySpan<byte> ReadSpan(int amount)
        {
            _pos += amount;
            return _data.AsSpan(_pos - amount, amount);
        }

        public int Get(byte[] buffer, int offset, int amount)
        {
            if (amount > Remaining)
            {
                amount = Remaining;
            }
            Array.Copy(_data, _pos, buffer, offset, amount);
            return amount;
        }

        public int Get(byte[] buffer, int offset)
        {
            return Get(buffer, offset: offset, amount: buffer.Length);
        }

        public byte[] GetBytes(int amount)
        {
            byte[] ret = new byte[amount];
            Array.Copy(_data, _pos, ret, 0, amount);
            return ret;
        }

        public bool GetBool(int offset)
        {
            return _data[_pos + offset] > 0;
        }

        public byte GetUInt8(int offset)
        {
            return _data[_pos + offset];
        }

        public ushort GetUInt16(int offset)
        {
            return BitConverter.ToUInt16(this._data, _pos + offset);
        }

        public uint GetUInt32(int offset)
        {
            return BitConverter.ToUInt32(this._data, _pos + offset);
        }

        public ulong GetUInt64(int offset)
        {
            return BitConverter.ToUInt64(this._data, _pos + offset);
        }

        public sbyte GetInt8(int offset)
        {
            return (sbyte)_data[_pos + offset];
        }

        public short GetInt16(int offset)
        {
            return BitConverter.ToInt16(this._data, _pos + offset);
        }

        public int GetInt32(int offset)
        {
            return BitConverter.ToInt32(this._data, _pos + offset);
        }

        public long GetInt64(int offset)
        {
            return BitConverter.ToInt64(this._data, _pos + offset);
        }

        public float GetFloat(int offset)
        {
            return BitConverter.ToSingle(this._data, _pos + offset);
        }

        public double GetDouble(int offset)
        {
            return BitConverter.ToDouble(this._data, _pos + offset);
        }

        public string GetString(int amount, int offset)
        {
            return BinaryUtility.BytesToString(_data, _pos + offset, amount);
        }
    }
}

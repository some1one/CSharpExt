﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noggog
{
    public interface IBinaryReadStream : IDisposable
    {
        long Position { get; set; }
        long Length { get; }
        long Remaining { get; }
        bool Complete { get; }
        ReadOnlySpan<byte> RemainingSpan { get; }
        ReadOnlyMemorySlice<byte> RemainingMemory { get; }
        int Read(byte[] buffer, int offset, int amount);
        int Get(byte[] buffer, int targetOffset, int amount);
        int Read(byte[] buffer);
        int Get(byte[] buffer, int targetOffset);
        byte[] ReadBytes(int amount);
        byte[] GetBytes(int amount);
        ReadOnlySpan<byte> ReadSpan(int amount);
        ReadOnlySpan<byte> ReadSpan(int amount, int offset);
        ReadOnlySpan<byte> GetSpan(int amount);
        ReadOnlySpan<byte> GetSpan(int amount, int offset);
        ReadOnlyMemorySlice<byte> ReadMemory(int amount);
        ReadOnlyMemorySlice<byte> ReadMemory(int amount, int offset);
        ReadOnlyMemorySlice<byte> GetMemory(int amount);
        ReadOnlyMemorySlice<byte> GetMemory(int amount, int offset);
        bool ReadBool();
        bool GetBool();
        bool GetBool(int offset);
        byte ReadUInt8();
        byte GetUInt8();
        byte GetUInt8(int offset);
        ushort ReadUInt16();
        ushort GetUInt16();
        ushort GetUInt16(int offset);
        uint ReadUInt32();
        uint GetUInt32();
        uint GetUInt32(int offset);
        ulong ReadUInt64();
        ulong GetUInt64();
        ulong GetUInt64(int offset);
        sbyte ReadInt8();
        sbyte GetInt8();
        sbyte GetInt8(int offset);
        short ReadInt16();
        short GetInt16();
        short GetInt16(int offset);
        int ReadInt32();
        int GetInt32();
        int GetInt32(int offset);
        long ReadInt64();
        long GetInt64();
        long GetInt64(int offset);
        float ReadFloat();
        float GetFloat();
        float GetFloat(int offset);
        double ReadDouble();
        double GetDouble();
        double GetDouble(int offset);
        string ReadStringUTF8(int amount);
        string GetStringUTF8(int amount);
        string GetStringUTF8(int amount, int offset);
        void WriteTo(Stream stream, int amount);
    }
}
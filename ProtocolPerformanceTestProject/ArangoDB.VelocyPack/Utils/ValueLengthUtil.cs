﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB.VelocyPack.Utils
{
    public class ValueLengthUtil
    {
        private static int DOUBLE_BYTES = sizeof(double);
        private static int LONG_BYTES = sizeof(long);
        private static int CHARACTER_BYTES = sizeof(char);

        private static readonly Dictionary<byte, int> MAP;

        static ValueLengthUtil()
        {
            MAP = new Dictionary<byte, int>();
            MAP.Add(0x00, 1);
            MAP.Add(0x01, 1);
            MAP.Add(0x02, 0);
            MAP.Add(0x03, 0);
            MAP.Add(0x04, 0);
            MAP.Add(0x05, 0);
            MAP.Add(0x06, 0);
            MAP.Add(0x07, 0);
            MAP.Add(0x08, 0);
            MAP.Add(0x09, 0);
            MAP.Add(0x0a, 1);
            MAP.Add(0x0b, 0);
            MAP.Add(0x0c, 0);
            MAP.Add(0x0d, 0);
            MAP.Add(0x0e, 0);
            MAP.Add(0x0f, 0);
            MAP.Add(0x10, 0);
            MAP.Add(0x11, 0);
            MAP.Add(0x12, 0);
            MAP.Add(0x13, 0);
            MAP.Add(0x14, 0);
            MAP.Add(0x15, 0);
            MAP.Add(0x16, 0);
            MAP.Add(0x17, 1);
            MAP.Add(0x18, 1);
            MAP.Add(0x19, 1);
            MAP.Add(0x1a, 1);
            MAP.Add(0x1b, 1 + DOUBLE_BYTES);
            MAP.Add(0x1c, 1 + LONG_BYTES);
            MAP.Add(0x1d, 1 + CHARACTER_BYTES);
            MAP.Add(0x1e, 1);
            MAP.Add(0x1f, 1);
            MAP.Add(0x20, 2);
            MAP.Add(0x21, 3);
            MAP.Add(0x22, 4);
            MAP.Add(0x23, 5);
            MAP.Add(0x24, 6);
            MAP.Add(0x25, 7);
            MAP.Add(0x26, 8);
            MAP.Add(0x27, 9);
            MAP.Add(0x28, 2);
            MAP.Add(0x29, 3);
            MAP.Add(0x2a, 4);
            MAP.Add(0x2b, 5);
            MAP.Add(0x2c, 6);
            MAP.Add(0x2d, 7);
            MAP.Add(0x2e, 8);
            MAP.Add(0x2f, 9);
            MAP.Add(0x30, 1);
            MAP.Add(0x31, 1);
            MAP.Add(0x32, 1);
            MAP.Add(0x33, 1);
            MAP.Add(0x34, 1);
            MAP.Add(0x35, 1);
            MAP.Add(0x36, 1);
            MAP.Add(0x37, 1);
            MAP.Add(0x38, 1);
            MAP.Add(0x39, 1);
            MAP.Add(0x3a, 1);
            MAP.Add(0x3b, 1);
            MAP.Add(0x3c, 1);
            MAP.Add(0x3d, 1);
            MAP.Add(0x3e, 1);
            MAP.Add(0x3f, 1);
            MAP.Add(0x40, 1);
            MAP.Add(0x41, 2);
            MAP.Add(0x42, 3);
            MAP.Add(0x43, 4);
            MAP.Add(0x44, 5);
            MAP.Add(0x45, 6);
            MAP.Add(0x46, 7);
            MAP.Add(0x47, 8);
            MAP.Add(0x48, 9);
            MAP.Add(0x49, 10);
            MAP.Add(0x4a, 11);
            MAP.Add(0x4b, 12);
            MAP.Add(0x4c, 13);
            MAP.Add(0x4d, 14);
            MAP.Add(0x4e, 15);
            MAP.Add(0x4f, 16);
            MAP.Add(0x50, 17);
            MAP.Add(0x51, 18);
            MAP.Add(0x52, 19);
            MAP.Add(0x53, 20);
            MAP.Add(0x54, 21);
            MAP.Add(0x55, 22);
            MAP.Add(0x56, 23);
            MAP.Add(0x57, 24);
            MAP.Add(0x58, 25);
            MAP.Add(0x59, 26);
            MAP.Add(0x5a, 27);
            MAP.Add(0x5b, 28);
            MAP.Add(0x5c, 29);
            MAP.Add(0x5d, 30);
            MAP.Add(0x5e, 31);
            MAP.Add(0x5f, 32);
            MAP.Add(0x60, 33);
            MAP.Add(0x61, 34);
            MAP.Add(0x62, 35);
            MAP.Add(0x63, 36);
            MAP.Add(0x64, 37);
            MAP.Add(0x65, 38);
            MAP.Add(0x66, 39);
            MAP.Add(0x67, 40);
            MAP.Add(0x68, 41);
            MAP.Add(0x69, 42);
            MAP.Add(0x6a, 43);
            MAP.Add(0x6b, 44);
            MAP.Add(0x6c, 45);
            MAP.Add(0x6d, 46);
            MAP.Add(0x6e, 47);
            MAP.Add(0x6f, 48);
            MAP.Add(0x70, 49);
            MAP.Add(0x71, 50);
            MAP.Add(0x72, 51);
            MAP.Add(0x73, 52);
            MAP.Add(0x74, 53);
            MAP.Add(0x75, 54);
            MAP.Add(0x76, 55);
            MAP.Add(0x77, 56);
            MAP.Add(0x78, 57);
            MAP.Add(0x79, 58);
            MAP.Add(0x7a, 59);
            MAP.Add(0x7b, 60);
            MAP.Add(0x7c, 61);
            MAP.Add(0x7d, 62);
            MAP.Add(0x7e, 63);
            MAP.Add(0x7f, 64);
            MAP.Add(0x80, 65);
            MAP.Add(0x81, 66);
            MAP.Add(0x82, 67);
            MAP.Add(0x83, 68);
            MAP.Add(0x84, 69);
            MAP.Add(0x85, 70);
            MAP.Add(0x86, 71);
            MAP.Add(0x87, 72);
            MAP.Add(0x88, 73);
            MAP.Add(0x89, 74);
            MAP.Add(0x8a, 75);
            MAP.Add(0x8b, 76);
            MAP.Add(0x8c, 77);
            MAP.Add(0x8d, 78);
            MAP.Add(0x8e, 79);
            MAP.Add(0x8f, 80);
            MAP.Add(0x90, 81);
            MAP.Add(0x91, 82);
            MAP.Add(0x92, 83);
            MAP.Add(0x93, 84);
            MAP.Add(0x94, 85);
            MAP.Add(0x95, 86);
            MAP.Add(0x96, 87);
            MAP.Add(0x97, 88);
            MAP.Add(0x98, 89);
            MAP.Add(0x99, 90);
            MAP.Add(0x9a, 91);
            MAP.Add(0x9b, 92);
            MAP.Add(0x9c, 93);
            MAP.Add(0x9d, 94);
            MAP.Add(0x9e, 95);
            MAP.Add(0x9f, 96);
            MAP.Add(0xa0, 97);
            MAP.Add(0xa1, 98);
            MAP.Add(0xa2, 99);
            MAP.Add(0xa3, 100);
            MAP.Add(0xa4, 101);
            MAP.Add(0xa5, 102);
            MAP.Add(0xa6, 103);
            MAP.Add(0xa7, 104);
            MAP.Add(0xa8, 105);
            MAP.Add(0xa9, 106);
            MAP.Add(0xaa, 107);
            MAP.Add(0xab, 108);
            MAP.Add(0xac, 109);
            MAP.Add(0xad, 110);
            MAP.Add(0xae, 111);
            MAP.Add(0xaf, 112);
            MAP.Add(0xb0, 113);
            MAP.Add(0xb1, 114);
            MAP.Add(0xb2, 115);
            MAP.Add(0xb3, 116);
            MAP.Add(0xb4, 117);
            MAP.Add(0xb5, 118);
            MAP.Add(0xb6, 119);
            MAP.Add(0xb7, 120);
            MAP.Add(0xb8, 121);
            MAP.Add(0xb9, 122);
            MAP.Add(0xba, 123);
            MAP.Add(0xbb, 124);
            MAP.Add(0xbc, 125);
            MAP.Add(0xbd, 126);
            MAP.Add(0xbe, 127);
            MAP.Add(0xbf, 0);
            MAP.Add(0xc0, 0);
            MAP.Add(0xc1, 0);
            MAP.Add(0xc2, 0);
            MAP.Add(0xc3, 0);
            MAP.Add(0xc4, 0);
            MAP.Add(0xc5, 0);
            MAP.Add(0xc6, 0);
            MAP.Add(0xc7, 0);
            MAP.Add(0xc8, 0);
            MAP.Add(0xc9, 0);
            MAP.Add(0xca, 0);
            MAP.Add(0xcb, 0);
            MAP.Add(0xcc, 0);
            MAP.Add(0xcd, 0);
            MAP.Add(0xce, 0);
            MAP.Add(0xcf, 0);
            MAP.Add(0xd0, 0);
            MAP.Add(0xd1, 0);
            MAP.Add(0xd2, 0);
            MAP.Add(0xd3, 0);
            MAP.Add(0xd4, 0);
            MAP.Add(0xd5, 0);
            MAP.Add(0xd6, 0);
            MAP.Add(0xd7, 0);
            MAP.Add(0xd8, 0);
            MAP.Add(0xd9, 0);
            MAP.Add(0xda, 0);
            MAP.Add(0xdb, 0);
            MAP.Add(0xdc, 0);
            MAP.Add(0xdd, 0);
            MAP.Add(0xde, 0);
            MAP.Add(0xdf, 0);
            MAP.Add(0xe0, 0);
            MAP.Add(0xe1, 0);
            MAP.Add(0xe2, 0);
            MAP.Add(0xe3, 0);
            MAP.Add(0xe4, 0);
            MAP.Add(0xe5, 0);
            MAP.Add(0xe6, 0);
            MAP.Add(0xe7, 0);
            MAP.Add(0xe8, 0);
            MAP.Add(0xe9, 0);
            MAP.Add(0xea, 0);
            MAP.Add(0xeb, 0);
            MAP.Add(0xec, 0);
            MAP.Add(0xed, 0);
            MAP.Add(0xee, 0);
            MAP.Add(0xef, 0);
            MAP.Add(0xf0, 2);
            MAP.Add(0xf1, 3);
            MAP.Add(0xf2, 5);
            MAP.Add(0xf3, 9);
            MAP.Add(0xf4, 0);
            MAP.Add(0xf5, 0);
            MAP.Add(0xf6, 0);
            MAP.Add(0xf7, 0);
            MAP.Add(0xf8, 0);
            MAP.Add(0xf9, 0);
            MAP.Add(0xfa, 0);
            MAP.Add(0xfb, 0);
            MAP.Add(0xfc, 0);
            MAP.Add(0xfd, 0);
            MAP.Add(0xfe, 0);
            MAP.Add(0xff, 0);
        }

        private ValueLengthUtil()
        {
        }

        public static int Get(byte key)
        {
            return MAP[key];
        }
    }
}
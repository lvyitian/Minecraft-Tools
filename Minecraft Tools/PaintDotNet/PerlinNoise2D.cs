using System;

namespace PaintDotNet.Effects
{
	internal static class PerlinNoise2D
	{
		private static readonly double rot_11;

		private static readonly double rot_12;

		private static readonly double rot_21;

		private static readonly double rot_22;

		private static readonly int[] permuteLookup;

		private static readonly int[] permutationTable;

		static PerlinNoise2D()
		{
			permutationTable = new int[256]
			{
				151,
				160,
				137,
				91,
				90,
				15,
				131,
				13,
				201,
				95,
				96,
				53,
				194,
				233,
				7,
				225,
				140,
				36,
				103,
				30,
				69,
				142,
				8,
				99,
				37,
				240,
				21,
				10,
				23,
				190,
				6,
				148,
				247,
				120,
				234,
				75,
				0,
				26,
				197,
				62,
				94,
				252,
				219,
				203,
				117,
				35,
				11,
				32,
				57,
				177,
				33,
				88,
				237,
				149,
				56,
				87,
				174,
				20,
				125,
				136,
				171,
				168,
				68,
				175,
				74,
				165,
				71,
				134,
				139,
				48,
				27,
				166,
				77,
				146,
				158,
				231,
				83,
				111,
				229,
				122,
				60,
				211,
				133,
				230,
				220,
				105,
				92,
				41,
				55,
				46,
				245,
				40,
				244,
				102,
				143,
				54,
				65,
				25,
				63,
				161,
				1,
				216,
				80,
				73,
				209,
				76,
				132,
				187,
				208,
				89,
				18,
				169,
				200,
				196,
				135,
				130,
				116,
				188,
				159,
				86,
				164,
				100,
				109,
				198,
				173,
				186,
				3,
				64,
				52,
				217,
				226,
				250,
				124,
				123,
				5,
				202,
				38,
				147,
				118,
				126,
				255,
				82,
				85,
				212,
				207,
				206,
				59,
				227,
				47,
				16,
				58,
				17,
				182,
				189,
				28,
				42,
				223,
				183,
				170,
				213,
				119,
				248,
				152,
				2,
				44,
				154,
				163,
				70,
				221,
				153,
				101,
				155,
				167,
				43,
				172,
				9,
				129,
				22,
				39,
				253,
				19,
				98,
				108,
				110,
				79,
				113,
				224,
				232,
				178,
				185,
				112,
				104,
				218,
				246,
				97,
				228,
				251,
				34,
				242,
				193,
				238,
				210,
				144,
				12,
				191,
				179,
				162,
				241,
				81,
				51,
				145,
				235,
				249,
				14,
				239,
				107,
				49,
				192,
				214,
				31,
				181,
				199,
				106,
				157,
				184,
				84,
				204,
				176,
				115,
				121,
				50,
				45,
				127,
				4,
				150,
				254,
				138,
				236,
				205,
				93,
				222,
				114,
				67,
				29,
				24,
				72,
				243,
				141,
				128,
				195,
				78,
				66,
				215,
				61,
				156,
				180
			};
			permuteLookup = new int[512];
			for (int i = 0; i < 256; i++)
			{
				permuteLookup[256 + i] = permutationTable[i];
				permuteLookup[i] = permutationTable[i];
			}
			double num = 2.39459173373622;
			rot_11 = Math.Cos(num);
			rot_12 = 0.0 - Math.Sin(num);
			rot_21 = Math.Sin(num);
			rot_22 = Math.Cos(num);
		}

		public static double Noise(double x, double y, double detail, double roughness, byte seed)
		{
			double num = 0.0;
			double num2 = 1.0;
			double num3 = 1.0;
			double num4 = detail;
			int num5 = (int)Math.Ceiling(detail);
			for (int i = 0; i < num5; i++)
			{
				double num6 = x * rot_11 + y * rot_12;
				double num7 = x * rot_21 + y * rot_22;
				double num8 = Noise(num6 * num2, num7 * num2, seed);
				num8 *= num3;
				if (num4 < 1.0)
				{
					num8 *= num4;
				}
				num += num8;
				num3 *= roughness;
				if (num3 < 0.001)
				{
					break;
				}
				num2 += num2;
				num4 -= 1.0;
				x = num6 + 499.0;
				y = num7 + 506.0;
			}
			return num;
		}

		private static double Fade(double t)
		{
			return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
		}

		private static double Grad(int hash, double x, double y)
		{
			int num = hash & 0xF;
			double num2 = (num < 8) ? x : y;
			double num3 = (num < 4) ? y : ((num == 12 || num == 14) ? x : 0.0);
			return (((num & 1) == 0) ? num2 : (0.0 - num2)) + (((num & 2) == 0) ? num3 : (0.0 - num3));
		}

		private static double Lerp(double a, double b, double t)
		{
			return a + t * (b - a);
		}

		private static double Noise(double x, double y, byte seed)
		{
			double num = Math.Floor(x);
			double num2 = Math.Floor(y);
			int num3 = (int)num & 0xFF;
			int num4 = (int)num2 & 0xFF;
			x -= num;
			y -= num2;
			double t = Fade(x);
			double t2 = Fade(y);
			int num5 = permuteLookup[num3 + seed] + num4;
			int num6 = permuteLookup[num5];
			int num7 = permuteLookup[num5 + 1];
			int num8 = permuteLookup[num3 + 1 + seed] + num4;
			int num9 = permuteLookup[num8];
			int num10 = permuteLookup[num8 + 1];
			double a = Grad(permuteLookup[num6], x, y);
			double b = Grad(permuteLookup[num9], x - 1.0, y);
			double a2 = Lerp(a, b, t);
			double a3 = Grad(permuteLookup[num7], x, y - 1.0);
			double b2 = Grad(permuteLookup[num10], x - 1.0, y - 1.0);
			double b3 = Lerp(a3, b2, t);
			return Lerp(a2, b3, t2);
		}
	}
}

namespace Terraria
{
	public class Tile
	{
		private ushort _type;

		public ushort wall;

		public byte wallFrameX;

		public byte wallFrameY;

		public byte wallFrameNumber;

		public byte liquid;

		public byte frameNumber;

		public short frameX;

		public short frameY;

		private byte boolValues;

		public ushort type
		{
			get
			{
				if (Config.tilesPretend && Config.tileDefs.pretendType.ContainsKey(_type))
				{
					return (ushort)Config.tileDefs.pretendType[_type];
				}
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		public bool active
		{
			get
			{
				return (boolValues & 1) == 1;
			}
			set
			{
				if (value != ((boolValues & 1) == 1))
				{
					boolValues ^= 1;
				}
			}
		}

		public bool wire
		{
			get
			{
				return (boolValues & 2) == 2;
			}
			set
			{
				if (value != ((boolValues & 2) == 2))
				{
					boolValues ^= 2;
				}
			}
		}

		public bool checkingLiquid
		{
			get
			{
				return (boolValues & 4) == 4;
			}
			set
			{
				if (value != ((boolValues & 4) == 4))
				{
					boolValues ^= 4;
				}
			}
		}

		public bool skipLiquid
		{
			get
			{
				return (boolValues & 8) == 8;
			}
			set
			{
				if (value != ((boolValues & 8) == 8))
				{
					boolValues ^= 8;
				}
			}
		}

		public bool lava
		{
			get
			{
				return (boolValues & 0x10) == 16;
			}
			set
			{
				if (value != ((boolValues & 0x10) == 16))
				{
					boolValues ^= 16;
				}
			}
		}

		public override string ToString()
		{
			return "Tile " + Main.tileName[type] + " | Wall: " + wall + "(" + boolValues + ")";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool isTheSameAs(Tile compTile)
		{
			if (active != compTile.active)
			{
				return false;
			}
			if (active)
			{
				if (type != compTile.type)
				{
					return false;
				}
				if (Main.tileFrameImportant[type])
				{
					if (frameX != compTile.frameX)
					{
						return false;
					}
					if (frameY != compTile.frameY)
					{
						return false;
					}
					if (frameNumber != compTile.frameNumber)
					{
						return false;
					}
				}
			}
			if (wall == compTile.wall && liquid == compTile.liquid && lava == compTile.lava)
			{
				return wire == compTile.wire;
			}
			return false;
		}
	}
}

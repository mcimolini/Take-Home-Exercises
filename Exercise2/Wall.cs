using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoSystem
{
	public class Wall
	{
		//data members

		private string _Color;
		private int _Height;
		private string _PlanId;
		private int _Width;

		//constants

		private const int WALLMINIMUMHEIGHT = 100;
		private const int WALLMINIMUMWIDTH = 26;

		//properties

		public string Color
		{
			get
			{
				return _Color;
			}
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Wall Color is missing! Please enter a Color to proceed.");
				}

				_Color = value.Trim();
			}
		}

		public int Height
		{
			get
			{
				return _Height;
			}
			private set
			{
				if (!Utilities.IsNonZeroPositive(value))
				{
					throw new ArgumentException($"The Wall height value: {value} is invalid. Must be a positive value.");
				}

				else if (!Utilities.MeetsMinimumCriteria(value, WALLMINIMUMHEIGHT))
				{
					throw new ArgumentException($"The Wall height of {value} in too short, should be a minimum of {WALLMINIMUMHEIGHT} cm");
				}

				_Height = value;
			}
		}

		public string PlanId
		{
			get
			{
				return _PlanId;
			}
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Wall PlanId is missing! Please enter a PlanId to proceed.");
				}

				_PlanId = value.Trim();
			}
		}

		public int SurfaceArea
		{
			get
			{
				int wallopening = 0;
				if (WallOpening != null)
					wallopening = WallOpening.Area;
				return (_Width * _Height) - wallopening;
			}

		}

		public Opening WallOpening
		{
			get;
			private set;
		}

		public int Width
		{
			get
			{
				return _Width;
			}
			private set
			{
                if (!Utilities.IsNonZeroPositive(value))
                {
                    throw new ArgumentException($"The Wall width value: {value} is invalid. Must be a positive value.");
                }

                else if (!Utilities.MeetsMinimumCriteria(value, WALLMINIMUMWIDTH))
                {
                    throw new ArgumentException($"The Wall width of {value} in too narrow, should be a minimum of {WALLMINIMUMWIDTH} cm");
                }

                _Width = value;
			}
		}

		//behaviours

		public Wall(string planid, int width, int height, string color, Opening wallopening)
		{
			PlanId = planid;
			Width = width;
			Height = height;
			Color = color;
			WallOpening = wallopening;

			
			if (WallOpening != null)
			{
				ValidateOpeningToWallSurfaceRatio();
            }
		}

		private void ValidateOpeningToWallSurfaceRatio()
		{
            int WallArea = _Width * _Height;
            double MinWallArea = 0.90 * WallArea;
            if (WallOpening.Area >= MinWallArea)
            {
                throw new ArgumentException($"Opening limit exceeded: The area for the current opening is {WallOpening.Area}cm. It should be less than {MinWallArea}cm that is 90% of the wall area.");
            }
        }

        public override string ToString()
        {
            if (WallOpening == null)
            {
                return $"{PlanId},{Width},{Height},{Color}";

            }
            else
            {
                return $"{PlanId},{Width},{Height},{Color},{WallOpening.ToString()}";
            }
        }

        public void ChangeColor(string color)
		{
            //property will validate color value and throw appropriate exception if needed
            Color = color;
		}
		public void ChangeWallHeight(int height)
		{
			//property will validate height value and throw appropriate exception if needed
			Height = height;
            if (WallOpening != null)
            {
                ValidateOpeningToWallSurfaceRatio();
            }
        }

        public void ChangeWallWidth(int width)
        {
            //property will validate width value and throw appropriate exception if needed
            Width = width;
            if (WallOpening != null)
            {
                ValidateOpeningToWallSurfaceRatio();
            }
        }

    }
}

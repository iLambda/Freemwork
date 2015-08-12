using System;
using System.Text;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Graphic   
{
    public struct Color : IEquatable<Color>
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        private static readonly Color transparent = new Color(255, 255, 255, 0);
        private static readonly Color aliceBlue = new Color(240, 248, 255, 255);
        private static readonly Color antiqueWhite = new Color(250, 235, 215, 255);
        private static readonly Color aqua = new Color(0, 255, 255, 255);
        private static readonly Color aquamarine = new Color(127, 255, 212, 255);
        private static readonly Color azure = new Color(240, 255, 255, 255);
        private static readonly Color beige = new Color(245, 245, 220, 255);
        private static readonly Color bisque = new Color(255, 228, 196, 255);
        private static readonly Color black = new Color(0, 0, 0, 255);
        private static readonly Color blanchedAlmond = new Color(255, 235, 205, 255);
        private static readonly Color blue = new Color(0, 0, 255, 255);
        private static readonly Color blueViolet = new Color(138, 43, 226, 255);
        private static readonly Color brown = new Color(165, 42, 42, 255);
        private static readonly Color burlyWood = new Color(222, 184, 135, 255);
        private static readonly Color cadetBlue = new Color(95, 158, 160, 255);
        private static readonly Color chartreuse = new Color(127, 255, 0, 255);
        private static readonly Color chocolate = new Color(210, 105, 30, 255);
        private static readonly Color coral = new Color(255, 127, 80, 255);
        private static readonly Color cornflowerBlue = new Color(100, 149, 237, 255);
        private static readonly Color cornsilk = new Color(255, 248, 220, 255);
        private static readonly Color crimson = new Color(220, 20, 60, 255);
        private static readonly Color cyan = new Color(0, 255, 255, 255);
        private static readonly Color darkBlue = new Color(0, 0, 139, 255);
        private static readonly Color darkCyan = new Color(0, 139, 139, 255);
        private static readonly Color darkGoldenrod = new Color(184, 134, 11, 255);
        private static readonly Color darkGray = new Color(169, 169, 169, 255);
        private static readonly Color darkGreen = new Color(0, 100, 0, 255);
        private static readonly Color darkKhaki = new Color(189, 183, 107, 255);
        private static readonly Color darkMagenta = new Color(139, 0, 139, 255);
        private static readonly Color darkOliveGreen = new Color(85, 107, 47, 255);
        private static readonly Color darkOrange = new Color(255, 140, 0, 255);
        private static readonly Color darkOrchid = new Color(153, 50, 204, 255);
        private static readonly Color darkRed = new Color(139, 0, 0, 255);
        private static readonly Color darkSalmon = new Color(233, 150, 122, 255);
        private static readonly Color darkSeaGreen = new Color(143, 188, 139, 255);
        private static readonly Color darkSlateBlue = new Color(72, 61, 139, 255);
        private static readonly Color darkSlateGray = new Color(47, 79, 79, 255);
        private static readonly Color darkTurquoise = new Color(0, 206, 209, 255);
        private static readonly Color darkViolet = new Color(148, 0, 211, 255);
        private static readonly Color deepPink = new Color(255, 20, 147, 255);
        private static readonly Color deepSkyBlue = new Color(0, 191, 255, 255);
        private static readonly Color dimGray = new Color(105, 105, 105, 255);
        private static readonly Color dodgerBlue = new Color(30, 144, 255, 255);
        private static readonly Color firebrick = new Color(178, 34, 34, 255);
        private static readonly Color floralWhite = new Color(255, 250, 240, 255);
        private static readonly Color forestGreen = new Color(34, 139, 34, 255);
        private static readonly Color fuchsia = new Color(255, 0, 255, 255);
        private static readonly Color gainsboro = new Color(220, 220, 220, 255);
        private static readonly Color ghostWhite = new Color(248, 248, 255, 255);
        private static readonly Color gold = new Color(255, 215, 0, 255);
        private static readonly Color goldenrod = new Color(218, 165, 32, 255);
        private static readonly Color gray = new Color(128, 128, 128, 255);
        private static readonly Color green = new Color(0, 128, 0, 255);
        private static readonly Color greenYellow = new Color(173, 255, 47, 255);
        private static readonly Color honeydew = new Color(240, 255, 240, 255);
        private static readonly Color hotPink = new Color(255, 105, 180, 255);
        private static readonly Color indianRed = new Color(205, 92, 92, 255);
        private static readonly Color indigo = new Color(75, 0, 130, 255);
        private static readonly Color ivory = new Color(255, 255, 240, 255);
        private static readonly Color khaki = new Color(240, 230, 140, 255);
        private static readonly Color lavender = new Color(230, 230, 250, 255);
        private static readonly Color lavenderBlush = new Color(255, 240, 245, 255);
        private static readonly Color lawnGreen = new Color(124, 252, 0, 255);
        private static readonly Color lemonChiffon = new Color(255, 250, 205, 255);
        private static readonly Color lightBlue = new Color(173, 216, 230, 255);
        private static readonly Color lightCoral = new Color(240, 128, 128, 255);
        private static readonly Color lightCyan = new Color(224, 255, 255, 255);
        private static readonly Color lightGoldenrodYellow = new Color(250, 250, 210, 255);
        private static readonly Color lightGreen = new Color(144, 238, 144, 255);
        private static readonly Color lightGray = new Color(211, 211, 211, 255);
        private static readonly Color lightPink = new Color(255, 182, 193, 255);
        private static readonly Color lightSalmon = new Color(255, 160, 122, 255);
        private static readonly Color lightSeaGreen = new Color(32, 178, 170, 255);
        private static readonly Color lightSkyBlue = new Color(135, 206, 250, 255);
        private static readonly Color lightSlateGray = new Color(119, 136, 153, 255);
        private static readonly Color lightSteelBlue = new Color(176, 196, 222, 255);
        private static readonly Color lightYellow = new Color(255, 255, 224, 255);
        private static readonly Color lime = new Color(0, 255, 0, 255);
        private static readonly Color limeGreen = new Color(50, 205, 50, 255);
        private static readonly Color linen = new Color(250, 240, 230, 255);
        private static readonly Color magenta = new Color(255, 0, 255, 255);
        private static readonly Color maroon = new Color(128, 0, 0, 255);
        private static readonly Color mediumAquamarine = new Color(102, 205, 170, 255);
        private static readonly Color mediumBlue = new Color(0, 0, 205, 255);
        private static readonly Color mediumOrchid = new Color(186, 85, 211, 255);
        private static readonly Color mediumPurple = new Color(147, 112, 219, 255);
        private static readonly Color mediumSeaGreen = new Color(60, 179, 113, 255);
        private static readonly Color mediumSlateBlue = new Color(123, 104, 238, 255);
        private static readonly Color mediumSpringGreen = new Color(0, 250, 154, 255);
        private static readonly Color mediumTurquoise = new Color(72, 209, 204, 255);
        private static readonly Color mediumVioletRed = new Color(199, 21, 133, 255);
        private static readonly Color midnightBlue = new Color(25, 25, 112, 255);
        private static readonly Color mintCream = new Color(245, 255, 250, 255);
        private static readonly Color mistyRose = new Color(255, 228, 225, 255);
        private static readonly Color moccasin = new Color(255, 228, 181, 255);
        private static readonly Color navajoWhite = new Color(255, 222, 173, 255);
        private static readonly Color navy = new Color(0, 0, 128, 255);
        private static readonly Color oldLace = new Color(253, 245, 230, 255);
        private static readonly Color olive = new Color(128, 128, 0, 255);
        private static readonly Color oliveDrab = new Color(107, 142, 35, 255);
        private static readonly Color orange = new Color(255, 165, 0, 255);
        private static readonly Color orangeRed = new Color(255, 69, 0, 255);
        private static readonly Color orchid = new Color(218, 112, 214, 255);
        private static readonly Color paleGoldenrod = new Color(238, 232, 170, 255);
        private static readonly Color paleGreen = new Color(152, 251, 152, 255);
        private static readonly Color paleTurquoise = new Color(175, 238, 238, 255);
        private static readonly Color paleVioletRed = new Color(219, 112, 147, 255);
        private static readonly Color papayaWhip = new Color(255, 239, 213, 255);
        private static readonly Color peachPuff = new Color(255, 218, 185, 255);
        private static readonly Color peru = new Color(205, 133, 63, 255);
        private static readonly Color pink = new Color(255, 192, 203, 255);
        private static readonly Color plum = new Color(221, 160, 221, 255);
        private static readonly Color powderBlue = new Color(176, 224, 230, 255);
        private static readonly Color purple = new Color(128, 0, 128, 255);
        private static readonly Color red = new Color(255, 0, 0, 255);
        private static readonly Color rosyBrown = new Color(188, 143, 143, 255);
        private static readonly Color royalBlue = new Color(65, 105, 225, 255);
        private static readonly Color saddleBrown = new Color(139, 69, 19, 255);
        private static readonly Color salmon = new Color(250, 128, 114, 255);
        private static readonly Color sandyBrown = new Color(244, 164, 96, 255);
        private static readonly Color seaGreen = new Color(46, 139, 87, 255);
        private static readonly Color seaShell = new Color(255, 245, 238, 255);
        private static readonly Color sienna = new Color(160, 82, 45, 255);
        private static readonly Color silver = new Color(192, 192, 192, 255);
        private static readonly Color skyBlue = new Color(135, 206, 235, 255);
        private static readonly Color slateBlue = new Color(106, 90, 205, 255);
        private static readonly Color slateGray = new Color(112, 128, 144, 255);
        private static readonly Color snow = new Color(255, 250, 250, 255);
        private static readonly Color springGreen = new Color(0, 255, 127, 255);
        private static readonly Color steelBlue = new Color(70, 130, 180, 255);
        private static readonly Color tan = new Color(210, 180, 140, 255);
        private static readonly Color teal = new Color(0, 128, 128, 255);
        private static readonly Color thistle = new Color(216, 191, 216, 255);
        private static readonly Color tomato = new Color(255, 99, 71, 255);
        private static readonly Color turquoise = new Color(64, 224, 208, 255);
        private static readonly Color violet = new Color(238, 130, 238, 255);
        private static readonly Color wheat = new Color(245, 222, 179, 255);
        private static readonly Color white = new Color(255, 255, 255, 255);
        private static readonly Color whiteSmoke = new Color(245, 245, 245, 255);
        private static readonly Color yellow = new Color(255, 255, 0, 255);
        private static readonly Color yellowGreen = new Color(154, 205, 50, 255);

        public static Color Transparent { get { return transparent; } }
        public static Color AliceBlue { get { return aliceBlue; } }
        public static Color AntiqueWhite { get { return antiqueWhite; } }
        public static Color Aqua { get { return aqua; } }
        public static Color Aquamarine { get { return aquamarine; } }
        public static Color Azure { get { return azure; } }
        public static Color Beige { get { return beige; } }
        public static Color Bisque { get { return bisque; } }
        public static Color Black { get { return black; } }
        public static Color BlanchedAlmond { get { return blanchedAlmond; } }
        public static Color Blue { get { return blue; } }
        public static Color BlueViolet { get { return blueViolet; } }
        public static Color Brown { get { return brown; } }
        public static Color BurlyWood { get { return burlyWood; } }
        public static Color CadetBlue { get { return cadetBlue; } }
        public static Color Chartreuse { get { return chartreuse; } }
        public static Color Chocolate { get { return chocolate; } }
        public static Color Coral { get { return coral; } }
        public static Color CornflowerBlue { get { return cornflowerBlue; } }
        public static Color Cornsilk { get { return cornsilk; } }
        public static Color Crimson { get { return crimson; } }
        public static Color Cyan { get { return cyan; } }
        public static Color DarkBlue { get { return darkBlue; } }
        public static Color DarkCyan { get { return darkCyan; } }
        public static Color DarkGoldenrod { get { return darkGoldenrod; } }
        public static Color DarkGray { get { return darkGray; } }
        public static Color DarkGreen { get { return darkGreen; } }
        public static Color DarkKhaki { get { return darkKhaki; } }
        public static Color DarkMagenta { get { return darkMagenta; } }
        public static Color DarkOliveGreen { get { return darkOliveGreen; } }
        public static Color DarkOrange { get { return darkOrange; } }
        public static Color DarkOrchid { get { return darkOrchid; } }
        public static Color DarkRed { get { return darkRed; } }
        public static Color DarkSalmon { get { return darkSalmon; } }
        public static Color DarkSeaGreen { get { return darkSeaGreen; } }
        public static Color DarkSlateBlue { get { return darkSlateBlue; } }
        public static Color DarkSlateGray { get { return darkSlateGray; } }
        public static Color DarkTurquoise { get { return darkTurquoise; } }
        public static Color DarkViolet { get { return darkViolet; } }
        public static Color DeepPink { get { return deepPink; } }
        public static Color DeepSkyBlue { get { return deepSkyBlue; } }
        public static Color DimGray { get { return dimGray; } }
        public static Color DodgerBlue { get { return dodgerBlue; } }
        public static Color Firebrick { get { return firebrick; } }
        public static Color FloralWhite { get { return floralWhite; } }
        public static Color ForestGreen { get { return forestGreen; } }
        public static Color Fuchsia { get { return fuchsia; } }
        public static Color Gainsboro { get { return gainsboro; } }
        public static Color GhostWhite { get { return ghostWhite; } }
        public static Color Gold { get { return gold; } }
        public static Color Goldenrod { get { return goldenrod; } }
        public static Color Gray { get { return gray; } }
        public static Color Green { get { return green; } }
        public static Color GreenYellow { get { return greenYellow; } }
        public static Color Honeydew { get { return honeydew; } }
        public static Color HotPink { get { return hotPink; } }
        public static Color IndianRed { get { return indianRed; } }
        public static Color Indigo { get { return indigo; } }
        public static Color Ivory { get { return ivory; } }
        public static Color Khaki { get { return khaki; } }
        public static Color Lavender { get { return lavender; } }
        public static Color LavenderBlush { get { return lavenderBlush; } }
        public static Color LawnGreen { get { return lawnGreen; } }
        public static Color LemonChiffon { get { return lemonChiffon; } }
        public static Color LightBlue { get { return lightBlue; } }
        public static Color LightCoral { get { return lightCoral; } }
        public static Color LightCyan { get { return lightCyan; } }
        public static Color LightGoldenrodYellow { get { return lightGoldenrodYellow; } }
        public static Color LightGreen { get { return lightGreen; } }
        public static Color LightGray { get { return lightGray; } }
        public static Color LightPink { get { return lightPink; } }
        public static Color LightSalmon { get { return lightSalmon; } }
        public static Color LightSeaGreen { get { return lightSeaGreen; } }
        public static Color LightSkyBlue { get { return lightSkyBlue; } }
        public static Color LightSlateGray { get { return lightSlateGray; } }
        public static Color LightSteelBlue { get { return lightSteelBlue; } }
        public static Color LightYellow { get { return lightYellow; } }
        public static Color Lime { get { return lime; } }
        public static Color LimeGreen { get { return limeGreen; } }
        public static Color Linen { get { return linen; } }
        public static Color Magenta { get { return magenta; } }
        public static Color Maroon { get { return maroon; } }
        public static Color MediumAquamarine { get { return mediumAquamarine; } }
        public static Color MediumBlue { get { return mediumBlue; } }
        public static Color MediumOrchid { get { return mediumOrchid; } }
        public static Color MediumPurple { get { return mediumPurple; } }
        public static Color MediumSeaGreen { get { return mediumSeaGreen; } }
        public static Color MediumSlateBlue { get { return mediumSlateBlue; } }
        public static Color MediumSpringGreen { get { return mediumSpringGreen; } }
        public static Color MediumTurquoise { get { return mediumTurquoise; } }
        public static Color MediumVioletRed { get { return mediumVioletRed; } }
        public static Color MidnightBlue { get { return midnightBlue; } }
        public static Color MintCream { get { return mintCream; } }
        public static Color MistyRose { get { return mistyRose; } }
        public static Color Moccasin { get { return moccasin; } }
        public static Color NavajoWhite { get { return navajoWhite; } }
        public static Color Navy { get { return navy; } }
        public static Color OldLace { get { return oldLace; } }
        public static Color Olive { get { return olive; } }
        public static Color OliveDrab { get { return oliveDrab; } }
        public static Color Orange { get { return orange; } }
        public static Color OrangeRed { get { return orangeRed; } }
        public static Color Orchid { get { return orchid; } }
        public static Color PaleGoldenrod { get { return paleGoldenrod; } }
        public static Color PaleGreen { get { return paleGreen; } }
        public static Color PaleTurquoise { get { return paleTurquoise; } }
        public static Color PaleVioletRed { get { return paleVioletRed; } }
        public static Color PapayaWhip { get { return papayaWhip; } }
        public static Color PeachPuff { get { return peachPuff; } }
        public static Color Peru { get { return peru; } }
        public static Color Pink { get { return pink; } }
        public static Color Plum { get { return plum; } }
        public static Color PowderBlue { get { return powderBlue; } }
        public static Color Purple { get { return purple; } }
        public static Color Red { get { return red; } }
        public static Color RosyBrown { get { return rosyBrown; } }
        public static Color RoyalBlue { get { return royalBlue; } }
        public static Color SaddleBrown { get { return saddleBrown; } }
        public static Color Salmon { get { return salmon; } }
        public static Color SandyBrown { get { return sandyBrown; } }
        public static Color SeaGreen { get { return seaGreen; } }
        public static Color SeaShell { get { return seaShell; } }
        public static Color Sienna { get { return sienna; } }
        public static Color Silver { get { return silver; } }
        public static Color SkyBlue { get { return skyBlue; } }
        public static Color SlateBlue { get { return slateBlue; } }
        public static Color SlateGray { get { return slateGray; } }
        public static Color Snow { get { return snow; } }
        public static Color SpringGreen { get { return springGreen; } }
        public static Color SteelBlue { get { return steelBlue; } }
        public static Color Tan { get { return tan; } }
        public static Color Teal { get { return teal; } }
        public static Color Thistle { get { return thistle; } }
        public static Color Tomato { get { return tomato; } }
        public static Color Turquoise { get { return turquoise; } }
        public static Color Violet { get { return violet; } }
        public static Color Wheat { get { return wheat; } }
        public static Color White { get { return white; } }
        public static Color WhiteSmoke { get { return whiteSmoke; } }
        public static Color Yellow { get { return yellow; } }
        public static Color YellowGreen { get { return yellowGreen; } }

        public Color(Color Color, int Alpha)
        {
            R = Color.R;
            G = Color.G;
            B = Color.B;
            A = (byte)Maths.Clamp(Alpha, Byte.MinValue, Byte.MaxValue);
        }

        public Color(Color Color, float Alpha)
        {
            R = Color.R;
            G = Color.G;
            B = Color.B;
            A = (byte)Maths.Clamp(Alpha * 255, Byte.MinValue, Byte.MaxValue);
        }

        public Color(float R, float G, float B)
        {
            this.R = (byte)Maths.Clamp(R * 255, Byte.MinValue, Byte.MaxValue);
            this.G = (byte)Maths.Clamp(G * 255, Byte.MinValue, Byte.MaxValue);
            this.B = (byte)Maths.Clamp(B * 255, Byte.MinValue, Byte.MaxValue);
            A = 255;
        }

        public Color(int R, int G, int B)
        {
            this.R = (byte)Maths.Clamp(R, Byte.MinValue, Byte.MaxValue);
            this.G = (byte)Maths.Clamp(G, Byte.MinValue, Byte.MaxValue);
            this.B = (byte)Maths.Clamp(B, Byte.MinValue, Byte.MaxValue);
            A = 255;
        }

        public Color(float R, float G, float B, float Alpha)
        {
            this.R = (byte)Maths.Clamp(R * 255, Byte.MinValue, Byte.MaxValue);
            this.G = (byte)Maths.Clamp(G * 255, Byte.MinValue, Byte.MaxValue);
            this.B = (byte)Maths.Clamp(B * 255, Byte.MinValue, Byte.MaxValue);
            A = (byte)Maths.Clamp(Alpha * 255, Byte.MinValue, Byte.MaxValue);
        }

        public Color(int R, int G, int B, int Alpha)
        {
            this.R = (byte)Maths.Clamp(R, Byte.MinValue, Byte.MaxValue);
            this.G = (byte)Maths.Clamp(G, Byte.MinValue, Byte.MaxValue);
            this.B = (byte)Maths.Clamp(B, Byte.MinValue, Byte.MaxValue);
            A = (byte)Maths.Clamp(Alpha, Byte.MinValue, Byte.MaxValue);
        }
        
        public static bool operator ==(Color ColorA, Color ColorB)
        {
            return (ColorA.A == ColorB.A &&
                ColorA.R == ColorB.R &&
                ColorA.G == ColorB.G &&
                ColorA.B == ColorB.B);
        }
	
        public static bool operator !=(Color ColorA, Color ColorB)
        {
            return !(ColorA == ColorB);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }
	
        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            return Obj is Color && Equals((Color) Obj);
        }

        public static Color Lerp(Color Value1, Color Value2, float Amount)
        {		
            return new Color(
                Maths.Lerp(Value1.R, Value2.R, Amount),
                Maths.Lerp(Value1.G, Value2.G, Amount),
                Maths.Lerp(Value1.B, Value2.B, Amount),
                Maths.Lerp(Value1.A, Value2.A, Amount));
        }

        public static Color Multiply(Color Value, float Scale)
	    {
	        return new Color((int)(Value.R * Scale), (int)(Value.G * Scale), (int)(Value.B * Scale), (int)(Value.A * Scale));
        }
	
        public static Color operator *(Color Value, float Scale)
        {
            return new Color((int)(Value.R * Scale), (int)(Value.G * Scale), (int)(Value.B * Scale), (int)(Value.A * Scale));
        }	



	    public override string ToString ()
	    {
            var sb = new StringBuilder(25);
            sb.Append("{R:");
            sb.Append(R);
            sb.Append(" G:");
            sb.Append(G);
            sb.Append(" B:");
            sb.Append(B);
            sb.Append(" A:");
            sb.Append(A);
            sb.Append("}");
            return sb.ToString();
	    }
	    public bool Equals(Color Other)
        {
	        return R == Other.R && G == Other.G && B == Other.B && A == Other.A;
        }
    }
}

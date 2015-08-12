using System;
using System.Xml.Linq;
using Freemwork.Primitives.Math;

namespace Freemwork.Utilities
{
    public static class Tools
    {
        public static Rectangle<int>[] GenerateGrid(int X, int Y, int Width, int Height, int HorizontalNumber, int VerticalNumber)
        {
            var grid = new Rectangle<int>[HorizontalNumber * VerticalNumber];

            for (int i = 0; i < VerticalNumber; i++)
                for (int j = 0; j < HorizontalNumber; j++)
                    grid[j + i * HorizontalNumber] = new Rectangle<int>(X + j * Width, Y + i * Height, Width, Height);

            return grid;
        }

        public static Tuple<int, int>[, ,] GetTuplesFromT64(String Data)
        {
            var root = XElement.Parse(Data);

            var w = int.Parse(root.Attribute("width").Value);
            var h = int.Parse(root.Attribute("height").Value);
            var b64 = root.Element("data").Value;
            var byteArray = Convert.FromBase64String(b64);
            var intArray = new int[w * h];
            var tupleList = new Tuple<int, int>[w, h, 1];

            for (int i = 0; i != w * h; i++)
                intArray[i] = BitConverter.ToInt32(byteArray, 4 * i);

            for (int j = 0; j != h; j++)
                for (int i = 0; i != w; i++)
                    tupleList[i, j, 0] = Tuple.Create(intArray[i + j * w] >> 16, intArray[i + j * w] & 0xFFFF);

            return tupleList;
        }
    }
}

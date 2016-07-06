using System;
using System.Collections.Generic;

internal class ArabicTable
{
    private static ArabicTable arabicMapper;
    private static List<ArabicMapping> mapList = new List<ArabicMapping>();

    private ArabicTable()
    {
        mapList.Add(new ArabicMapping(0x621, 0xfe80));
        mapList.Add(new ArabicMapping(0x627, 0xfe8d));
        mapList.Add(new ArabicMapping(0x623, 0xfe83));
        mapList.Add(new ArabicMapping(0x624, 0xfe85));
        mapList.Add(new ArabicMapping(0x625, 0xfe87));
        mapList.Add(new ArabicMapping(0x649, 0xfeef));
        mapList.Add(new ArabicMapping(0x626, 0xfe89));
        mapList.Add(new ArabicMapping(0x628, 0xfe8f));
        mapList.Add(new ArabicMapping(0x62a, 0xfe95));
        mapList.Add(new ArabicMapping(0x62b, 0xfe99));
        mapList.Add(new ArabicMapping(0x62c, 0xfe9d));
        mapList.Add(new ArabicMapping(0x62d, 0xfea1));
        mapList.Add(new ArabicMapping(0x62e, 0xfea5));
        mapList.Add(new ArabicMapping(0x62f, 0xfea9));
        mapList.Add(new ArabicMapping(0x630, 0xfeab));
        mapList.Add(new ArabicMapping(0x631, 0xfead));
        mapList.Add(new ArabicMapping(0x632, 0xfeaf));
        mapList.Add(new ArabicMapping(0x633, 0xfeb1));
        mapList.Add(new ArabicMapping(0x634, 0xfeb5));
        mapList.Add(new ArabicMapping(0x635, 0xfeb9));
        mapList.Add(new ArabicMapping(0x636, 0xfebd));
        mapList.Add(new ArabicMapping(0x637, 0xfec1));
        mapList.Add(new ArabicMapping(0x638, 0xfec5));
        mapList.Add(new ArabicMapping(0x639, 0xfec9));
        mapList.Add(new ArabicMapping(0x63a, 0xfecd));
        mapList.Add(new ArabicMapping(0x641, 0xfed1));
        mapList.Add(new ArabicMapping(0x642, 0xfed5));
        mapList.Add(new ArabicMapping(0x643, 0xfed9));
        mapList.Add(new ArabicMapping(0x644, 0xfedd));
        mapList.Add(new ArabicMapping(0x645, 0xfee1));
        mapList.Add(new ArabicMapping(0x646, 0xfee5));
        mapList.Add(new ArabicMapping(0x647, 0xfee9));
        mapList.Add(new ArabicMapping(0x648, 0xfeed));
        mapList.Add(new ArabicMapping(0x64a, 0xfef1));
        mapList.Add(new ArabicMapping(0x622, 0xfe81));
        mapList.Add(new ArabicMapping(0x629, 0xfe93));
        mapList.Add(new ArabicMapping(0x67e, 0xfb56));
        mapList.Add(new ArabicMapping(0x686, 0xfb7a));
        mapList.Add(new ArabicMapping(0x698, 0xfb8a));
        mapList.Add(new ArabicMapping(0x6af, 0xfb92));
    }

    internal int Convert(int toBeConverted)
    {
        foreach (ArabicMapping mapping in mapList)
        {
            if (mapping.from == toBeConverted)
            {
                return mapping.to;
            }
        }
        return toBeConverted;
    }

    internal static ArabicTable ArabicMapper
    {
        get
        {
            if (arabicMapper == null)
            {
                arabicMapper = new ArabicTable();
            }
            return arabicMapper;
        }
    }
}


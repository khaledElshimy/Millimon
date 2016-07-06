
    using System;

    public class ArabicFixer
    {
    public static string Fix(string str)
    {
        return Fix(str, false, true);
    }
        public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
        {
            ArabicFixerTool.showTashkeel = showTashkeel;
            ArabicFixerTool.useHinduNumbers = useHinduNumbers;
            if (str.Contains("\n"))
            {
                str = str.Replace("\n", Environment.NewLine);
            }
            if (str.Contains(Environment.NewLine))
            {
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray2 = str.Split(separator, StringSplitOptions.None);
                if (strArray2.Length == 0)
                {
                    return ArabicFixerTool.FixLine(str);
                }
                if (strArray2.Length == 1)
                {
                    return ArabicFixerTool.FixLine(str);
                }
                string str2 = ArabicFixerTool.FixLine(strArray2[0]);
                int index = 1;
                if (strArray2.Length > 1)
                {
                    while (index < strArray2.Length)
                    {
                        str2 = str2 + Environment.NewLine + ArabicFixerTool.FixLine(strArray2[index]);
                        index++;
                    }
                }
                return str2;
            }
            return ArabicFixerTool.FixLine(str);
        }
    }



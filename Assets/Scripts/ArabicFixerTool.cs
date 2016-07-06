using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

internal class ArabicFixerTool
{
    internal static bool showTashkeel = true;
    internal static bool useHinduNumbers = false;
    internal static string FixLine(string str)
    {
        List<TashkeelLocation> list;
        int num2;
        int num3;
        string str2 = "";
        string str3 = RemoveTashkeel(str, out list);
        char[] letters = str3.ToCharArray();
        char[] chArray2 = str3.ToCharArray();
        int num = 0;
        for (num2 = 0; num2 < letters.Length; num2++)
        {
            letters[num2] = (char)ArabicTable.ArabicMapper.Convert(letters[num2]);
        }
        for (num2 = 0; num2 < letters.Length; num2++)
        {
            bool flag = false;
            if (num2 == 2)
            {
                num = 0;
            }
            if (letters[num2] == 0xdf21)
            {
                num = num;
            }
            if ((letters[num2] == 0xfedd) && (num2 < (letters.Length - 1)))
            {
                if (letters[num2 + 1] == 0xfe87)
                {
                    letters[num2] = (char)ArabicTable.ArabicMapper.Convert(0xfef7);
                    chArray2[num2 + 1] = (char)ArabicTable.ArabicMapper.Convert(0xffff);
                    flag = true;
                }
                else if (letters[num2 + 1] == 0xfe8d)
                {
                    letters[num2] = (char)ArabicTable.ArabicMapper.Convert(0xfef9);
                    chArray2[num2 + 1] = (char)ArabicTable.ArabicMapper.Convert(0xffff);
                    flag = true;
                }
                else if (letters[num2 + 1] == 0xfe83)
                {
                    letters[num2] = (char)ArabicTable.ArabicMapper.Convert(0xfef5);
                    chArray2[num2 + 1] = (char)ArabicTable.ArabicMapper.Convert(0xffff);
                    flag = true;
                }
                else if (letters[num2 + 1] == 0xfe81)
                {
                    letters[num2] = (char)ArabicTable.ArabicMapper.Convert(0xfef3);
                    chArray2[num2 + 1] = (char)ArabicTable.ArabicMapper.Convert(0xffff);
                    flag = true;
                }
            }
            if ((!IsIgnoredCharacter(letters[num2]) && (letters[num2] != 'A')) && (letters[num2] != 'A'))
            {
                if (IsMiddleLetter(letters, num2))
                {
                    chArray2[num2] = (char)(letters[num2] + '\x0003');
                }
                else if (IsFinishingLetter(letters, num2))
                {
                    chArray2[num2] = (char)(letters[num2] + '\x0001');
                }
                else if (IsLeadingLetter(letters, num2))
                {
                    chArray2[num2] = (char)(letters[num2] + '\x0002');
                }
            }
            str2 = str2 + Convert.ToString((int)letters[num2], 0x10) + " ";
            if (flag)
            {
                num2++;
            }
            if (useHinduNumbers)
            {
                if (letters[num2] == '0')
                {
                    chArray2[num2] = '٠';
                }
                else if (letters[num2] == '1')
                {
                    chArray2[num2] = '١';
                }
                else if (letters[num2] == '2')
                {
                    chArray2[num2] = '٢';
                }
                else if (letters[num2] == '3')
                {
                    chArray2[num2] = '٣';
                }
                else if (letters[num2] == '4')
                {
                    chArray2[num2] = '٤';
                }
                else if (letters[num2] == '5')
                {
                    chArray2[num2] = '٥';
                }
                else if (letters[num2] == '6')
                {
                    chArray2[num2] = '٦';
                }
                else if (letters[num2] == '7')
                {
                    chArray2[num2] = '٧';
                }
                else if (letters[num2] == '8')
                {
                    chArray2[num2] = '٨';
                }
                else if (letters[num2] == '9')
                {
                    chArray2[num2] = '٩';
                }
            }
        }
        if (showTashkeel)
        {
            chArray2 = ReturnTashkeel(chArray2, list);
        }
        List<char> list2 = new List<char>();
        List<char> list3 = new List<char>();
        for (num2 = chArray2.Length - 1; num2 >= 0; num2--)
        {
            if (((char.IsPunctuation(chArray2[num2]) && (num2 > 0)) && (num2 < (chArray2.Length - 1))) && (char.IsPunctuation(chArray2[num2 - 1]) || char.IsPunctuation(chArray2[num2 + 1])))
            {
                if (chArray2[num2] == '(')
                {
                    list2.Add(')');
                }
                else if (chArray2[num2] == ')')
                {
                    list2.Add('(');
                }
                else if (chArray2[num2] == '<')
                {
                    list2.Add('>');
                }
                else if (chArray2[num2] == '>')
                {
                    list2.Add('<');
                }
                else if (chArray2[num2] != 0xffff)
                {
                    list2.Add(chArray2[num2]);
                }
            }
            else if (((((chArray2[num2] == ' ') && (num2 > 0)) && (num2 < (chArray2.Length - 1))) && ((char.IsLower(chArray2[num2 - 1]) || char.IsUpper(chArray2[num2 - 1])) || char.IsNumber(chArray2[num2 - 1]))) && ((char.IsLower(chArray2[num2 + 1]) || char.IsUpper(chArray2[num2 + 1])) || char.IsNumber(chArray2[num2 + 1])))
            {
                list3.Add(chArray2[num2]);
            }
            else if (((char.IsNumber(chArray2[num2]) || char.IsLower(chArray2[num2])) || (char.IsUpper(chArray2[num2]) || char.IsSymbol(chArray2[num2]))) || char.IsPunctuation(chArray2[num2]))
            {
                if (chArray2[num2] == '(')
                {
                    list3.Add(')');
                }
                else if (chArray2[num2] == ')')
                {
                    list3.Add('(');
                }
                else if (chArray2[num2] == '<')
                {
                    list3.Add('>');
                }
                else if (chArray2[num2] == '>')
                {
                    list3.Add('<');
                }
                else
                {
                    list3.Add(chArray2[num2]);
                }
            }
            else if (((chArray2[num2] >= 0xd800) && (chArray2[num2] <= 0xdbff)) || ((chArray2[num2] >= 0xdc00) && (chArray2[num2] <= 0xdfff)))
            {
                list3.Add(chArray2[num2]);
            }
            else
            {
                if (list3.Count > 0)
                {
                    num3 = 0;
                    while (num3 < list3.Count)
                    {
                        list2.Add(list3[(list3.Count - 1) - num3]);
                        num3++;
                    }
                    list3.Clear();
                }
                if (chArray2[num2] != 0xffff)
                {
                    list2.Add(chArray2[num2]);
                }
            }
        }
        if (list3.Count > 0)
        {
            for (num3 = 0; num3 < list3.Count; num3++)
            {
                list2.Add(list3[(list3.Count - 1) - num3]);
            }
            list3.Clear();
        }
        chArray2 = new char[list2.Count];
        for (num2 = 0; num2 < chArray2.Length; num2++)
        {
            chArray2[num2] = list2[num2];
        }
        str = new string(chArray2);
        return str;
    }

    internal static bool IsFinishingLetter(char[] letters, int index) {
        return (((((((index != 0) && (letters[index - 1] != ' ')) && ((letters[index - 1] != '*') && (letters[index - 1] != 'A'))) && (((letters[index - 1] != 0xfea9) && (letters[index - 1] != 0xfeab)) && ((letters[index - 1] != 0xfead) && (letters[index - 1] != 0xfeaf)))) && ((((letters[index - 1] != 0xfb8a) && (letters[index - 1] != 0xfeef)) && ((letters[index - 1] != 0xfeed) && (letters[index - 1] != 0xfe8d))) && (((letters[index - 1] != 0xfe81) && (letters[index - 1] != 0xfe83)) && ((letters[index - 1] != 0xfe87) && (letters[index - 1] != 0xfe85))))) && ((((letters[index - 1] != 0xfe80) && !char.IsPunctuation(letters[index - 1])) && ((letters[index - 1] != '>') && (letters[index - 1] != '<'))) && ((letters[index] != ' ') && (index < letters.Length)))) && (letters[index] != 0xfe80));
    }
    internal static bool IsIgnoredCharacter(char ch)
    {
        bool flag = char.IsPunctuation(ch);
        bool flag2 = char.IsNumber(ch);
        bool flag3 = char.IsLower(ch);
        bool flag4 = char.IsUpper(ch);
        bool flag5 = char.IsSymbol(ch);
        bool flag6 = (((ch == 0xfb56) || (ch == 0xfb7a)) || (ch == 0xfb8a)) || (ch == 0xfb92);
        bool flag8 = ((ch <= 0xfeff) && (ch >= 0xfe70)) || flag6;
        return (((((flag || flag2) || (flag3 || flag4)) || ((flag5 || !flag8) || ((ch == 'a') || (ch == '>')))) || (ch == '<')) || (ch == '؛'));
    }

    internal static bool IsLeadingLetter(char[] letters, int index) { 
     return   (((((((index == 0) || (letters[index - 1] == ' ')) || ((letters[index - 1] == '*') || (letters[index - 1] == 'A'))) || ((char.IsPunctuation(letters[index - 1]) || (letters[index - 1] == '>')) || ((letters[index - 1] == '<') || (letters[index - 1] == 0xfe8d)))) || ((((letters[index - 1] == 0xfea9) || (letters[index - 1] == 0xfeab)) || ((letters[index - 1] == 0xfead) || (letters[index - 1] == 0xfeaf))) || ((((letters[index - 1] == 0xfb8a) || (letters[index - 1] == 0xfeef)) || ((letters[index - 1] == 0xfeed) || (letters[index - 1] == 0xfe81))) || (((letters[index - 1] == 0xfe83) || (letters[index - 1] == 0xfe87)) || (letters[index - 1] == 0xfe85))))) && (((((letters[index] != ' ') && (letters[index] != 0xfea9)) && ((letters[index] != 0xfeab) && (letters[index] != 0xfead))) && (((letters[index] != 0xfeaf) && (letters[index] != 0xfb8a)) && ((letters[index] != 0xfe8d) && (letters[index] != 0xfe83)))) && ((((letters[index] != 0xfe87) && (letters[index] != 0xfeed)) && ((letters[index] != 0xfe80) && (index< (letters.Length - 1)))) && ((letters[index + 1] != ' ') && !char.IsPunctuation(letters[index + 1]))))) && (letters[index + 1] != 0xfe80));
}
    internal static bool IsMiddleLetter(char[] letters, int index)
    {
        if ((((((((index != 0) && (letters[index] != ' ')) && ((letters[index] != 0xfe8d) && (letters[index] != 0xfea9))) && (((letters[index] != 0xfeab) && (letters[index] != 0xfead)) && ((letters[index] != 0xfeaf) && (letters[index] != 0xfb8a)))) && ((((letters[index] != 0xfeef) && (letters[index] != 0xfeed)) && ((letters[index] != 0xfe81) && (letters[index] != 0xfe83))) && (((letters[index] != 0xfe87) && (letters[index] != 0xfe85)) && ((letters[index] != 0xfe80) && (letters[index - 1] != 0xfe8d))))) && (((((letters[index - 1] != 0xfea9) && (letters[index - 1] != 0xfeab)) && ((letters[index - 1] != 0xfead) && (letters[index - 1] != 0xfeaf))) && (((letters[index - 1] != 0xfb8a) && (letters[index - 1] != 0xfeef)) && ((letters[index - 1] != 0xfeed) && (letters[index - 1] != 0xfe81)))) && ((((letters[index - 1] != 0xfe83) && (letters[index - 1] != 0xfe87)) && ((letters[index - 1] != 0xfe85) && (letters[index - 1] != 0xfe80))) && (((letters[index - 1] != '>') && (letters[index - 1] != '<')) && ((letters[index - 1] != ' ') && (letters[index - 1] != '*')))))) && (((!char.IsPunctuation(letters[index - 1]) && (index < (letters.Length - 1))) && ((letters[index + 1] != ' ') && (letters[index + 1] != '\r'))) && (((letters[index + 1] != 'A') && (letters[index + 1] != '>')) && (letters[index + 1] != '>')))) && (letters[index + 1] != 0xfe80))
        {
            try
            {
                if (char.IsPunctuation(letters[index + 1]))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    internal static string RemoveTashkeel(string str, out List<TashkeelLocation> tashkeelLocation)
    {
        tashkeelLocation = new List<TashkeelLocation>();
        char[] chArray = str.ToCharArray();
        for (int i = 0; i < chArray.Length; i++)
        {
            if (chArray[i] == 'ً')
            {
                tashkeelLocation.Add(new TashkeelLocation('ً', i));
            }
            else if (chArray[i] == 'ٌ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ٌ', i));
            }
            else if (chArray[i] == 'ٍ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ٍ', i));
            }
            else if (chArray[i] == 'َ')
            {
                tashkeelLocation.Add(new TashkeelLocation('َ', i));
            }
            else if (chArray[i] == 'ُ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ُ', i));
            }
            else if (chArray[i] == 'ِ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ِ', i));
            }
            else if (chArray[i] == 'ّ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ّ', i));
            }
            else if (chArray[i] == 'ْ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ْ', i));
            }
            else if (chArray[i] == 'ٓ')
            {
                tashkeelLocation.Add(new TashkeelLocation('ٓ', i));
            }
        }
        string[] strArray = str.Split(new char[] { 'ً', 'ٌ', 'ٍ', 'َ', 'ُ', 'ِ', 'ّ', 'ْ', 'ٓ' });
        str = "";
        foreach (string str2 in strArray)
        {
            str = str + str2;
        }
        return str;
    }

    internal static char[] ReturnTashkeel(char[] letters, List<TashkeelLocation> tashkeelLocation)
    {
        char[] chArray = new char[letters.Length + tashkeelLocation.Count];
        int index = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            chArray[index] = letters[i];
            index++;
            foreach (TashkeelLocation location in tashkeelLocation)
            {
                if (location.position == index)
                {
                    chArray[index] = location.tashkeel;
                    index++;
                }
            }
        }
        return chArray;
    }
}


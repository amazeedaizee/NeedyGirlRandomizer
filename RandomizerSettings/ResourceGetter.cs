using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RandomSettings
{
    public enum RandomType
    {
        None,
        Stats,
        LoseFollowChance,
        JineText,
        SearchStText,
        Tweets,
        TweetReplies,
        StreamText,
        NotepadText,
        EndMsgText,
        SpeBorders,
        DayBorders,
        Effects,
        Music,
        SoundFX,
        Days,
        Streams,
        IncludeSpecial,
        Animations,
        IncludeBoth
    }
    internal class ResourceGetter
    {
        public static string GetString(string parentId, RandomType type)
        {
            int langIndex = 0;
            string currentLang = CultureInfo.CurrentCulture.Name.ToUpper();
            List<string>? readData;
            using (StringReader streamReader = new StringReader(Settings.Properties.Resources.TitlesDescript))
            {
                while (streamReader.Peek() != -1)
                {
                    if (langIndex == 0)
                    {
                        readData = GetListString(streamReader);
                        if (readData == null)
                        {
                            return "";
                        }
                        for (int i = 0; i < readData.Count && langIndex == 0; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(readData[i]) && currentLang.Contains(readData[i].Trim()))
                            {
                                langIndex = i;
                            }
                        }
                        if (langIndex == 0)
                        {
                            langIndex = 2;
                        }
                    }
                    else
                    {
                        readData = GetListString(streamReader);
                        if (readData == null)
                        {
                            return "";
                        }
                        if (readData[0] == parentId && readData[1] == type.ToString())
                        {
                            try
                            {
                                return readData[langIndex];
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return readData[2];
                            }
                        }
                    }
                }
            }
            return "";
        }

        static List<string>? GetListString(StringReader stringReader)
        {
            bool isQuoteString = false;
            int tempChar;
            string tempString = "";
            List<string> strList = new List<string>();

            while (stringReader.Peek() != -1)
            {
                tempChar = stringReader.Read();
                if (tempChar == '\"' && isQuoteString == true)
                {
                    if (stringReader.Peek() == '\"')
                    {
                        tempChar = stringReader.Read();
                        tempString += (char)tempChar;
                    }
                    else
                    {
                        isQuoteString = false;
                    }
                }
                else if (tempChar == '\"')
                {
                    isQuoteString = true;

                }
                else if (tempChar == ',' && isQuoteString == false)
                {
                    strList.Add(tempString);
                    tempString = "";
                }
                else if (tempChar == '\n' && isQuoteString == false)
                {
                    strList.Add(tempString);
                    return strList;
                }
                else if (tempChar != '\r')
                {
                    tempString += (char)tempChar;
                }
            }
            return null;
        }
    }
}

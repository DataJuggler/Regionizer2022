using System;
using System.Collections.Generic;
using System.Text;
using XmlMirror.Runtime.Objects;
using DataJuggler.Core.UltimateHelper.Objects;
using DataJuggler.Core.UltimateHelper;

namespace DataJuggler.Regionizer.Parsers
{
    public static class GridColumnAttributeReader
    {

        #region ParseAttributesFromLines
        /// <summary>
        /// Flattens TextLine objects and parses GridColumn attributes into List<FieldValuePair>.
        /// Uses ListHelper.HasOneOrMoreItems(lines); single exit point.
        /// </summary>
        public static List<FieldValuePair> ParseAttributesFromLines(List<TextLine> lines)
        {
            // initial value
            List<FieldValuePair> pairs = new List<FieldValuePair>();

            // only proceed if we have lines
            if (ListHelper.HasOneOrMoreItems(lines))
            {
                // 1) Flatten
                StringBuilder sb = new StringBuilder();
                int count = lines.Count;

                for (int i = 0; i < count; i++)
                {
                    string piece = lines[i].Text.Trim();

                    if (i > 0)
                    {
                        sb.Append(' ');
                    }

                    sb.Append(piece);
                }

                string flattened = sb.ToString();

                // 2) Strip the tag wrapper if present
                string attrs = ExtractAttributeSlice(flattened);

                // 3) Parse the attributes
                List<FieldValuePair> parsed = ParseAttributesCore(attrs);

                // 4) Append results
                if (ListHelper.HasOneOrMoreItems(parsed))
                {
                    pairs.AddRange(parsed);
                }
            }

            // return value
            return pairs;
        }
        #endregion

        #region ExtractAttributeSlice
        /// <summary>
        /// Removes the leading &lt;GridColumn and trailing /&gt; or &gt;...&lt;/GridColumn&gt; if present.
        /// Returns only the attribute text; single exit point.
        /// </summary>
        private static string ExtractAttributeSlice(string input)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(input))
            {
                string text = input.Trim();
                string candidate = text;

                int tagStart = text.IndexOf("<GridColumn", StringComparison.OrdinalIgnoreCase);
                if (tagStart >= 0)
                {
                    int afterName = tagStart + "<GridColumn".Length;
                    int closeAngle = text.IndexOf('>', afterName);

                    if (closeAngle >= 0)
                    {
                        int searchLen = closeAngle - afterName + 1;
                        if (searchLen < 0)
                        {
                            searchLen = 0;
                        }

                        int selfClose = text.LastIndexOf("/>", closeAngle, searchLen, StringComparison.Ordinal);
                        bool isSelfClosed = (selfClose >= afterName);

                        if (isSelfClosed)
                        {
                            result = text.Substring(afterName, selfClose - afterName).Trim();
                        }
                        else
                        {
                            result = text.Substring(afterName, closeAngle - afterName).Trim();
                        }
                    }
                }
                else
                {
                    result = text;
                }
            }

            return result;
        }
        #endregion

        #region ParseAttributesCore
        /// <summary>
        /// Reads name=value where value may be "quoted", 'quoted', or unquoted (stops at whitespace or '/' or '>').
        /// Produces List<FieldValuePair> with FieldName / FieldValue (kept as string).
        /// </summary>
        private static List<FieldValuePair> ParseAttributesCore(string attributes)
        {
            List<FieldValuePair> list = new List<FieldValuePair>();

            if (!string.IsNullOrEmpty(attributes))
            {
                int i = 0;
                int n = attributes.Length;

                while (i < n)
                {
                    // skip whitespace
                    while (i < n && char.IsWhiteSpace(attributes[i]))
                    {
                        i++;
                    }

                    if (i >= n)
                    {
                        break;
                    }

                    // read name
                    int nameStart = i;
                    while (i < n)
                    {
                        char c = attributes[i];
                        if (char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == ':' || c == '.')
                        {
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    string name = attributes.Substring(nameStart, i - nameStart);

                    // skip whitespace
                    while (i < n && char.IsWhiteSpace(attributes[i]))
                    {
                        i++;
                    }

                    if (i >= n || attributes[i] != '=')
                    {
                        // boolean-style attribute
                        FieldValuePair flag = new FieldValuePair();
                        flag.FieldName = name;
                        flag.FieldValue = "true";
                        list.Add(flag);
                        continue;
                    }

                    i++; // skip '='

                    // skip whitespace
                    while (i < n && char.IsWhiteSpace(attributes[i]))
                    {
                        i++;
                    }

                    string value = string.Empty;

                    if (i < n)
                    {
                        char ch = attributes[i];
                        if (ch == '"' || ch == '\'')
                        {
                            char quote = ch;
                            i++;
                            int valStart = i;

                            while (i < n && attributes[i] != quote)
                            {
                                i++;
                            }

                            value = attributes.Substring(valStart, i - valStart);

                            if (i < n && attributes[i] == quote)
                            {
                                i++;
                            }
                        }
                        else
                        {
                            int valStart2 = i;
                            while (i < n && !char.IsWhiteSpace(attributes[i]) && attributes[i] != '/' && attributes[i] != '>')
                            {
                                i++;
                            }

                            value = attributes.Substring(valStart2, i - valStart2);
                        }
                    }

                    FieldValuePair pair = new FieldValuePair();
                    pair.FieldName = name;
                    pair.FieldValue = value;
                    list.Add(pair);
                }
            }

            return list;
        }
        #endregion

    }
}
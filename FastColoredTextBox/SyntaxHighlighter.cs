
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

// RegEx improved for use in RedJ Code.

namespace FastColoredTextBoxNS
{
    public class SyntaxHighlighter : IDisposable
    {
        //styles
        //protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        public readonly Style BlueBoldStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public readonly Style BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        public readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public readonly Style BrownStyle2 = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public readonly Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        public readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        public readonly Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public readonly Style NoStyle = new TextStyle(null, null, FontStyle.Regular);
        public readonly Style SlateGrayStyle = new TextStyle(Brushes.DarkSlateGray, null, FontStyle.Regular);
        public readonly Style CyanStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Regular);
        public readonly Style VioletStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        public readonly Style DarkBlueStyle = new TextStyle(Brushes.DarkBlue, null, FontStyle.Regular);
        public readonly Style OrangeStyle = new TextStyle(Brushes.OrangeRed, null, FontStyle.Regular);
        public readonly Style DarkBlueBoldStyle = new TextStyle(Brushes.DarkBlue, null, FontStyle.Bold | FontStyle.Italic);
        public readonly Style RedBoldStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold | FontStyle.Italic);
        public readonly Style DarkGreenStyle = new TextStyle(Brushes.DarkGreen, null, FontStyle.Bold);
        public readonly Style SeaStyle = new TextStyle(Brushes.SeaGreen, null, FontStyle.Regular);
        //
        protected readonly Dictionary<string, SyntaxDescriptor> descByXMLfileNames =
            new Dictionary<string, SyntaxDescriptor>();

        protected readonly List<Style> resilientStyles = new List<Style>(5);

        protected Regex CSharpAttributeRegex,
                        CSharpClassNameRegex;

        protected Regex CSharpCommentRegex1,
                        CSharpCommentRegex2,
                        CSharpCommentRegex3,
                        CSharpCommentRegex4;

        protected Regex CSharpKeywordRegex;
        protected Regex CSharpNumberRegex;
        protected Regex CSharpStringRegex;
        protected Regex CSharpPreprocessorRegex; 

        protected Regex CSharpInlineDocumentRegex,
                        CSharpInlineDocumentTagRegex;

        protected Regex CSharpInterpolatedStringRegex,
                        CSharpInterpolationExpressionRegex;

        protected Regex HTMLAttrRegex,
                        HTMLAttrRegex2,
                        HTMLAttrValRegex,
                        HTMLAttrValRegex2,
                        HTMLCommentRegex1,
                        HTMLCommentRegex2;

        protected Regex HTMLEndTagRegex;

        protected Regex HTMLEntityRegex,
                        HTMLTagContentRegex;

        protected Regex HTMLTagNameRegex;
        protected Regex HTMLTagRegex;

        protected string[] HTMLFoldingBlockRegexTags = new string[]
            {
                "html",
                "head",
                "body",
                "div",
                "header",
                "nav",
                "main",
                "footer",
                "atricle",
                "table",
                "thead",
                "tbody",
                "tfoot",
                "tr",
                "th",
                "td",
                "ul",
                "ol",
                "li",
                "form",
                "fieldset",
                "select",
                "optgroup",
                "picture",
                "audio",
                "video",
                "applet",
                "dir",
                "style",
                "script",
                "p",
                "blockquote"
            };
        protected Regex[,] HTMLFoldingBlockRegex;

        protected Regex HTMLEmbeddedJSRegex;
        protected Regex HTMLEmbeddedCSSRegex;
        protected Regex HTMLEmbeddedPHPRegex;

        protected Regex XMLAttrRegex,
                        XMLAttrValRegex,
                        XMLCommentRegex1,
                        XMLCommentRegex2;

        protected Regex XMLEndTagRegex;

        protected Regex XMLEntityRegex,
                        XMLTagContentRegex;

        protected Regex XMLTagNameRegex;
        protected Regex XMLTagRegex;
        protected Regex XMLCDataRegex;
        protected Regex XMLFoldingRegex;

        protected Regex JScriptCommentRegex1,
                        JScriptCommentRegex2,
                        JScriptCommentRegex3;

        protected Regex JScriptKeywordRegex,
                        JScriptKeywordRegex2;
        protected Regex JScriptNumberRegex;
        protected Regex JScriptStringRegex,
                        JScriptStringRegex2;
        protected Regex JScriptJSONValueRegex;

        protected Regex JScriptEmbeddedHTMLRegex;
        protected Regex JScriptEmbeddedValueRegex;

        protected Regex JSONKeywordRegex,
                        JSONKeywordRegex2;
        protected Regex JSONNumberRegex;
        protected Regex JSONStringRegex;
        protected Regex JSONValueRegex;

        protected Regex LuaCommentRegex1,
                        LuaCommentRegex2,
                        LuaCommentRegex3;

        protected Regex LuaKeywordRegex;
        protected Regex LuaNumberRegex;
        protected Regex LuaStringRegex;
        protected Regex LuaFunctionsRegex;

        protected Regex PHPCommentRegex1,
                        PHPCommentRegex2,
                        PHPCommentRegex3;

        protected Regex PHPKeywordRegex1,
                        PHPKeywordRegex2,
                        PHPKeywordRegex3;

        protected Regex PHPNumberRegex;
        protected Regex PHPStringRegex;
        protected Regex PHPVarRegex;

        protected Regex SQLCommentRegex1,
                        SQLCommentRegex2,
                        SQLCommentRegex3, 
                        SQLCommentRegex4;
        protected Regex SQLStringRegex;
        protected Regex SQLNumberRegex;

        protected Regex SQLServerFunctionsRegex;
        protected Regex SQLServerKeywordsRegex;
        protected Regex SQLServerStatementsRegex;
        protected Regex SQLServerTypesRegex;
        protected Regex SQLServerVarRegex;

        protected Regex MySQLFunctionsRegex;
        protected Regex MySQLKeywordsRegex;
        protected Regex MySQLTypesRegex;

        protected Regex VBClassNameRegex;
        protected Regex VBCommentRegex;
        protected Regex VBKeywordRegex;
        protected Regex VBNumberRegex;
        protected Regex VBStringRegex;
        protected Regex VBPreprocessorRegex;
        protected Regex VBInlineDocumentRegex;
        protected Regex VBInlineDocumentTagRegex;

        protected Regex BatchKeywordRegex;
        protected Regex BatchCommentRegex;
        protected Regex BatchLabelRegex;
        protected Regex BatchEnvironmentVariableRegex;
        protected Regex BatchStringRegex;

        protected Regex JavaCommentRegex;
        protected Regex JavaCommentRegex2;
        protected Regex JavaStringRegex;
        protected Regex JavaKeywordRegex;
        protected Regex JavaNumberRegex;
        protected Regex JavaClassNameRegex;
        protected Regex JavaAnnotationsRegex;
        protected Regex JavaInlineDocumentRegex;
        protected Regex JavaInlineDocumentTagRegex;

        protected Regex PythonCommentRegex;
        protected Regex PythonStringRegex;
        protected Regex PythonStringRegex2;
        protected Regex PythonKeywordRegex;
        protected Regex PythonNumberRegex;
        protected Regex PythonClassNameRegex;
        protected Regex PythonFunctionsRegex;
        protected Regex PythonFunctionsRegex2;

        protected char[] DefaultAutoCompleteBracketsList =  new char[] { '{', '}', '(', ')', '[', ']', '"', '"', '\'', '\'' };
        protected char[] MarkupAutoCompleteBracketsList =   new char[] { '<', '>', '"', '"', '\'', '\'', '&', ';' };
        protected char[] VBAutoCompleteBracketsList =       new char[] { '{', '}', '(', ')', '[', ']', '"', '"' };
        protected char[] JSAutoCompleteBracketsList =       new char[] { '{', '}', '(', ')', '[', ']', '"', '"', '\'', '\'', '`', '`' };
        protected char[] JSONAutoCompleteBracketsList =     new char[] { '{', '}', '[', ']', '"', '"' };
        protected char[] SQLAutoCompleteBracketsList =      new char[] { '(', ')', '"', '"', '\'', '\'' };
        protected char[] BatchAutoCompleteBracketsList =    new char[] { '(', ')', '%', '%', '"', '"' };

        protected FastColoredTextBox currentTb;

        public static RegexOptions RegexCompiledOption
            = RegexOptions.Compiled;
        //{
        //    get
        //    {
        //        if (platformType == Platform.X86)
        //            return RegexOptions.Compiled;
        //        else
        //            return RegexOptions.None;
        //    }
        //}
        
        public SyntaxHighlighter(FastColoredTextBox currentTb) {
            this.currentTb = currentTb;
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (SyntaxDescriptor desc in descByXMLfileNames.Values)
                desc.Dispose();
        }

        #endregion

        /// <summary>
        /// Highlights syntax for given language
        /// </summary>
        public virtual void HighlightSyntax(Language language, Range range)
        {
            switch (language)
            {
                case Language.CSharp:
                    CSharpSyntaxHighlight(range);
                    break;
                case Language.VB:
                    VBSyntaxHighlight(range);
                    break;
                case Language.HTML:
                    HTMLSyntaxHighlight(range);
                    break;
                case Language.XML:
                    XMLSyntaxHighlight(range);
                    break;
                case Language.SQLServer:
                    SQLSyntaxHighlight(range);
                    break;
                case Language.PHP:
                    PHPSyntaxHighlight(range);
                    break;
                case Language.JS:
                    JScriptSyntaxHighlight(range);
                    break;
                case Language.Lua:
                    LuaSyntaxHighlight(range);
                    break;
                case Language.JSON:
                    JSONSyntaxHighlight(range);
                    break;
                case Language.PlainText:
                    PlainTextSyntaxHighlight(range);
                    break;
                case Language.MySQL:
                    MySQLSyntaxHighlight(range);
                    break;
                case Language.Batch:
                    BatchSyntaxHighlight(range);
                    break;
                case Language.Java:
                    JavaSyntaxHighlight(range);
                    break;
                case Language.Python:
                    PythonSyntaxHighlight(range);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Highlights syntax for given XML description file
        /// </summary>
        public virtual void HighlightSyntax(string XMLdescriptionFile, Range range)
        {
            SyntaxDescriptor desc = null;
            if (!descByXMLfileNames.TryGetValue(XMLdescriptionFile, out desc))
            {
                var doc = new XmlDocument();
                string file = XMLdescriptionFile;
                if (!File.Exists(file))
                    file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(file));

                doc.LoadXml(File.ReadAllText(file));
                desc = ParseXmlDescription(doc);
                descByXMLfileNames[XMLdescriptionFile] = desc;
            }

            HighlightSyntax(desc, range);
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = (sender as FastColoredTextBox);
            Language language = tb.Language;
            AutoIndentNeeded(sender, args, language);
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args, Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.VB:
                    VBAutoIndentNeeded(sender, args);
                    break;
                case Language.HTML:
                    HTMLAutoIndentNeeded(sender, args);
                    break;
                case Language.XML:
                    XMLAutoIndentNeeded(sender, args);
                    break;
                case Language.SQLServer:
                    SQLAutoIndentNeeded(sender, args);
                    break;
                case Language.PHP:
                    PHPAutoIndentNeeded(sender, args);
                    break;
                case Language.JS:
                    CSharpAutoIndentNeeded(sender, args);
                    break; //JS like C#
                case Language.Lua:
                    LuaAutoIndentNeeded(sender, args);
                    break;
                case Language.JSON:
                    JSONAutoIndentNeeded(sender, args);
                    break;
                case Language.MySQL:
                    MySQLAutoIndentNeeded(sender, args);
                    break;
                case Language.Batch:
                    BatchAutoIndentNeeded(sender, args);
                    break;
                case Language.Java:
                    CSharpAutoIndentNeeded(sender, args);
                    break; //Java like C#
                case Language.Python:
                    PythonAutoIndentNeeded(sender, args);
                    break;
                default:
                    break;
            }
        }

        protected void PHPAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            /*
            FastColoredTextBox tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);*/
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        protected void SQLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void HTMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void XMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void VBAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //end of block
            if (Regex.IsMatch(args.LineText, @"^\s*(End|EndIf|Next|Loop)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //start of declaration
            if (Regex.IsMatch(args.LineText,
                              @"\b(Class|Module|Property|Enum|Structure|Sub|Function|Namespace|Interface|Get)\b|(Set\s*\()",
                              RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            // then ...
            if (Regex.IsMatch(args.LineText, @"\b(Then)\s*\S+", RegexOptions.IgnoreCase))
                return;
            //start of operator block
            if (Regex.IsMatch(args.LineText, @"^\s*(If|While|For|Do|Try|With|Using|Select)\b", RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }

            //Statements else, elseif, case etc
            if (Regex.IsMatch(args.LineText, @"^\s*(Else|ElseIf|Case|Catch|Finally)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                return;
            }

            //Char _
            if (args.PrevLineText.TrimEnd().EndsWith("_"))
            {
                args.Shift = args.TabLength;
                return;
            }
        }

        protected void CSharpAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start and end of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\}[^""']*\{"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = 0;
                return;
            }
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                //if (args.PrevLineText.TrimEnd().EndsWith("{"))
                //    args.ShiftNextLines = args.TabLength;
                //else
                //    args.Shift = -args.TabLength;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|do|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        /// <summary>
        /// Uses the given <paramref name="doc"/> to parse a XML description and adds it as syntax descriptor. 
        /// The syntax descriptor is used for highlighting when 
        /// <list type="bullet">
        ///     <item>Language property of FCTB is set to <see cref="Language.Custom"/></item>
        ///     <item>DescriptionFile property of FCTB has the same value as the method parameter <paramref name="descriptionFileName"/></item>
        /// </list>
        /// </summary>
        /// <param name="descriptionFileName">Name of the description file</param>
        /// <param name="doc">XmlDocument to parse</param>
        public virtual void AddXmlDescription(string descriptionFileName, XmlDocument doc)
        {
            SyntaxDescriptor desc = ParseXmlDescription(doc);
            descByXMLfileNames[descriptionFileName] = desc;
        }

        /// <summary>
        /// Adds the given <paramref name="style"/> as resilient style. A resilient style is additionally available when highlighting is 
        /// based on a syntax descriptor that has been derived from a XML description file. In the run of the highlighting routine 
        /// the styles used by the FCTB are always dropped and replaced with the (initial) ones from the syntax descriptor. Resilient styles are 
        /// added afterwards and can be used anyway. 
        /// </summary>
        /// <param name="style">Style to add</param>
        public virtual void AddResilientStyle(Style style)
        {
            if (resilientStyles.Contains(style)) return;
            currentTb.CheckStylesBufferSize(); // Prevent buffer overflow
            resilientStyles.Add(style);
        }

        public static SyntaxDescriptor ParseXmlDescription(XmlDocument doc)
        {
            var desc = new SyntaxDescriptor();
            XmlNode brackets = doc.SelectSingleNode("doc/brackets");
            if (brackets != null)
            {
                if (brackets.Attributes["left"] == null || brackets.Attributes["right"] == null ||
                    brackets.Attributes["left"].Value == "" || brackets.Attributes["right"].Value == "")
                {
                    desc.leftBracket = '\x0';
                    desc.rightBracket = '\x0';
                }
                else
                {
                    desc.leftBracket = brackets.Attributes["left"].Value[0];
                    desc.rightBracket = brackets.Attributes["right"].Value[0];
                }

                if (brackets.Attributes["left2"] == null || brackets.Attributes["right2"] == null ||
                    brackets.Attributes["left2"].Value == "" || brackets.Attributes["right2"].Value == "")
                {
                    desc.leftBracket2 = '\x0';
                    desc.rightBracket2 = '\x0';
                }
                else
                {
                    desc.leftBracket2 = brackets.Attributes["left2"].Value[0];
                    desc.rightBracket2 = brackets.Attributes["right2"].Value[0];
                }

                if (brackets.Attributes["strategy"] == null || brackets.Attributes["strategy"].Value == "")
                    desc.bracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                else
                    desc.bracketsHighlightStrategy = (BracketsHighlightStrategy)Enum.Parse(typeof(BracketsHighlightStrategy), brackets.Attributes["strategy"].Value);
            }

            var styleByName = new Dictionary<string, Style>();

            foreach (XmlNode style in doc.SelectNodes("doc/style"))
            {
                Style s = ParseStyle(style);
                styleByName[style.Attributes["name"].Value] = s;
                desc.styles.Add(s);
            }
            foreach (XmlNode rule in doc.SelectNodes("doc/rule"))
                desc.rules.Add(ParseRule(rule, styleByName));
            foreach (XmlNode folding in doc.SelectNodes("doc/folding"))
                desc.foldings.Add(ParseFolding(folding));

            return desc;
        }

        protected static FoldingDesc ParseFolding(XmlNode foldingNode)
        {
            var folding = new FoldingDesc();
            //regex
            folding.startMarkerRegex = foldingNode.Attributes["start"].Value;
            folding.finishMarkerRegex = foldingNode.Attributes["finish"].Value;
            //options
            XmlAttribute optionsA = foldingNode.Attributes["options"];
            if (optionsA != null)
                folding.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), optionsA.Value);

            return folding;
        }

        protected static RuleDesc ParseRule(XmlNode ruleNode, Dictionary<string, Style> styles)
        {
            var rule = new RuleDesc();
            rule.pattern = ruleNode.InnerText;
            //
            XmlAttribute styleA = ruleNode.Attributes["style"];
            XmlAttribute optionsA = ruleNode.Attributes["options"];
            //Style
            if (styleA == null)
                throw new Exception("Rule must contain style name.");
            if (!styles.ContainsKey(styleA.Value))
                throw new Exception("Style '" + styleA.Value + "' is not found.");
            rule.style = styles[styleA.Value];
            //options
            if (optionsA != null)
                rule.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), optionsA.Value);

            return rule;
        }

        protected static Style ParseStyle(XmlNode styleNode)
        {
            XmlAttribute typeA = styleNode.Attributes["type"];
            XmlAttribute colorA = styleNode.Attributes["color"];
            XmlAttribute backColorA = styleNode.Attributes["backColor"];
            XmlAttribute fontStyleA = styleNode.Attributes["fontStyle"];
            XmlAttribute nameA = styleNode.Attributes["name"];
            //colors
            SolidBrush foreBrush = null;
            if (colorA != null)
                foreBrush = new SolidBrush(ParseColor(colorA.Value));
            SolidBrush backBrush = null;
            if (backColorA != null)
                backBrush = new SolidBrush(ParseColor(backColorA.Value));
            //fontStyle
            FontStyle fontStyle = FontStyle.Regular;
            if (fontStyleA != null)
                fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontStyleA.Value);

            return new TextStyle(foreBrush, backBrush, fontStyle);
        }

        protected static Color ParseColor(string s)
        {
            if (s.StartsWith("#"))
            {
                if (s.Length <= 7)
                    return Color.FromArgb(255,
                                          Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
                else
                    return Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
            }
            else
                return Color.FromName(s);
        }

        public void HighlightSyntax(SyntaxDescriptor desc, Range range)
        {
            //set style order
            range.tb.ClearStylesBuffer();
            for (int i = 0; i < desc.styles.Count; i++)
                range.tb.Styles[i] = desc.styles[i];
            // add resilient styles
            int l = desc.styles.Count;
            for (int i = 0; i < resilientStyles.Count; i++)
                range.tb.Styles[l + i] = resilientStyles[i];
            //brackets
            char[] oldBrackets = RememberBrackets(range.tb);
            range.tb.LeftBracket = desc.leftBracket;
            range.tb.RightBracket = desc.rightBracket;
            range.tb.LeftBracket2 = desc.leftBracket2;
            range.tb.RightBracket2 = desc.rightBracket2;
            //clear styles of range
            range.ClearStyle(desc.styles.ToArray());
            //highlight syntax
            foreach (RuleDesc rule in desc.rules)
                range.SetStyle(rule.style, rule.Regex);
            //clear folding
            range.ClearFoldingMarkers();
            //folding markers
            foreach (FoldingDesc folding in desc.foldings)
                range.SetFoldingMarkers(folding.startMarkerRegex, folding.finishMarkerRegex, folding.options);

            //
            RestoreBrackets(range.tb, oldBrackets);
        }

        protected void RestoreBrackets(FastColoredTextBox tb, char[] oldBrackets)
        {
            tb.LeftBracket = oldBrackets[0];
            tb.RightBracket = oldBrackets[1];
            tb.LeftBracket2 = oldBrackets[2];
            tb.RightBracket2 = oldBrackets[3];
            tb.LeftBracket3 = oldBrackets[4];
            tb.RightBracket3 = oldBrackets[5];
        }

        protected char[] RememberBrackets(FastColoredTextBox tb)
        {
            return new[] { tb.LeftBracket, tb.RightBracket, tb.LeftBracket2, tb.RightBracket2, tb.LeftBracket3, tb.RightBracket3 };
        }

        protected void InitCSharpRegex()
        {
            //CSharpStringRegex = new Regex( @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'", RegexCompiledOption);

            CSharpStringRegex =
                new Regex(
                    @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '
                            |
                            # Normal string & verbatim strings definitions:
                            \$?
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR
            
                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                    RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                    RegexCompiledOption
                    ); //thanks to rittergig for this regex

            //CSharpCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
            //CSharpCommentRegex1 = new Regex(@"(?<=^(?:(?:[^""]|\\"")*""(?:[^""]|\\"")*"")*?(?:[^""]|\\"")*)//.*$", RegexOptions.Multiline | RegexCompiledOption);
            CSharpCommentRegex1 = new Regex(@"(?<=^(?:(?:(?:[^""]|\\"")*?""){2})*?(?:[^""]|\\"")*?)//.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            CSharpCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            CSharpCommentRegex3 = new Regex(@"(/\*.*?)", RegexCompiledOption);
            CSharpCommentRegex4 = new Regex(@"(.*?\*/)", RegexOptions.RightToLeft | RegexCompiledOption);
            //CSharpNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfFuUmM]?\b|\b0x[a-fA-F\d]+\b|\b0b[01]\b", RegexCompiledOption);
            CSharpNumberRegex = new Regex(@"(\b|\B-)([0-9_]+(\.[0-9_]*)?([e][\-]?[0-9_]+)?|0x[0-9a-f_]+|0b[01_]+)[ldfum]?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            CSharpAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            CSharpClassNameRegex = new Regex(@"\b(class|struct|enum|interface|record\s+struct|record)\s+(?<range>\w+?)\b", RegexCompiledOption);
            CSharpKeywordRegex = new Regex(@"\b(abstract|add|alias|and|as|ascending|async|await|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|global|goto|group|if|implicit|in|init|int|interface|internal|into|is|join|let|lock|long|nameof|namespace|new|nint|not|nuint|null|object|on|operator|or|orderby|out|override|params|partial|private|protected|public|readonly|record|ref|remove|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|when|where|while|with|yield|_)\b", RegexCompiledOption); 
            CSharpPreprocessorRegex = new Regex(@"^\s*#\s*(region|endregion|define|undef|if|elif|else|endif|pragma\s+(warning\s+(restore|enable|disable)|checksum)|error|warning|nullable\s+(restore|enable|disable)|line(\s+(default|hidden))?)\b", RegexOptions.Multiline | RegexCompiledOption);
            CSharpInlineDocumentRegex = new Regex(@"^\s*///([^/].*)?$", RegexOptions.Multiline | RegexCompiledOption);
            CSharpInlineDocumentTagRegex = new Regex(@"^\s*///", RegexOptions.Multiline | RegexCompiledOption);
            CSharpInterpolatedStringRegex = new Regex(@"\$@?""(?<range>(\\""|[^""])*?)""", RegexOptions.ExplicitCapture | RegexCompiledOption);
            CSharpInterpolationExpressionRegex = new Regex(@"[{].*?[}]", RegexCompiledOption);
        }
        
        public void InitStyleSchema(Language lang)
        {
            switch (lang)
            {
                case Language.CSharp:
                    StringStyle = BrownStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    AttributeStyle = SlateGrayStyle;
                    PreprocessorStyle = CyanStyle;
                    ClassNameStyle = BoldStyle;
                    KeywordStyle = BlueStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.VB:
                    StringStyle = BrownStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    PreprocessorStyle = CyanStyle;
                    ClassNameStyle = BoldStyle;
                    KeywordStyle = BlueStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.HTML:
                    CommentStyle = GreenStyle;
                    TagBracketStyle = BlueStyle;
                    TagNameStyle = MaroonStyle;
                    AttributeStyle = RedStyle;
                    AttributeValueStyle = BlueStyle;
                    HtmlEntityStyle = RedBoldStyle;
                    break;
                case Language.XML:
                    CommentStyle = GreenStyle;
                    XmlTagBracketStyle = BlueStyle;
                    XmlTagNameStyle = MaroonStyle;
                    XmlAttributeStyle = RedStyle;
                    XmlAttributeValueStyle = BlueStyle;
                    XmlEntityStyle = RedBoldStyle;
                    XmlCDataStyle = NoStyle;
                    break;
                case Language.JS:
                    StringStyle = BrownStyle;
                    StringStyle2 = BrownStyle2;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueStyle;
                    KeywordStyle2 = MagentaStyle;
                    VariableStyle = SeaStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.Lua:
                    StringStyle = RedStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueBoldStyle;
                    FunctionsStyle = MaroonStyle;
                    break;
                case Language.PHP:
                    StringStyle = RedStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    VariableStyle = MaroonStyle;
                    KeywordStyle = MagentaStyle;
                    KeywordStyle2 = BlueStyle;
                    KeywordStyle3 = VioletStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.SQLServer:
                    StringStyle = RedStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueBoldStyle;
                    StatementsStyle = BlueBoldStyle;
                    FunctionsStyle = MaroonStyle;
                    VariableStyle = MaroonStyle;
                    TypesStyle = BlueBoldStyle;
                    break;
                case Language.JSON:
                    StringStyle = BrownStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueStyle;
                    KeywordStyle2 = DarkBlueStyle;
                    VariableStyle = SeaStyle;
                    break;
                case Language.MySQL:
                    CommentStyle = GreenStyle;
                    StringStyle = RedStyle; 
                    NumberStyle = OrangeStyle; 
                    KeywordStyle = BlueBoldStyle;
                    FunctionsStyle = MaroonStyle; 
                    TypesStyle = BlueBoldStyle;
                    break;
                case Language.Batch:
                    CommentStyle = GreenStyle;
                    KeywordStyle = BlueBoldStyle;
                    KeywordStyle2 = BlueStyle;
                    VariableStyle = SlateGrayStyle; 
                    LabelStyle = RedStyle;
                    StringStyle = NoStyle;
                    break;
                case Language.Java:
                    CommentStyle = GreenStyle;
                    StringStyle = BrownStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueStyle;
                    KeywordStyle2 = DarkGreenStyle;
                    ClassNameStyle = BoldStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.Python:
                    CommentStyle = GreenStyle;
                    StringStyle = BrownStyle;
                    NumberStyle = OrangeStyle;
                    KeywordStyle = BlueBoldStyle;
                    FunctionsStyle = MagentaStyle;
                    ClassNameStyle = BoldStyle;
                    break;
            }
        }

        /// <summary>
        /// Highlights C# code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void CSharpSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "//";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = DefaultAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                    = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;=]+);
^\s*(case|default)\s*[^:]*(?<range>:)\s*(?<range>[^;]+);
";
            }

            //clear style of changed range
            ClearStyles(range, Language.CSharp);
            //
            if (CSharpStringRegex == null)
                InitCSharpRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, CSharpCommentRegex1);
            range.SetStyle(CommentStyle, CSharpCommentRegex2);
            //range.SetStyle(CommentStyle, CSharpCommentRegex3);
            //range.SetStyle(CommentStyle, CSharpCommentRegex4);
            //string highlighting
            range.SetStyle(StringStyle, CSharpStringRegex);

            //find String interpolation $"..."
            foreach (Range r in range.GetRanges(CSharpInterpolatedStringRegex))
            {
                //remove string highlighting {...}
                foreach (Range rr in r.GetRanges(CSharpInterpolationExpressionRegex))
                {
                    rr.ClearStyle(StringStyle);
                }
                //add string highlighting {{ }}
                foreach (Range rr in r.GetRanges(@"[{]{2}"))
                {
                    rr.SetStyle(StringStyle);
                }
                foreach (Range rr in r.GetRanges(@"[}]{2}", RegexOptions.RightToLeft))
                {
                    rr.SetStyle(StringStyle);
                }
            }

            //number highlighting
            range.SetStyle(NumberStyle, CSharpNumberRegex);
            //preprocessor directives highlighting
            range.SetStyle(PreprocessorStyle, CSharpPreprocessorRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, CSharpKeywordRegex);
            //class name highlighting
            range.SetStyle(ClassNameStyle, CSharpClassNameRegex);

            if (HTMLTagRegex == null)
                InitHTMLRegex();
            //find document comments
            foreach (Range r in range.GetRanges(CSharpInlineDocumentRegex))
            {
                //remove C# highlighting from this fragment
                r.ClearStyle(StringStyle, CommentStyle, NumberStyle, AttributeStyle, ClassNameStyle, KeywordStyle, PreprocessorStyle);
                //
                r.SetStyle(CommentStyle);
                //do XML highlighting
                foreach (Range rr in r.GetRanges(HTMLTagContentRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, AttributeStyle, ClassNameStyle, KeywordStyle, PreprocessorStyle);
                    rr.SetStyle(CommentTagStyle);
                }
                //prefix '///'
                foreach (Range rr in r.GetRanges(CSharpInlineDocumentTagRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, AttributeStyle, ClassNameStyle, KeywordStyle, PreprocessorStyle);
                    rr.SetStyle(CommentTagStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"#region\b", @"#endregion\b"); //allow to collapse #region blocks
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        protected void InitVBRegex()
        {
            VBStringRegex = new Regex(@"""""|"".*?[^\\]""", RegexCompiledOption);
            VBCommentRegex = new Regex(@"(?<=^(?:(?:(?:[^""]|\\"")*?""){2})*?(?:[^""]|\\"")*?)'.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            VBNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            VBClassNameRegex = new Regex(@"\b(Class|Module|Structure|Enum|Interface)\s+(?<range>\w+?)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            VBKeywordRegex = new Regex(@"\b(AddHandler|AddressOf|Alias|And|AndAlso|As|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDbl|CDec|Char|CInt|Class|CLng|CObj|Const|Continue|CSByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Exit|False|Finally|For|Friend|Function|Get|GetType|GetXMLNamespace|Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Integer|Interface|Is|IsNot|Let|Lib|Like|Long|Loop|Me|Mod|Module|MustInherit|MustOverride|MyBase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|On|Operator|Option|Optional|Or|OrElse|Overloads|Overridable|Overrides|ParamArray|Partial|Private|Property|Protected|Public|RaiseEvent|ReadOnly|ReDim|REM|RemoveHandler|Resume|Return|SByte|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|String|Structure|Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|UInteger|ULong|UShort|Using|Variant|Wend|When|While|Widening|With|WithEvents|WriteOnly|Xor|_)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            VBPreprocessorRegex = new Regex(@"^\s*#\s*(Const|Region|End\s+Region|Disable\s+Warning|Enable\s+Warning|If|ElseIf|Else|End\s+If|ExternalChecksum|ExternalSource|End\s+ExternalSource)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexCompiledOption);
            VBInlineDocumentRegex = new Regex(@"^\s*'''([^'].*)?$", RegexOptions.Multiline | RegexCompiledOption);
            VBInlineDocumentTagRegex = new Regex(@"^\s*'''", RegexOptions.Multiline | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights VB code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void VBSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "'";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '\x0';
                range.tb.RightBracket2 = '\x0';
                range.tb.LeftBracket3 = '{';
                range.tb.RightBracket3 = '}';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = VBAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                    = @"
^\s*[\w\.\(\)]+\s*(?<range>=)\s*(?<range>.+)
";
            }

            //clear style of changed range
            ClearStyles(range, Language.VB);
            //
            if (VBStringRegex == null)
                InitVBRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, VBCommentRegex);
            //string highlighting
            range.SetStyle(StringStyle, VBStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, VBNumberRegex);
            //class name highlighting
            range.SetStyle(ClassNameStyle, VBClassNameRegex);
            //keyword preprocessor directives
            range.SetStyle(PreprocessorStyle, VBPreprocessorRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, VBKeywordRegex);

            if (HTMLTagRegex == null)
                InitHTMLRegex();
            //find document comments
            foreach (Range r in range.GetRanges(VBInlineDocumentRegex))
            {
                //remove C# highlighting from this fragment
                r.ClearStyle(StringStyle, CommentStyle, NumberStyle, ClassNameStyle, KeywordStyle);
                //
                r.SetStyle(CommentStyle);
                //do XML highlighting
                foreach (Range rr in r.GetRanges(HTMLTagContentRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, ClassNameStyle, KeywordStyle);
                    rr.SetStyle(CommentTagStyle);
                }
                //prefix '''''
                foreach (Range rr in r.GetRanges(VBInlineDocumentTagRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, ClassNameStyle, KeywordStyle);
                    rr.SetStyle(CommentTagStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"#Region\b", @"#End\s+Region\b", RegexOptions.IgnoreCase);
            range.SetFoldingMarkers(@"\b(Class|Property|Enum|Structure|Interface)[ \t]+\S+",
                                    @"\bEnd (Class|Property|Enum|Structure|Interface)\b", RegexOptions.IgnoreCase);
            range.SetFoldingMarkers(@"^\s*(?<range>While)[ \t]+\S+", @"^\s*(?<range>End While)\b",
                                    RegexOptions.Multiline | RegexOptions.IgnoreCase);
            range.SetFoldingMarkers(@"\b(Sub|Function)[ \t]+[^\s']+", @"\bEnd (Sub|Function)\b", RegexOptions.IgnoreCase);
            //this declared separately because Sub and Function can be unclosed
            range.SetFoldingMarkers(@"(\r|\n|^)[ \t]*(?<range>Get|Set)[ \t]*(\r|\n|$)", @"\bEnd (Get|Set)\b",
                                    RegexOptions.IgnoreCase);
            range.SetFoldingMarkers(@"^\s*(?<range>For|For\s+Each)\b", @"^\s*(?<range>Next)\b",
                                    RegexOptions.Multiline | RegexOptions.IgnoreCase);
            range.SetFoldingMarkers(@"^\s*(?<range>Do)\b", @"^\s*(?<range>Loop)\b",
                                    RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        protected void InitHTMLRegex()
        {
            HTMLCommentRegex1 = new Regex(@"(<\s*!\s*--.*?--\s*>)", RegexOptions.Singleline | RegexCompiledOption);
            //HTMLCommentRegex1 = new Regex(@"(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline | RegexCompiledOption);
            //HTMLCommentRegex2 = new Regex(@"(<!--.*?-->)|(.*-->)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            HTMLTagRegex = new Regex(@"<\s*!|<\s*/|>|<|/\s*>", RegexCompiledOption);;
            HTMLTagNameRegex = new Regex(@"<\s*(?<range>[!\w:]+)", RegexCompiledOption);
            HTMLEndTagRegex = new Regex(@"<\s*/\s*(?<range>[\w:]+)\s*>", RegexCompiledOption);
            HTMLTagContentRegex = new Regex(@"<[^>]+>", RegexCompiledOption);
            HTMLAttrRegex =
                new Regex(
                    @"(?<range>[\w\d\-]{1,20}?)\s*=\s*'[^']*'|(?<range>[\w\d\-]{1,20})\s*=\s*""[^""]*""|(?<range>[\w\d\-]{1,20})\s*=\s*[\w\d\-]{1,20}|(?<range>[\w\d\-]{1,20}?)\s*=\s*{[^}]*}",
                    RegexCompiledOption);
            HTMLAttrRegex2 = 
                new Regex(
                    @"(?<=<([^>]*\s+)?)[\w\d\-]{1,20}(?=(\s+[^<]*)?>)",
                    RegexCompiledOption);
            HTMLAttrValRegex =
                new Regex(
                    @"[\w\d\-]{1,20}?\s*=\s*(?<range>'[^']*')|[\w\d\-]{1,20}\s*=\s*(?<range>""[^""]*"")|[\w\d\-]{1,20}\s*=\s*(?<range>[\w\d\-]{1,20})",
                    RegexCompiledOption);
            HTMLAttrValRegex2 =
                new Regex(
                    @"(?<=<\s*![^>]*)"".*""",
                    RegexCompiledOption);
            //HTMLEntityRegex = new Regex(@"\&(amp|gt|lt|nbsp|quot|apos|copy|reg|rarr|larr|uarr|darr|tab|newline|excl|num|dollar|percnt|ast|commat|cent|pound|current|yen|brvbar|sect|dot|not|pm|sup2|sup3|#[0-9]{1,8}|#x[0-9a-f]{1,8});",
            //                            RegexCompiledOption | RegexOptions.IgnoreCase);
            HTMLEntityRegex = new Regex(@"\&([a-z]+|#[0-9]{1,8}|#x[0-9a-f]{1,8});",
                                        RegexCompiledOption | RegexOptions.IgnoreCase);
            HTMLFoldingBlockRegex = new Regex[HTMLFoldingBlockRegexTags.Length, 2];
            for (int i = 0; i < HTMLFoldingBlockRegexTags.Length; i++)
            {
                HTMLFoldingBlockRegex[i, 0] = new Regex($@"<\s*{HTMLFoldingBlockRegexTags[i]}(\s+|>)", RegexOptions.IgnoreCase | RegexCompiledOption);
                HTMLFoldingBlockRegex[i, 1] = new Regex($@"<\s*/\s*{HTMLFoldingBlockRegexTags[i]}\s*>", RegexOptions.IgnoreCase | RegexCompiledOption);
            }
            HTMLEmbeddedJSRegex = new Regex(@"(?<=<\s*script\b[^>]*>).*?(?=<\s*/\s*script\s*>)", RegexOptions.IgnoreCase | RegexOptions.Singleline |RegexCompiledOption);
            HTMLEmbeddedCSSRegex = new Regex(@"(?<=<\s*style\b[^>]*>).*?(?=<\s*/\s*style\s*>)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexCompiledOption);
            HTMLEmbeddedPHPRegex = new Regex(@"(?<=<\s*\?\s*php\b).*?(?=\?\s*>)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights HTML code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        /// <param name="findEmbedded"></param>
        public virtual void HTMLSyntaxHighlight(Range range, bool changeTb = true, bool findEmbedded = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = null;
                range.tb.LeftBracket = '<';
                range.tb.RightBracket = '>';
                range.tb.LeftBracket2 = '\0';
                range.tb.RightBracket2 = '\0';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy1;
                range.tb.AutoCompleteBracketsList = MarkupAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            if (findEmbedded)
            {
                //clear PHP style
                ClearStyles(range, Language.PHP);
                //clear JS style
                ClearStyles(range, Language.JS);
            }

            //clear style of changed range
            ClearStyles(range, Language.HTML);
            //
            if (HTMLTagRegex == null)
                InitHTMLRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, HTMLCommentRegex1);
            //range.SetStyle(CommentStyle, HTMLCommentRegex2);
            //tag brackets highlighting
            range.SetStyle(TagBracketStyle, HTMLTagRegex);
            //html entity
            range.SetStyle(HtmlEntityStyle, HTMLEntityRegex);
            //tag name
            range.SetStyle(TagNameStyle, HTMLTagNameRegex);
            //end of tag
            range.SetStyle(TagNameStyle, HTMLEndTagRegex);
            //attributes
            range.SetStyle(AttributeStyle, HTMLAttrRegex);
            //attribute values
            range.SetStyle(AttributeValueStyle, HTMLAttrValRegex);
            range.SetStyle(AttributeValueStyle, HTMLAttrValRegex2);
            //attributes without value
            range.SetStyle(AttributeStyle, HTMLAttrRegex2);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            for (int i = 0; i < HTMLFoldingBlockRegex.GetLength(0); i++)
            {
                range.SetFoldingMarkers(HTMLFoldingBlockRegex[i, 0], HTMLFoldingBlockRegex[i, 1]);
            }

            if (findEmbedded)
            {

                //find embedded javascript
                foreach (Range r in range.tb.Range.GetRanges(HTMLEmbeddedJSRegex))
                {
                    //remove html highlighting from this fragment
                    r.ClearStyle(CommentStyle, TagBracketStyle, HtmlEntityStyle, TagNameStyle, AttributeStyle, AttributeValueStyle);
                    //
                    if (JScriptStringRegex == null)
                        InitJScriptRegex();
                    InitStyleSchema(Language.JS);
                    //javascript highlight
                    JScriptSyntaxHighlight(r, false, false);
                }

                //find embedded css
                foreach (Range r in range.tb.Range.GetRanges(HTMLEmbeddedCSSRegex))
                {
                    // CSS highlighting is not supported, so we treat it as plain text
                    PlainTextSyntaxHighlight(r, false);
                }

                //find embedded php
                foreach (Range r in range.tb.Range.GetRanges(HTMLEmbeddedPHPRegex))
                {
                    //remove html highlighting from this fragment
                    r.ClearStyle(CommentStyle, TagBracketStyle, HtmlEntityStyle, TagNameStyle, AttributeStyle, AttributeValueStyle);
                    //
                    if (PHPStringRegex == null)
                        InitPHPRegex();
                    InitStyleSchema(Language.PHP);
                    //php highlight
                    PHPSyntaxHighlight(r, false);
                }

            }
        }

        protected void InitXMLRegex()
        {
            XMLCommentRegex1 = new Regex(@"(<\s*!\s*--.*?--\s*>)", RegexOptions.Singleline | RegexCompiledOption);
            //XMLCommentRegex1 = new Regex(@"(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline | RegexCompiledOption);
            //XMLCommentRegex2 = new Regex(@"(<!--.*?-->)|(.*-->)",  RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);

            XMLTagRegex = new Regex(@"<\s*\?|<\s*/|>|<|/\s*>|\?\s*>", RegexCompiledOption);
            XMLTagNameRegex = new Regex(@"<\s*[?](?<range1>[x][m][l]{1})|<\s*(?<range>[!\w:\-\.]+)", RegexCompiledOption);
            XMLEndTagRegex = new Regex(@"<\s*/\s*(?<range>[\w:\-\.]+)\s*>", RegexCompiledOption);

            XMLTagContentRegex = new Regex(@"<[^>]+>", RegexCompiledOption);
            XMLAttrRegex =
                new Regex(
                    @"(?<range>[\w\d\-\:]+)[ ]*=[ ]*'[^']*'|(?<range>[\w\d\-\:]+)[ ]*=[ ]*""[^""]*""|(?<range>[\w\d\-\:]+)[ ]*=[ ]*[\w\d\-\:]+",
                    RegexCompiledOption);
            XMLAttrValRegex =
                new Regex(
                    @"[\w\d\-]+?=(?<range>'[^']*')|[\w\d\-]+[ ]*=[ ]*(?<range>""[^""]*"")|[\w\d\-]+[ ]*=[ ]*(?<range>[\w\d\-]+)",
                    RegexCompiledOption);
            //XMLEntityRegex = new Regex(@"\&(amp|gt|lt|nbsp|quot|apos|copy|reg|rarr|larr|uarr|darr|tab|newline|excl|num|dollar|percnt|ast|commat|cent|pound|current|yen|brvbar|sect|dot|not|pm|sup2|sup3|#[0-9]{1,8}|#x[0-9a-f]{1,8});",
            //                            RegexCompiledOption | RegexOptions.IgnoreCase);
            XMLEntityRegex = new Regex(@"\&([a-z]+|#[0-9]{1,8}|#x[0-9a-f]{1,8});",
                                        RegexCompiledOption | RegexOptions.IgnoreCase);
            XMLCDataRegex = new Regex(@"<!\s*\[CDATA\s*\[(?<text>(?>[^]]+|](?!]>))*)]]>", RegexCompiledOption | RegexOptions.IgnoreCase); // http://stackoverflow.com/questions/21681861/i-need-a-regex-that-matches-cdata-elements-in-html
            XMLFoldingRegex = new Regex(@"<(?<range>/?[\w:\-\.]+)\s[^>]*?[^/]>|<(?<range>/?[\w:\-\.]+)\s*>", RegexOptions.Singleline | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights XML code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void XMLSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = null;
                range.tb.LeftBracket = '<';
                range.tb.RightBracket = '>';
                range.tb.LeftBracket2 = '\x0';
                range.tb.RightBracket2 = '\x0';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy1;
                range.tb.AutoCompleteBracketsList = MarkupAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            //clear style of changed range
            ClearStyles(range, Language.XML);
            //
            if (XMLTagRegex == null)
            {
                InitXMLRegex();
            }
            
            //xml CData
            range.SetStyle(XmlCDataStyle, XMLCDataRegex);

            //comment highlighting
            range.SetStyle(CommentStyle, XMLCommentRegex1);
            //range.SetStyle(CommentStyle, XMLCommentRegex2);

            //tag brackets highlighting
            range.SetStyle(XmlTagBracketStyle, XMLTagRegex);

            //xml entity
            range.SetStyle(XmlEntityStyle, XMLEntityRegex);

            //tag name
            range.SetStyle(XmlTagNameStyle, XMLTagNameRegex);

            //end of tag
            range.SetStyle(XmlTagNameStyle, XMLEndTagRegex);

            //attributes
            range.SetStyle(XmlAttributeStyle, XMLAttrRegex);

            //attribute values
            range.SetStyle(XmlAttributeValueStyle, XMLAttrValRegex);

            //clear folding markers
            range.ClearFoldingMarkers();

            //set folding markers
            XmlFolding(range);
        }

        private void XmlFolding(Range range)
        {
            var stack = new Stack<XmlFoldingTag>();
            var id = 0;
            var fctb = range.tb;
            //extract opening and closing tags (exclude open-close tags: <TAG/>)
            foreach (var r in range.GetRanges(XMLFoldingRegex))
            {
                var tagName = r.Text;
                var iLine = r.Start.iLine;
                //if it is opening tag...
                if (tagName[0] != '/')
                {
                    // ...push into stack
                    var tag = new XmlFoldingTag { Name = tagName, id = id++, startLine = r.Start.iLine };
                    stack.Push(tag);
                    // if this line has no markers - set marker
                    if (string.IsNullOrEmpty(fctb[iLine].FoldingStartMarker))
                        fctb[iLine].FoldingStartMarker = tag.Marker;
                }
                else
                {
                    //if it is closing tag - pop from stack
                    if (stack.Count > 0)
                    {
                        var tag = stack.Pop();
                        //compare line number
                        if (iLine == tag.startLine)
                        {
                            //remove marker, because same line can not be folding
                            if (fctb[iLine].FoldingStartMarker == tag.Marker) //was it our marker?
                                fctb[iLine].FoldingStartMarker = null;
                        }
                        else
                        {
                            //set end folding marker
                            if (string.IsNullOrEmpty(fctb[iLine].FoldingEndMarker))
                                fctb[iLine].FoldingEndMarker = tag.Marker;
                        }
                    }
                }
            }
        }

        class XmlFoldingTag
        {
            public string Name;
            public int id;
            public int startLine;
            public string Marker { get { return Name + id; } }
        }

        protected void InitSQLRegex()
        {
            SQLStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            SQLNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            SQLCommentRegex1 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)--.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            SQLCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            //SQLCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            //SQLCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            SQLCommentRegex4 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)#.*$", RegexOptions.Multiline | RegexCompiledOption);
            SQLServerVarRegex = new Regex(@"@[a-zA-Z_\d]*\b", RegexCompiledOption);
            SQLServerStatementsRegex = new Regex(@"\b(ALTER APPLICATION ROLE|ALTER ASSEMBLY|ALTER ASYMMETRIC KEY|ALTER AUTHORIZATION|ALTER BROKER PRIORITY|ALTER CERTIFICATE|ALTER CREDENTIAL|ALTER CRYPTOGRAPHIC PROVIDER|ALTER DATABASE|ALTER DATABASE AUDIT SPECIFICATION|ALTER DATABASE ENCRYPTION KEY|ALTER ENDPOINT|ALTER EVENT SESSION|ALTER FULLTEXT CATALOG|ALTER FULLTEXT INDEX|ALTER FULLTEXT STOPLIST|ALTER FUNCTION|ALTER INDEX|ALTER LOGIN|ALTER MASTER KEY|ALTER MESSAGE TYPE|ALTER PARTITION FUNCTION|ALTER PARTITION SCHEME|ALTER PROCEDURE|ALTER QUEUE|ALTER REMOTE SERVICE BINDING|ALTER RESOURCE GOVERNOR|ALTER RESOURCE POOL|ALTER ROLE|ALTER ROUTE|ALTER SCHEMA|ALTER SERVER AUDIT|ALTER SERVER AUDIT SPECIFICATION|ALTER SERVICE|ALTER SERVICE MASTER KEY|ALTER SYMMETRIC KEY|ALTER TABLE|ALTER TRIGGER|ALTER USER|ALTER VIEW|ALTER WORKLOAD GROUP|ALTER XML SCHEMA COLLECTION|BULK INSERT|CREATE AGGREGATE|CREATE APPLICATION ROLE|CREATE ASSEMBLY|CREATE ASYMMETRIC KEY|CREATE BROKER PRIORITY|CREATE CERTIFICATE|CREATE CONTRACT|CREATE CREDENTIAL|CREATE CRYPTOGRAPHIC PROVIDER|CREATE DATABASE|CREATE DATABASE AUDIT SPECIFICATION|CREATE DATABASE ENCRYPTION KEY|CREATE DEFAULT|CREATE ENDPOINT|CREATE EVENT NOTIFICATION|CREATE EVENT SESSION|CREATE FULLTEXT CATALOG|CREATE FULLTEXT INDEX|CREATE FULLTEXT STOPLIST|CREATE FUNCTION|CREATE INDEX|CREATE LOGIN|CREATE MASTER KEY|CREATE MESSAGE TYPE|CREATE PARTITION FUNCTION|CREATE PARTITION SCHEME|CREATE PROCEDURE|CREATE QUEUE|CREATE REMOTE SERVICE BINDING|CREATE RESOURCE POOL|CREATE ROLE|CREATE ROUTE|CREATE RULE|CREATE SCHEMA|CREATE SERVER AUDIT|CREATE SERVER AUDIT SPECIFICATION|CREATE SERVICE|CREATE SPATIAL INDEX|CREATE STATISTICS|CREATE SYMMETRIC KEY|CREATE SYNONYM|CREATE TABLE|CREATE TRIGGER|CREATE TYPE|CREATE USER|CREATE VIEW|CREATE WORKLOAD GROUP|CREATE XML INDEX|CREATE XML SCHEMA COLLECTION|DELETE|DISABLE TRIGGER|DROP AGGREGATE|DROP APPLICATION ROLE|DROP ASSEMBLY|DROP ASYMMETRIC KEY|DROP BROKER PRIORITY|DROP CERTIFICATE|DROP CONTRACT|DROP CREDENTIAL|DROP CRYPTOGRAPHIC PROVIDER|DROP DATABASE|DROP DATABASE AUDIT SPECIFICATION|DROP DATABASE ENCRYPTION KEY|DROP DEFAULT|DROP ENDPOINT|DROP EVENT NOTIFICATION|DROP EVENT SESSION|DROP FULLTEXT CATALOG|DROP FULLTEXT INDEX|DROP FULLTEXT STOPLIST|DROP FUNCTION|DROP INDEX|DROP LOGIN|DROP MASTER KEY|DROP MESSAGE TYPE|DROP PARTITION FUNCTION|DROP PARTITION SCHEME|DROP PROCEDURE|DROP QUEUE|DROP REMOTE SERVICE BINDING|DROP RESOURCE POOL|DROP ROLE|DROP ROUTE|DROP RULE|DROP SCHEMA|DROP SERVER AUDIT|DROP SERVER AUDIT SPECIFICATION|DROP SERVICE|DROP SIGNATURE|DROP STATISTICS|DROP SYMMETRIC KEY|DROP SYNONYM|DROP TABLE|DROP TRIGGER|DROP TYPE|DROP USER|DROP VIEW|DROP WORKLOAD GROUP|DROP XML SCHEMA COLLECTION|ENABLE TRIGGER|EXEC|EXECUTE|REPLACE|FROM|INSERT|MERGE|OPTION|OUTPUT|SELECT|TOP|TRUNCATE TABLE|UPDATE|UPDATE STATISTICS|WHERE|WITH|INTO|IN|SET)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            SQLServerKeywordsRegex = new Regex(@"\b(ADD|ALL|AND|ANY|AS|ASC|AUTHORIZATION|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BY|CASCADE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COLLATE|COLUMN|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTINUE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DENY|DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DUMP|ELSE|END|ERRLVL|ESCAPE|EXCEPT|EXISTS|EXIT|EXTERNAL|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|IDENTITY_INSERT|IDENTITYCOL|IF|INDEX|INNER|INTERSECT|IS|JOIN|KEY|KILL|LIKE|LINENO|LOAD|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|OF|OFF|OFFSETS|ON|OPEN|OR|ORDER|OUTER|OVER|PERCENT|PIVOT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVERT|REVOKE|ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SECURITYAUDIT|SHUTDOWN|SOME|STATISTICS|TABLE|TABLESAMPLE|TEXTSIZE|THEN|TO|TRAN|TRANSACTION|TRIGGER|TSEQUAL|UNION|UNIQUE|UNPIVOT|UPDATETEXT|USE|USER|VALUES|VARYING|VIEW|WAITFOR|WHEN|WHILE|WRITETEXT)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            SQLServerFunctionsRegex = new Regex(@"(@@CONNECTIONS|@@CPU_BUSY|@@CURSOR_ROWS|@@DATEFIRST|@@DATEFIRST|@@DBTS|@@ERROR|@@FETCH_STATUS|@@IDENTITY|@@IDLE|@@IO_BUSY|@@LANGID|@@LANGUAGE|@@LOCK_TIMEOUT|@@MAX_CONNECTIONS|@@MAX_PRECISION|@@NESTLEVEL|@@OPTIONS|@@PACKET_ERRORS|@@PROCID|@@REMSERVER|@@ROWCOUNT|@@SERVERNAME|@@SERVICENAME|@@SPID|@@TEXTSIZE|@@TRANCOUNT|@@VERSION)\b|\b(ABS|ACOS|APP_NAME|ASCII|ASIN|ASSEMBLYPROPERTY|AsymKey_ID|ASYMKEY_ID|asymkeyproperty|ASYMKEYPROPERTY|ATAN|ATN2|AVG|CASE|CAST|CEILING|Cert_ID|Cert_ID|CertProperty|CHAR|CHARINDEX|CHECKSUM_AGG|COALESCE|COL_LENGTH|COL_NAME|COLLATIONPROPERTY|COLLATIONPROPERTY|COLUMNPROPERTY|COLUMNS_UPDATED|COLUMNS_UPDATED|CONTAINSTABLE|CONVERT|COS|COT|COUNT|COUNT_BIG|CRYPT_GEN_RANDOM|CURRENT_TIMESTAMP|CURRENT_TIMESTAMP|CURRENT_USER|CURRENT_USER|CURSOR_STATUS|DATABASE_PRINCIPAL_ID|DATABASE_PRINCIPAL_ID|DATABASEPROPERTY|DATABASEPROPERTYEX|DATALENGTH|DATALENGTH|DATEADD|DATEDIFF|DATENAME|DATEPART|DAY|DB_ID|DB_NAME|DECRYPTBYASYMKEY|DECRYPTBYCERT|DECRYPTBYKEY|DECRYPTBYKEYAUTOASYMKEY|DECRYPTBYKEYAUTOCERT|DECRYPTBYPASSPHRASE|DEGREES|DENSE_RANK|DIFFERENCE|ENCRYPTBYASYMKEY|ENCRYPTBYCERT|ENCRYPTBYKEY|ENCRYPTBYPASSPHRASE|ERROR_LINE|ERROR_MESSAGE|ERROR_NUMBER|ERROR_PROCEDURE|ERROR_SEVERITY|ERROR_STATE|EVENTDATA|EXP|FILE_ID|FILE_IDEX|FILE_NAME|FILEGROUP_ID|FILEGROUP_NAME|FILEGROUPPROPERTY|FILEPROPERTY|FLOOR|fn_helpcollations|fn_listextendedproperty|fn_servershareddrives|fn_virtualfilestats|fn_virtualfilestats|FORMATMESSAGE|FREETEXTTABLE|FULLTEXTCATALOGPROPERTY|FULLTEXTSERVICEPROPERTY|GETANSINULL|GETDATE|GETUTCDATE|GROUPING|HAS_PERMS_BY_NAME|HOST_ID|HOST_NAME|IDENT_CURRENT|IDENT_CURRENT|IDENT_INCR|IDENT_INCR|IDENT_SEED|IDENTITY\(|INDEX_COL|INDEXKEY_PROPERTY|INDEXPROPERTY|IS_MEMBER|IS_OBJECTSIGNED|IS_SRVROLEMEMBER|ISDATE|ISDATE|ISNULL|ISNUMERIC|Key_GUID|Key_GUID|Key_ID|Key_ID|KEY_NAME|KEY_NAME|LEFT|LEN|LOG|LOG10|LOWER|LTRIM|MAX|MIN|MONTH|NCHAR|NEWID|NTILE|NULLIF|OBJECT_DEFINITION|OBJECT_ID|OBJECT_NAME|OBJECT_SCHEMA_NAME|OBJECTPROPERTY|OBJECTPROPERTYEX|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|ORIGINAL_LOGIN|ORIGINAL_LOGIN|PARSENAME|PATINDEX|PATINDEX|PERMISSIONS|PI|POWER|PUBLISHINGSERVERNAME|PWDCOMPARE|PWDENCRYPT|QUOTENAME|RADIANS|RAND|RANK|REPLICATE|REVERSE|RIGHT|ROUND|ROW_NUMBER|ROWCOUNT_BIG|RTRIM|SCHEMA_ID|SCHEMA_ID|SCHEMA_NAME|SCHEMA_NAME|SCOPE_IDENTITY|SERVERPROPERTY|SESSION_USER|SESSION_USER|SESSIONPROPERTY|SETUSER|SIGN|SignByAsymKey|SignByCert|SIN|SOUNDEX|SPACE|SQL_VARIANT_PROPERTY|SQRT|SQUARE|STATS_DATE|STDEV|STDEVP|STR|STUFF|SUBSTRING|SUM|SUSER_ID|SUSER_NAME|SUSER_SID|SUSER_SNAME|SWITCHOFFSET|SYMKEYPROPERTY|symkeyproperty|sys\.dm_db_index_physical_stats|sys\.fn_builtin_permissions|sys\.fn_my_permissions|SYSDATETIME|SYSDATETIMEOFFSET|SYSTEM_USER|SYSTEM_USER|SYSUTCDATETIME|TAN|TERTIARY_WEIGHTS|TEXTPTR|TODATETIMEOFFSET|TRIGGER_NESTLEVEL|TYPE_ID|TYPE_NAME|TYPEPROPERTY|UNICODE|UPDATE\(|UPPER|USER_ID|USER_NAME|USER_NAME|VAR|VARP|VerifySignedByAsymKey|VerifySignedByCert|XACT_STATE|YEAR)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            SQLServerTypesRegex = new Regex(@"\b(BIGINT|NUMERIC|BIT|SMALLINT|DECIMAL|SMALLMONEY|INT|TINYINT|MONEY|FLOAT|REAL|DATE|DATETIMEOFFSET|DATETIME2|SMALLDATETIME|DATETIME|TIME|CHAR|VARCHAR|TEXT|NCHAR|NVARCHAR|NTEXT|BINARY|VARBINARY|IMAGE|TIMESTAMP|HIERARCHYID|TABLE|UNIQUEIDENTIFIER|SQL_VARIANT|XML)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights SQL code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void SQLSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "--";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '\x0';
                range.tb.RightBracket2 = '\x0';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = SQLAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            //clear style of changed range
            ClearStyles(range, Language.SQLServer);
            //
            if (SQLServerFunctionsRegex == null)
                InitSQLRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, SQLCommentRegex1);
            range.SetStyle(CommentStyle, SQLCommentRegex2);
            //range.SetStyle(CommentStyle, SQLCommentRegex3);
            range.SetStyle(CommentStyle, SQLCommentRegex4);
            //string highlighting
            range.SetStyle(StringStyle, SQLStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, SQLNumberRegex);
            //types highlighting
            range.SetStyle(TypesStyle, SQLServerTypesRegex);
            //var highlighting
            range.SetStyle(VariableStyle, SQLServerVarRegex);
            //statements
            range.SetStyle(StatementsStyle, SQLServerStatementsRegex);
            //keywords
            range.SetStyle(KeywordStyle, SQLServerKeywordsRegex);
            //functions
            range.SetStyle(FunctionsStyle, SQLServerFunctionsRegex);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"\bBEGIN\b", @"\bEND\b", RegexOptions.IgnoreCase); //allow to collapse BEGIN..END blocks
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
            range.SetFoldingMarkers(@"\(", @"\)"); //allow to collapse parentheses
        }

        protected void InitPHPRegex()
        {
            PHPStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            PHPNumberRegex = new Regex(@"\b([0-9_]+[\.]?[0-9_]*|0x[0-9A-F_]+|0o[0-7_]+|0b[01_]+)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            PHPCommentRegex1 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)(//|#).*$", RegexOptions.Multiline | RegexOptions.Compiled);
            PHPCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            //PHPCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            //PHPCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            PHPVarRegex = new Regex(@"\$\w+\b", RegexCompiledOption);
            PHPKeywordRegex1 =
                new Regex(
                    @"\b(die|echo|empty|exit|eval|include|include_once|isset|list|require|require_once|return|print|unset)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            PHPKeywordRegex2 =
                new Regex(
                    @"\b(abstract|and|array|as|break|case|catch|cfunction|class|clone|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|extends|final|for|foreach|function|global|goto|if|implements|instanceof|interface|namespace|new|or|private|protected|public|static|switch|throw|try|use|var|while|xor|true|false|null)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            PHPKeywordRegex3 = new Regex(@"\b(__CLASS__|__DIR__|__FILE__|__LINE__|__FUNCTION__|__METHOD__|__NAMESPACE__|__TRAIT__)\b",
                                         RegexCompiledOption);
        }

        /// <summary>
        /// Highlights PHP code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void PHPSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "//";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = DefaultAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                = @"
^\s*\$[\w\.\[\]\'\""]+\s*(?<range>=)\s*(?<range>[^;]+);
";
            }
            //clear style of changed range
            ClearStyles(range, Language.PHP);
            //
            if (PHPStringRegex == null)
                InitPHPRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, PHPCommentRegex1);
            range.SetStyle(CommentStyle, PHPCommentRegex2);
            //range.SetStyle(CommentStyle, PHPCommentRegex3);
            //string highlighting
            range.SetStyle(StringStyle, PHPStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, PHPNumberRegex);
            //var highlighting
            range.SetStyle(VariableStyle, PHPVarRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, PHPKeywordRegex1);
            range.SetStyle(KeywordStyle2, PHPKeywordRegex2);
            range.SetStyle(KeywordStyle3, PHPKeywordRegex3);

            if (JavaInlineDocumentRegex == null)
                InitJavaRegex();
            //find document comments
            foreach (Range r in range.GetRanges(JavaInlineDocumentRegex))
            {
                //remove java highlighting from this fragment
                r.ClearStyle(StringStyle, CommentStyle, NumberStyle, VariableStyle, KeywordStyle, KeywordStyle2,
                             KeywordStyle3);
                //
                r.SetStyle(CommentStyle);
                //annotations
                foreach (Range rr in r.GetRanges(JavaAnnotationsRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, VariableStyle, KeywordStyle, KeywordStyle2,
                             KeywordStyle3);
                    rr.SetStyle(CommentTagStyle);
                }
                //prefix '/**'
                foreach (Range rr in r.GetRanges(JavaInlineDocumentTagRegex))
                {
                    rr.ClearStyle(StringStyle, CommentStyle, NumberStyle, VariableStyle, KeywordStyle, KeywordStyle2,
                             KeywordStyle3);
                    rr.SetStyle(CommentTagStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        protected void InitJScriptRegex()
        {
            //JScriptStringRegex = new Regex(@"""""|''|``|"".*?[^\\]""|'.*?[^\\]'|`.*?[^\\]`", RegexCompiledOption);
            JScriptStringRegex = new Regex(@"(""""|""(([^""\\\n]|\\.)*)"")", RegexCompiledOption);
            JScriptStringRegex2 = new Regex(@"(''|``|'((\\.|[^'\\\n])*)'|`((\\.|[^`\\\n])*)`)", RegexCompiledOption);
            JScriptCommentRegex1 = new Regex(@"(?<=^(([^""'`]*?[""'`]){2})*?([^""'`]|\\""|\\')*?)//.*$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
            JScriptCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            //JScriptCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            //JScriptCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            JScriptNumberRegex = new Regex(@"\b\d+[\.]?\d*([e]\-?\d+)?[ldf]?\b|\b0x[a-f\d]+\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            JScriptKeywordRegex = new Regex(@"\b(true|false|break|case|catch|const|continue|default|delete|do|else|export|from|finally|for|function|if|import|in|instanceof|let|new|null|of|return|switch|this|throw|try|undefined|var|void|while|with|typeof)\b", RegexCompiledOption);
            JScriptKeywordRegex2 = new Regex(@"\b(alert|atob|blur|btoa|cancelAnimationFrame|cancelIdleCallback|clearInterval|clearTimeout|close|confirm|createImageBitmap|dump|decodeURI|decodeURIComponent|encodeURI|encodeURIComponent|escape|eval|fetch|find|focus|getComputedStyle|getDefaultComputedStyle|getScreenDetails|getSelection|isFinite|isNaN|matchMedia|moveBy|moveTo|open|parseFloat|parseInt|postMessage|print|prompt|queryLocalFonts|queueMicrotask|reportError|requestAnimationFrame|requestIdelCallback|resizeBy|resizeTo|scroll|scrollBy|scrollByLines|scrollByPages|scrollTo|setInterval|setTimeout|showDirectoryPiacker|showOpenFilePicker|showSaveFilePicker|sizeToContext|stop|structuredClone|updateCommands|unescape)\b", RegexCompiledOption);
            JScriptJSONValueRegex = new Regex(@"(?<range>""([^""\\]|\\.)*"")\s*:", RegexCompiledOption);
            JScriptEmbeddedHTMLRegex = new Regex(@"^\s*<.*?>;?\s*$", RegexOptions.Multiline | RegexOptions.Singleline | RegexCompiledOption);
            JScriptEmbeddedValueRegex = new Regex(@"{.*}", RegexCompiledOption);
        }

        /// <summary>
        /// Highlights JavaScript code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        /// <param name="findEmbedded"></param>
        public virtual void JScriptSyntaxHighlight(Range range, bool changeTb = true, bool findEmbedded = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "//";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = JSAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                    = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;]+);
";
            }

            if (findEmbedded)
            {
                //clear HTML style
                ClearStyles(range, Language.HTML);
            }

            //clear style of changed range
            ClearStyles(range, Language.JS);
            //
            if (JScriptStringRegex == null)
                InitJScriptRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, JScriptCommentRegex1);
            range.SetStyle(CommentStyle, JScriptCommentRegex2);
            //range.SetStyle(CommentStyle, JScriptCommentRegex3);
            //string highlighting
            range.SetStyle(StringStyle2, JScriptStringRegex2);
            //json value highlight
            range.SetStyle(VariableStyle, JScriptJSONValueRegex);
            //string highlighting
            range.SetStyle(StringStyle, JScriptStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, JScriptNumberRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, JScriptKeywordRegex);
            range.SetStyle(KeywordStyle2, JScriptKeywordRegex2);

            if (JavaInlineDocumentRegex == null)
                InitJavaRegex();
            //find document comments
            foreach (Range r in range.GetRanges(JavaInlineDocumentRegex))
            {
                //remove javascript highlighting from this fragment
                r.ClearStyle(StringStyle, StringStyle2, CommentStyle, NumberStyle, KeywordStyle, KeywordStyle2, VariableStyle);
                //
                r.SetStyle(CommentStyle);
                //annotations
                foreach (Range rr in r.GetRanges(JavaAnnotationsRegex))
                {
                    rr.ClearStyle(StringStyle, StringStyle2, CommentStyle, NumberStyle, KeywordStyle, KeywordStyle2, VariableStyle);
                    rr.SetStyle(CommentTagStyle);
                }
                //prefix '/**'
                foreach (Range rr in r.GetRanges(JavaInlineDocumentTagRegex))
                {
                    rr.ClearStyle(StringStyle, StringStyle2, CommentStyle, NumberStyle, KeywordStyle, KeywordStyle2, VariableStyle);
                    rr.SetStyle(CommentTagStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block

            if (findEmbedded)
            {

                if (HTMLTagRegex == null)
                    InitHTMLRegex();
                //find embedded html
                foreach (Range r in range.GetRanges(JScriptEmbeddedHTMLRegex))
                {
                    //remove javascript highlighting from this fragment
                    ClearStyles(r, Language.JS);
                    //
                    InitStyleSchema(Language.HTML);
                    HTMLSyntaxHighlight(r, false, false);
                    // {*}
                    foreach (Range rr in r.GetRanges(JScriptEmbeddedValueRegex))
                    {
                        ClearStyles(rr, Language.HTML);
                    }
                }

            }
        }

        protected void InitLuaRegex()
        {
            LuaStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            LuaCommentRegex1 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)--.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            LuaCommentRegex2 = new Regex(@"(--\[\[.*?\]\])", RegexOptions.Singleline | RegexCompiledOption);
            //LuaCommentRegex2 = new Regex(@"(--\[\[.*?\]\])|(--\[\[.*)", RegexOptions.Singleline | RegexCompiledOption);
            //LuaCommentRegex3 = new Regex(@"(--\[\[.*?\]\])|(.*\]\])",
            //                                 RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            LuaNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                                           RegexCompiledOption);
            LuaKeywordRegex =
                new Regex(
                    @"\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\b",
                    RegexCompiledOption);

            LuaFunctionsRegex =
                new Regex(
                    @"\b(assert|collectgarbage|dofile|error|getfenv|getmetatable|ipairs|load|loadfile|loadstring|module|next|pairs|pcall|print|rawequal|rawget|rawset|require|select|setfenv|setmetatable|tonumber|tostring|type|unpack|xpcall)\b",
                    RegexCompiledOption);
        }

        /// <summary>
        /// Highlights Lua code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void LuaSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "--";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = DefaultAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                    = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>.+)
";
            }

            //clear style of changed range
            ClearStyles(range, Language.PlainText);
            //
            if (LuaStringRegex == null)
                InitLuaRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, LuaCommentRegex1);
            range.SetStyle(CommentStyle, LuaCommentRegex2);
            //range.SetStyle(CommentStyle, LuaCommentRegex3);
            //string highlighting
            range.SetStyle(StringStyle, LuaStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, LuaNumberRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, LuaKeywordRegex);
            //functions highlighting
            range.SetStyle(FunctionsStyle, LuaFunctionsRegex);
            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"--\[\[", @"\]\]"); //allow to collapse comment block
        }

        protected void LuaAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //end of block
            if (Regex.IsMatch(args.LineText, @"^\s*(end|until)\b"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            // then ...
            if (Regex.IsMatch(args.LineText, @"\b(then)\s*\S+"))
                return;
            //start of operator block
            if (Regex.IsMatch(args.LineText, @"^\s*(function|do|for|while|repeat|if)\b"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }

            //Statements else, elseif, case etc
            if (Regex.IsMatch(args.LineText, @"^\s*(else|elseif)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                return;
            }
        }

        protected void InitJSONRegex()
        {
            //JSONStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            //JSONNumberRegex = new Regex(@"\b(\d+[\.]?\d*)\b", RegexCompiledOption);
            JSONStringRegex = new Regex(@"""""|""([^""\\]|\\.)*""", RegexCompiledOption);
            JSONNumberRegex = new Regex(@"(\b|\B-)(\d+(\.\d+)?)\b", RegexCompiledOption);
            JSONKeywordRegex = new Regex(@"\b(true|false)\b", RegexCompiledOption);
            JSONKeywordRegex2 = new Regex(@"\b(null)\b", RegexCompiledOption);
            JSONValueRegex = new Regex(@"(?<range>""([^""\\]|\\.)*"")\s*:", RegexCompiledOption);
        }

        /// <summary>
        /// Highlights JSON code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void JSONSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = null;
                range.tb.LeftBracket = '[';
                range.tb.RightBracket = ']';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = JSONAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns
                    = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;]+);
";
            }

            //clear style of changed range
            ClearStyles(range, Language.JSON);
            //
            if (JSONStringRegex == null)
                InitJSONRegex();
            //property highlighting
            range.SetStyle(VariableStyle, JSONValueRegex);
            //string highlighting
            range.SetStyle(StringStyle, JSONStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, JSONNumberRegex);
            //keyword highlighting
            range.SetStyle(KeywordStyle, JSONKeywordRegex);
            //null highlighting
            range.SetStyle(KeywordStyle2, JSONKeywordRegex2);
            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"\{", @"\}"); //allow to collapse objects
            range.SetFoldingMarkers(@"\[", @"\]"); //allow to collapse arrays
        }

        protected void JSONAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        /// <summary>
        /// Highlights nothing
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void PlainTextSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = null;
                range.tb.LeftBracket = '\0';
                range.tb.RightBracket = '\0';
                range.tb.LeftBracket2 = '\0';
                range.tb.RightBracket2 = '\0';
                range.tb.LeftBracket3 = '\0';
                range.tb.RightBracket3 = '\0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = new char[0];
                range.tb.AutoIndentCharsPatterns = @"";
            }

            range.ClearStyle(NoStyle, BlueBoldStyle, BlueStyle, BoldStyle, BrownStyle, BrownStyle2, CyanStyle, DarkBlueBoldStyle, DarkBlueStyle, DarkGreenStyle, GrayStyle, GreenStyle, MagentaStyle, MaroonStyle, OrangeStyle, RedBoldStyle, RedStyle, SeaStyle, SlateGrayStyle, VioletStyle, SeaStyle);
            range.ClearFoldingMarkers();
        }

        protected void InitMySQLRegex()
        {
            SQLStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
            SQLNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?\b", RegexCompiledOption);
            SQLCommentRegex1 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)--.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            SQLCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            //SQLCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            //SQLCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            SQLCommentRegex4 = new Regex(@"(?<=^(?:(?:(?:[^""']|\\""|\\')*?(?:""|')){2})*?(?:[^""']|\\""|\\')*?)#.*$", RegexOptions.Multiline | RegexCompiledOption);
            MySQLKeywordsRegex = new Regex(@"\b(?:ACCESSIBLE|ACCOUNT|ACTION|ACTIVE|ADD|ADMIN|AFTER|AGAINST|AGGREGATE|ALGORITHM|ALL|ALTER|ALWAYS|ANALYZE|AND|ANY|ARRAY|AS|ASC|ASCII|ASENSITIVE|AT|ATTRIBUTE|AUTHENTICATION|AUTO|AUTOEXTEND_SIZE|AUTO_INCREMENT|AVG|AVG_ROW_LENGTH|BACKUP|BEFORE|BEGIN|BERNOULLI|BETWEEN|BIGINT|BINARY|BINLOG|BIT|BLOB|BLOCK|BOOL|BOOLEAN|BOTH|BTREE|BUCKETS|BULK|BY|BYTE|CACHE|CALL|CASCADE|CASCADED|CASE|CATALOG_NAME|CHAIN|CHALLENGE_RESPONSE|CHANGE|CHANGED|CHANNEL|CHAR|CHARACTER|CHARSET|CHECK|CHECKSUM|CIPHER|CLASS_ORIGIN|CLIENT|CLONE|CLOSE|COALESCE|CODE|COLLATE|COLLATION|COLUMN|COLUMNS|COLUMN_FORMAT|COLUMN_NAME|COMMENT|COMMIT|COMMITTED|COMPACT|COMPLETION|COMPONENT|COMPRESSED|COMPRESSION|CONCURRENT|CONDITION|CONNECTION|CONSISTENT|CONSTRAINT|CONSTRAINT_CATALOG|CONSTRAINT_NAME|CONSTRAINT_SCHEMA|CONTAINS|CONTEXT|CONTINUE|CONVERT|CPU|CREATE|CROSS|CUBE|CUME_DIST|CURRENT|CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|CURSOR|CURSOR_NAME|DATA|DATABASE|DATABASES|DATAFILE|DATE|DATETIME|DAY|DAY_HOUR|DAY_MICROSECOND|DAY_MINUTE|DAY_SECOND|DEALLOCATE|DEC|DECIMAL|DECLARE|DEFAULT|DEFAULT_AUTH|DEFINER|DEFINITION|DELAYED|DELAY_KEY_WRITE|DELETE|DENSE_RANK|DESC|DESCRIBE|DESCRIPTION|DETERMINISTIC|DIAGNOSTICS|DIRECTORY|DISABLE|DISCARD|DISK|DISTINCT|DISTINCTROW|DIV|DO|DOUBLE|DROP|DUAL|DUMPFILE|DUPLICATE|DYNAMIC|EACH|ELSE|ELSEIF|EMPTY|ENABLE|ENCLOSED|ENCRYPTION|END|ENDS|ENFORCED|ENGINE|ENGINES|ENGINE_ATTRIBUTE|ENUM|ERROR|ERRORS|ESCAPE|ESCAPED|EVENT|EVENTS|EVERY|EXCEPT|EXCHANGE|EXCLUDE|EXECUTE|EXISTS|EXIT|EXPANSION|EXPIRE|EXPLAIN|EXPORT|EXTENDED|EXTENT_SIZE|FACTOR|FAILED_LOGIN_ATTEMPTS|FALSE|FAST|FAULTS|FETCH|FIELDS|FILE|FILE_BLOCK_SIZE|FILTER|FINISH|FIRST|FIRST_VALUE|FIXED|FLOAT|FLOAT4|FLOAT8|FLUSH|FOLLOWING|FOLLOWS|FOR|FORCE|FOREIGN|FORMAT|FOUND|FROM|FULL|FULLTEXT|FUNCTION|GENERAL|GENERATE|GENERATED|GEOMCOLLECTION|GEOMETRY|GEOMETRYCOLLECTION|GET|GET_FORMAT|GET_SOURCE_PUBLIC_KEY|GLOBAL|GRANT|GRANTS|GROUP|GROUPING|GROUPS|GROUP_REPLICATION|GTIDS|GTID_ONLY|HANDLER|HASH|HAVING|HELP|HIGH_PRIORITY|HISTOGRAM|HISTORY|HOST|HOSTS|HOUR|HOUR_MICROSECOND|HOUR_MINUTE|HOUR_SECOND|IDENTIFIED|IF|IGNORE|IGNORE_SERVER_IDS|IMPORT|IN|INACTIVE|INDEX|INDEXES|INFILE|INITIAL|INITIAL_SIZE|INITIATE|INNER|INOUT|INSENSITIVE|INSERT|INSERT_METHOD|INSTALL|INSTANCE|INT|INT1|INT2|INT3|INT4|INT8|INTEGER|INTERSECT|INTERVAL|INTO|INVISIBLE|INVOKER|IO|IO_AFTER_GTIDS|IO_BEFORE_GTIDS|IO_THREAD|IPC|IS|ISOLATION|ISSUER|ITERATE|JOIN|JSON|JSON_TABLE|JSON_VALUE|KEY|KEYRING|KEYS|KEY_BLOCK_SIZE|KILL|LAG|LANGUAGE|LAST|LAST_VALUE|LATERAL|LEAD|LEADING|LEAVE|LEAVES|LEFT|LESS|LEVEL|LIKE|LIMIT|LINEAR|LINES|LINESTRING|LIST|LOAD|LOCAL|LOCALTIME|LOCALTIMESTAMP|LOCK|LOCKED|LOCKS|LOG|LOGFILE|LOGS|LONG|LONGBLOB|LONGTEXT|LOOP|LOW_PRIORITY|MANUAL|MASTER|MATCH|MAXVALUE|MAX_CONNECTIONS_PER_HOUR|MAX_QUERIES_PER_HOUR|MAX_ROWS|MAX_SIZE|MAX_UPDATES_PER_HOUR|MAX_USER_CONNECTIONS|MEDIUM|MEDIUMBLOB|MEDIUMINT|MEDIUMTEXT|MEMBER|MEMORY|MERGE|MESSAGE_TEXT|MICROSECOND|MIDDLEINT|MIGRATE|MINUTE|MINUTE_MICROSECOND|MINUTE_SECOND|MIN_ROWS|MOD|MODE|MODIFIES|MODIFY|MONTH|MULTILINESTRING|MULTIPOINT|MULTIPOLYGON|MUTEX|MYSQL_ERRNO|NAME|NAMES|NATIONAL|NATURAL|NCHAR|NDB|NDBCLUSTER|NESTED|NETWORK_NAMESPACE|NEVER|NEW|NEXT|NO|NODEGROUP|NONE|NOT|NOWAIT|NO_WAIT|NO_WRITE_TO_BINLOG|NTH_VALUE|NTILE|NULL|NULLS|NUMBER|NUMERIC|NVARCHAR|OF|OFF|OFFSET|OJ|OLD|ON|ONE|ONLY|OPEN|OPTIMIZE|OPTIMIZER_COSTS|OPTION|OPTIONAL|OPTIONALLY|OPTIONS|OR|ORDER|ORDINALITY|ORGANIZATION|OTHERS|OUT|OUTER|OUTFILE|OVER|OWNER|PACK_KEYS|PAGE|PARALLEL|PARSER|PARSE_TREE|PARTIAL|PARTITION|PARTITIONING|PARTITIONS|PASSWORD|PASSWORD_LOCK_TIME|PATH|PERCENT_RANK|PERSIST|PERSIST_ONLY|PHASE|PLUGIN|PLUGINS|PLUGIN_DIR|POINT|POLYGON|PORT|PRECEDES|PRECEDING|PRECISION|PREPARE|PRESERVE|PREV|PRIMARY|PRIVILEGES|PRIVILEGE_CHECKS_USER|PROCEDURE|PROCESS|PROCESSLIST|PROFILE|PROFILES|PROXY|PURGE|QUALIFY|QUARTER|QUERY|QUICK|RANDOM|RANGE|RANK|READ|READS|READ_ONLY|READ_WRITE|REAL|REBUILD|RECOVER|RECURSIVE|REDO_BUFFER_SIZE|REDUNDANT|REFERENCE|REFERENCES|REGEXP|REGISTRATION|RELAY|RELAYLOG|RELAY_LOG_FILE|RELAY_LOG_POS|RELAY_THREAD|RELEASE|RELOAD|REMOVE|RENAME|REORGANIZE|REPAIR|REPEAT|REPEATABLE|REPLACE|REPLICA|REPLICAS|REPLICATE_DO_DB|REPLICATE_DO_TABLE|REPLICATE_IGNORE_DB|REPLICATE_IGNORE_TABLE|REPLICATE_REWRITE_DB|REPLICATE_WILD_DO_TABLE|REPLICATE_WILD_IGNORE_TABLE|REPLICATION|REQUIRE|REQUIRE_ROW_FORMAT|RESET|RESIGNAL|RESOURCE|RESPECT|RESTART|RESTORE|RESTRICT|RESUME|RETAIN|RETURN|RETURNED_SQLSTATE|RETURNING|RETURNS|REUSE|REVERSE|REVOKE|RIGHT|RLIKE|ROLE|ROLLBACK|ROLLUP|ROTATE|ROUTINE|ROW|ROWS|ROW_COUNT|ROW_FORMAT|ROW_NUMBER|RTREE|S3|SAVEPOINT|SCHEDULE|SCHEMA|SCHEMAS|SCHEMA_NAME|SECOND|SECONDARY|SECONDARY_ENGINE|SECONDARY_ENGINE_ATTRIBUTE|SECONDARY_LOAD|SECONDARY_UNLOAD|SECOND_MICROSECOND|SECURITY|SELECT|SENSITIVE|SEPARATOR|SERIAL|SERIALIZABLE|SERVER|SESSION|SET|SHARE|SHOW|SHUTDOWN|SIGNAL|SIGNED|SIMPLE|SKIP|SLAVE|SLOW|SMALLINT|SNAPSHOT|SOCKET|SOME|SONAME|SOUNDS|SOURCE|SOURCE_AUTO_POSITION|SOURCE_BIND|SOURCE_COMPRESSION_ALGORITHMS|SOURCE_CONNECT_RETRY|SOURCE_DELAY|SOURCE_HEARTBEAT_PERIOD|SOURCE_HOST|SOURCE_LOG_FILE|SOURCE_LOG_POS|SOURCE_PASSWORD|SOURCE_PORT|SOURCE_PUBLIC_KEY_PATH|SOURCE_RETRY_COUNT|SOURCE_SSL|SOURCE_SSL_CA|SOURCE_SSL_CAPATH|SOURCE_SSL_CERT|SOURCE_SSL_CIPHER|SOURCE_SSL_CRL|SOURCE_SSL_CRLPATH|SOURCE_SSL_KEY|SOURCE_SSL_VERIFY_SERVER_CERT|SOURCE_TLS_CIPHERSUITES|SOURCE_TLS_VERSION|SOURCE_USER|SOURCE_ZSTD_COMPRESSION_LEVEL|SPATIAL|SPECIFIC|SQL|SQLEXCEPTION|SQLSTATE|SQLWARNING|SQL_AFTER_GTIDS|SQL_AFTER_MTS_GAPS|SQL_BEFORE_GTIDS|SQL_BIG_RESULT|SQL_BUFFER_RESULT|SQL_CALC_FOUND_ROWS|SQL_NO_CACHE|SQL_SMALL_RESULT|SQL_THREAD|SQL_TSI_DAY|SQL_TSI_HOUR|SQL_TSI_MINUTE|SQL_TSI_MONTH|SQL_TSI_QUARTER|SQL_TSI_SECOND|SQL_TSI_WEEK|SQL_TSI_YEAR|SRID|SSL|STACKED|START|STARTING|STARTS|STATS_AUTO_RECALC|STATS_PERSISTENT|STATS_SAMPLE_PAGES|STATUS|STOP|STORAGE|STORED|STRAIGHT_JOIN|STREAM|STRING|SUBCLASS_ORIGIN|SUBJECT|SUBPARTITION|SUBPARTITIONS|SUPER|SUSPEND|SWAPS|SWITCHES|SYSTEM|TABLE|TABLES|TABLESAMPLE|TABLESPACE|TABLE_CHECKSUM|TABLE_NAME|TEMPORARY|TEMPTABLE|TERMINATED|TEXT|THAN|THEN|THREAD_PRIORITY|TIES|TIME|TIMESTAMP|TIMESTAMPADD|TIMESTAMPDIFF|TINYBLOB|TINYINT|TINYTEXT|TLS|TO|TRAILING|TRANSACTION|TRIGGER|TRIGGERS|TRUE|TRUNCATE|TYPE|TYPES|UNBOUNDED|UNCOMMITTED|UNDEFINED|UNDO|UNDOFILE|UNDO_BUFFER_SIZE|UNICODE|UNINSTALL|UNION|UNIQUE|UNKNOWN|UNLOCK|UNREGISTER|UNSIGNED|UNTIL|UPDATE|UPGRADE|URL|USAGE|USE|USER|USER_RESOURCES|USE_FRM|USING|UTC_DATE|UTC_TIME|UTC_TIMESTAMP|VALIDATION|VALUE|VALUES|VARBINARY|VARCHAR|VARCHARACTER|VARIABLES|VARYING|VCPU|VIEW|VIRTUAL|VISIBLE|WAIT|WARNINGS|WEEK|WEIGHT_STRING|WHEN|WHERE|WHILE|WINDOW|WITH|WITHOUT|WORK|WRAPPER|WRITE|X509|XA|XID|XML|XOR|YEAR|YEAR_MONTH|ZEROFILL|ZONE|||AUTO|BERNOULLI|GTIDS|LOG|MANUAL|PARALLEL|PARSE_TREE|QUALIFY|S3|TABLESAMPLE|||GET_MASTER_PUBLIC_KEY|MASTER_AUTO_POSITION|MASTER_BIND|MASTER_COMPRESSION_ALGORITHMS|MASTER_CONNECT_RETRY|MASTER_DELAY|MASTER_HEARTBEAT_PERIOD|MASTER_HOST|MASTER_LOG_FILE|MASTER_LOG_POS|MASTER_PASSWORD|MASTER_PORT|MASTER_PUBLIC_KEY_PATH|MASTER_RETRY_COUNT|MASTER_SSL|MASTER_SSL_CA|MASTER_SSL_CAPATH|MASTER_SSL_CERT|MASTER_SSL_CIPHER|MASTER_SSL_CRL|MASTER_SSL_CRLPATH|MASTER_SSL_KEY|MASTER_SSL_VERIFY_SERVER_CERT|MASTER_TLS_CIPHERSUITES|MASTER_TLS_VERSION|MASTER_USER|MASTER_ZSTD_COMPRESSION_LEVEL)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            MySQLFunctionsRegex = new Regex(@"\b(?:ABS|ACOS|ADDDATE|ADDTIME|AES_DECRYPT|AES_ENCRYPT|AND|ANY_VALUE|ASCII|ASIN|asynchronous_connection_failover_add_managed|asynchronous_connection_failover_add_source|asynchronous_connection_failover_delete_managed|asynchronous_connection_failover_delete_source|asynchronous_connection_failover_reset|ATAN|ATAN2|AVG|BENCHMARK|BETWEEN|BIN|BIN_TO_UUID|BINARY|BIT_AND|BIT_COUNT|BIT_LENGTH|BIT_OR|BIT_XOR|CAN_ACCESS_COLUMN|CAN_ACCESS_DATABASE|CAN_ACCESS_TABLE|CAN_ACCESS_USER|CAN_ACCESS_VIEW|CASE|CAST|CEIL|CEILING|CHAR|CHAR_LENGTH|CHARACTER_LENGTH|CHARSET|COALESCE|COERCIBILITY|COLLATION|COMPRESS|CONCAT|CONCAT_WS|CONNECTION_ID|CONV|CONVERT|CONVERT_TZ|COS|COT|COUNT|COUNT|CRC32|CUME_DIST|CURDATE|CURRENT_DATE|CURRENT_ROLE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|CURTIME|DATABASE|DATE|DATE_ADD|DATE_FORMAT|DATE_SUB|DATEDIFF|DAY|DAYNAME|DAYOFMONTH|DAYOFWEEK|DAYOFYEAR|DEFAULT|DEGREES|DENSE_RANK|DIV|ELT|EXISTS|EXP|EXPORT_SET|EXTRACT|ExtractValue|FIELD|FIND_IN_SET|FIRST_VALUE|FLOOR|FORMAT|FORMAT_BYTES|FORMAT_PICO_TIME|FOUND_ROWS|FROM_DAYS|FROM_UNIXTIME|GeomCollection|GeometryCollection|GET_DD_COLUMN_PRIVILEGES|GET_DD_CREATE_OPTIONS|GET_DD_INDEX_SUB_PART_LENGTH|GET_FORMAT|GET_LOCK|GREATEST|GROUP_CONCAT|group_replication_disable_member_action|group_replication_enable_member_action|group_replication_get_communication_protocol|group_replication_get_write_concurrency|group_replication_reset_member_actions|group_replication_set_as_primary|group_replication_set_communication_protocol|group_replication_set_write_concurrency|group_replication_switch_to_multi_primary_mode|group_replication_switch_to_single_primary_mode|GROUPING|HEX|HOUR|ICU_VERSION|IF|IFNULL|IN|INET_ATON|INET_NTOA|INSERT|INSTR|INTERNAL_AUTO_INCREMENT|INTERNAL_AVG_ROW_LENGTH|INTERNAL_CHECK_TIME|INTERNAL_CHECKSUM|INTERNAL_DATA_FREE|INTERNAL_DATA_LENGTH|INTERNAL_DD_CHAR_LENGTH|INTERNAL_GET_COMMENT_OR_ERROR|INTERNAL_GET_ENABLED_ROLE_JSON|INTERNAL_GET_HOSTNAME|INTERNAL_GET_USERNAME|INTERNAL_GET_VIEW_WARNING_OR_ERROR|INTERNAL_INDEX_COLUMN_CARDINALITY|INTERNAL_INDEX_LENGTH|INTERNAL_IS_ENABLED_ROLE|INTERNAL_IS_MANDATORY_ROLE|INTERNAL_KEYS_DISABLED|INTERNAL_MAX_DATA_LENGTH|INTERNAL_TABLE_ROWS|INTERNAL_UPDATE_TIME|INTERVAL|IS|IS_FREE_LOCK|ISNOT|ISNOTNULL|ISNULL|IS_USED_LOCK|IS_UUID|ISNULL|JSON_ARRAY|JSON_ARRAY_APPEND|JSON_ARRAY_INSERT|JSON_ARRAYAGG|JSON_CONTAINS|JSON_CONTAINS_PATH|JSON_DEPTH|JSON_EXTRACT|JSON_INSERT|JSON_KEYS|JSON_LENGTH|JSON_MERGE|JSON_MERGE_PATCH|JSON_MERGE_PRESERVE|JSON_OBJECT|JSON_OBJECTAGG|JSON_OVERLAPS|JSON_PRETTY|JSON_QUOTE|JSON_REMOVE|JSON_REPLACE|JSON_SCHEMA_VALID|JSON_SCHEMA_VALIDATION_REPORT|JSON_SEARCH|JSON_SET|JSON_STORAGE_FREE|JSON_STORAGE_SIZE|JSON_TABLE|JSON_TYPE|JSON_UNQUOTE|JSON_VALID|JSON_VALUE|LAG|LAST_DAY|LAST_INSERT_ID|LAST_VALUE|LCASE|LEAD|LEAST|LEFT|LENGTH|LIKE|LineString|LN|LOAD_FILE|LOCALTIME|LOCALTIMESTAMP|LOCATE|LOG|LOG10|LOG2|LOWER|LPAD|LTRIM|MAKE_SET|MAKEDATE|MAKETIME|MASTER_POS_WAIT|MATCH|MAX|MBRContains|MBRCoveredBy|MBRCovers|MBRDisjoint|MBREquals|MBRIntersects|MBROverlaps|MBRTouches|MBRWithin|MD5|MEMBEROF|MICROSECOND|MID|MIN|MINUTE|MOD|MONTH|MONTHNAME|MultiLineString|MultiPoint|MultiPolygon|NAME_CONST|NOT|NOTBETWEEN|NOTEXISTS|NOTIN|NOTLIKE|NOTREGEXP|NOW|NTH_VALUE|NTILE|NULLIF|OCT|OCTET_LENGTH|OR|ORD|PERCENT_RANK|PERIOD_ADD|PERIOD_DIFF|PI|Point|Polygon|POSITION|POW|POWER|PS_CURRENT_THREAD_ID|PS_THREAD_ID|QUARTER|QUOTE|RADIANS|RAND|RANDOM_BYTES|RANK|REGEXP|REGEXP_INSTR|REGEXP_LIKE|REGEXP_REPLACE|REGEXP_SUBSTR|RELEASE_ALL_LOCKS|RELEASE_LOCK|REPEAT|REPLACE|REVERSE|RIGHT|RLIKE|ROLES_GRAPHML|ROUND|ROW_COUNT|ROW_NUMBER|RPAD|RTRIM|SCHEMA|SEC_TO_TIME|SECOND|SESSION_USER|SHA1|SHA2|SIGN|SIN|SLEEP|SOUNDEX|SOUNDSLIKE|SOURCE_POS_WAIT|SPACE|SQRT|ST_Area|ST_AsBinary|ST_AsGeoJSON|ST_AsText|ST_Buffer|ST_Buffer_Strategy|ST_Centroid|ST_Collect|ST_Contains|ST_ConvexHull|ST_Crosses|ST_Difference|ST_Dimension|ST_Disjoint|ST_Distance|ST_Distance_Sphere|ST_EndPoint|ST_Envelope|ST_Equals|ST_ExteriorRing|ST_FrechetDistance|ST_GeoHash|ST_GeomCollFromText|ST_GeomCollFromWKB|ST_GeometryN|ST_GeometryType|ST_GeomFromGeoJSON|ST_GeomFromText|ST_GeomFromWKB|ST_HausdorffDistance|ST_InteriorRingN|ST_Intersection|ST_Intersects|ST_IsClosed|ST_IsEmpty|ST_IsSimple|ST_IsValid|ST_LatFromGeoHash|ST_Latitude|ST_Length|ST_LineFromText|ST_LineFromWKB|ST_LineInterpolatePoint|ST_LineInterpolatePoints|ST_LongFromGeoHash|ST_Longitude|ST_MakeEnvelope|ST_MLineFromText|ST_MLineFromWKB|ST_MPointFromText|ST_MPointFromWKB|ST_MPolyFromText|ST_MPolyFromWKB|ST_NumGeometries|ST_NumInteriorRing|ST_NumPoints|ST_Overlaps|ST_PointAtDistance|ST_PointFromGeoHash|ST_PointFromText|ST_PointFromWKB|ST_PointN|ST_PolyFromText|ST_PolyFromWKB|ST_Simplify|ST_SRID|ST_StartPoint|ST_SwapXY|ST_SymDifference|ST_Touches|ST_Transform|ST_Union|ST_Validate|ST_Within|ST_X|ST_Y|STATEMENT_DIGEST|STATEMENT_DIGEST_TEXT|STD|STDDEV|STDDEV_POP|STDDEV_SAMP|STR_TO_DATE|STRCMP|SUBDATE|SUBSTR|SUBSTRING|SUBSTRING_INDEX|SUBTIME|SUM|SYSDATE|SYSTEM_USER|TAN|TIME|TIME_FORMAT|TIME_TO_SEC|TIMEDIFF|TIMESTAMP|TIMESTAMPADD|TIMESTAMPDIFF|TO_DAYS|TO_SECONDS|TRIM|TRUNCATE|UCASE|UNCOMPRESS|UNCOMPRESSED_LENGTH|UNHEX|UNIX_TIMESTAMP|UpdateXML|UPPER|USER|UTC_DATE|UTC_TIME|UTC_TIMESTAMP|UUID|UUID_SHORT|UUID_TO_BIN|VALIDATE_PASSWORD_STRENGTH|VALUES|VAR_POP|VAR_SAMP|VARIANCE|VERSION|WAIT_FOR_EXECUTED_GTID_SET|WEEK|WEEKDAY|WEEKOFYEAR|WEIGHT_STRING|XOR|YEAR|YEARWEEK)\b", RegexOptions.IgnoreCase | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights MySQL code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void MySQLSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "--";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '\x0';
                range.tb.RightBracket2 = '\x0';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = SQLAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            //clear style of changed range
            ClearStyles(range, Language.MySQL);
            //
            if (MySQLFunctionsRegex == null)
                InitMySQLRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, SQLCommentRegex1);
            range.SetStyle(CommentStyle, SQLCommentRegex2);
            //range.SetStyle(CommentStyle, SQLCommentRegex3);
            range.SetStyle(CommentStyle, SQLCommentRegex4);
            //string highlighting
            range.SetStyle(StringStyle, SQLStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, SQLNumberRegex);
            //keywords
            range.SetStyle(KeywordStyle, MySQLKeywordsRegex);
            //functions
            range.SetStyle(FunctionsStyle, MySQLFunctionsRegex);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"\(", @"\)"); //allow to collapse parentheses
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }

        protected void MySQLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void InitBatchRegex()
        {
            //try {
            //    string[] exeFiles = Directory.GetFiles(Environment.SystemDirectory);
            //    string exeFilesRegex = "";
            //    foreach (string exeFile in exeFiles)
            //        if (exeFile.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase) || exeFile.EndsWith(".com", StringComparison.InvariantCultureIgnoreCase))
            //            exeFilesRegex += $@"{Path.GetFileNameWithoutExtension(exeFile)}({Path.GetExtension(exeFile)})?|";
            //    BatchKeywordRegex2 = new Regex($@"(?<=(^|&|\|)\s*)(?:{exeFilesRegex})\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexCompiledOption);
            //} catch {
            //    BatchKeywordRegex2 = new Regex(@"");
            //}
            BatchCommentRegex = new Regex(@"^\s*(@?rem\b|::).*$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexCompiledOption);
            //BatchKeywordRegex = new Regex(@"(?<=(^|&|\|)\s*)@?(?:assoc|call|cd|chdir|cls|color|date|del|dir|echo|endlocal|erase|exit|for|ftype|goto|if|md|mkdir|mklink|move|path|pause|popd|prompt|pushd|rd|ren|rename|rmdir|set|setlocal|shift|start|time|title|type|ver|verify|vol)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexCompiledOption);
            BatchKeywordRegex = new Regex(@"(\b|\B@)(?:assoc|call|cd|chdir|cls|color|date|del|dir|echo|endlocal|erase|exit|for|in|do|ftype|goto|if|not|exist|errorlevel|equ|neq|lss|leq|grt|geq|cmdextversion|defined|else|md|mkdir|mklink|move|path|pause|popd|prompt|pushd|rd|ren|rename|rmdir|set|setlocal|shift|start|time|title|type|ver|verify|vol|con|prn|aux|nul|com[1-9]|lpt[1-9])\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            BatchLabelRegex = new Regex(@"^\s*:\s*[\w]+", RegexOptions.Multiline | RegexCompiledOption);
            BatchEnvironmentVariableRegex = new Regex(@"%[\w]*%", RegexCompiledOption);
            BatchStringRegex = new Regex(@"(?<=(\b|\B@)echo\s+).*$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights cmd batch code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void BatchSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "REM ";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '\x0';
                range.tb.RightBracket2 = '\x0';
                range.tb.LeftBracket3 = '\x0';
                range.tb.RightBracket3 = '\x0';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = BatchAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            //clear style of changed range
            ClearStyles(range, Language.Batch);
            //
            if (BatchKeywordRegex == null)
                InitBatchRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, BatchCommentRegex);
            //labels
            range.SetStyle(LabelStyle, BatchLabelRegex);
            //environment variables
            range.SetStyle(VariableStyle, BatchEnvironmentVariableRegex);
            //strings
            range.SetStyle(StringStyle, BatchStringRegex);
            //keywords
            range.SetStyle(KeywordStyle, BatchKeywordRegex);
            //System32 EXE and COM files
            //range.SetStyle(KeywordStyle2, BatchKeyword2Regex);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"\(", @"\)"); //allow to collapse parentheses
        }

        protected void BatchAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void InitJavaRegex()
        {
            JavaCommentRegex = new Regex(@"(?<=^(?:(?:(?:[^""]|\\"")*?""){2})*?(?:[^""]|\\"")*?)//.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            JavaCommentRegex2 = new Regex(@"(/\*.*?\*/)", RegexOptions.Singleline | RegexCompiledOption);
            JavaStringRegex = new Regex(@"""""|""((\\.|[^""\\])*)""|'(.|\\.{1,5}|\\')'", RegexCompiledOption);
            JavaNumberRegex = new Regex(@"(?:\b|(?<=\s)|^)[\-]?(\d+(\.\d*)?([e][\-]?\d+)?|0x[a-f\d]+|0b[01]+)[ldf]?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            JavaClassNameRegex = new Regex(@"\b(class|enum|interface|record)\s+(?<range>\w+?)\b", RegexCompiledOption);
            JavaKeywordRegex = new Regex(@"\b(abstract|assert|boolean|break|byte|case|catch|char|class|const|continue|default|do|double|else|enum|extends|false|final|finally|float|for|goto|if|implements|import|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|strictfp|super|switch|synchronized|this|throw|throws|transient|true|try|void|volatile|while)\b", RegexCompiledOption);
            JavaAnnotationsRegex = new Regex(@"\B@\s*[\w\d_]+\b", RegexCompiledOption);
            JavaInlineDocumentRegex = new Regex(@"^\s*/\*\*.*?\*/", RegexOptions.Multiline | RegexOptions.Singleline | RegexCompiledOption);
            JavaInlineDocumentTagRegex = new Regex(@"^\s*(/\*\*|\*/|\*)", RegexOptions.Multiline | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights java code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void JavaSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "//";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = DefaultAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns // same as C#
                    = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;=]+);
^\s*(case|default)\s*[^:]*(?<range>:)\s*(?<range>[^;]+);
"; 
            }

            //clear style of changed range
            ClearStyles(range, Language.Java);
            //
            if (JavaKeywordRegex == null)
                InitJavaRegex();
            //comment highlighting
            range.SetStyle(CommentStyle, JavaCommentRegex);
            range.SetStyle(CommentStyle, JavaCommentRegex2);
            //strings
            range.SetStyle(StringStyle, JavaStringRegex);
            //numbers
            range.SetStyle(NumberStyle, JavaNumberRegex);
            //annotations
            range.SetStyle(KeywordStyle2, JavaAnnotationsRegex);
            //keywords
            range.SetStyle(KeywordStyle, JavaKeywordRegex);
            //class names
            range.SetStyle(ClassNameStyle, JavaClassNameRegex);
            
            //find document comments
            foreach (Range r in range.GetRanges(JavaInlineDocumentRegex))
            {
                //remove java highlighting from this fragment
                r.ClearStyle(CommentStyle, StringStyle, NumberStyle, KeywordStyle, KeywordStyle2, ClassNameStyle);
                //
                r.SetStyle(CommentStyle);
                //annotations
                foreach (Range rr in r.GetRanges(JavaAnnotationsRegex))
                {
                    rr.ClearStyle(CommentStyle, StringStyle, NumberStyle, KeywordStyle, KeywordStyle2, ClassNameStyle);
                    rr.SetStyle(CommentTagStyle);
                }
                //prefix '/**'
                foreach (Range rr in r.GetRanges(JavaInlineDocumentTagRegex))
                {
                    rr.ClearStyle(CommentStyle, StringStyle, NumberStyle, KeywordStyle, KeywordStyle2, ClassNameStyle);
                    rr.SetStyle(CommentTagStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers()
                ;
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
        }


        protected void InitPythonRegex()
        {
            PythonCommentRegex = new Regex(@"(?<=^(([^""']*?[""']){2})*?([^""']|\\""|\\')*?)#.*$", RegexOptions.Multiline | RegexOptions.Compiled);
            PythonStringRegex = new Regex(@"""""|''|""((\\.|[^""\\\n])*)""|'((\\.|[^'\\\n])*)'", RegexCompiledOption);
            PythonStringRegex2 = new Regex(@"""""""((\\.|[^""\\])*)""""""", RegexOptions.Singleline | RegexCompiledOption);
            PythonNumberRegex = new Regex(@"(\b|\B-)(\d+(\.\d*)?([e][\-]?\d+)?)[j]?\b", RegexOptions.IgnoreCase | RegexCompiledOption);
            PythonClassNameRegex = new Regex(@"\bclass\s+(?<range>\w+?)\s*[(:]", RegexOptions.ExplicitCapture | RegexCompiledOption);
            PythonKeywordRegex = new Regex(@"\b(and|as|assert|break|class|continue|def|del|elif|else|except|False|finally|for|from|global|if|import|in|is|lambda|None|nonlocal|not|or|pass|raise|return|True|try|while|with|yield)\b", RegexCompiledOption);
            PythonFunctionsRegex = new Regex(@"\b(?<range>abs|aiter|all|anext|any|ascii|bin|bool|breakpoint|bytearray|bytes|callable|chr|classmethod|compile|complex|delattr|dict|dir|divmod|enumerate|eval|exec|filter|float|format|frozenset|getattr|globals|hasattr|hash|help|hex|id|input|int|isinstance|issubclass|iter|len|list|locals|map|max|memoryview|min|next|object|oct|open|ord|pow|print|property|range|repr|reversed|round|set|setattr|slice|sorted|staticmethod|str|sum|super|tuple|type|vars|zip|__import__)\b\s*[(]", RegexOptions.ExplicitCapture | RegexCompiledOption);
            PythonFunctionsRegex2 = new Regex(@"\b\w+\s*[.]\s*(?<range>\w+)\b\s*[(]", RegexOptions.ExplicitCapture | RegexCompiledOption);
        }

        /// <summary>
        /// Highlights Python code
        /// </summary>
        /// <param name="range"></param>
        /// <param name="changeTb"></param>
        public virtual void PythonSyntaxHighlight(Range range, bool changeTb = true)
        {
            if (changeTb)
            {
                range.tb.CommentPrefix = "#";
                range.tb.LeftBracket = '(';
                range.tb.RightBracket = ')';
                range.tb.LeftBracket2 = '{';
                range.tb.RightBracket2 = '}';
                range.tb.LeftBracket3 = '[';
                range.tb.RightBracket3 = ']';
                range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
                range.tb.AutoCompleteBracketsList = DefaultAutoCompleteBracketsList;
                range.tb.AutoIndentCharsPatterns = @"";
            }

            //clear style of changed range
            ClearStyles(range, Language.Python);
            //
            if (PythonKeywordRegex == null)
                InitPythonRegex();
            //comments
            range.SetStyle(CommentStyle, PythonCommentRegex);
            //multiline strings
            range.SetStyle(StringStyle, PythonStringRegex2);
            //singleline strings
            range.SetStyle(StringStyle, PythonStringRegex);
            //numbers
            range.SetStyle(NumberStyle, PythonNumberRegex);
            //keywords
            range.SetStyle(KeywordStyle, PythonKeywordRegex);
            //class name
            range.SetStyle(ClassNameStyle, PythonClassNameRegex);
            //built-in functions
            range.SetStyle(FunctionsStyle, PythonFunctionsRegex);
            //imported functions
            //range.SetStyle(FunctionsStyle, PythonFunctionsRegex2);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
        }

        protected void PythonAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //start of block
            if (Regex.IsMatch(args.LineText, @"^\s*\b(def|class|if|for|while|with|try)\b.*:\s*$"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            if (Regex.IsMatch(args.LineText, @"^\s*\b(elif|else|except|finally)\b.*:\s*$"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //end of block
            if (Regex.IsMatch(args.LineText, @"^\s*\b(return|break|continue|raise)\b.*$"))
            {
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //end of block (empty line)
            if (Regex.IsMatch(args.LineText, @"^\s*$") &&
                !Regex.IsMatch(args.PrevLineText, @"^\s*$"))
            {
                args.ShiftNextLines = -args.TabLength;
                return;
            }
        }

        /// <summary>
        /// Clear <see cref="Style"/> for <see cref="Language"/> in <see cref="Range"/>
        /// </summary>
        /// <param name="range"></param>
        /// <param name="language"></param>
        public void ClearStyles(Range range, Language language) 
        {
            switch (language)
            {
                case Language.CSharp:
                    range.ClearStyle(StringStyle, CommentStyle, CommentTagStyle, NumberStyle, ClassNameStyle, KeywordStyle, PreprocessorStyle);
                    break;
                case Language.VB:
                    range.ClearStyle(StringStyle, CommentStyle, CommentTagStyle, NumberStyle, ClassNameStyle, KeywordStyle, PreprocessorStyle);
                    break;
                case Language.HTML:
                    range.ClearStyle(CommentStyle, TagBracketStyle, HtmlEntityStyle, TagNameStyle, AttributeStyle, AttributeValueStyle);
                    break;
                case Language.XML:
                    range.ClearStyle(CommentStyle, XmlTagBracketStyle, XmlTagNameStyle, XmlAttributeStyle, XmlAttributeValueStyle, XmlEntityStyle, XmlCDataStyle);
                    break;
                case Language.SQLServer:
                    range.ClearStyle(CommentStyle, StringStyle, NumberStyle, VariableStyle, StatementsStyle, KeywordStyle, FunctionsStyle, TypesStyle);
                    break;
                case Language.PHP:
                    range.ClearStyle(StringStyle, CommentStyle, CommentTagStyle, NumberStyle, VariableStyle, KeywordStyle, KeywordStyle2, KeywordStyle3);
                    break;
                case Language.JS:
                    range.ClearStyle(StringStyle, StringStyle2, CommentStyle, CommentTagStyle, VariableStyle, NumberStyle, KeywordStyle, KeywordStyle2);
                    break;
                case Language.Lua:
                    range.ClearStyle(StringStyle, CommentStyle, NumberStyle, KeywordStyle, FunctionsStyle);
                    break;
                case Language.JSON:
                    range.ClearStyle(StringStyle, NumberStyle, VariableStyle, KeywordStyle, KeywordStyle2);
                    break;
                case Language.MySQL:
                    range.ClearStyle(CommentStyle, StringStyle, NumberStyle, KeywordStyle, FunctionsStyle);
                    break;
                case Language.Batch:
                    range.ClearStyle(CommentStyle, KeywordStyle, StringStyle, VariableStyle, LabelStyle);
                    break;
                case Language.Java:
                    range.ClearStyle(CommentStyle, CommentTagStyle, StringStyle, NumberStyle, KeywordStyle, KeywordStyle2, ClassNameStyle);
                    break;
                case Language.Python:
                    range.ClearStyle(CommentStyle, StringStyle, NumberStyle, KeywordStyle, FunctionsStyle, ClassNameStyle);
                    break;
                default:
                    break;
            }
        }

        #region Styles

        /// <summary>
        /// String style
        /// </summary>
        public Style StringStyle { get; set; }
        /// <summary>
        /// String style 2
        /// </summary>
        public Style StringStyle2 { get; set; }

        /// <summary>
        /// Comment style
        /// </summary>
        public Style CommentStyle { get; set; }

        /// <summary>
        /// Number style
        /// </summary>
        public Style NumberStyle { get; set; }

        /// <summary>
        /// C# attribute style
        /// </summary>
        public Style AttributeStyle { get; set; }

        /// <summary>
        /// C# preprocessor directives style 
        /// </summary>
        public Style PreprocessorStyle { get; set; }

        /// <summary>
        /// Class name style
        /// </summary>
        public Style ClassNameStyle { get; set; }

        /// <summary>
        /// Keyword style
        /// </summary>
        public Style KeywordStyle { get; set; }

        /// <summary>
        /// Style of tags in comments of C#
        /// </summary>
        public Style CommentTagStyle { get; set; }

        /// <summary>
        /// HTML attribute value style
        /// </summary>
        public Style AttributeValueStyle { get; set; }

        /// <summary>
        /// HTML tag brackets style
        /// </summary>
        public Style TagBracketStyle { get; set; }

        /// <summary>
        /// HTML tag name style
        /// </summary>
        public Style TagNameStyle { get; set; }

        /// <summary>
        /// HTML Entity style
        /// </summary>
        public Style HtmlEntityStyle { get; set; }

        /// <summary>
        /// XML attribute style
        /// </summary>
        public Style XmlAttributeStyle { get; set; }

        /// <summary>
        /// XML attribute value style
        /// </summary>
        public Style XmlAttributeValueStyle { get; set; }

        /// <summary>
        /// XML tag brackets style
        /// </summary>
        public Style XmlTagBracketStyle { get; set; }

        /// <summary>
        /// XML tag name style
        /// </summary>
        public Style XmlTagNameStyle { get; set; }

        /// <summary>
        /// XML Entity style
        /// </summary>
        public Style XmlEntityStyle { get; set; }

        /// <summary>
        /// XML CData style
        /// </summary>
        public Style XmlCDataStyle { get; set; }

        /// <summary>
        /// Variable style
        /// </summary>
        public Style VariableStyle { get; set; }

        /// <summary>
        /// Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle2 { get; set; }

        /// <summary>
        /// Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle3 { get; set; }

        /// <summary>
        /// SQL Statements style
        /// </summary>
        public Style StatementsStyle { get; set; }

        /// <summary>
        /// SQL Functions style
        /// </summary>
        public Style FunctionsStyle { get; set; }

        /// <summary>
        /// SQL Types style
        /// </summary>
        public Style TypesStyle { get; set; }

        /// <summary>
        /// Batch label style
        /// </summary>
        public Style LabelStyle { get; set; }

        #endregion
    }

    /// <summary>
    /// Language
    /// </summary>
    public enum Language
    {
        Custom,
        CSharp,
        VB,
        HTML,
        XML,
        SQLServer,
        PHP,
        JS,
        Lua,
        JSON,
        PlainText,
        MySQL,
        Batch,
        Java,
        Python
    }
}

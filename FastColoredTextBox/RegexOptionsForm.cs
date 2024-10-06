using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public partial class RegexOptionsForm : Form
    {
        public const string IgnoreCase_Description = "Case-insensitive matching.";
        public const string Multiline_Description = "Multiline mode. Changes the meaning of ^ and $ so they match at the beginning and end, respectively, of any line, and not just the beginning and end of the entire string.";
        public const string ExplicitCapture_Description = "Specifies that the only valid captures are explicitly named or numbered groups of the form (?<name>...). This allows unnamed parentheses to act as noncapturing groups without the syntactic clumsiness of the expression (?:...).";
        public const string Singleline_Description = "Specifies single-line mode. Changes the meaning of the dot (.) so it matches every character (instead of every character except \n).";
        public const string RightToLeft_Description = "Specifies that the search will be from right to left instead of from left to right.";
        public const string ECMAScript_Description = "Enables ECMAScript-compliant behavior for the expression. This value can be used only in conjunction with the Ignore Case and Multiline values. The use of this value with any other values results in an exception.";
        public const string CultureInvariant_Description = "Specifies that cultural differences in language is ignored.";

        public bool IgnoreCase
        {
            get
            {
                return ignoreCaseCheckBox.Checked;
            }
            set
            {
                ignoreCaseCheckBox.Checked = value;
            }
        }
        public bool Multiline
        {
            get
            {
                return multilineCheckBox.Checked;
            }
            set
            {
                multilineCheckBox.Checked = value;
            }
        }
        public bool ExplicitCapture
        {
            get
            {
                return explicitCaptureCheckBox.Checked;
            }
            set
            {
                explicitCaptureCheckBox.Checked = value;
            }
        }
        public bool Singleline
        {
            get
            {
                return singlelineCheckBox.Checked;
            }
            set
            {
                singlelineCheckBox.Checked = value;
            }
        }
        public bool RightToLeft
        {
            get
            {
                return rightToLeftCheckBox.Checked;
            }
            set
            {
                rightToLeftCheckBox.Checked = value;
            }
        }
        public bool ECMAScript
        {
            get
            {
                return ecmaScriptCheckBox.Checked;
            }
            set
            {
                ecmaScriptCheckBox.Checked = value;
            }
        }
        public bool CultureInvariant
        {
            get
            {
                return cultureInvariantCheckBox.Checked;
            }
            set
            {
                cultureInvariantCheckBox.Checked = value;
            }
        }
        public bool Compiled { get; }

        public RegexOptionsForm()
        {
            InitializeComponent();

            toolTip.SetToolTip(ignoreCaseCheckBox, IgnoreCase_Description);
            toolTip.SetToolTip(multilineCheckBox, Multiline_Description);
            toolTip.SetToolTip(explicitCaptureCheckBox, ExplicitCapture_Description);
            toolTip.SetToolTip(singlelineCheckBox, Singleline_Description);
            toolTip.SetToolTip(rightToLeftCheckBox, RightToLeft_Description);
            toolTip.SetToolTip(ecmaScriptCheckBox, ECMAScript_Description);
            toolTip.SetToolTip(cultureInvariantCheckBox, CultureInvariant_Description);

            helpProvider.SetShowHelp(ignoreCaseCheckBox, true);
            helpProvider.SetHelpString(ignoreCaseCheckBox, IgnoreCase_Description);
            helpProvider.SetShowHelp(multilineCheckBox, true);
            helpProvider.SetHelpString(multilineCheckBox, Multiline_Description);
            helpProvider.SetShowHelp(explicitCaptureCheckBox, true);
            helpProvider.SetHelpString(explicitCaptureCheckBox, ExplicitCapture_Description);
            helpProvider.SetShowHelp(singlelineCheckBox, true);
            helpProvider.SetHelpString(singlelineCheckBox, Singleline_Description);
            helpProvider.SetShowHelp(rightToLeftCheckBox, true);
            helpProvider.SetHelpString(rightToLeftCheckBox, RightToLeft_Description);
            helpProvider.SetShowHelp(ecmaScriptCheckBox, true);
            helpProvider.SetHelpString(ecmaScriptCheckBox, ECMAScript_Description);
            helpProvider.SetShowHelp(cultureInvariantCheckBox, true);
            helpProvider.SetHelpString(cultureInvariantCheckBox, CultureInvariant_Description);

            Compiled = PlatformType.GetOperationSystemPlatform() == Platform.X86;
        }
    }
}

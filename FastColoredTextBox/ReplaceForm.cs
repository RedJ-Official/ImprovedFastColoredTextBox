
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;

namespace FastColoredTextBoxNS
{
    public partial class ReplaceForm : Form
    {
        FastColoredTextBox tb;
        //bool firstSearch = true;
        //Place startPlace;
        bool markerNeedsClearing = false;

        private Place TbEnd => new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);

        protected MarkerStyle marker = new MarkerStyle(new SolidBrush(Color.FromArgb(0x7F, Color.Yellow)));

        public ReplaceForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            FindNext(tbFind.Text, true);
        }

        private void btFindPrev_Click(object sender, EventArgs e)
        {
            FindPrevious(tbFind.Text, true);
        }

        private void btFindAll_Click(object sender, EventArgs e)
        {
            FindAll(tbFind.Text);
        }

        public void FindAll(string pattern)
        {
            {
                //var opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                //if (!cbRegex.Checked)
                //    pattern = Regex.Escape(pattern);
                //if (cbWholeWord.Checked)
                //    pattern = "\\b" + pattern + "\\b";
                ////
                //var range = tb.Selection.IsEmpty ? tb.Range.Clone() : tb.Selection.Clone();
                ////
                //var list = new List<Range>();
                //foreach (var r in range.GetRangesByLines(pattern, opt))
                //    list.Add(r);

                //return list;
            }

            ClearMarker();

            RegexOptions opt = RegexOptions.Multiline;
            if (!cbMatchCase.Checked) opt |= RegexOptions.IgnoreCase;
            if (!cbRegex.Checked) pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked) pattern = $@"\b{pattern}\b";

            foreach (Range range in tb.GetRanges(pattern, opt))
            {
                range.SetStyle(marker);
            }

            markerNeedsClearing = true;
        }

        public void ClearMarker()
        {
            if (!markerNeedsClearing) return;
            tb.Range.ClearStyle(marker);
            markerNeedsClearing = false;
        }

        public bool FindNext(string pattern, bool recursive, bool verbose = true)
        {
            {
                //var opt = RegexOptions.Multiline;
                //if (!cbMatchCase.Checked)
                //    opt |= RegexOptions.IgnoreCase;
                //if (!cbRegex.Checked)
                //    pattern = Regex.Escape(pattern);
                //if (cbWholeWord.Checked)
                //    pattern = "\\b" + pattern + "\\b";
                ////
                //Range range = tb.Selection.Clone();
                //range.Normalize();
                ////
                //if (firstSearch)
                //{
                //    startPlace = range.Start;
                //    firstSearch = false;
                //}
                ////
                //range.Start = range.End;
                //if (range.Start >= startPlace)
                //    range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                //else
                //    range.End = startPlace;
                ////
                //foreach (var r in range.GetRangesByLines(pattern, opt))
                //{
                //    tb.Selection.Start = r.Start;
                //    tb.Selection.End = r.End;
                //    tb.DoSelectionVisible();
                //    tb.Invalidate();
                //    return true;
                //}
                //if (range.Start >= startPlace && startPlace > Place.Empty)
                //{
                //    tb.Selection.Start = new Place(0, 0);
                //    return FindNext(pattern);
                //}
                //return false;
            }

            RegexOptions opt = RegexOptions.Multiline;
            if (!cbMatchCase.Checked) opt |= RegexOptions.IgnoreCase;
            if (!cbRegex.Checked) pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked) pattern = $@"\b{pattern}\b";

            Range range = tb.Selection.Clone();
            range.Normalize();

            Place start = range.End;

            Range searchRange = tb.GetRange(start, TbEnd);

            Match match = Regex.Match(searchRange.Text, pattern, opt);

            if (match.Success)
            {
                tb.SelectionLength = 0;
                tb.Selection.Start = tb.Selection.End;
                tb.SelectionStart += match.Index;
                tb.SelectionLength = match.Length;

                tb.DoSelectionVisible();
                tb.Invalidate();

                return true;
            }
            else if (recursive)
            {
                tb.GoHome();

                return FindNext(pattern, false);
            }
            else if (verbose)
            {
                MessageBox.Show(this, "Not found.");
            }

            return false;
        }

        public virtual bool FindPrevious(string pattern, bool recursive, bool verbose = true)
        {

            RegexOptions opt = RegexOptions.Multiline | RegexOptions.RightToLeft;
            if (!cbMatchCase.Checked) opt |= RegexOptions.IgnoreCase;
            if (!cbRegex.Checked) pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked) pattern = $@"\b{pattern}\b";

            Range range = tb.Selection.Clone();
            range.Normalize();

            Place start = range.Start;

            Range searchRange = tb.GetRange(Place.Empty, start);

            Match match = Regex.Match(searchRange.Text, pattern, opt);

            if (match.Success)
            {
                tb.SelectionStart = match.Index;
                tb.SelectionLength = match.Length;

                tb.DoSelectionVisible();
                tb.Invalidate();

                return true;
            }
            else if (recursive)
            {
                tb.GoEnd();

                return FindPrevious(pattern, false, verbose);
            }
            else if (verbose)
            {
                MessageBox.Show(this, "Not found.");
            }

            return false;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (ModifierKeys == Keys.Shift)
                    btFindPrev.PerformClick();
                else
                    btFindNext.PerformClick();
            if (e.KeyChar == '\x1b')
                Hide();
        }

        private void tbReplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (ModifierKeys == Keys.Shift)
                    btReplacePrev.PerformClick();
                else
                    btReplace.PerformClick();
            if (e.KeyChar == '\x1b')
            {
                ClearMarker();
                Hide();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) // David
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearMarker();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        private void btReplace_Click(object sender, EventArgs e)
        {
            {
                //try
                //{
                //    if (tb.SelectionLength != 0)
                //    if (!tb.Selection.ReadOnly)
                //        tb.InsertText(tbReplace.Text);
                //    btFindNext_Click(sender, null);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }

            if (FindNext(tbFind.Text, false, false))
            {
                ReplaceSelection(tbReplace.Text);
            }
            else
            {
                MessageBox.Show(this, "Not found or end of file reached.");
            }
        }

        private void btReplacePrev_Click(object sender, EventArgs e)
        {
            if (FindPrevious(tbFind.Text, false, false))
            {
                ReplaceSelection(tbReplace.Text);
            }
            else
            {
                MessageBox.Show(this, "Not found or start of file reached.");
            }
        }

        private void btReplaceAll_Click(object sender, EventArgs e)
        {
            {
            //try
            //{
            //    tb.Selection.BeginUpdate();

            //    //search
            //    var ranges = FindAll(tbFind.Text);
            //    //check readonly
            //    var ro = false;
            //    foreach (var r in ranges)
            //        if (r.ReadOnly)
            //        {
            //            ro = true;
            //            break;
            //        }
            //    //replace
            //    if (!ro)
            //    if (ranges.Count > 0)
            //    {
            //        tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(tb.TextSource, ranges, tbReplace.Text));
            //        tb.Selection.Start = new Place(0, 0);
            //    }
            //    //
            //    tb.Invalidate();
            //    MessageBox.Show(this, ranges.Count + " occurrence(s) replaced.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //tb.Selection.EndUpdate();
            }

            ReplaceAll(tbFind.Text, tbReplace.Text);
        }

        protected virtual void ReplaceSelection(string text)
        {
            if (!tb.Selection.IsEmpty && !tb.Selection.ReadOnly)
            {
                tb.SelectedText = text;
            }
        }

        protected virtual void ReplaceAll(string pattern, string text)
        {
            tb.Selection.BeginUpdate();

            RegexOptions opt = RegexOptions.Multiline;
            if (!cbMatchCase.Checked) opt |= RegexOptions.IgnoreCase;
            if (!cbRegex.Checked) pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked) pattern = $@"\b{pattern}\b";

            Range range = tb.Selection.IsEmpty ? tb.Range.Clone() : tb.Selection.Clone();
            List<Range> ranges = new List<Range>();

            foreach (Range r in range.GetRanges(pattern, opt))
            {
                if (!r.ReadOnly) ranges.Add(r);
            }

            tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(tb.TextSource, new List<Range>(ranges), text));

            tb.Invalidate();
            tb.Selection.EndUpdate();

            MessageBox.Show(this, $"{ranges.Count} occurrance(s) replaced.");

        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        void ResetSerach()
        {
            ClearMarker();
            //firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }
    }
}

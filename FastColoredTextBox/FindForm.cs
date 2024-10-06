
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace FastColoredTextBoxNS
{
    public partial class FindForm : Form
    {
        //bool firstSearchFwd = true;
        //bool firstSearchBack = true;
        //Place startPlace;
        FastColoredTextBox tb;
        bool markerNeedsClearing;

        private Place TbEnd => new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);

        protected MarkerStyle marker = new MarkerStyle(new SolidBrush(Color.FromArgb(0x7F, Color.Yellow)));

        public FindForm(FastColoredTextBox tb)
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

        public virtual void FindNext(string pattern, bool recursive)
        {
            {
            //try
            //{
            //    RegexOptions opt = RegexOptions.Multiline;
            //    if (!cbMatchCase.Checked ) 
            //        opt |= RegexOptions.IgnoreCase;
            //    if (!cbRegex.Checked)
            //        pattern = Regex.Escape(pattern);
            //    if (cbWholeWord.Checked)
            //        pattern = "\\b" + pattern + "\\b";

            //    //
            //    Range range = tb.Selection.Clone();
            //    range.Normalize();
            //    //
            //    if (firstSearchFwd)
            //    {
            //        startPlace = range.Start;
            //        firstSearchFwd = false;
            //    }
            //    //
            //    range.Start = range.End;
            //    if (range.Start >= startPlace)
            //        range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
            //    else
            //        range.End = startPlace;
            //    //
            //    foreach (var r in range.GetRangesByLines(pattern, opt))
            //    {
            //        tb.Selection = r;
            //        tb.DoSelectionVisible();
            //        tb.Invalidate();
            //        return;
            //    }
            //    //
            //    if (range.Start >= startPlace && startPlace > Place.Empty)
            //    {
            //        tb.Selection.Start = new Place(0, 0);
            //        FindNext(pattern);
            //        return;
            //    }
            //    MessageBox.Show(this, "Not found or end of file reached.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            }

            RegexOptions opt = RegexOptions.Multiline;
            if (!cbMatchCase.Checked) opt |= RegexOptions.IgnoreCase;
            if (!cbRegex.Checked)     pattern = Regex.Escape(pattern);
            if (cbWholeWord.Checked)  pattern = $@"\b{pattern}\b";

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
            }
            else if (recursive)
            {
                tb.GoHome();

                FindNext(pattern, false);
            }
            else
            {
                MessageBox.Show(this, "Not found.");
            }
        }

        private void btFindPrev_Click(object sender, EventArgs e)
        {
            FindPrevious(tbFind.Text, true);
        }

        public virtual void FindPrevious(string pattern, bool recursive)
        {
            {
            //try
            //{
            //    RegexOptions opt = RegexOptions.Multiline;
            //    if (!cbMatchCase.Checked)
            //        opt |= RegexOptions.IgnoreCase;
            //    if (!cbRegex.Checked)
            //        pattern = Regex.Escape(pattern);
            //    if (cbWholeWord.Checked)
            //        pattern = "\\b" + pattern + "\\b";

            //    //
            //    Range range = tb.Selection.Clone();
            //    range.Normalize();
            //    //
            //    if (firstSearchBack)
            //    {
            //        startPlace = range.End;
            //        firstSearchBack = false;
            //    }
            //    //
            //    var start = range.Start;
            //    if (range.Start <= startPlace)
            //        range.Start = new Place(0, 0);
            //    else
            //        range.Start = startPlace;
            //    range.End = start;
            //    //
            //    foreach (var r in range.GetRangesByLinesReversed(pattern, opt))
            //    {
            //        tb.Selection = r;
            //        tb.DoSelectionVisible();
            //        tb.Invalidate();
            //        return;
            //    }
            //    //
            //    var tbEnd = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
            //    if (range.Start <= startPlace && startPlace < tbEnd)
            //    {
            //        tb.Selection.Start = tbEnd;
            //        FindPrevious(pattern);
            //        return;
            //    }
            //    MessageBox.Show(this, "Not found or start of file reached.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            }

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
            }
            else if (recursive)
            {
                tb.GoEnd();

                FindPrevious(pattern, false);
            }
            else
            {
                MessageBox.Show(this, "Not found.");
            }
        }

        private void btFindAll_Click(object sender, EventArgs e)
        {

            FindAll(tbFind.Text);
        }

        public virtual void FindAll(string pattern)
        {
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
            if (!markerNeedsClearing) { return; }
            tb.Range.ClearStyle(marker);
            markerNeedsClearing = false;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (ModifierKeys  == Keys.Shift)
                    btFindPrev.PerformClick();
                else
                    btFindNext.PerformClick();
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                ClearMarker();
                Hide();
                e.Handled = true;
                return;
            }
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearMarker();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        void ResetSerach()
        {
            ClearMarker();
            //firstSearchFwd = true;
            //firstSearchBack = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }
    }
}

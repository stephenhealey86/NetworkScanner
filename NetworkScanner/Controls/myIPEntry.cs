using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NetworkScanner
{
    class myIPEntry : TextBox
    {
        Brush DefaultForeground;

        public myIPEntry()
        {
            DefaultForeground = Foreground;
        }

        /// <summary>
        /// Only allow certain keys
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            var key = e.Key;
            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9))
            {
                if (SelectionStart == Text.Length)
                {
                    /// If at end of ip address stop entry
                    if (Text[SelectionStart - 1] != '.' && Text[SelectionStart - 2] != '.' && Text[SelectionStart - 3] != '.')
                    {
                        e.Handled = true;
                    }
                }
                return;
            }
            else if (key == Key.OemPeriod || key == Key.Decimal)
            {
                GetNextSection();
                e.Handled = true;
                return;
            }
            else if (key == Key.Delete || key == Key.Back)
            {
                /// Delete selction if not containing deicmal
                if (SelectionLength >= 1)
                {
                    for (int i = SelectionStart; i < SelectionStart + SelectionLength; i++)
                    {
                        if (Text[i] == '.')
                        {
                            e.Handled = true;
                            break;
                        }
                    }
                    return;
                }
                // Delete single if not decimal
                var backSelection = SelectionStart == 0 ? SelectionStart : SelectionStart - 1;
                if (Text[backSelection] == '.' && key == Key.Back)
                {
                    e.Handled = true;
                }
                else if (Text.Length != SelectionStart && Text[SelectionStart] == '.' && key == Key.Delete)
                {
                    e.Handled = true;
                }
                return;
            }
            else if (key == Key.Left || key == Key.Right)
            {
                return;
            }
            else if (key == Key.Tab || key == Key.Enter)
            {
                return;
            }

            e.Handled = true;
        }

        private void GetNextSection()
        {
            var cursorStart = SelectionStart;
            bool firstDecimal = false;
            for (int i = cursorStart; i < Text.Length; i++)
            {
                if (Text[i] == '.' && !firstDecimal)
                {
                    SelectionStart = i + 1;
                    firstDecimal = true;
                    continue;
                }
                if (Text[i] == '.' && firstDecimal)
                {
                    SelectionLength = i - SelectionStart;
                    break;
                }
                SelectionLength = Text.Length - SelectionStart;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            /// Highlight first section
            SelectionStart = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '.')
                {
                    SelectionLength = i;
                    break;
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            // If section is three digits long select next section
            if (SelectionStart >= 3 && SelectionStart < Text.Length)
            {
                if (Text[SelectionStart] == '.' && Text[SelectionStart - 1] != '.' && Text[SelectionStart - 2] != '.' && Text[SelectionStart - 3] != '.')
                {
                    GetNextSection();
                }
            }

            // Compare new text
            if (!Regex.IsMatch(Text, @"\b(?:(?:2(?:[0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9])\.){3}(?:(?:2([0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9]))\b"))
            {
                Foreground = Brushes.Red;
            }
            else
            {
                Foreground = DefaultForeground;
            }
        }
    }
}

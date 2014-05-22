using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        private const string EQUALS_OP = "Equals";
        private const string SUBTRACT_OP = "Subtract";
        private const decimal EPSILON = 0.0000000001M;

        public Calculator()
        {
            InitializeComponent();

            // Establish events
            // Some of these probably could have been
            // done using the designer, but what's the
            // fun in doing that?
            btnClear.Click += btnClear_Click;
            btn0.Click += btnNumeric_Add;
            btn1.Click += btnNumeric_Add;
            btn2.Click += btnNumeric_Add;
            btn3.Click += btnNumeric_Add;
            btn4.Click += btnNumeric_Add;
            btn5.Click += btnNumeric_Add;
            btn6.Click += btnNumeric_Add;
            btn7.Click += btnNumeric_Add;
            btn8.Click += btnNumeric_Add;
            btn9.Click += btnNumeric_Add;
            btnDecimal.Click += btnNumeric_Add;
            btnDelete.Click += btnDelete_Click;
            btnAdd.Click += btnOperation_Add;
            btnSubtract.Click += btnOperation_Add;
            btnMultiply.Click += btnOperation_Add;
            btnDivide.Click += btnOperation_Add;
            btnEquals.Click += btnOperation_Add;
            btnMemoryClear.Click += btnMemoryClear_Click;
            btnMemoryRecall.Click += btnMemoryRecall_Click;
            btnMemoryAdd.Click += btnMemory_Operation;
            btnMemorySubtract.Click += btnMemory_Operation;
            txtCurrentValue.KeyPress += txtCurrentValue_KeyPress;
        }

        private void txtCurrentValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Does not allow a keypress if the keypress is not a control, digit or decimal
            // and disallows multiple decimals
            e.Handled =
                (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                ||
                (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') >= 0);
        }

        private void btnNumeric_Add(object sender, EventArgs e)
        {
            if (txtCurrentValue.Text.Length >= 9)
            {
                MessageBox.Show(
                    "Numbers may not exceed 9 digits", 
                    "Max length problem", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
                return;
            }

            var valueToAdd = ((Button)sender).Text.Trim();

            if (StartNewEntry || Math.Abs(GetDecimalValue()) < EPSILON)
                txtCurrentValue.Text = "";

            txtCurrentValue.Text += valueToAdd;

            StartNewEntry = false;
            OperationNewlySet = false;
        }

        private void btnOperation_Add(object sender, EventArgs e)
        {
            // These are needed to set the proper operation
            var operation = ((Button)sender).Name.Substring(3);

            // If the operation is being clicked again without 
            // first having another number input, e.g. someone
            // clicks add then clicks multiply, this will be
            // a replacement operation
            if (OperationNewlySet)
            {
                ActiveOperation = operation;
                return;
            }

            var value = GetDecimalValue();
            
            // The selected operation to compute was already clicked
            // in the last event. We need to complete that operation
            // before moving on to the next one
            switch (ActiveOperation)
            {
                case "Add":
                    RunningValue += value;
                    break;
                case "Subtract":
                    RunningValue -= value;
                    break;
                case "Multiply":
                    RunningValue *= value;
                    break;
                case "Divide":
                    RunningValue /= value;
                    break;
                default:
                    RunningValue = value;
                    break;
            }

            txtCurrentValue.Text = RunningValue.ToString();

            // At this point, we need to reset
            // the active operation to the newly selected
            // operation
            if (!EQUALS_OP.Equals(operation, StringComparison.OrdinalIgnoreCase))
            {
                ActiveOperation = operation;
                OperationNewlySet = true;
            }
            else
            {
                ActiveOperation = string.Empty;
            }

            // We set StartNewEntry so that the current value 
            // textbox is cleared on the next keystroke
            StartNewEntry = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCurrentValue.Text))
            {
                txtCurrentValue.Text =
                    txtCurrentValue.Text.Substring(0, txtCurrentValue.Text.Length - 1);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCurrentValue.Text = "";
            RunningValue = 0;
            StartNewEntry = true;
            OperationNewlySet = false;
        }

        private void btnMemoryClear_Click(object sender, EventArgs e)
        {
            Memory = 0;
            lblMemorySet.Visible = false;
        }

        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            txtCurrentValue.Text = Memory.ToString();
            StartNewEntry = true;
        }

        private void btnMemory_Operation(object sender, EventArgs e)
        {
            var value = GetDecimalValue();

            var btn = (Button)sender;
            if (btn.Name.Contains(SUBTRACT_OP))
                value *= -1;

            Memory += value; // Subtracts will be negative
            lblMemorySet.Visible = true;
            StartNewEntry = true;
        }

        private decimal GetDecimalValue()
        {
            decimal value;
            decimal.TryParse(txtCurrentValue.Text, out value);

            return value;
        }

        public decimal Memory { get; set; }
        public bool StartNewEntry { get; set; }
        public decimal RunningValue { get; set; }
        public string ActiveOperation { get; set; }
        public bool OperationNewlySet { get; set; }
    }
}

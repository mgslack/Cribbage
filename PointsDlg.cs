using System;
using System.Windows.Forms;

/*
 * Class defines the dialog used to get the points for a cribbage or crib
 * hand from the player (human).
 * 
 * Author:  M. G. Slack
 * Written: 2014-01-25
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd - XXXX.
 *
 */
namespace Cribbage
{
    public partial class PointsDlg : Form
    {
        private bool _cribPoints = false;
        public bool CribPoints { set { _cribPoints = value; } }

        private int _actualPoints = 0;
        public int ActualPoints { set { _actualPoints = value; } }

        private int _enteredPoints = 0;
        public int EnteredPoints { get { return _enteredPoints; } }

        // --------------------------------------------------------------------

        public PointsDlg()
        {
            InitializeComponent();
        }

        private void PointsDlg_Load(object sender, EventArgs e)
        {
            if (_cribPoints) this.Text = "Cribbage Crib Points";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _enteredPoints = Convert.ToInt32(numUpDown.Value);
            if (_enteredPoints > _actualPoints) {
                MessageBox.Show("Too many points for that hand! Try again.", "PointsDlg Error");
                _enteredPoints = 0;
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
        }
    }
}

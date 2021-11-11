using System;
using System.Windows.Forms;
using PlayingCards;

/*
 * Primary class defines the partial class of the options dialog for the
 * Cribbage game.
 *
 * Author:  M. G. Slack
 * Written: 2014-01-22
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-02-01 - Added option to turn on/off match points instead of
 *                       games won.
 *          2014-02-03 - Added a couple of additional match point options to
 *                       set the match points needed to win and if double/triple
 *                       skunk points are scored.
 *
 */
namespace Cribbage
{
    public partial class OptionsWin : Form
    {
        private CardBacks _cardBack = CardBacks.Spheres;
        public CardBacks CardBack { get { return _cardBack; } set { _cardBack = value; } }

        private bool _promptOn = true;
        public bool PromptOn { get { return _promptOn; } set { _promptOn = value; } }

        private bool _alwaysStart = false;
        public bool AlwaysStart { get { return _alwaysStart; } set { _alwaysStart = value; } }

        private bool _soundsOn = true;
        public bool SoundsOn { get { return _soundsOn; } set { _soundsOn = value; } }

        private bool _matchPoint = false;
        public bool MatchPointPlay { get { return _matchPoint; } set { _matchPoint = value; } }

        private int _matchPointsWin = 3;
        public int MatchPointsForWin { get { return _matchPointsWin; } set { _matchPointsWin = value; } }

        private bool _scoreDTSkunk = false;
        public bool ScoreDoubleTripleSkunk { get { return _scoreDTSkunk; } set { _scoreDTSkunk = value; } }

        private bool _verbose = false;
        public bool Verbose { get { return _verbose; } set { _verbose = value; } }

        private PlayingCardImage _images = null;
        public PlayingCardImage Images { set { _images = value; } }

        public OptionsWin()
        {
            InitializeComponent();
        }

        private void OptionsWin_Load(object sender, EventArgs e)
        {
            int idx = 0;
            
            cbPrompts.Checked = _promptOn;
            cbAlwaysStart.Checked = _alwaysStart;
            cbSoundsOn.Checked = _soundsOn;
            cbMatchPoint.Checked = _matchPoint;
            udWinPoints.Value = _matchPointsWin;
            cbDTSkunk.Checked = _scoreDTSkunk;
            // cbVerbose.Checked = _verbose;

            foreach (string name in Enum.GetNames(typeof(CardBacks))) {
                cbImage.Items.Add(name);
            }

            foreach (int val in Enum.GetValues(typeof(CardBacks))) {
                if (val == (int) _cardBack) {
                    idx = (int) _cardBack - (int) CardBacks.Spheres;
                }
            }
            cbImage.SelectedIndex = idx;

            if (_images != null) {
                pbBack.Image = _images.GetCardBackImage(_cardBack);
            }
        }

        private void cbPrompts_CheckedChanged(object sender, EventArgs e)
        {
            _promptOn = cbPrompts.Checked;
        }

        private void cbAlwaysStart_CheckedChanged(object sender, EventArgs e)
        {
            _alwaysStart = cbAlwaysStart.Checked;
        }

        private void cbSoundsOn_CheckedChanged(object sender, EventArgs e)
        {
            _soundsOn = cbSoundsOn.Checked;
        }

        private void cbMatchPoint_CheckedChanged(object sender, EventArgs e)
        {
            _matchPoint = cbMatchPoint.Checked;
            if (_matchPoint) {
                udWinPoints.Enabled = true;
                cbDTSkunk.Enabled = true;
            }
            else {
                udWinPoints.Enabled = false;
                cbDTSkunk.Enabled = false;
            }
        }

        private void udWinPoints_ValueChanged(object sender, EventArgs e)
        {
            _matchPointsWin = (int) udWinPoints.Value;
        }

        private void cbDTSkunk_CheckedChanged(object sender, EventArgs e)
        {
            _scoreDTSkunk = cbDTSkunk.Checked;
        }

        private void cbVerbose_CheckedChanged(object sender, EventArgs e)
        {
            // _verbose = cbVerbose.Checked;
        }

        private void cbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cardBack = (CardBacks) (cbImage.SelectedIndex + (int) CardBacks.Spheres);
            if (_images != null) {
                pbBack.Image = _images.GetCardBackImage(_cardBack);
            }
        }
    }
}

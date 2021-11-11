using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PlayingCards;
using CribbageEng;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using GameStatistics;

/*
 * Primary class defines the partial class of the main window for the
 * game of Cribbage.
 *
 * Author:  M. G. Slack
 * Written: 2014-01-21
 * Version: 1.0.3.0
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-02-01 - Added code to do 'match point' play (instead of just
 *                       showing games won). This is a toggle-able option.
 *          2014-02-03 - Expanded the support for match points including a
 *                       win dialog for winning the match based on match points
 *                       set in the options dialog.
 *          2014-03-21 - Added on GameStatistics library (assembly) to track
 *                       games won/lost/started along with a few custom
 *                       statistics (most points in hand (player/computer)
 *                       along with most points in crib).
 *
 */
namespace Cribbage
{
    public partial class MainWin : Form
    {
        #region Constants
        private const string HTML_HELP_FILE = "Cribbage_help.html";
        private const string DISCARD = "Discard";
        private const string KEEP = "Keep";
        private const string PLAY = "Play";
        private const string ONE_POINT = " gets one point for last card!";
        private const string MATCH_POINT_LABEL = "Match Points:";
        // custom statistics tracking
        private const string CSTAT_MATCHES_WON = "Matches Won";
        private const string CSTAT_MATCHES_LOST = "Matches Lost";
        private const string CSTAT_MOST_POINTS_HAND = "Most Points in Players Hand";
        private const string CSTAT_MOST_COMP_POINTS = "Most Points in Computers Hand";
        private const string CSTAT_MOST_POINTS_CRIB = "Most Points in Crib Hand";
        private const string CSTAT_MOST_POINTS_PLAY = "Most Points Made During Play";
        #endregion

        #region Registry Constants
        private const string REG_NAME = @"HKEY_CURRENT_USER\Software\Slack and Associates\Games\Cribbage";
        private const string REG_KEY1 = "PosX";
        private const string REG_KEY2 = "PosY";
        private const string REG_KEY3 = "CardBack";
        private const string REG_KEY4 = "PromptsOn";
        private const string REG_KEY5 = "AlwaysStarts";
        private const string REG_KEY6 = "SoundsOn";
        private const string REG_KEY7 = "VerboseMode";
        private const string REG_KEY8 = "MatchPointPlay";
        private const string REG_KEY9 = "MatchPointsForWin";
        private const string REG_KEYA = "MatchScoreDTSkunk";
        #endregion

        #region Playing Card Instances
        // card deck and images
        private CardDeck cards = new CardDeck();
        private PlayingCardImage images = new PlayingCardImage();
        private CardBacks cardBack = CardBacks.Spheres;
        private CardPlaceholders placeholder = CardPlaceholders.Gray;

        // card hand instances needed by the game (assume 2 player board)
        private CardHand[] hands = new CardHand[CribbageBoard.MAX_PLAYERS];      // card hands (6 slots) - original deal
        private CardHand[] playHands = new CardHand[CribbageBoard.MAX_PLAYERS];  // card hands to play from (4 slots)
        private CardHand crib = new CardHand(CribbageEngine.CRIBBAGE_CRIB_SIZE); // crib cards (4)
        #endregion

        #region Other Instances/Variables
        private CribbageEngine cribEng = new CribbageEngine();
        private PlayingCard upCard = PlayingCard.EMPTY_CARD;
        private bool cribFlag = true; // true = players crib, false = computers crib
        private PictureBox[] cardImgs = new PictureBox[6];
        private Button[] cardBtns = new Button[6];
        private int discardCount = 0, handTally = 0, playerGamesWon = 0, compGamesWon = 0;
        private bool goFlagP = false, goFlagC = false, scoreLastPoint = true, scoreDTSkunk = false;
        private bool gameOver = true, pPlayedLast = false, matchWon = true, matchPointPlay = false;
        private bool promptOn = true, alwaysStart = false, soundsOn = true, verbose = false, X31 = false;
        private string svGWLabel = "";
        private int winningMatchPoints = 3;
        private Statistics stats = new Statistics(REG_NAME);
        #endregion

        #region Custom Events
        private event EventHandler CompsPlay;
        private event EventHandler HumsPlay;
        private event EventHandler ScoreHand;
        #endregion

        // --------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        #region Private Methods
        private void DoEvent(EventHandler handler)
        {
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void LoadRegistryValues()
        {
            int winX = -1, winY = -1, cardB = (int) CardBacks.Spheres;
			string tempBool = "";

            try {
                winX = (int) Registry.GetValue(REG_NAME, REG_KEY1, winX);
                winY = (int) Registry.GetValue(REG_NAME, REG_KEY2, winY);
                cardB = (int) Registry.GetValue(REG_NAME, REG_KEY3, cardB);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEY4, "True");
                if (tempBool != null) promptOn = Convert.ToBoolean(tempBool);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEY5, "False");
                if (tempBool != null) alwaysStart = Convert.ToBoolean(tempBool);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEY6, "True");
                if (tempBool != null) soundsOn = Convert.ToBoolean(tempBool);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEY7, "False");
                if (tempBool != null) verbose = Convert.ToBoolean(tempBool);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEY8, "False");
                if (tempBool != null) matchPointPlay = Convert.ToBoolean(tempBool);
                winningMatchPoints = (int) Registry.GetValue(REG_NAME, REG_KEY9, winningMatchPoints);
                tempBool = (string) Registry.GetValue(REG_NAME, REG_KEYA, "False");
                if (tempBool != null) scoreDTSkunk = Convert.ToBoolean(tempBool);
            }
            catch (Exception ex) { /* ignore, go with defaults */ }

            if ((winX != -1) && (winY != -1)) this.SetDesktopLocation(winX, winY);
            if (Enum.IsDefined(typeof(CardBacks), cardB)) cardBack = (CardBacks) cardB;
            cribBoard.SoundOn = soundsOn;
        }

        private void ShowMatchPointLabels()
        {
            lblGW.Text = MATCH_POINT_LABEL;
            lblWinPts.Text = "(Win = " + Convert.ToString(winningMatchPoints) + " MP)";
            lblWinPts.Visible = true;
        }

        private void WriteRegistryValues()
        {
            Registry.SetValue(REG_NAME, REG_KEY3, (int) cardBack);
            Registry.SetValue(REG_NAME, REG_KEY4, promptOn);
            Registry.SetValue(REG_NAME, REG_KEY5, alwaysStart);
            Registry.SetValue(REG_NAME, REG_KEY6, soundsOn);
            Registry.SetValue(REG_NAME, REG_KEY7, verbose);
            Registry.SetValue(REG_NAME, REG_KEY8, matchPointPlay);
            Registry.SetValue(REG_NAME, REG_KEY9, winningMatchPoints);
            Registry.SetValue(REG_NAME, REG_KEYA, scoreDTSkunk);
            // reset game components to new values
            if (upCard == PlayingCard.EMPTY_CARD)
                pbUpCard.Image = images.GetCardBackImage(cardBack);
            cribBoard.SoundOn = soundsOn;
            if (matchPointPlay)
                ShowMatchPointLabels();
            else {
                lblGW.Text = svGWLabel;
                lblWinPts.Visible = false;
            }
        }

        private void SetupContextMenu()
        {
            ContextMenu mnu = new ContextMenu();
            MenuItem mnuStats = new MenuItem("Game Statistics");
            MenuItem sep = new MenuItem("-");
            MenuItem mnuHelp = new MenuItem("Help");
            MenuItem mnuAbout = new MenuItem("About");

            mnuStats.Click += new EventHandler(mnuStats_Click);
            mnuHelp.Click += new EventHandler(mnuHelp_Click);
            mnuAbout.Click += new EventHandler(mnuAbout_Click);
            mnu.MenuItems.AddRange(new MenuItem[] { mnuStats, sep, mnuHelp, mnuAbout });
            this.ContextMenu = mnu;
        }

        private void ResetPlayerCardImages()
        {
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++)
                cardImgs[i].Image = images.GetCardPlaceholderImage(placeholder);
        }

        private void InitControlsAndEvents()
        {
            cardImgs[0] = pbCard1; cardImgs[1] = pbCard2; cardImgs[2] = pbCard3;
            cardImgs[3] = pbCard4; cardImgs[4] = pbCard5; cardImgs[5] = pbCard6;
            cardBtns[0] = btnCard1; cardBtns[1] = btnCard2; cardBtns[2] = btnCard3;
            cardBtns[3] = btnCard4; cardBtns[4] = btnCard5; cardBtns[5] = btnCard6;
            CompsPlay += customCompsPlay;
            HumsPlay += customHumsPlay;
            ScoreHand += customScoreHand;
            ResetPlayerCardImages();
            pbUpCard.Image = images.GetCardBackImage(cardBack);
            for (int i = 0; i < CribbageBoard.MAX_PLAYERS; i++) {
                hands[i] = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE);
                playHands[i] = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD);
            }
            playedCanvas.Images = images;
            svGWLabel = lblGW.Text;
            if (matchPointPlay) ShowMatchPointLabels();
        }

        private bool StartNew()
        {
            bool ret = true;

            if ((!gameOver) && (promptOn)) {
                DialogResult res = MessageBox.Show("Current game not finished, really start new?", this.Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                ret = (res == DialogResult.Yes);
            }

            return ret;
        }

        private void ResetGame()
        {
            cribBoard.ResetBoard();
            gameOver = false;
            lblCompScore.Text = "0"; lblHumScore.Text = "0";
            lblCrib.BackColor = Color.Gray;
            if (matchWon) {
                lblHumWon.Text = "0"; playerGamesWon = 0;
                lblCompWon.Text = "0"; compGamesWon = 0;
                matchWon = false;
            }
        }

        private void DrawForCrib()
        {
            Random rnd = new Random();

            if (alwaysStart)
                cribFlag = true;
            else
                cribFlag = (rnd.Next(100) >= 50);

            if (cribFlag)
                MessageBox.Show("Player gets first crib.", this.Text);
            else
                MessageBox.Show("Computer gets first crib.", this.Text);
        }

        private void ClearPlayedCards()
        {
            handTally = 0; goFlagP = false; goFlagC = false;
            lblPlayedTally.Text = "0";
            playedCanvas.Clear();
        }

        private void StartHand()
        {
            ClearPlayedCards();
            pbUpCard.Image = images.GetCardBackImage(cardBack);
            upCard = PlayingCard.EMPTY_CARD;
            cards.Shuffle();
            if (cribFlag)
                lblCrib.BackColor = Color.Red;
            else
                lblCrib.BackColor = Color.Blue;
            scoreLastPoint = true; discardCount = 0;
            // clear out card hands
            crib.RemoveAll();
            for (int i = 0; i < CribbageBoard.MAX_PLAYERS; i++) {
                hands[i].RemoveAll();
                playHands[i].RemoveAll();
            }
            // deal
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++) {
                hands[(int) Players.Computer].Add(cards.GetNextCard());
                hands[(int) Players.Human].Add(cards.GetNextCard());
                cardBtns[i].Text = DISCARD;
                cardBtns[i].Visible = true;
                cardBtns[i].Enabled = true;
            }
            // show players (human) cards
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++)
                cardImgs[i].Image = images.GetCardImage(hands[(int) Players.Human].CardAt(i));
        }

        private void ProcessPlay(Button btn, int idx)
        {
            if (handTally + playHands[(int) Players.Human].CardAt(idx).GetCardPointValueFace10() > 31) {
                MessageBox.Show("That totals more than 31, try again.", this.Text);
                return;
            }
            btn.Visible = false;
            cardImgs[idx].Image = images.GetCardPlaceholderImage(placeholder);
            DoThePlay(Players.Human, idx);
            if ((!gameOver) && (!goFlagC)) DoEvent(CompsPlay);
        }

        private void ProcessDiscard(Button btn, string txt, int idx)
        {
            if (txt == DISCARD) {
                discardCount++;
                btn.Text = KEEP;
            }
            else {
                discardCount--;
                btn.Text = DISCARD;
            }
            if (discardCount == 2)
                btnContinue.Enabled = true;
            else
                btnContinue.Enabled = false;
        }

        private void DoDiscard(int who, int[] what)
        {
            crib.Add(hands[who].Remove(what[0]));
            crib.Add(hands[who].Remove(what[1]));
            hands[who].CompressHand();
        }

        private string GetMPMsg(string player, Players opponent)
        {
            bool skunk = (cribBoard.GetScore(opponent) < 91);
            string msg = player + " wins and gets match point";
            int skunkPoints = 0;

            if (skunk) {
                if ((scoreDTSkunk) && (cribBoard.GetScore(opponent) < 31)) {
                    msg += " plus 7 match points for a triple skunk";
                    skunkPoints = 7;
                }
                else if ((scoreDTSkunk) && (cribBoard.GetScore(opponent) < 61)) {
                    msg += " plus 3 match points for a double skunk";
                    skunkPoints = 3;
                }
                else {
                    msg += " plus 1 skunk point";
                    skunkPoints = 1;
                }
            }
            msg += "!";

            if (opponent == Players.Computer)
                playerGamesWon += skunkPoints;
            else
                compGamesWon += skunkPoints;

            return msg;
        }

        private void CheckMatchWon(Players whoWon)
        {
            int match = (whoWon == Players.Computer) ? compGamesWon : playerGamesWon;
            string player = (whoWon == Players.Computer) ? "Computer" : "Player";

            if (match >= winningMatchPoints) {
                string msg = player + " has won the match with " + Convert.ToString(match) + " points.";
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (whoWon == Players.Human)
                    stats.IncCustomStatistic(CSTAT_MATCHES_WON);
                else
                    stats.IncCustomStatistic(CSTAT_MATCHES_LOST);
                stats.SaveStatistics();
                // setup to reset match play now
                matchWon = true;
            }
        }

        private void DisplayWonMsg(Players whoWon)
        {
            string wmpMsg = "";

            if (whoWon == Players.Human) {
                playerGamesWon++;
                if (matchPointPlay)
                    wmpMsg = GetMPMsg("Player", Players.Computer);
                else
                    wmpMsg = "Player has won!";
                lblHumWon.Text = Convert.ToString(playerGamesWon);
                stats.GameWon(0);
            }
            else {
                compGamesWon++;
                if (matchPointPlay)
                    wmpMsg = GetMPMsg("Computer", Players.Human);
                else
                    wmpMsg = "Computer has won!";
                lblCompWon.Text = Convert.ToString(compGamesWon);
                stats.GameLost(0);
            }
            MessageBox.Show(wmpMsg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (matchPointPlay) CheckMatchWon(whoWon);
        }

        private void FinishGame(Players whoWon)
        {
            DisplayWonMsg(whoWon);
            btnContinue.Enabled = false;
            btnGo.Enabled = false;
            lblCrib.BackColor = Color.Gray;
            pbUpCard.Image = images.GetCardBackImage(cardBack);
            upCard = PlayingCard.EMPTY_CARD;
            ResetPlayerCardImages();
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++)
                if (cardBtns[i].Visible) cardBtns[i].Visible = false;
            ClearPlayedCards();
        }

        private void MovePeg(Players player, int amount)
        {
            gameOver = cribBoard.MovePeg(player, amount);
            if (player == Players.Computer)
                lblCompScore.Text = Convert.ToString(cribBoard.GetScore(player));
            else
                lblHumScore.Text = Convert.ToString(cribBoard.GetScore(player));
            if (gameOver) FinishGame(player);
        }

        private void GetUpCard()
        {
            upCard = cards.GetNextCard();
            pbUpCard.Image = images.GetCardImage(upCard);
            if (upCard.CardValue == CardValue.Jack) {
                string who = (cribFlag) ? "Player" : "Computer";
                MessageBox.Show(who + " gets 2 points for Jack.", this.Text);
                if (cribFlag)
                    MovePeg(Players.Human, 2);
                else
                    MovePeg(Players.Computer, 2);
            }
        }

        private bool IsHandOver()
        {
            bool done = false;

            if ((playHands[(int) Players.Computer].CurNumCardsInHand == 0) &&
                (playHands[(int) Players.Human].CurNumCardsInHand == 0)) {
                done = true;
                DoEvent(ScoreHand);
            }

            return done;
        }

        private void EnablePlayButtons(bool enab)
        {
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++)
                if (cardBtns[i].Visible) cardBtns[i].Enabled = enab;
        }

        private void DoOneMove()
        {
            MessageBox.Show("Computer" + ONE_POINT, this.Text);
            MovePeg(Players.Computer, 1);
            ClearPlayedCards();
        }

        private void Play_Card(Players player, int cardIdx)
        {
            int who = (int) player;
            int t = playHands[who].CardAt(cardIdx).GetCardPointValueFace10();

            pPlayedLast = (player == Players.Human);
            playedCanvas.PlayedCards.Add(playHands[who].Remove(cardIdx));
            handTally += t;
            lblPlayedTally.Text = Convert.ToString(handTally);
            playedCanvas.Invalidate();
        }

        private void DoThePlay(Players player, int cardIdx)
        {
            List<string> messages = new List<string>(4);
            int k = 0;
            string who = (player == Players.Computer) ? "Computer" : "Player";

            Play_Card(player, cardIdx);
            k = cribEng.PointsInGame(playedCanvas.PlayedCards, messages);
            if (messages.Count > 0) {
                foreach (string s in messages) MessageBox.Show(who + s, this.Text);
            }
            if (k > 0) MovePeg(player, k);
            if (handTally == 31) {
                X31 = true;
                ClearPlayedCards();
            }
            else
                X31 = false;
            if (k > stats.CustomStatistic(CSTAT_MOST_POINTS_PLAY))
                stats.SetCustomStatistic(CSTAT_MOST_POINTS_PLAY, k);
        }

        private void Show_Cards(CardHand hnd)
        {
            // display cards for viewing or scoring by player
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                cardBtns[i].Visible = false;
                cardImgs[i].Image = images.GetCardImage(hnd.CardAt(i));
            }
        }

        private int GetPointsFromDlg(bool cribFlg, int aPts)
        {
            int pts = 0;

            PointsDlg dlg = new PointsDlg();
            dlg.CribPoints = cribFlg;
            dlg.ActualPoints = aPts;
            if (dlg.ShowDialog(this) == DialogResult.OK)
                pts = dlg.EnteredPoints;
            dlg.Dispose();

            return pts;
        }

        private void Get_Points(bool cribFlg)
        {
            int pts = 0, aPts = 0, mPts = 0;

            if (cribFlg)
                aPts = cribEng.Figure_Points(crib, true, upCard);
            else
                aPts = cribEng.Figure_Points(hands[(int) Players.Human], false, upCard);
            pts = GetPointsFromDlg(cribFlg, aPts);
            if (pts > 0) MovePeg(Players.Human, pts); mPts = aPts - pts;
            if ((!gameOver) && (mPts > 0)) {
                // muggins! :-)
                MessageBox.Show("MUGGINS! Player missed " + Convert.ToString(mPts) + ", which computer gets.", this.Text);
                MovePeg(Players.Computer, mPts);
            }
            if ((cribFlg) && (aPts > stats.CustomStatistic(CSTAT_MOST_POINTS_CRIB)))
                stats.SetCustomStatistic(CSTAT_MOST_POINTS_CRIB, aPts);
            else if ((!cribFlg) && (aPts > stats.CustomStatistic(CSTAT_MOST_POINTS_HAND)))
                stats.SetCustomStatistic(CSTAT_MOST_POINTS_HAND, aPts);
        }

        private void ScoreCompsHand()
        {
            int pts = cribEng.Figure_Points(hands[(int) Players.Computer], false, upCard);

            Show_Cards(hands[(int) Players.Computer]);
            MessageBox.Show("Computer has " + Convert.ToString(pts) + " points.", this.Text);
            MovePeg(Players.Computer, pts);
            if (pts > stats.CustomStatistic(CSTAT_MOST_COMP_POINTS))
                stats.SetCustomStatistic(CSTAT_MOST_COMP_POINTS, pts);
        }
        #endregion

        // --------------------------------------------------------------------

        #region Event Handlers
        private void MainWin_Load(object sender, EventArgs e)
        {
            LoadRegistryValues();
            SetupContextMenu();
            InitControlsAndEvents();
            stats.GameName = this.Text;
        }

        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = DialogResult.Yes;
            string msg = "";

            if (promptOn) {
                if (!gameOver)
                    msg = "Quit the game and exit?";
                else if ((matchPointPlay) && (!matchWon))
                    msg = "Match not over, quit match and exit?";
                if (!msg.Equals(""))
                    ret = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            }
            e.Cancel = (ret != DialogResult.Yes);
        }

        private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) {
                Registry.SetValue(REG_NAME, REG_KEY1, this.Location.X);
                Registry.SetValue(REG_NAME, REG_KEY2, this.Location.Y);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (StartNew()) {
                ResetGame();
                DrawForCrib();
                StartHand();
                stats.StartGame(false);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OptionsWin opts = new OptionsWin();

            opts.Images = images;
            opts.CardBack = cardBack;
            opts.PromptOn = promptOn;
            opts.AlwaysStart = alwaysStart;
            opts.SoundsOn = soundsOn;
            opts.MatchPointPlay = matchPointPlay;
            opts.MatchPointsForWin = winningMatchPoints;
            opts.ScoreDoubleTripleSkunk = scoreDTSkunk;
            // opts.Verbose = verbose; - not settable via options dialog currently

            if (opts.ShowDialog(this) == DialogResult.OK) {
                cardBack = opts.CardBack;
                promptOn = opts.PromptOn;
                alwaysStart = opts.AlwaysStart;
                soundsOn = opts.SoundsOn;
                matchPointPlay = opts.MatchPointPlay;
                winningMatchPoints = opts.MatchPointsForWin;
                scoreDTSkunk = opts.ScoreDoubleTripleSkunk;
                // verbose = opts.Verbose;
                WriteRegistryValues();
            }

            opts.Dispose();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++)
                if ((cardBtns[i].Visible) &&
                    ((handTally + playHands[(int) Players.Human].CardAt(i).GetCardPointValueFace10()) <= 31)) {
                    MessageBox.Show("Shame! Shame! Player has a play.", this.Text);
                    return;
                }
            if (goFlagC) {
                MessageBox.Show("Player" + ONE_POINT, this.Text);
                MovePeg(Players.Human, 1);
                ClearPlayedCards();
            }
            else {
                if (playHands[(int) Players.Computer].CurNumCardsInHand > 0)
                    goFlagP = true;
                else {
                    scoreLastPoint = false;
                    MessageBox.Show("Computer" + ONE_POINT, this.Text);
                    MovePeg(Players.Computer, 1);
                    ClearPlayedCards();
                }
            }
            DoEvent(CompsPlay);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = false;

            // computers discards
            int player = (int) Players.Computer, j = 0;
            int[] dis = cribEng.AnalyzeHandForDiscards(hands[player]);

            DoDiscard(player, dis);
            lblCardsLeft.Text = Convert.ToString(hands[player].CurNumCardsInHand);
            // now humans
            player = (int) Players.Human;
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++) {
                if (cardBtns[i].Text == KEEP) dis[j++] = i;
                if (j == 2) break;
            }
            DoDiscard(player, dis);

            // copy hands to 'play' hands, need originals for end of hand scoring
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                playHands[(int) Players.Computer].Add(hands[(int) Players.Computer].CardAt(i));
                playHands[player].Add(hands[player].CardAt(i));
            }

            // show players (humans) cards
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++) {
                if (i < playHands[player].CurNumCardsInHand) {
                    cardBtns[i].Text = PLAY;
                    cardImgs[i].Image = images.GetCardImage(playHands[player].CardAt(i));
                }
                else {
                    cardBtns[i].Visible = false;
                    cardImgs[i].Image = images.GetCardPlaceholderImage(placeholder);
                }
            }

            // finish continue
            GetUpCard();
            if (!gameOver) {
                if (cribFlag)
                    DoEvent(HumsPlay);
                else
                    DoEvent(CompsPlay);
            }
        }

        private void btnPlayDisX_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int idx = Convert.ToInt32(btn.Tag);
            string txt = btn.Text;

            if (txt == PLAY)
                ProcessPlay(btn, idx);
            else
                ProcessDiscard(btn, txt, idx);
        }

        private void mnuStats_Click(object sender, EventArgs e)
        {
            stats.ShowStatistics(this);
        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            var asm = Assembly.GetEntryAssembly();
            var asmLocation = Path.GetDirectoryName(asm.Location);
            var htmlPath = Path.Combine(asmLocation, HTML_HELP_FILE);

            try {
                Process.Start(htmlPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Cannot load help: " + ex.Message, "Help Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();

            about.ShowDialog(this);
            about.Dispose();
        }
        #endregion

        #region Custom Event Handlers
        private void customCompsPlay(object sender, EventArgs e)
        {
            bool done = false;
            int cc = -1;

            if (!gameOver) {
                btnGo.Enabled = false;
                EnablePlayButtons(false);
                if (IsHandOver()) return;
                if (playHands[(int) Players.Computer].CurNumCardsInHand > 0) {
                    do {
                        cc = cribEng.GetCardToPlay(playedCanvas.PlayedCards, playHands[(int) Players.Computer]);
                        if (cc != -1) {
                            DoThePlay(Players.Computer, cc);
                            lblCardsLeft.Text = Convert.ToString(playHands[(int) Players.Computer].CurNumCardsInHand);
                        }
                        else { // process 'GO'
                            if (goFlagP)
                                DoOneMove();
                            else {
                                if (playHands[(int)Players.Human].CurNumCardsInHand > 0) {
                                    MessageBox.Show("Computer doesn't have a play, go.", this.Text);
                                    goFlagC = true;
                                }
                                else {
                                    scoreLastPoint = false;
                                    MessageBox.Show("Computer has no play, Player gets last point.", this.Text);
                                    MovePeg(Players.Human, 1);
                                    ClearPlayedCards();
                                }
                            }
                        }
                        done = ((!goFlagP) ||
                                (playHands[(int) Players.Computer].CurNumCardsInHand == 0) ||
                                (gameOver));
                    } while (!done);
                    // make sure last point is counted right...
                    if ((playHands[(int) Players.Computer].CurNumCardsInHand == 0) &&
                        (playHands[(int) Players.Human].CurNumCardsInHand > 0) && (goFlagP)) {
                        DoOneMove();
                    }
                }
                if (!gameOver) DoEvent(HumsPlay);
            }
        }

        private void customHumsPlay(object sender, EventArgs e)
        {
            if (!gameOver) {
                if (IsHandOver()) return;
                if (playHands[(int) Players.Human].CurNumCardsInHand > 0) {
                    // player has cards to play
                    btnGo.Enabled = true;
                    EnablePlayButtons(true);
                    if (verbose) MessageBox.Show("Players turn.", this.Text);
                }
                else {
                    DoEvent(CompsPlay);
                }
            }
        }

        private void customScoreHand(object sender, EventArgs e)
        {
            if (!gameOver) {
                if ((!X31) && (scoreLastPoint)) {
                    // don't get last card if score 31 with last card or go
                    if (pPlayedLast) {
                        MessageBox.Show("Player" + ONE_POINT, this.Text);
                        MovePeg(Players.Human, 1);
                    }
                    else {
                        MessageBox.Show("Computer" + ONE_POINT, this.Text);
                        MovePeg(Players.Computer, 1);
                    }
                }
                if (!gameOver) { // could win with 'last point'
                    ClearPlayedCards();
                    if (cribFlag) {  // comp scores first
                        if (verbose) MessageBox.Show("Computer will score first.", this.Text);
                        ScoreCompsHand();
                        if (!gameOver) { // now let human score
                            Show_Cards(hands[(int) Players.Human]);
                            Get_Points(false);
                            if (!gameOver) {
                                Show_Cards(crib);
                                Get_Points(true);
                            }
                        }
                    }
                    else { // human scores first
                        if (verbose) MessageBox.Show("Player will score first.", this.Text);
                        Show_Cards(hands[(int) Players.Human]);
                        Get_Points(false);
                        if (!gameOver) { // now let comp score
                            ScoreCompsHand();
                            if (!gameOver) {
                                Show_Cards(crib);
                                int pts = cribEng.Figure_Points(crib, true, upCard);
                                MessageBox.Show("Computer gets " + Convert.ToString(pts) + " points from the crib.", this.Text);
                                MovePeg(Players.Computer, pts);
                                if (pts > stats.CustomStatistic(CSTAT_MOST_POINTS_CRIB))
                                    stats.SetCustomStatistic(CSTAT_MOST_POINTS_CRIB, pts);
                            }
                        }
                    }
                    if (!gameOver) {
                        cribFlag = !cribFlag;
                        StartHand();
                    }
                }
            }
        }
        #endregion
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Media;
using System.Timers;

/*
 * Class is a user control implementing the cribbage board used.  The
 * board is a port of the one used by the cribbage game written in
 * Java.  The board is a standard, 121 point, cribbage board.
 * 
 * Author:  M. G. Slack
 * Written: 2014-01-21
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-02-02 - Changed paint event to override OnPaint.
 *
 */
namespace Cribbage
{
    /* Enum representing the two players used with this cribbage board. */
    public enum Players { Computer, Human };

    public partial class CribbageBoard : UserControl
    {
        private const string IMAGE_NAMESPACE = "Cribbage.images.";
        private const string IMAGE_EXT = ".gif";
		private const string SOUND_NAMESPACE = "Cribbage.sounds.";
		private const string SOUND_EXT = ".wav";

        /* Const representing a empty peg hole. */
        private const int CB_EMPTY = 1;
        /* Const representing a blue peg in the hole. */
        private const int CB_BLUE = 2;
        /* Const representing a red peg in the hole. */
        private const int CB_RED = 3;

        /* Const representing the maximum number of players allowed with this board. */
        public const int MAX_PLAYERS = 2;
        /* Const representing the maximum score that can be won with this board. */
        public const int MAX_SCORE = 121;

        #region Cribbage Board Static Structures

        /*
         * Static representing the board at start-up (or reset).
         *  - Blank position = 0
         *  - Empty peg (hole) = 1
         *  - Blue peg (in hole) = 2
         *  - Red peg (in hole) = 3
         *  - Arrow picture (->) in position = 4
         */
        private static readonly int[,] DEF_BOARD =
            {{0,2,0,4,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0},
             {0,2,0,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0},
             {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
             {0,3,0,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0},
             {0,3,0,4,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0}};

        /*
         * Static representing the positions to move 'pegs' to in the board structure.
         * Used by the peg move method to correctly 'move' the pegs around the board.
         * Size is [2][121][2].
         */
        private static readonly int[,,] BOARD_POS = {{{4,0},  {5,0},  {6,0},  {7,0},  {8,0},
                                                      {10,0}, {11,0}, {12,0}, {13,0}, {14,0},
                                                      {16,0}, {17,0}, {18,0}, {19,0}, {20,0},
                                                      {22,0}, {23,0}, {24,0}, {25,0}, {26,0},
                                                      {28,0}, {29,0}, {30,0}, {31,0}, {32,0},
                                                      {34,0}, {35,0}, {36,0}, {37,0}, {38,0},
                                                      {38,1}, {37,1}, {36,1}, {35,1}, {34,1},
                                                      {32,1}, {31,1}, {30,1}, {29,1}, {28,1},
                                                      {26,1}, {25,1}, {24,1}, {23,1}, {22,1},
                                                      {20,1}, {19,1}, {18,1}, {17,1}, {16,1},
                                                      {14,1}, {13,1}, {12,1}, {11,1}, {10,1},
                                                      {8,1},  {7,1},  {6,1},  {5,1},  {4,1},
                                                      {4,0},  {5,0},  {6,0},  {7,0},  {8,0},
                                                      {10,0}, {11,0}, {12,0}, {13,0}, {14,0},
                                                      {16,0}, {17,0}, {18,0}, {19,0}, {20,0},
                                                      {22,0}, {23,0}, {24,0}, {25,0}, {26,0},
                                                      {28,0}, {29,0}, {30,0}, {31,0}, {32,0},
                                                      {34,0}, {35,0}, {36,0}, {37,0}, {38,0},
                                                      {38,1}, {37,1}, {36,1}, {35,1}, {34,1},
                                                      {32,1}, {31,1}, {30,1}, {29,1}, {28,1},
                                                      {26,1}, {25,1}, {24,1}, {23,1}, {22,1},
                                                      {20,1}, {19,1}, {18,1}, {17,1}, {16,1},
                                                      {14,1}, {13,1}, {12,1}, {11,1}, {10,1},
                                                      {8,1},  {7,1},  {6,1},  {5,1},  {4,1},
                                                      {1,1}},
                                                     {{4,4},  {5,4},  {6,4},  {7,4},  {8,4},
                                                      {10,4}, {11,4}, {12,4}, {13,4}, {14,4},
                                                      {16,4}, {17,4}, {18,4}, {19,4}, {20,4},
                                                      {22,4}, {23,4}, {24,4}, {25,4}, {26,4},
                                                      {28,4}, {29,4}, {30,4}, {31,4}, {32,4},
                                                      {34,4}, {35,4}, {36,4}, {37,4}, {38,4},
                                                      {38,3}, {37,3}, {36,3}, {35,3}, {34,3},
                                                      {32,3}, {31,3}, {30,3}, {29,3}, {28,3},
                                                      {26,3}, {25,3}, {24,3}, {23,3}, {22,3},
                                                      {20,3}, {19,3}, {18,3}, {17,3}, {16,3},
                                                      {14,3}, {13,3}, {12,3}, {11,3}, {10,3},
                                                      {8,3},  {7,3},  {6,3},  {5,3},  {4,3},
                                                      {4,4},  {5,4},  {6,4},  {7,4},  {8,4},
                                                      {10,4}, {11,4}, {12,4}, {13,4}, {14,4},
                                                      {16,4}, {17,4}, {18,4}, {19,4}, {20,4},
                                                      {22,4}, {23,4}, {24,4}, {25,4}, {26,4},
                                                      {28,4}, {29,4}, {30,4}, {31,4}, {32,4},
                                                      {34,4}, {35,4}, {36,4}, {37,4}, {38,4},
                                                      {38,3}, {37,3}, {36,3}, {35,3}, {34,3},
                                                      {32,3}, {31,3}, {30,3}, {29,3}, {28,3},
                                                      {26,3}, {25,3}, {24,3}, {23,3}, {22,3},
                                                      {20,3}, {19,3}, {18,3}, {17,3}, {16,3},
                                                      {14,3}, {13,3}, {12,3}, {11,3}, {10,3},
                                                      {8,3},  {7,3},  {6,3},  {5,3},  {4,3},
                                                      {1,3}}};

        #endregion

        // images used by the board to paint itself with
        /* Board image (arrow image). */
        private Bitmap imgArrow = null;
        /* Board image (blank space image). */
        private Bitmap imgBlank = null;
        /* Board image (blue peg in space image). */
        private Bitmap imgBluePeg = null;
        /* Board image (no peg in space image). */
        private Bitmap imgEmptyPeg = null;
        /* Board image (red peg in space image). */
        private Bitmap imgRedPeg = null;
        /* SoundPlayer for 'tic' sound of peg move (if on). */
        private SoundPlayer player = null;
        /* Timer to wait for peg move with. */
        private System.Timers.Timer timer = new System.Timers.Timer(200);

        private int[,] board = new int[5, 40];
        private int[] playerScore = {0, 0};
        private int p1PegP = 120, p2PegP = 120;
        private bool timedOut = false;

        private bool _soundLoaded = false;
        private bool _soundOn = false;
        public bool SoundOn { get { return _soundOn; } set { if (_soundLoaded) _soundOn = value; } }

        // --------------------------------------------------------------------

        public CribbageBoard()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            int x = 0, y = 1;

            graphics.Clear(Color.Black);
            for (int i = 0; i < 5; i++) {
                x = 1;
                for (int j = 0; j < 40; j++) {
                    switch (board[i, j]) {
                        case 0: graphics.DrawImage(imgBlank, x, y);
                            break;
                        case 1: graphics.DrawImage(imgEmptyPeg, x, y);
                            break;
                        case 2: graphics.DrawImage(imgBluePeg, x, y);
                            break;
                        case 3: graphics.DrawImage(imgRedPeg, x, y);
                            break;
                        case 4: graphics.DrawImage(imgArrow, x, y);
                            break;
                        default: break;
                    }
                    x += imgBlank.Width + 1;
                }
                y += imgBlank.Height + 1;
            }
        }

        #endregion

        // --------------------------------------------------------------------

        #region Private Methods

        private Stream GetResourceStream(string path)
		{
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(path);
		}
		
        private Bitmap LoadImage(string name)
        {
            string path = IMAGE_NAMESPACE + name + IMAGE_EXT;
            Bitmap bitmap = null;

            try {
                Stream stream = GetResourceStream(path);
                bitmap = new Bitmap(stream);
            }
            catch (Exception e) {
                MessageBox.Show("Image (" + path + "): " + e.Message, "CribbageBoard LoadImage Error");
            }

            return bitmap;
        }

        private void LoadSoundPlayer()
        {
            string path = SOUND_NAMESPACE + "tac" + SOUND_EXT;

            try {
                player = new SoundPlayer(GetResourceStream(path));
                _soundLoaded = true;
            }
            catch (Exception e) {
                MessageBox.Show("Wav (" + path + "): " + e.Message, "CribbageBoard LoadSoundPlayer Error");
                player = null;
                _soundOn = false;
            }
        }

        private void WaitTimer()
        {
            timedOut = false;
            timer.Enabled = true;
            do { Application.DoEvents(); } while (!timedOut);
        }

        private void MovePegOneSpace(int bc, int br, int nc, int nr, int c)
        {
            // do peg move
            board[br,bc] = CB_EMPTY;
            board[nr,nc] = c;
            this.Invalidate();
            WaitTimer();
        }

        private void PlayPegSound()
        {
            if ((_soundLoaded) && (_soundOn)) {
                player.PlaySync();
            }
        }

        #endregion

        // --------------------------------------------------------------------

        #region Event Handlers

        private void CribbageBoard_Load(object sender, EventArgs e)
        {
            imgArrow = LoadImage("arrow");
            imgBlank = LoadImage("blank");
            imgBluePeg = LoadImage("bluepeg");
            imgEmptyPeg = LoadImage("emptypeg");
            imgRedPeg = LoadImage("redpeg");
            LoadSoundPlayer();
            ResetBoard();
            timer.Elapsed += Timer_OnTimedEvent;
        }

        private void Timer_OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timedOut = true;
        }

        #endregion

        // --------------------------------------------------------------------

        #region Public Methods

        /*
         * Method used to reset the board back to the loaded state.  Clears out
         * the players scores and resets peg positions back to the start.  Board
         * is re-painted.
         */
        public void ResetBoard()
        {
            // reset players scores
            for (int i = 0; i < playerScore.Length; i++)
                playerScore[i] = 0;
            // reset board to start-up board positions now
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 40; j++)
                    board[i, j] = DEF_BOARD[i, j];
            // set peg position indexes
            p1PegP = MAX_SCORE - 1;
            p2PegP = MAX_SCORE - 1;
            Invalidate();
        }

        /*
         * Method moves one of the players pegs forward the number
         * of spaces requested. Will return a true if the player whose
         * pegs are being moved, has won the game. If sound is on, will
         * play the peg move sound with each peg move.
         */
        public bool MovePeg(Players player, int spaces)
        {
            bool won = HasWon(player);
            int iPlayer = (int) player;

            if ((!won) && (spaces > 0)) { // only move if can move
                int fp = playerScore[iPlayer];
                int bp = (iPlayer == 0) ? p1PegP : p2PegP;
                int c = (iPlayer == 0) ? CB_BLUE : CB_RED;
                int bc = 0, br = 0, nc = 0, nr = 0;

                // setup for move
                if (fp != 0) { // get back position to clear
                    bc = BOARD_POS[iPlayer,bp,0];
                    br = BOARD_POS[iPlayer,bp,1];
                    if (iPlayer == 0)
                        p1PegP = fp - 1;
                    else
                        p2PegP = fp - 1;
                }
                else { // just clear front peg position
                    bc = 1;
                    br = (iPlayer == 0) ? 0 : 4;
                }

                // do all of move in one swoop
                for (int i = 0; i < spaces; i++) {
                    PlayPegSound();
                    fp = playerScore[iPlayer];
                    playerScore[iPlayer]++;
                    nc = BOARD_POS[iPlayer,fp,0];
                    nr = BOARD_POS[iPlayer,fp,1];
                    MovePegOneSpace(bc, br, nc, nr, c);
                    br = nr; bc = nc;
                    if (playerScore[iPlayer] >= MAX_SCORE) {
                        won = true;
                        break;
                    }
                }
            }

            return won;
        }

        /*
         * Method to move the passed in players peg forward one space.
         * Calls MovePeg(player, spaces) with spaces == 1.
         */
        public bool MovePeg(Players player)
        {
            return MovePeg(player, 1);
        }

        /*
         * Method to return the score of a given player from the board.
         */
        public int GetScore(Players player)
        {
            return playerScore[(int) player];
        }

        /*
         * Method returns true if given player has won the game, false if not.
         */
        public bool HasWon(Players player)
        {
            return (playerScore[(int) player] >= MAX_SCORE);
        }

        #endregion
    }
}

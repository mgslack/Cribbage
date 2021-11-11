using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlayingCards;
using CribbageEng;

namespace CribbageEngineTest
{
    public partial class MainWin : Form
    {
        private CribbageEngine ce = new CribbageEngine();

        private CardDeck deck = new CardDeck();
        private CardHand playedCards = new CardHand(CribbageEngine.CRIBBAGE_PLAYED_HAND_SIZE, false);
        private CardHand hand1 = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE);
        private CardHand hand2 = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE);
        private CardHand h1 = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD);
        private CardHand h2 = new CardHand(CribbageEngine.CRIBBAGE_HAND_SIZE_AFTER_DISCARD);
        private CardHand crib = new CardHand(CribbageEngine.CRIBBAGE_CRIB_SIZE);
        private PlayingCard upCard = PlayingCard.EMPTY_CARD;

        // --------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        private void Deal()
        {
            deck.Shuffle();
            // clear results
            tbResults.Clear();
            // clear hands
            playedCards.RemoveAll();
            hand1.RemoveAll();
            hand2.RemoveAll();
            h1.RemoveAll();
            h2.RemoveAll();
            crib.RemoveAll();
            // deal cards
            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++) {
                hand1.Add(deck.GetNextCard());
                hand2.Add(deck.GetNextCard());
            }
        }

        private void AppendLine(string text)
        {
            tbResults.AppendText(text + Environment.NewLine);
        }

        private void ShowHands(bool disToo, bool plydToo, bool pCards)
        {
            AppendLine("----- Hands -----");
            if (pCards) {
                AppendLine("Hand 1: " + h1.ToString());
                AppendLine("Hand 2: " + h2.ToString());
            }
            else {
                AppendLine("Hand 1: " + hand1.ToString());
                AppendLine("Hand 2: " + hand2.ToString());
            }
            if (disToo)
                AppendLine("Crib: " + crib.ToString());
            if (plydToo)
                AppendLine("Played: " + playedCards.ToString());
        }

        private void DoDiscard(int[] dis1, int[] dis2)
        {
            crib.Add(hand1.Remove(dis1[0]));
            crib.Add(hand1.Remove(dis1[1]));
            crib.Add(hand2.Remove(dis2[0]));
            crib.Add(hand2.Remove(dis2[1]));
            hand1.CompressHand();
            hand2.CompressHand();
            upCard = deck.GetNextCard();
        }

        /*
         * Plays first two cards of each hand after discarding...
         */
        private void DoPlay()
        {
            int p1 = -1, p2 = -1, h1p = 0, h2p = 0;
            List<string> a1 = new List<string>(4);
            List<string> a2 = new List<string>(4);

            for (int i = CardHand.FIRST; i < CribbageEngine.CRIBBAGE_HAND_SIZE; i++) {
                h1.Add(hand1.CardAt(i));
                h2.Add(hand2.CardAt(i));
            }

            for (int i = 0; i < 2; i++) {
                p1 = ce.GetCardToPlay(playedCards, h1);
                if (p1 != -1) playedCards.Add(h1.Remove(p1));
                h1p = ce.PointsInGame(playedCards, a1);
                p2 = ce.GetCardToPlay(playedCards, h2);
                if (p2 != -1) {
                    playedCards.Add(h2.Remove(p2));
                    h2p = ce.PointsInGame(playedCards, a2);
                }
                else {
                    h2p = 0;
                    a2.Clear();
                }
                AppendLine("--- Playing ---");
                AppendLine("Hand 1 plays - " + Convert.ToString(p1) + ", scores: " + Convert.ToString(h1p));
                if (a1.Count > 0) { // show score messages
                    foreach (string s in a1)
                        AppendLine(" Hand 1" + s);
                }
                AppendLine("Hand 2 plays - " + Convert.ToString(p2) + ", scores: " + Convert.ToString(h2p));
                if (a2.Count > 0) { // show score messages
                    foreach (string s in a2)
                        AppendLine(" Hand 2" + s);
                }
                ShowHands(false, true, true);
            }
        }

        private void DoPoints()
        {
            int p1 = ce.Figure_Points(hand1, false, upCard);
            int p2 = ce.Figure_Points(hand2, false, upCard);
            int p3 = ce.Figure_Points(crib, true, upCard);
            AppendLine("----- Scores (with upcard) -----");
            AppendLine("Hand 1: " + Convert.ToString(p1) + ", Hand 2: " + Convert.ToString(p2) + ", Crib: " + Convert.ToString(p3));
        }

        // --------------------------------------------------------------------

        private void btnTest_Click(object sender, EventArgs e)
        {
            Deal();
            ShowHands(false, false, false);
            // determine discards
            int[] dis1 = ce.AnalyzeHandForDiscards(hand1);
            int[] dis2 = ce.AnalyzeHandForDiscards(hand2);
            AppendLine("----- Have crib discards now -----");
            AppendLine("Hand 1 discards (slot's): " + Convert.ToString(dis1[0]) + ", " + Convert.ToString(dis1[1]));
            AppendLine("Hand 2 discards (slot's): " + Convert.ToString(dis2[0]) + ", " + Convert.ToString(dis2[1]));
            DoDiscard(dis1, dis2);
            ShowHands(true, false, false);
            AppendLine("----- Upcard dealt -----");
            AppendLine("Upcard - " + upCard.ToString());
            DoPlay(); // only first two cards...
            DoPoints();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

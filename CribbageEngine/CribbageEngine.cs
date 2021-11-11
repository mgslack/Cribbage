using System;
using System.Collections.Generic;
using PlayingCards;

/*
 * Primary class defines a cribbage game engine. The class relies on the
 * PlayingCards package for the PlayingCard and CardHand definitions. The
 * game engine provides a method to determine the score of a cribbage hand
 * (will use the up-card if passed in), pick the best cards to discard into
 * the crib from the initial 6 (used mainly to determine cards to discard by
 * a computer opponent, but could be used by a hint routine), determine the
 * points scored during game play (requires an 8 card hand to store cards in
 * that have been played), and a method to determine the best card to play
 * from a hand of 4 (usually used for determining the card to play for the
 * computer opponent, but could be used in a hint routine).
 * The class should be used by instantiating an instance during game start-up
 * and keeping alive a reference to the engine as game play continues.
 * The cribbage engine is based off of code originally written by David
 * Addison (1986?) in Basic that was converted to Turbo Pascal, eventually
 * to object Pascal (Sibyl), then finally into Java before converting to C#.
 *
 * Author:  M. G. Slack
 * Written: 2014-01-19
 * Version: 1.0.0.0
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd -
 *
 */
namespace CribbageEng
{
    public class CribbageEngine
    {
        public const int CRIBBAGE_HAND_SIZE = 6;
        public const int CRIBBAGE_HAND_SIZE_AFTER_DISCARD = 4;
        public const int CRIBBAGE_CRIB_SIZE = 4;
        public const int CRIBBAGE_PLAYED_HAND_SIZE = 8;

        /* Static used to determine score of runs within 5 cards. */
        private static readonly int[,] CONQ = {{1,1,1,2,3,9}, {1,1,2,2,3,12}, {1,1,2,3,3,12},
                                               {1,1,2,3,4,8}, {1,2,2,2,3, 9}, {1,2,2,3,3,12},
                                               {1,2,2,3,4,8}, {1,2,3,3,3, 9}, {1,2,3,3,4, 8},
                                               {1,2,3,4,4,8}, {1,2,3,4,5, 5}};
        /* Static used to determine score of runs within 4 cards. */
        private static readonly int[,] CONR = {{1,1,2,3,6}, {1,2,2,3,6}, {1,2,3,3,6}, {1,2,3,4,4}};
        /* Static used to determine score of a run within 3 cards. */
        private static readonly int[] CONS  = {1,2,3,3};
        /* Static used to determine discards to use (different hand combinations). */
        private static readonly int[,] CONV = {{0,1,2,3,4,5,0}, {0,1,2,4,3,5,0}, {0,1,2,5,3,4,0},
                                               {0,1,3,4,2,5,0}, {0,1,3,5,2,4,0}, {0,1,4,5,2,3,0},
                                               {0,2,3,4,1,5,0}, {0,2,3,5,1,4,0}, {0,2,4,5,1,3,0},
                                               {0,3,4,5,1,2,0}, {1,2,3,4,0,5,0}, {1,2,3,5,0,4,0},
                                               {1,2,4,5,0,3,0}, {1,3,4,5,0,2,0}, {2,3,4,5,0,1,0}};

        // --------------------------------------------------------------------

        /* Default constructor to create the cribbage game engine. */
        public CribbageEngine() { }

        // --------------------------------------------------------------------

        /* 
         * Method used to check to see if the hand contains a jack that matches
         * the suit of the up-card.  If upCard is not true, method returns 0
         * otherwise will return 1 if hand contains a jack with the same suit
         * as the up-card suit.
         */
        private int CheckForJack(CardHand hand, bool upCard, Suit upSuit)
        {
            int iRet = 0;

            if (upCard) {
                for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                    PlayingCard pc = hand.CardAt(i);
                    if ((pc != PlayingCard.BAD_CARD) && (pc.Suit == upSuit) &&
                        (pc.CardValue == CardValue.Jack)) {
                        iRet = 1;
                        break;
                    }
                }
            }

            return iRet;
        }

        /*
         * Method used to check for flushes in the hand.  A flush is valid
         * if all cards in a players hand have the same suit (plus an extra
         * point if match up-card suit) or if all of the cards and the up-card
         * suit of the cribhand match.
         * Will return 0, 4 or 5 depending on if the card hand has all of
         * one suit or not and if that suit matches the up card suit.
         */
        private int CheckFlush(CardHand hand, bool cribScore, Suit upSuit)
        {
            int iRet = 0;
            bool flush = true;

            for (int i = CardHand.FIRST; i < 3; i++) {
                PlayingCard pc1 = hand.CardAt(i);
                PlayingCard pc2 = hand.CardAt(i + 1);

                if ((pc1 == PlayingCard.BAD_CARD) || (pc2 == PlayingCard.BAD_CARD) ||
                    (pc1.Suit != pc2.Suit)) {
                    flush = false;
                    break;
                }
            }

            // score points now
            // flush in cribhand only counts if suit in hand also matches up card suit
            PlayingCard pc = hand.CardAt(CardHand.FIRST);
            bool cribMatches = ((pc != PlayingCard.BAD_CARD) && (pc.Suit == upSuit));

            if ((!cribScore) && (flush)) {
                iRet = 4;
                if (cribMatches) iRet++;
            }
            else if ((flush) && (cribMatches)) {
                iRet = 5;
            }

            return iRet;
        }

        /*
         * Method used to check for scores of 15's in the hand.  Will
         * loop through all possible combinations of 15.
         */
        private int Check15(CardHand hand, int iUpValue)
        {
            int iRet = 0;

            // check for 15 between two cards
            for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                for (int j = i + 1; j < 5; j++) {
                    PlayingCard pc1 = hand.CardAt(i);
                    PlayingCard pc2 = hand.CardAt(j);
                    int ii = 0;

                    if ((j == CRIBBAGE_HAND_SIZE_AFTER_DISCARD) && (pc1 != PlayingCard.BAD_CARD))
                        ii = pc1.GetCardPointValueFace10() + iUpValue;
                    else if ((pc1 != PlayingCard.BAD_CARD) && (pc2 != PlayingCard.BAD_CARD))
                        ii = pc1.GetCardPointValueFace10() + pc2.GetCardPointValueFace10();

                    if (ii == 15) iRet += 2;
                }
            }

            // check for 15 between three cards
            for (int i = CardHand.FIRST; i < 3; i++) {
                for (int j = i + 1; j < 4; j++) {
                    for (int k = j + 1; k < 5; k++) {
                        PlayingCard pc1 = hand.CardAt(i);
                        PlayingCard pc2 = hand.CardAt(j);
                        PlayingCard pc3 = hand.CardAt(k);
                        int ii = 0;

                        if ((pc1 != PlayingCard.BAD_CARD) && (pc2 != PlayingCard.BAD_CARD))
                            ii = pc1.GetCardPointValueFace10() + pc2.GetCardPointValueFace10();

                        if (k == CRIBBAGE_HAND_SIZE_AFTER_DISCARD)
                            ii += iUpValue;
                        else if (pc3 != PlayingCard.BAD_CARD)
                            ii += pc3.GetCardPointValueFace10();

                        if (ii == 15) iRet += 2;
                    }
                }
            }

            // check for 15 between four cards
            for (int i = CardHand.FIRST; i < 2; i++) {
                for (int j = i + 1; j < 3; j++) {
                    for (int k = j + 1; k < 4; k++) {
                        for (int l = k + 1; l < 5; l++) {
                            PlayingCard pc1 = hand.CardAt(i);
                            PlayingCard pc2 = hand.CardAt(j);
                            PlayingCard pc3 = hand.CardAt(k);
                            PlayingCard pc4 = hand.CardAt(l);
                            int ii = 0;

                            if ((pc1 != PlayingCard.BAD_CARD) && (pc2 != PlayingCard.BAD_CARD) &&
                                (pc3 != PlayingCard.BAD_CARD))
                                ii = pc1.GetCardPointValueFace10() +
                                     pc2.GetCardPointValueFace10() +
                                     pc3.GetCardPointValueFace10();

                            if (l == CRIBBAGE_HAND_SIZE_AFTER_DISCARD)
                                ii += iUpValue;
                            else if (pc4 != PlayingCard.BAD_CARD)
                                ii += pc4.GetCardPointValueFace10();

                            if (ii == 15) iRet += 2;
                        }
                    }
                }
            }

            // check for 15 with all 4 cards + upcard
            int iii = iUpValue;
            for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                PlayingCard pc = hand.CardAt(i);
                if (pc != PlayingCard.BAD_CARD) iii += pc.GetCardPointValueFace10();
            }
            if (iii == 15) iRet += 2;

            return iRet;
        }

        /*
         * Method used to check for pairs, three of kinds, etc.
         * Returns 0, 2 (for each pair), 6 (for 3 of a kind) and
         * 12 (for 4 of a kind).
         */
        private int CheckDups(CardHand hand, bool upCard, int iUpV)
        {
            int iRet = 0;
            int[] jj = new int[13];

            for (int i = 0; i < jj.Length; i++)
                jj[i] = 0;

            // run check for dups
            for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                PlayingCard pc = hand.CardAt(i);
                if (pc != PlayingCard.BAD_CARD) jj[pc.GetCardPointValue()-1]++;
            }
            if (upCard) jj[iUpV-1]++;

            // score dups now...
            for (int i = 0; i < jj.Length; i++) {
                switch (jj[i]) {
                    case 2: iRet += 2; // pair
                            break;
                    case 3: iRet += 6; // three of a kind
                            break;
                    case 4: iRet += 12; // four of a kind
                            break;
                    default: break; // nothing...
                }
            }

            return iRet;
        }

        /*
         * Method used to check for a run (straight) in the card hand.
         * Returns zero or the value of the runs (may be more than one).
         * Maximum that will return is 12.
        */
        private int CheckRuns(CardHand hand, int iUpV)
        {
            int iRet = 0;
            int[] vv = new int[5];
            int t = 0, l = 0, m = 0;
            int[,] q = new int[11,6];
            int[,] r = new int[4,5];
            int[] s = new int[4];

            for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                PlayingCard pc = hand.CardAt(i);
                if (pc != PlayingCard.BAD_CARD) 
                    vv[i] = pc.GetCardPointValue();
                else
                    vv[i] = 0;
            }
            vv[4] = iUpV;

            // sort
            for (int i = 0; i < vv.Length; i++) {
                for (int j = i; j < vv.Length; j++) {
                    if (vv[i] > vv[j]) { // swap
                        t = vv[j]; vv[j] = vv[i]; vv[i] = t;
                    }
                }
            }

            // check runs now (first pass - 5 card run/mult runs using all 5)
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 6; j++)
                    q[i,j] = CONQ[i,j];
            l = vv[0] - q[0,0];
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 5; j++)
                    q[i,j] += l;
            for (int i = 0; i < 11; i++) {
                for (int j = 0; j < 5; j++) {
                    if (vv[j] != q[i,j]) {
                        break;
                    }
                    else if (j == 4) {
                        return q[i,5]; // return run points
                    }
                }
            }
            // second pass - 4 card run/mult runs in 4 cards
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 5; j++)
                    r[i,j] = CONR[i,j];
            for (int i = 0; i < 2; i++) {
                m = vv[i] - r[0,0];
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        r[j,k] += m;
                for (int j = 0; j < 4; j++) {
                    for (int k = 0; k < 4; k++) {
                        if (vv[k+i] != r[j,k]) {
                            break;
                        }
                        else if (k == 3) {
                            return r[j,4];
                        }
                    }
                }
            }
            // third pass - 3 card run somewhere in hand
            for (int i = 0; i < 4; i++)
                s[i] = CONS[i];
            for (int i = 0; i < 3; i++) {
                m = vv[i] - s[0];
                for (int j = 0; j < 3; j++)
                    s[j] += m;
                for (int j = 0; j < 3; j++) {
                    if (vv[i+j] != s[j]) {
                        break;
                    }
                    else if (j == 2) {
                        return s[3];
                    }
                }
            }

            return iRet;
        }

        /*
         * Method used to check to see if a run has been played during cribbage
         * game play.
         * playedCards is the CardHand containing the cards played during the
         * game.  Hand should have enough space for eight cards and not be sorted.
         * Returns the value of any run count (3 to potentially 8).  Will be zero
         * if no runs are in the played cards.
        */
        private int CheckForRun(CardHand playedCards, int end)
        {
            int iRet = 0;
            int[] jj = new int[20];
            int x = 0;

            for (int i = 0; i < jj.Length; i++)
                if (i < 10)
                    jj[i] = 0;
                else
                    jj[i] = 14;
            x = playedCards.CurNumCardsInHand;
            for (int i = 0; i < x; i++) {
                // store here last card to first card played...
                PlayingCard pc = playedCards.CardAt(x - i - 1);
                jj[10 + i] = pc.GetCardPointValue();
            }
            // sort now...
            for (int k = 0; k < (end - 1); k++) {
                for (int j = k + 1; j < end; j++) {
                    if (jj[k + 10] > jj[j + 10]) { // swap...
                        x = jj[k + 10]; jj[k + 10] = jj[j + 10]; jj[j + 10] = x;
                    }
                }
            }
            // check run...
            for (int i = 0; i < (end - 1); i++)
                if (jj[i + 10] != (jj[i + 11] - 1)) return iRet;
            // have at least 'end' card run
            return end;
        }

        // --------------------------------------------------------------------

        /*
         * Method used to determine the amount of points a cribbage hand has
         * (with or without the up-card).  The points available in the hand are
         * returned (which could be zero).
         * The hand is the CardHand to get points of.  Hand checked needs to have at
         * least 4 cards and they must be the first four cards in the hand (use
         * 'compressHand()' on a 6 card hand after discarding).
         * The cribScore is set to true if scoring the crib hand, otherwise set
         * to false.  Crib hands can only count flushes if all cards and the up-card
         * are the same suit.
         * The upCard PlayingCard is the cribbage up-card for the hand.  This should
         * be set to EMPTY_CARD if the points a hand has (without the up-card) is wanted.
         * Returns the number of points in the cribbage hand.
         */
        public int Figure_Points(CardHand hand, bool cribScore, PlayingCard upCard)
        {
            int iRet = 0;
			// up-card setup for internal calls
            Suit upSuit  = Suit.None;
            int iUpValue = 25; // value of up-card (face cards = 10)
            int iUpV     = 25; // value of up-card (1 - 13)
            bool bUpCard = false;

            // setup up-card stuff to passed in up-card
            if ((upCard != null) && (upCard != PlayingCard.BAD_CARD) && (upCard != PlayingCard.EMPTY_CARD)) {
                bUpCard = true;
                upSuit = upCard.Suit;
                iUpValue = upCard.GetCardPointValueFace10();
                iUpV = upCard.GetCardPointValue();
            }

            // figure points now
            iRet += CheckForJack(hand, bUpCard, upSuit);
            iRet += CheckFlush(hand, cribScore, upSuit);
            iRet += Check15(hand, iUpValue);
            iRet += CheckDups(hand, bUpCard, iUpV);
            iRet += CheckRuns(hand, iUpV);

            return iRet;
        }

        /*
         * Method used to analyze a 6 card cribbage hand to determine what cards
         * would be best discarded into the crib.  Method is mainly used to determine
         * computer moves, but could be used to implement a hint facility also.
         * The hand is theCardHand to analyze for discards.  Must contain 6 cards, else
         * -1 is returned for both discard indexes.
         * Returns an int array of two elements with the indexes of the cards to discard
         * contained inside.  May contain -1 (in both spots) if an error occurred in the
         * analysis (not enough cards in hand, etc.).
         */
        public int[] AnalyzeHandForDiscards(CardHand hand)
        {
            int[] iiRet = {-1, -1};

            if (hand.CurNumCardsInHand == CRIBBAGE_HAND_SIZE) { // have a cribbage hand to analyze
                int[,] v = new int[15,7];
                int b9 = 0, p9 = 0, j = 0;
                int[] ii = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
                int[] jj = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

                for (int i = 0; i < 15; i++)
                    for (int k = 0; k < 7; k++)
                        v[i,k] = CONV[i,k];
                // figure points for each hand combination (15 of them)
                for (int i = 0; i < 15; i++) {
                    CardHand tmpHand = new CardHand(CRIBBAGE_HAND_SIZE);
                    // create temporary hand of cards from hand
                    for (int k = 0; k < 4; k++)
                        tmpHand.Add(hand.CardAt(v[i,k]));
                    // figure points for created hand
                    v[i,6] = Figure_Points(tmpHand, false, PlayingCard.EMPTY_CARD);
                    if (v[i,6] > p9) p9 = v[i,6]; // p9 has largest amount of points so far
                }
                // more than one hand with largest amount of points?
                for (int i = 0; i < 15; i++)
                    if (v[i,6] == p9) {
                        ii[j++] = i; // save off hand combo with high points
                    }
                // set index of hand combo with high points (this may be the only one)
                b9 = ii[0];
                if (j != 1) { // had multiple hands with high score
                    int c9 = 5, zz = 1; // check for hands with '5's in them
                    do {
                        p9 = 0;
                        for (int i = 0; i < jj.Length; i++)
                            jj[i] = 0;
                        // does hand combo contain card(s) valued c9?
                        for (int i = 0; i < j; i++) {
                            // check each card in high score combination
                            for (int k = 0; k < 4; k++) {
                                int l = v[ii[i],k];
                                PlayingCard pc = hand.CardAt(l);
                                if ((pc != PlayingCard.BAD_CARD) && (pc.GetCardPointValue() == c9)) { // mark it
                                    jj[i]++;
                                    if (jj[i] > p9) p9 = jj[i]; // maybe more than one...
                                }
                            }
                        }
                        // more than one hand combo with our card in it?
                        int y = 0;
                        for (int i = 0; i < j; i++) {
                            if ((jj[i] != 0) && (jj[i] == p9)) { // maybe
                                y++; b9 = ii[i];
                            }
                        }
                        // do we have more than one hand combo?
                        if (y != 1) { // maybe none...
                            if (y != 0) { // rebuild list of hand combos to look at
                                for (int i = (j-2); i >= 0; i--) {
                                    if (jj[i] == 0) {
                                        for (int k = i; k < (j-2); k++) {
                                            ii[k] = ii[k+1];
                                        }
                                    }
                                }
                                j = y; // reset count to number of hands left...
                            }
                            zz++;
                            switch(zz) {
                                case 2: c9 = 8;  // eights?
                                        break;
                                case 3: c9 = 7;  // sevens?
                                        break;
                                case 4: c9 = 11; // jacks?
                                        break;
                                case 5: c9 = 1;  // aces?
                                        break;
                                default: Random rnd = new Random();
                                         b9 = ii[(int) Math.Floor(rnd.NextDouble() * j)]; // pick one..
                                         zz = 6;
                                         break;
                            }
                        }
                        else { // only one - have it picked, now exit...
                            zz = 6;
                        }
                    } while (zz < 6);
                }
                // set indexes to pass back
                iiRet[0] = v[b9,4];
                iiRet[1] = v[b9,5];
            }

            return iiRet;
        }

        /*
         * Method used to tally up the point value of all of the played cards.
         * The playedCards is the CardHand containing the cards played during the
         * game. Hand should have enough space for eight cards and not be sorted.
         * Returns total point value of all played cards.
         */
        public int GetGameTally(CardHand playedCards)
        {
            int iRet = 0;

            for (int i = CardHand.FIRST; i < playedCards.CurNumCardsInHand; i++) {
                PlayingCard c = playedCards.CardAt(i);
                if (c != PlayingCard.EMPTY_CARD)
                    iRet += c.GetCardPointValueFace10();
            }

            return iRet;
        }

        /*
         * Method used to return the number of points a hand scores in the game. This
         * method is generally called after a card is played to get the points (if any)
         * scored.
         * It is up to the caller to manage the played cards hand. If, for instance, 31
         * points is reached, the caller must clear the played cards and reset everything.
         * The caller should use the 'GetGameTally()' method to check where the played
         * point total is.
         * The playedCards is the CardHand containing the cards played during the
         * game. Hand should have enough space for eight cards and not be sorted.
         * The scoreMsgs is an ArrayList containing the score messages from the analysis
         * of the cards in play. Messages should match the score returned. Will be an
         * array of strings.
         * Returns number of points (maybe zero) scored in game play.
         */
        public int PointsInGame(CardHand playedCards, List<string> scoreMsgs)
        {
            int iRet = 0;
            int tally = GetGameTally(playedCards);
            int cardsPlayed = playedCards.CurNumCardsInHand;
            int n = 2, flg = 0;

            if (scoreMsgs == null)
                throw new Exception("PointsInGame: scoreMsgs cannot be null.");

            // clear out message list
            scoreMsgs.Clear();

            if (cardsPlayed > 1) {
                if (tally == 15) { // score two points (have 15 with play)
                    iRet += 2;
                    scoreMsgs.Add(" gets 2 points for fifteen.");
                }
                if (tally == 31) { // score two points (have 31 with play)
                    iRet += 2;     // playedCards need to be reset for game to continue...
                    scoreMsgs.Add(" gets 2 points for thirty-one.");
                }
                // score dups...
                if ((cardsPlayed - 2) > 2) n = cardsPlayed - 2;
                for (int i = cardsPlayed; i >= n; i--) {
                    PlayingCard c1 = playedCards.CardAt(i - 1);
                    PlayingCard c2 = playedCards.CardAt(i - 2);

                    if ((c1 != PlayingCard.EMPTY_CARD) && (c2 != PlayingCard.EMPTY_CARD)) {
                        if (c1.GetCardPointValue() != c2.GetCardPointValue())
                            break;
                    }
                    else
                        break;
                    // score dups now
                    switch (cardsPlayed - i + 1) {
                        case 1: iRet += 2;
                                flg = 1;
                                break;
                        case 2: iRet += 4; // cumulative points
                                flg = 2;
                                break;
                        case 3: iRet += 6; //  " " "
                                flg = 3;
                                break;
                    }
                }
                if (flg != 0) { // messages for duplicates
                    switch (flg) {
                        case 1: scoreMsgs.Add(" gets 2 points for a pair.");
                                break;
                        case 2: scoreMsgs.Add(" gets 6 points for three-of-a-kind.");
                                break;
                        case 3: scoreMsgs.Add(" gets 12 points for four-of-a-kind.");
                                break;
                    }
                }
                // check for runs?
                if (cardsPlayed > 2) { // have more than two cards, check 'em
                    n = 0;
                    int k = 3, nn = 0;

                    while (k <= cardsPlayed) {
                        n = CheckForRun(playedCards, k);
                        if (n != 0) nn = n;
                        k++;
                    }
                    // score message (if needed)
                    if (nn != 0)
                        scoreMsgs.Add(" gets " + Convert.ToString(nn) + " points for a " + Convert.ToString(nn) + " card run.");
                    iRet += nn;
                }
            }

            return iRet;
        }

        /*
         * Method used to return the best card to play for the given hand and
         * the given set of cards already played. Will return a -1 if no card
         * can be played. This method is generally used to determine a computer
         * opponents move, but could be used to implement a play hint for other
         * players.
         * The playedCards is the CardHand containing the cards played during the
         * game. Hand should have enough space for eight cards and not be sorted.
         * The hand is the CardHand to get the best card to play from. The card
         * hand must contain space for a least 4 cards.
         * Returns the index of the card in the hand that is the best card to
         * play. Will be -1 if no card is available to play in the hand.
         */
        public int GetCardToPlay(CardHand playedCards, CardHand hand)
        {
            int iRet = -1;
            int lastCard = playedCards.CurNumCardsInHand; // save this for now
            int p = 0, p9 = 0, j = 0, k = 0;
            int[] ii = new int[4];
            int[] jj = new int[4];
            List<string> scratch = new List<string>();

            for (int i = CardHand.FIRST; i < CRIBBAGE_HAND_SIZE_AFTER_DISCARD; i++) {
                PlayingCard c = hand.CardAt(i);
                if (c != PlayingCard.EMPTY_CARD) {
                    if ((GetGameTally(playedCards) + c.GetCardPointValueFace10()) <= 31) {
                        playedCards.Add(c); // add temporarily..
                        p = PointsInGame(playedCards, scratch);
                        playedCards.Remove(lastCard); // remove now (played cards not sorted!!!)
                        if (p > p9) { // save off reference...
                            p9 = p; iRet = i;
                        }
                        // check to make sure never throwing five as first card (unless have to)
                        if ((c.GetCardPointValue() == 5) && (playedCards.CurNumCardsInHand == 0)) {
                            jj[k++] = i;
                        }
                        else { // this may be a card we could play..
                            ii[j++] = i;
                        }
                    }
                }
            }
            // select a card to play (if no points can be scored)
            Random rnd = new Random();
            if ((iRet == -1) && (j != 0)) {
                if (j == 1)
                    iRet = ii[0];
                else // we have multiple cards we could play, pick one randomly...
                    iRet = ii[(int) Math.Floor(rnd.NextDouble() * j)];
            }
            // still don't have a card to play, check secondary play list
            //  - may only be a '5' we can play...
            if ((iRet == -1) && (k != 0)) {
                if (k == 1)
                    iRet = jj[0];
                else // have more than one five in hand...
                    iRet = jj[(int) Math.Floor(rnd.NextDouble() * k)];
            }

            return iRet; // may still not have a card to play (=GO=)
        }
    }
}

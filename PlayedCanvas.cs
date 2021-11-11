using System.Drawing;
using System.Windows.Forms;
using PlayingCards;
using CribbageEng;

/*
 * Class is a user control implementing the area to draw the cards
 * played during a hand of cribbage.
 * 
 * Author:  M. G. Slack
 * Written: 2014-01-22
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-02-02 - Changed paint event to override OnPaint.
 *
 */
namespace Cribbage
{
    public partial class PlayedCanvas : UserControl
    {
        private const int ImgStartX = 4;
        private const int ImgStartY = 6;

        private PlayingCardImage images = null;
        public PlayingCardImage Images { set { images = value; } }

        private CardHand playedCards = new CardHand(CribbageEngine.CRIBBAGE_PLAYED_HAND_SIZE, false);
        public CardHand PlayedCards { get { return playedCards; } }

        // --------------------------------------------------------------------

        public PlayedCanvas()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;

            graphics.Clear(this.BackColor);
            if ((images != null) && (playedCards != null)) {
                int x = ImgStartX, y = ImgStartY, count = playedCards.CurNumCardsInHand;
                for (int i = CardHand.FIRST; i < count; i++) {
                    PlayingCard card = playedCards.CardAt(i);
                    if ((card != PlayingCard.BAD_CARD) && (card != PlayingCard.EMPTY_CARD)) {
                        graphics.DrawImage(images.GetCardImage(card), x, y); // paint it
                        x += PlayingCardImage.IMAGE_OFFSET;
                    }
                }
            }
        }

        // --------------------------------------------------------------------

        /*
         * Method resets the played cards (removed all) and re-paints the canvas.
         */
        public void Clear()
        {
            playedCards.RemoveAll();
            this.Invalidate();
        }
    }
}

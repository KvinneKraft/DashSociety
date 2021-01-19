for (int k1 = 0, tid = 0, h1 = 20; k1 < 2; k1 += 1)
{
    try
    {
        int x1 = 0;
        int y1 = 0;

        int w3 = 0;
        int x3 = 0;

        if (k1 >= 1)
        {
            y1 += LabelObjects[tid - 1].Height + LabelObjects[tid - 1].Top + 5;
        }

        for (int k2 = 0; k2 < 2; k2 += 1)
        {
            var LText = LabelTexts[tid];
            var LSize = Tools.GetFontSize(LText, 11);
            
            if (k2 > 0) x1 += (w3 + x3 + 10);
        
            var LLoca = new Point(x1, y1);

            Controls.Label(InnerMainContainer, LabelObjects[tid], LSize, LLoca, InnerMainContainer.BackColor, Color.White, 1, 11, LText);

            int w2 = TextBoxWidths[k1];

            if (k2 > 0) w2 = (InnerMainContainer.Width - LLoca.X - LSize.Width);
            
            var TLoca = new Point(LLoca.X + LSize.Width, y1);
            var TSize = new Size(w2, h1);

            x3 = TLoca.X;
            w3 = w2;

            Controls.TextBox(InnerMainContainer, TextBoxObjects[tid], TSize, TLoca, Color.FromArgb(8, 8, 8), Color.White, 1, 9, Color.Empty);

            tid += 1;
        }
    }

    catch (Exception E)
    {
        MessageBox.Show($"{E.Message}\r\n|\r\n{E.StackTrace}");
    }
}
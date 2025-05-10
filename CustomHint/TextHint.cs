namespace CustomHintPlugin
{
    internal class TextHint
    {
        private string hintMessage;
        private float maxValue;

        public TextHint(string hintMessage, float maxValue)
        {
            this.hintMessage = hintMessage;
            this.maxValue = maxValue;
        }
    }
}
using Forge.Core;
using Forge.Core.Utilities;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.UI
{
    public class AnimatedText : Template
    {
        private CompletionTimer _delayTimer;
        private CompletionTimer _characterRevealTimer;
        private int _revealPosition = -1;

        public AnimatedText(string text, TimeSpan? timePerCharacter = null, TimeSpan? delayTime = null)
        {
            Text = text;
            TimePerCharacter = timePerCharacter ?? TimeSpan.FromMilliseconds(100);
            _characterRevealTimer = new CompletionTimer(TimePerCharacter);
            _delayTimer = new CompletionTimer(delayTime ?? TimeSpan.Zero);
        }

        public string Text { get; }
        public TimeSpan TimePerCharacter { get; }
        public string Font { get; set; } = "Default";

        public override void Tick(TickContext context)
        {
            base.Tick(context);

            if (!_delayTimer.Completed)
            {
                _delayTimer.Tick(context.DeltaTime);
                return;
            }

            if (_revealPosition == Text.Length)
            {
                return;
            }

            _characterRevealTimer.Tick(context.DeltaTime);
            if (_characterRevealTimer.Completed)
            {
                _revealPosition++;
                _characterRevealTimer.Restart();
                Reevaluate();
            }
        }

        public override IElement Evaluate()
        {
            return new Text(_revealPosition == -1 ? "" : Text.Substring(0, _revealPosition))
            {
                Font = Font
            };
        }
    }
}

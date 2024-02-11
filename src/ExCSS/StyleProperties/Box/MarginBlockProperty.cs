﻿namespace ExCSS
{
    internal sealed class MarginBlockProperty : ShorthandProperty
    {
        private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.Periodic(
                PropertyNames.MarginBlockStart, PropertyNames.MarginBlockEnd)
            .OrDefault(Length.Zero);

        internal MarginBlockProperty()
            : base(PropertyNames.MarginBlock)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}
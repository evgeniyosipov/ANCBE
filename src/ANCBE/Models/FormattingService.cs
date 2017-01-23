using System;

namespace ANCBE.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime dateTime)
        {
            return dateTime.ToString("d");
        }
    }
}

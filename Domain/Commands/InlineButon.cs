using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class InlineButton
    {
        public string Text { get; set; } = string.Empty;
        public string CallbackData { get; set; } = string.Empty;
    }
}

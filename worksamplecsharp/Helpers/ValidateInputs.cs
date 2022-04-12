using MultiValueDictionary.src.Enums;
using MultiValueDictionary.src.Model;
using System;
using System.Linq;

namespace MultiValueDictionary.Helpers
{
    public static class ValidateInputs
    {
        public static Validate ValidateInput(string arguments)
        {
            var inputs = arguments.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
            var operationValue = inputs.Any() ? inputs[0] : "";
            if(!Enum.TryParse(operationValue, out MultiValueDictionaryCommand command))
            {
                return new Validate { Command = command, IsValid = false, Key = "", Value = "" };
            }
            switch(command)
            {
                case MultiValueDictionaryCommand.KEYS:
                    return new Validate { Command = command, IsValid = true, Key = "", Value = "" };

                case MultiValueDictionaryCommand.MEMBERS:
                    if(inputs.Count == 2)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = "" };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.ADD:
                    if (inputs.Count == 3)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = inputs[2] };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.REMOVE:
                    if (inputs.Count == 3)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = inputs[2] };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.REMOVEALL:
                    if (inputs.Count == 2)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = "" };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.CLEAR:
                    return new Validate { Command = command, IsValid = true, Key = "", Value = "" };

                case MultiValueDictionaryCommand.KEYEXISTS:
                    if (inputs.Count == 2)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = "" };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.MEMBEREXISTS:
                    if (inputs.Count == 3)
                    {
                        return new Validate { Command = command, IsValid = true, Key = inputs[1], Value = inputs[2] };
                    }
                    return new Validate { Command = command, IsValid = false, Key = "", Value = "" };

                case MultiValueDictionaryCommand.ALLMEMBERS:
                    return new Validate { Command = command, IsValid = true, Key = "", Value = "" };

                case MultiValueDictionaryCommand.ITEMS:
                    return new Validate { Command = command, IsValid = true, Key = "", Value = "" };
            }
            return new Validate { Command = command, IsValid = false, Key = "", Value = "" };
        }
    }
}

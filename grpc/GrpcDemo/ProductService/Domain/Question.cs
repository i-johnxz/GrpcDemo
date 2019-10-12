using System;
using System.Collections.Generic;

namespace ProductService.Domain
{
    public class Question
    {
        public Guid Guid { get; protected set; }

        public string Code { get; protected set; }

        public int Index { get; protected set; }

        public string Text { get; protected set; }

        public Product Product { get; protected set; }

        public Question()
        {
            
        }


        public Question(string code, int index, string text)
        {
            Guid = Guid.NewGuid();
            Code = code;
            Index = index;
            Text = text;
        }
        
    }

    public class ChoiceQuestion : Question
    {
        public List<Choice> Choices { get; set; }

        public ChoiceQuestion()
        {
            
        }

        public ChoiceQuestion(string code,
            int index,
            string text,
            List<Choice> choices)
            : base(code, index, text)
        {
            Choices = choices;
        }

        public static IList<Choice> YesNoChoice()
        {
            return new List<Choice>
            {
                new Choice("YES", "YES"),
                new Choice("NO", "NO")
            };
        }
    }

    public class DateQuestion : Question
    {
        public DateQuestion(string code, int index, string text) : base(code, index, text)
        {
            
        }
    }

    public class NumericQuestion : Question
    {
        public NumericQuestion(string code, int index, string text) : base(code, index, text)
        {
            
        }
    }
}
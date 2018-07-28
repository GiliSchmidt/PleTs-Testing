using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    internal class AtlasConstraintValidationQueue
    {
        private int position;
        private string[] queue;

        internal AtlasConstraintValidationQueue(string input)
        {
            position = 0;
            queue = input.Split(' ');
        }

        internal string Pop()
        {
            if (position < queue.Length)
                return queue[position++];
            return null;
        }
        internal string Peek()
        {
            if (position < queue.Length)
                return queue[position];
            return null;
        }
        internal void Normalize()
        {
            for (int i = 0; i < queue.Length; i++)
            {
                string member = queue[i];
                switch (member)
                {
                    case "0":
                        member = "∧";
                        break;
                    case "1":
                        member = "∨";
                        break;
                    case "2":
                        member = "⇒";
                        break;
                    case "3":
                        member = "<=>"; //TODO Placeholder
                        break;
                    case "4":
                        member = "(";
                        break;
                    case "5":
                        member = ")";
                        break;
                    case "6":
                        member = "¬";
                        break;
                    default:
                        break;
                }
                queue[i] = member;
            }
        }
    }
}
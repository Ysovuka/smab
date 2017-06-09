using System;
using System.Collections.Generic;
using System.Text;

namespace Smab.Systems.Voting
{
    public interface IPollManager
    {
        Poll Create(string question, string timeDuration);
        bool AddChoice(string question, string choice);
        string GetResults(string question);
        void Open(string question);
        void Close(string question);
        bool Vote(string id, string question, string choice);
        string View(string question);
        string Search(string term);
    }
}

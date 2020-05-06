using System;
using System.Collections.Generic;
using Uno;

class Program
{
    static void Main(string[]args)
    {
        List<Player> players = new List<Player>();
        players.Add(new UserPlayer("Nolan"));
        players.Add(new Player("Bot"));
        Deck deck = new Uno.Deck();
        deck.Deal(players);
        Card top = deck.Draw();
        Card temp;
        Card wild_color = new Card(0, "WILD");

        bool winner = false;
        int index = 0;
        while(!winner)
        {
            System.Console.WriteLine("Top: {0}",top);
            if(top.GetValue()=="Wild")
            {
                temp = players[index].Play(wild_color);
            }
            else
            {
                temp = players[index].Play(top);
            }
            if(temp.GetValue()=="FAIL")
            {
                System.Console.WriteLine("{0} draws a card",players[index].ID);
                players[index].AddCard(deck.Draw());
                continue;
            }
            top = temp;
            winner = players[index].CheckWin();
            deck.AddToPile(top);

            if(top.GetValue()=="Reverse")
            {
                players.Reverse();
            }
            if(top.GetValue()=="Skip")
            {
                index++;
            }
            if(top.GetValue()=="Wild")
            {
                wild_color.SetColor("RED");
                System.Console.WriteLine("Wild: {0}!", "RED");
            }
            if(top.GetValue()=="Wild+4")
            {
                wild_color.SetColor("RED");
                System.Console.WriteLine("Wild+4: {0}!", "RED");
                for(int i=0; i<4; i++)
                {
                    players[(index+1)%players.Count].AddCard(deck.Draw());
                }
                index++;
            }
            index = (index+1)%players.Count;
        }
    }
}
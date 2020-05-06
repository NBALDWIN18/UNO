using System;
using System.Collections.Generic;
using Uno;

class Program
{
    static void Main(string[]args)
    {
        List<Player> players = new List<Player>();
        players.Add(new Player("Nolan"));
        players.Add(new Player("Bot"));
        Deck deck = new Uno.Deck();
        deck.Deal(players);
        Card top = deck.Draw();
        Card temp;
        Card wild_color = new Card(0, "WILD");
        System.Console.WriteLine(String.Format("The first card is {0}",top));

        bool winner = false;
        int index = 0;
        while(!winner)
        {
            if(top.GetValue()==13)
            {
                temp = players[index].Play(wild_color);
            }
            else
            {
                temp = players[index].Play(top);
            }
            if(temp.GetValue()==-1)
            {
                players[index].AddCard(deck.Draw());
                System.Console.WriteLine("FAIL");
                continue;
            }
            top = temp;
            winner = players[index].CheckWin();
            deck.AddToPile(top);

            if(top.GetValue()==11)
            {
                players.Reverse();
                System.Console.WriteLine("Reverse!");
            }
            if(top.GetValue()==12)
            {
                index++;
                System.Console.WriteLine("Skip!");
            }
            if(top.GetValue()==13)
            {
                wild_color.SetColor("RED");
                System.Console.WriteLine("Wild: {0}!", "RED");
            }
            if(top.GetValue()==14)
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
using System;
using System.Collections.Generic;
using System.Collections;

namespace Uno
{
    
    public class Player
    {
        public List<Card> hand;
        public string ID;

        public Player(string pid)
        {
            this.hand = new List<Card>();
            this.ID = pid;
        }

        public void AddCard(Card c)
        {
            hand.Add(c);
        }

        public static bool LegalMove(Card top, Card next)
        {
            if(next.GetValue() == top.GetValue() || next.GetColor()==top.GetColor() || next.GetColor() == "WILD")
            {
                return true;
            }
            return false;
        }

        public virtual Card Play(Card top)
        {
            Card temp = this.hand[0];
            bool found = false;
            int remv = 0;
            foreach(Card c in this.hand)
            {
                if(LegalMove(top, c))
                {
                    temp = c;
                    found = true;
                    break;
                }
                remv++;
            }
            if(!found)
            {
                return new Card(-1, "FAIL");
            }

            this.hand.RemoveAt(remv);
            return temp;
        }
    
        public bool CheckWin()
        {
            if(this.hand.Count==0)
            {
                System.Console.WriteLine("{0} WINS!", this.ID);
            }
            return this.hand.Count==0;
        }
    
        public override string ToString()
        {
            string s = "";
            int i = 1;
            foreach(Card c in this.hand)
            {
                s += i.ToString() + ". " + c.ToString() + ", ";
                i++;
            }
            return s;
        }
    }

    public class UserPlayer : Player
    {
        public UserPlayer(string pid) : base(pid){}

        public override Card Play(Card top)
        {
            System.Console.WriteLine(this);
            string input = System.Console.ReadLine();
            if(LegalMove(top, this.hand[Int32.Parse(input)-1]))
            {
                Card temp = this.hand[Int32.Parse(input)-1];
                this.hand.RemoveAt(Int32.Parse(input)-1);
                return temp;
            }
            return new Card(-1, "FAIL");
        }
    }
    
    public class Deck
    {
        private List<Card> deck;
        private List<Card> pile;
        public Deck()
        {
            this.deck = new List<Card>();
            this.pile = new List<Card>();
            string[] colors = {"RED", "BLUE", "GREEN", "YELLOW"}; 

            foreach(string color in colors)
            {
                this.pile.Add(new Card(0, color));
                for(int i=2; i<20; i++)
                {
                    this.pile.Add(new Card(i/2, color));
                }
                for(int i=0; i<2; i++)
                {
                    this.pile.Add(new Card(11, color));
                    this.pile.Add(new Card(12, color));
                    this.pile.Add(new Card(10, color));
                }
                this.pile.Add(new Card(13, "WILD"));
                this.pile.Add(new Card(14, "WILD"));
            }
            this.Shuffle();
        }

        public Card Draw()
        {
            Card temp = this.deck[0];
            this.deck.RemoveAt(0);
            if(this.deck.Count == 0)
            {
                this.Shuffle();
            }
            return temp;
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            int index;
            while(pile.Count!=0)
            {
                index = rnd.Next(pile.Count);
                this.deck.Add(this.pile[index]);
                this.pile.RemoveAt(index);
            }
        }
   
        public void Deal(List<Player> players)
        {
            for(int i=0; i<players.Count*7; i++)
            {
                players[i%players.Count].AddCard(this.Draw());
            }
        }

        public void AddToPile(Card c)
        {
            this.pile.Add(c);
        }

        public override string ToString()
        {
            string s = "";
            foreach(Card c in this.deck)
            {
                s += c.ToString() + ", ";
            }
            return s;
        }
    }

    public class Card
    {
        private string[] values = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Plus2", "Reverse", "Skip", "Wild", "Wild+4"};
        private string value;
        private string color;
        public Card(int val, string col)
        {
            this.value = val > -1 ? values[val] : "FAIL";
            this.color = col;
        }

        public void SetColor(string c)
        {
            this.color = c;
        }

        public string GetValue()
        {
            return this.value;
        }

        public string GetColor()
        {
            return this.color;
        }

        public override string ToString()
        {
            return this.value + " " + this.color;
        }
    }
}
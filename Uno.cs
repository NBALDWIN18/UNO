using System;
using System.Collections.Generic;
using System.Collections;

namespace Uno
{
    public enum Value
    {
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5,
        six = 6,
        seven = 7,
        eight = 8,
        nine = 9,
        plus2 = 10,
        rev = 11,
        skip = 12,
        wild = 13,
        wild4 = 14
    }

    public class Player
    {
        private List<Card> hand;
        private string ID;

        public Player(string pid)
        {
            this.hand = new List<Card>();
            this.ID = pid;
        }

        public void AddCard(Card c)
        {
            hand.Add(c);
        }

        public Card Play(Card top)
        {
            System.Console.WriteLine(this);
            Card temp = this.hand[0];
            bool found = false;
            int remv = 0;
            foreach(Card c in this.hand)
            {
                if(c.GetValue() == top.GetValue() || c.GetColor()==top.GetColor() || c.GetColor() == "WILD")
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
            System.Console.WriteLine(String.Format("{0} played {1}", this.ID, temp));
            if(this.hand.Count==1)
            {
                System.Console.WriteLine("UNO");
            }
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
            foreach(Card c in this.hand)
            {
                s += c.ToString() + ", ";
            }
            return s;
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
                    this.pile.Add(new Card((int)Value.rev, color));
                    this.pile.Add(new Card((int)Value.skip, color));
                    this.pile.Add(new Card((int)Value.plus2, color));
                }
                this.pile.Add(new Card((int)Value.wild, "WILD"));
                this.pile.Add(new Card((int)Value.wild4, "WILD"));
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
        private int value;
        private string color;
        public Card(int val, string col)
        {
            this.value = val;
            this.color = col;
        }

        public void SetColor(string c)
        {
            this.color = c;
        }

        public int GetValue()
        {
            return this.value;
        }

        public string GetColor()
        {
            return this.color;
        }

        public override string ToString()
        {
            return this.value.ToString() + " " + this.color;
        }
    }
}
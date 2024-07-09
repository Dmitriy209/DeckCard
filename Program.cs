using System;
using System.Collections.Generic;

namespace DeckCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Casino casino = new Casino();
            casino.Work();
        }
    }

    class Casino
    {
        private Croupier _croupier = new Croupier();
        private Player _player = new Player();

        public void Work()
        {
            Console.WriteLine("Вы сели за игральный стол.");

            _croupier.Work(_player);

            Console.WriteLine("Игра закончилась.");
        }
    }

    class Croupier
    {
        private Deck _deck;

        public Croupier()
        {
            _deck = CreateDeck();
        }

        public int Size => _deck.Size;

        public void Work(Player player)
        {
            player.TryTakeCards(GiveList(ReadNumberCards()));

            Console.WriteLine("Вы взяли следующие карты:");

            player.ShowCards();
        }

        public List<Card> GiveList(int numberOfCards)
        {
            List<Card> cards;
            cards = CreateTransmissionList(numberOfCards);
            return cards;
        }

        private int ReadNumberCards()
        {
            int numberCards;
            int deckSize = Size;

            do
            {
                Console.WriteLine($"Сколько карт вы хотите получить, введите число до {deckSize}?");
                numberCards = ReadInt();
            }
            while (numberCards <= 0 || numberCards > deckSize);

            return numberCards;
        }

        private int ReadInt()
        {
            int value;

            while (int.TryParse(Console.ReadLine(), out value) == false)
                Console.WriteLine("Это не число.");

            return value;
        }

        private Deck CreateDeck()
        {
            string[] suits = { "Spades", "Hearts", "Tiles", "Clover" };
            string[] nameOfCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            List<Card> cards = new List<Card>();

            for (int i = 0; i < suits.Length; i++)
            {
                string suit = suits[i];

                foreach (var item in nameOfCards)
                {
                    Card card = new Card(suit, item);
                    cards.Add(card);
                }
            }

            Deck deck = new Deck(cards);

            return deck;
        }

        private List<Card> CreateTransmissionList(int numberOfCards)
        {
            return _deck.CreateTransmissionList(numberOfCards);
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void ShowCards()
        {
            foreach (var element in _cards)
            {
                element.ShowStats();
            }

            Console.WriteLine();
        }

        public void TryTakeCards(List<Card> cards)
        {
            if (cards.Count == 0)
                Console.WriteLine("Карт нет.");
            else
                _cards.AddRange(cards);
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck(List<Card> cards)
        {
            _cards = cards;
        }

        public int Size => _cards.Count;

        public List<Card> CreateTransmissionList(int numberOfCards)
        {
            List<Card> transmissionList = new List<Card>();


            for (int i = 0; i < numberOfCards; i++)
            {
                int lastIndex = _cards.Count - 1;

                Card card = _cards[lastIndex];
                _cards.Remove(_cards[lastIndex]);
                transmissionList.Add(card);
            }

            return transmissionList;
        }
    }

    class Card
    {
        private string _suit;
        private string _name;

        public Card(string suit, string name)
        {
            _suit = suit;
            _name = name;
        }

        public void ShowStats()
        {
            Console.WriteLine($"У вас в руках: {_suit}{_name}");
        }
    }
}

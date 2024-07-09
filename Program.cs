using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;

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

            Deck tempDeck = _croupier.ReturnRemoveDeck(ReadNumberCards());

            _player.TakeDeck(tempDeck);

            Console.WriteLine("У вас в руках:");

            _player.ShowDeckPlayer();
        }

        private int ReadNumberCards()
        {
            int numberCards;

            do
            {
                Console.WriteLine("Сколько карт вы хотите получить?");
                numberCards = ReadInt();
            }
            while (numberCards <= 0);

            return numberCards;
        }

        private int ReadInt()
        {
            int value;

            while (int.TryParse(Console.ReadLine(), out value) == false)
                Console.WriteLine("Это не число.");

            return value;
        }
    }

    class Croupier
    {
        private static Deck _deckCroupier = new Deck();

        public Croupier()
        {
            CreateCroupierDeck();
        }

        private Deck CreateRandomDeck()
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

            ShuffleCards(cards);

            Deck deck = new Deck();
            deck.AddCards(cards);

            return deck;
        }

        private void CreateCroupierDeck()
        {
            _deckCroupier = CreateRandomDeck();
        }

        static void ShuffleCards(List<Card> cards)
        {
            Shuffle(cards);
        }

        static void Shuffle(List<Card> cards)
        {
            Random random = new Random();

            int minLimitRandom = 0;
            int maxLimitRandom = cards.Count;

            Card tempElement;

            for (int i = 0; i < cards.Count; i++)
            {
                int indexRandom = random.Next(minLimitRandom, maxLimitRandom);

                tempElement = cards[i];
                cards[i] = cards[indexRandom];
                cards[indexRandom] = tempElement;
            }
        }

        public Deck ReturnRemoveDeck(int numberOfCards)
        {
            Deck removeDeck = new Deck();

            return removeDeck.ReturnRemoveCards(_deckCroupier, numberOfCards);
        }
    }

    class Player
    {
        private Deck _deckPlayer = new Deck();

        public void ShowDeckPlayer()
        {
            _deckPlayer.ShowCards();
        }

        public void TakeCards(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
                _deckPlayer.AddCard(cards[i]);
        }

        public void TakeDeck(Deck deck)
        {
            _deckPlayer = deck;
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public void AddCards(List<Card> cards)
        {
            _cards = cards;
        }

        public Deck ReturnRemoveCards(Deck deck, int numberOfCards)
        {
            Deck removeDeck = new Deck();

            for (int i = 0; i < numberOfCards; i++)
            {
                Card removeCard = deck.ReturnRemoveCard(deck._cards[i]);
                removeDeck.AddCard(removeCard);
            }

            return removeDeck;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public Card ReturnRemoveCard(Card card)
        {
            _cards.Remove(card);
            return card;
        }

        public void ShowCards()
        {
            foreach (var element in _cards)
            {
                element.ShowCard();
            }

            Console.WriteLine();
        }
    }

    class Card
    {
        private string _suit;
        private string _nameOfCard;

        public Card(string suit, string nameOfCard)
        {
            _suit = suit;
            _nameOfCard = nameOfCard;
        }

        public void ShowCard()
        {
            Console.WriteLine($"У вас в руках: {_suit}{_nameOfCard}");
        }
    }
}

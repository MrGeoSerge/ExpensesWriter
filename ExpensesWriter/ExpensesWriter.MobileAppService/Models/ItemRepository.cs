using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ExpensesWriter.Models
{
    public class ItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        public ItemRepository()
        {
            Add(new Item { Id = Guid.NewGuid().ToString(), Money = 256, Name = "This is an item description." });
            Add(new Item { Id = Guid.NewGuid().ToString(), Money = 354, Name = "This is an item description." });
            Add(new Item { Id = Guid.NewGuid().ToString(), Money = 244, Name = "This is an item description." });
        }

        public IEnumerable<Item> GetAll()
        {
            return items.Values;
        }

        public void Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public Item Get(string id)
        {
            Item item;
            items.TryGetValue(id, out item);

            return item;
        }

        public Item Remove(string id)
        {
            Item item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(Item item)
        {
            items[item.Id] = item;
        }
    }
}

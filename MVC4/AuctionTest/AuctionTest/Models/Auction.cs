using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionTest.Models
{
    public class Member
    {
        public string LoginName { get; set; } // Уникальный ключ
        public int ReputationPoints { get; set; }
    }

    public class Item
    {
        public int ItemID { get; private set; } // Уникальный ключ
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public IList<Bid> Bids { get; set; }
    }

    public class Bid
    {
        public Member Member { get; set; }
        public DateTime DatePlaced { get; set; }
        public decimal BidAmount { get; set; }
    }

    public interface IMembersRepository
    {
        void AddMember(Member member);
        Member FetchByLoginName(string loginName);
        void SubmitChanges();
    }

    public class MembersRepository : IMembersRepository
    {
        public void AddMember(Member member)
        {
            /* Реализуй меня */
        }

        public Member FetchByLoginName(string loginName)
        {
            /* Реализуй меня */
            return null;
        }

        public void SubmitChanges()
        {
            /* Реализуй меня */
        }
    }

    public class ItemsRepository
    {
        public void AddItem(Item item)
        {
            /* Реализуй меня */
        }

        public Item FetchByID(int itemID)
        {
            /* Реализуй меня */
            return null;
        }

        public IList<Item> ListItems(int pageSize, int pageIndex)
        {
            /* Реализуй меня */
            return null;
        }

        public void SubmitChanges()
        {
            /* Реализуй меня */
        }
    }

    public class Auction
    {
    }
}
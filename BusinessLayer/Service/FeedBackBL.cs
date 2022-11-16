using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class FeedBackBL:IFeedBackBL
    {
        private readonly IFeedBackRL feedBackRL;
        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            this.feedBackRL = feedBackRL;
        }
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            return this.feedBackRL.AddFeedback(addFeedback, userId);
        }
        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            return feedBackRL.GetAllFeedbacks(bookId);
        }

    }
}

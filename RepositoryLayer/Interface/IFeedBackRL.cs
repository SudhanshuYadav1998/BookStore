using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedBackRL
    {
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);


    }
}

using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedBackBL
    {
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);


    }
}

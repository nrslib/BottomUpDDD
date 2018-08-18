using Domain.Domain.Users;

namespace Domain.Application.Models {
    public class UserSummaryModel{
        public UserSummaryModel(User source) {
            Id = source.Id.Value;
            UserName = source.UserName.Value;
        }

        public string Id { get; }
        public string UserName { get; }
    }
}

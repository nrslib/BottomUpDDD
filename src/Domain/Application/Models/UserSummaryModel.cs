using Domain.Domain.Users;

namespace Domain.Application.Models {
    public class UserSummaryModel{
        public UserSummaryModel(User source) {
            Id = source.Id.Value;
            FirstName = source.Name.FirstName;
        }

        public string Id { get; }
        public string FirstName { get; }
    }
}

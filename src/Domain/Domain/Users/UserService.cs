namespace Domain.Domain.Users {
    public class UserService {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public bool IsDuplicated(User user) {
            var name = user.UserName;
            var searched = userRepository.Find(name);

            return searched != null;
        }
    }
}
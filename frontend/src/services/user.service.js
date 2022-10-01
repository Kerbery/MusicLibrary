import axios from "axios";

const API_USERS_URL = "/gateway/Users";

class UserService {
  async getUserInfo(permalink) {
    return axios.get(`${API_USERS_URL}/permalink/${permalink}`).then(
      (response) => response.data,
      (error) => console.log(error)
    );
  }
}
export default new UserService();
